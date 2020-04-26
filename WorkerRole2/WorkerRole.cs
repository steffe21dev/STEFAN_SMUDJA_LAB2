using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;

namespace WorkerRole2
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private string accountName = "cloudstoragestefan";                  
        private string accountKey = "H9FBXvM3GXDiGQlWS6Lfj5elbhUxDSBPVOpmcmDuP8U9VSYnenhIkPjTzqJBduT/+T4gn8EsNeQarlTtAKjorA==";
        private StorageCredentials creds;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;
        private CloudQueue inqueue, outqueue;
        private CloudQueueMessage inMessage, outMessage;

    
        private void initQueue()
        {
     
            creds = new StorageCredentials(accountName, accountKey);
            storageAccount = new CloudStorageAccount(creds, useHttps: true);

            queueClient = storageAccount.CreateCloudQueueClient();

            inqueue = queueClient.GetQueueReference("hotelrequestqueue");

            inqueue.CreateIfNotExists();

            outqueue = queueClient.GetQueueReference("hotelresponsequeue");

            outqueue.CreateIfNotExists();
        }

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole2 is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            catch (Exception e) {; }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
          
            ServicePointManager.DefaultConnectionLimit = 12;

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRole2 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole2 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole2 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            initQueue();        //call the queue initialization method
            while (!cancellationToken.IsCancellationRequested)
            {
                // Async dequeue (read) the message
                inMessage = await inqueue.GetMessageAsync();    //not an optimal way to retrieve a message from a queue, but works

                if (inMessage != null)
                {
                    JObject jObject = JObject.Parse(inMessage.AsString);
                    string bigs = calculatePrice(jObject);
                    await inqueue.DeleteMessageAsync(inMessage);

                    // Create a message and add it to the queue.
                    outMessage = new CloudQueueMessage(bigs);
                    outqueue.AddMessage(outMessage);
                }

                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }

        private string calculatePrice(JObject jObject)
        {

            int totalAmount = 0;

            if ((bool)jObject.GetValue("single") == true)
            {
                int travelers = int.Parse(jObject.GetValue("travelers").ToString());
                int seniors = int.Parse(jObject.GetValue("hotelSeniors").ToString());
                int nights = int.Parse(jObject.GetValue("nights").ToString());
                totalAmount += travelers * 600 * nights;
                totalAmount += seniors * 300 * nights;

            }
            else
            {
                int travelers = int.Parse(jObject.GetValue("travelers").ToString());
                int seniors = int.Parse(jObject.GetValue("hotelSeniors").ToString());
                int nights = int.Parse(jObject.GetValue("nights").ToString());
                totalAmount += travelers * 900 * nights;
                totalAmount += seniors * 450 * nights;
            }


            return totalAmount.ToString();
        }

    }
}

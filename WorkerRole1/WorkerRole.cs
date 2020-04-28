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

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);



        //USING JOBJECT CLASS TO PASS JSON OBJECTS BETWEEN QUEUES

        private string accountName = "SECRET";                  
        private string accountKey = "SECRET";
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

            inqueue = queueClient.GetQueueReference("flightrequestqueue");

            inqueue.CreateIfNotExists();

            outqueue = queueClient.GetQueueReference("flightresponsequeue");

            outqueue.CreateIfNotExists();
        }

        public override void Run()
        {
            Trace.TraceInformation("WorkerRole1 is running");

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

            Trace.TraceInformation("WorkerRole1 has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            
            initQueue();
            while (!cancellationToken.IsCancellationRequested)
            {
                // Async dequeue (read) the message
                inMessage = await inqueue.GetMessageAsync();    //not an optimal way to retrieve a message from a queue, but works

                if (inMessage != null)
                {
                    JObject jObject = JObject.Parse(inMessage.AsString);
                    string bigs = getPrice(jObject).ToString();
                    await inqueue.DeleteMessageAsync(inMessage);

                    // Create a message and add it to the queue.
                    outMessage = new CloudQueueMessage(bigs);
                    outqueue.AddMessage(outMessage);
                }

                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }


        private string getPrice(JObject jObject)
        {
            double[] cord = new double[5];
            string[] message = new string[8];


            if (jObject.GetValue("from").ToString().Equals("STO"))
            {
                cord[1] = 59.6519;
                cord[2] = 17.9186;
            }
            if (jObject.GetValue("from").ToString().Equals("CPH"))
            {
                cord[1] = 55.6181;
                cord[2] = 12.6561;
            }
            if (jObject.GetValue("from").ToString().Equals("CDG"))
            {
                cord[1] = 49.0097;
                cord[2] = 2.5478;
            }
            if (jObject.GetValue("from").ToString().Equals("LHR"))
            {
                cord[1] = 31.5497;
                cord[2] = 74.3436;
            }
            if (jObject.GetValue("from").ToString().Equals("FRA"))
            {
                cord[1] = 50.1167;
                cord[2] = 8.6833;
            }

            if (jObject.GetValue("to").ToString().Equals("STO"))
            {
                cord[3] = 59.6519;
                cord[4] = 17.9186;
            }
            if (jObject.GetValue("to").ToString().Equals("CPH"))
            {
                cord[3] = 55.6181;
                cord[4] = 12.6561;
            }
            if (jObject.GetValue("to").ToString().Equals("CDG"))
            {
                cord[3] = 49.0097;
                cord[4] = 2.5478;
            }
            if (jObject.GetValue("to").ToString().Equals("LHR"))
            {
                cord[3] = 31.5497;
                cord[4] = 74.3436;
            }
            if (jObject.GetValue("to").ToString().Equals("FRA"))
            {
                cord[3] = 50.1167;
                cord[4] = 8.6833;
            }

            message[1] = jObject.GetValue("from").ToString();

            if (jObject.GetValue("infants").ToString() != null)
            {
                message[3] = jObject.GetValue("infants").ToString();
            }
            else { message[3] = 0.ToString(); }
            if (jObject.GetValue("children").ToString() != null)
            {
                message[4] = jObject.GetValue("children").ToString();
            }
            else { message[4] = 0.ToString(); }
            if (jObject.GetValue("adults").ToString() != null)
            {
                message[5] = jObject.GetValue("adults").ToString();
            }
            else { message[5] = 0.ToString(); }
            if (jObject.GetValue("seniors").ToString() != null)
            {
                message[6] = jObject.GetValue("seniors").ToString();
            }
            else { message[6] = 0.ToString(); }

            return calculatePrice(cord, message);
        }

        private string calculatePrice(double[] cord, string[] message)
        {
            double flightDistance = get_distance(cord[3], cord[4], cord[1], cord[2]);


            double STO = 0.234;
            double CPH = 0.2554;
            double CDG = 0.2255;
            double LHR = 0.2300;
            double FRA = 0.2400;

            double br = 0;
            string brCity = message[1];


            //Sets the baserate depending on passed city
            switch (brCity)
            {
                case "STO":
                    br = STO;
                    break;
                case "CPH":
                    br = CPH;
                    break;
                case "CDG":
                    br = CDG;
                    break;
                case "LHR":
                    br = LHR;
                    break;
                case "FRA":
                    br = FRA;
                    break;
            }
           


            int nbrInfants = int.Parse(message[3]);
            int nbrChildren = int.Parse(message[4]);
            int nbrAdults = int.Parse(message[5]);
            int nbrSeniors = int.Parse(message[6]);

            double infantsPrice = br * flightDistance * (1 - 0.9) * nbrInfants;
            double childrenPrice = br * flightDistance * (1 - 0.33) * nbrChildren;
            double adultsPrice = br * flightDistance * (1 - 0) * nbrAdults;
            double seniorsPrice = br * flightDistance * (1 - 0.25) * nbrSeniors;

            double fare = infantsPrice + childrenPrice + adultsPrice + seniorsPrice;

            int str = (int)fare;


            //returns calculated price as string
            return str.ToString();
        }


        //method for calculating distance
        private double get_distance(double lon1, double lon2, double lat1, double lat2)
        {
            lon1 = toRadians(lon1);
            lon2 = toRadians(lon2);
            lat1 = toRadians(lat1);
            lat2 = toRadians(lat2);

            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));


            double r = 6371;

            return (c * r);
        }

        //Convverts degrees to radians
        static double toRadians(double angleIn10thofaDegree)
        {
            return (angleIn10thofaDegree *
                           Math.PI) / 180;
        }

    }
}

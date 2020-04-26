using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace WebRole1
{
    public partial class Frs : System.Web.UI.Page
    {
        private string accountName = "cloudstoragestefan";
        private string accountKey = "H9FBXvM3GXDiGQlWS6Lfj5elbhUxDSBPVOpmcmDuP8U9VSYnenhIkPjTzqJBduT/+T4gn8EsNeQarlTtAKjorA==";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {


                if (Session["from"] != null)
                {

                    frombox.Text = Session["from"].ToString();

                }
                if (Session["to"] != null)
                {

                    tobox.Text = Session["to"].ToString();

                }
                if (Session["month"] != null)
                {

                    monthbox.Text = Session["month"].ToString();

                }
                if (Session["infants"] != null)
                {

                    Box1.Text = Session["infants"].ToString();

                }
                if (Session["children"] != null)
                {

                    Box2.Text = Session["children"].ToString();

                }
                if (Session["adults"] != null)
                {

                    Box3.Text = Session["adults"].ToString();

                }
                if (Session["seniors"] != null)
                {

                    Box4.Text = Session["seniors"].ToString();

                }
                if (Session["price"] != null)
                {

                    Box5.Text = Session["price"].ToString();

                }
            }
        }
        protected void BtnBook_Click(object sender, EventArgs e)
        {

            Session["from"] = frombox.Text;
            Session["to"] = tobox.Text;
            Session["month"] = monthbox.Text;
            Session["infants"] = Box1.Text;
            Session["children"] = Box2.Text;
            Session["adults"] = Box3.Text;
            Session["seniors"] = Box4.Text;
            Session["price"] = Box5.Text;

            Response.Redirect("Hrs.aspx");

        }
        protected void BtnCheck_Click(object sender, EventArgs e)
        {

            JObject jObject = new JObject();

            jObject.Add("from", frombox.Text);
            jObject.Add("to", tobox.Text);
            jObject.Add("infants", Box1.Text);
            jObject.Add("children", Box2.Text);
            jObject.Add("adults", Box3.Text);
            jObject.Add("seniors", Box4.Text);

            try
            {

                StorageCredentials creds = new StorageCredentials(accountName, accountKey);     //Account and key are already initialized
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient(); //Create an instance of a Cloud QueueClient object to access your queue in the storage

                // Retrieve a reference to a specific queue
                CloudQueue queue = queueClient.GetQueueReference("flightrequestqueue");

                // Create the queue if it doesn't already exist
                queue.CreateIfNotExists();

                //remove any existing messages (just in case)
                queue.Clear();

                // Create a message and add it to the queue.
                CloudQueueMessage message = new CloudQueueMessage(jObject.ToString());
                queue.AddMessage(message);


                //Show in the console that some activity is going on in the Web Role
                Debug.WriteLine("Message '" + message + "'stored in Queue");
            }
            catch (Exception ee) {; }



            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                // Retrieve a reference to a queue
                CloudQueue queue = queueClient.GetQueueReference("flightresponsequeue");
                try
                {
                    // Create the queue if it doesn't already exist
                    queue.CreateIfNotExists();

                    // retrieve the next message
                    CloudQueueMessage readMessage = queue.GetMessage();

                    // Display message (populate the textbox with the message you just retrieved.
                    Box5.Text = readMessage.AsString;

                    //Delete the message just read to avoid reading it over and over again
                    queue.DeleteMessage(queue.GetMessage());
                }
                catch (Exception ee) { Debug.WriteLine("Problem reading from queue"); }
            }
            catch (Exception eee) {; }

        }


    }
}
//Checks inputs and saves with sessions. 
// Checking price, adds input to json obj and then sends with turturial to queue
// reqeust queue and then respond queue
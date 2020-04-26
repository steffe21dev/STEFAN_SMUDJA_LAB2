using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class Hrs : Page
    {

        private string accountName = "cloudstoragestefan";
        private string accountKey = "H9FBXvM3GXDiGQlWS6Lfj5elbhUxDSBPVOpmcmDuP8U9VSYnenhIkPjTzqJBduT/+T4gn8EsNeQarlTtAKjorA==";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                if (Session["travelers"] != null)
                {

                    Textbox1.Text = Session["travelers"].ToString();

                }
                if (Session["nights"] != null)
                {

                    Textbox2.Text = Session["nights"].ToString();

                }
                if (Session["hotelSeniors"] != null)
                {

                    Textbox3.Text = Session["hotelSeniors"].ToString();

                }
                if (Session["name"] != null)
                {

                    Textbox4.Text = Session["name"].ToString();

                }
                if (Session["single"] != null)
                {

                    singleRoom.Checked = (bool)Session["single"];

                }
                if (Session["double"] != null)
                {

                    doubleRoom.Checked = (bool)Session["double"];

                }
                if (Session["hotelprice"] != null)
                {

                    Textbox5.Text = Session["hotelprice"].ToString();

                }
            }
        }

        protected void BtnNext_Click(object sender, EventArgs e)
        {


            Session["travelers"] = Textbox1.Text;
            Session["nights"] = Textbox2.Text;
            Session["hotelSeniors"] = Textbox3.Text;
            Session["name"] = Textbox4.Text;
            Session["single"] = singleRoom.Checked;
            Session["double"] = doubleRoom.Checked;
            Session["hotelprice"] = Textbox5.Text;



            Response.Redirect("Ps.aspx");

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Session["travelers"] = Textbox1.Text;
            Session["nights"] = Textbox2.Text;
            Session["hotelSeniors"] = Textbox3.Text;
            Session["name"] = Textbox4.Text;
            Session["single"] = singleRoom.Checked;
            Session["double"] = doubleRoom.Checked;
            Session["hotelprice"] = Textbox5.Text;

            Response.Redirect("Frs.aspx");

        }

        protected void BtnCheck_Click(object sender, EventArgs e)
        {

            JObject jObject = new JObject();

            jObject.Add("single", singleRoom.Checked);
            jObject.Add("double", doubleRoom.Checked);
            jObject.Add("travelers", Textbox1.Text);
            jObject.Add("nights", Textbox2.Text);
            jObject.Add("hotelSeniors", Textbox3.Text);
            jObject.Add("name", Textbox4.Text);

            try
            {

                StorageCredentials creds = new StorageCredentials(accountName, accountKey);     //Account and key are already initialized
                CloudStorageAccount storageAccount = new CloudStorageAccount(creds, useHttps: true);

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient(); //Create an instance of a Cloud QueueClient object to access your queue in the storage

                // Retrieve a reference to a specific queue
                CloudQueue queue = queueClient.GetQueueReference("hotelrequestqueue");

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
                CloudQueue responsequeue = queueClient.GetQueueReference("hotelresponsequeue");
                try
                {
                    // Create the queue if it doesn't already exist
                    responsequeue.CreateIfNotExists();

                    // retrieve the next message
                    CloudQueueMessage readMessage = responsequeue.GetMessage();


                    // Display message (populate the textbox with the message you just retrieved.
                    Textbox5.Text = readMessage.AsString;

                    //Delete the message just read to avoid reading it over and over again
                    responsequeue.DeleteMessage(responsequeue.GetMessage());
                }
                catch (Exception ee) { Debug.WriteLine("Problem reading from queue"); }
            }
            catch (Exception eee) {; }

        }
    }
}

//same as FRS
//Checks inputs and saves with sessions. 
// Checking price, adds input to json obj and then sends with turturial to queue
// reqeust queue and then respond queue
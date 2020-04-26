using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class Ps : Page
    {

        private string accountName = "cloudstoragestefan";
        private string accountKey = "H9FBXvM3GXDiGQlWS6Lfj5elbhUxDSBPVOpmcmDuP8U9VSYnenhIkPjTzqJBduT/+T4gn8EsNeQarlTtAKjorA==";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                totalAmount.Text = (int.Parse(Session["price"].ToString()) + int.Parse(Session["hotelPrice"].ToString())).ToString();

                if (Session["creditCard"] != null)
                {

                    cardNbr.Text = Session["creditCard"].ToString();

                }
                if (Session["payName"] != null)
                {

                    nameBox.Text = Session["payName"].ToString();

                }

            }
        }


        //Proceeds forward to last screen
        protected void BtnPay_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Trs.aspx");

        }


        //goes back to previous screen (HRS)
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Session["creditCard"] = cardNbr.Text;
            Session["payName"] = nameBox.Text;
            Response.Redirect("Hrs.aspx");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{

    public partial class Trs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["checkBox"] != null)
                {

                    Checkbox.Checked = (bool)Session["checkBox"];

                }
            }
        }

        protected void BtnPost_Click(object sender, EventArgs e)
        {


            Session["checkBox"] = Checkbox.Checked;


            Response.Redirect("Frs.aspx");

        }
    }
}

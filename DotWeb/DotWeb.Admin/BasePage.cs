using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotWeb.Admin
{
    public class BasePage : System.Web.UI.Page
    {
        protected SchemaInfo schemaInfo;

        protected void Page_Init(object sender, EventArgs e)
        {
            schemaInfo = Application["SchemaInfo"] as SchemaInfo;
        }
    }
}
using DevExpress.Web;
using DotWeb.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotWeb.Admin
{
    public partial class Apps : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gridView_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            GridViewHelper.gridView_CustomColumnDisplayText(sender, e, schemaInfo.App.GridTextColumnMaxLength);
        }
    }
}
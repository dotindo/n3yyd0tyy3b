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
    public partial class Tables : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gridView_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            GridViewHelper.gridView_CustomColumnDisplayText(sender, e, schemaInfo.App.GridTextColumnMaxLength);
        }

        protected void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if ((string)Session["AppId"] == e.Parameters) return;
            Session["AppId"] = e.Parameters;
            gridView.DataBind();
        }

        protected void gridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            Session["TableId"] = e.KeyValue.ToString();
        }

        protected void gridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["AppId"] = Session["AppId"].ToString();
        }

        protected void appFilterComboBox_DataBound(object sender, EventArgs e)
        {
            if (Session["AppId"] != null)
            {
                foreach (ListEditItem item in appFilterComboBox.Items)
                {
                    if (item.Value.ToString() == Session["AppId"].ToString())
                    {
                        appFilterComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        protected void columnsGridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "DataType")
            {
                var comboBox = e.Editor as ASPxComboBox;
                comboBox.DataSource = Enum.GetNames(typeof(TypeCode));
                comboBox.DataBind();
            }
        }

        protected void columnsGridView_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }

        protected void columnsGridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }

        protected void relationsGridView_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }

        protected void relationsGridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["TableId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }

    }
}
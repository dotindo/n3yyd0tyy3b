using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotWeb.Admin
{
    public partial class Modules : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void gridView_Init(object sender, EventArgs e)
        {
            var gridView = (sender as ASPxGridView);
            gridView.ForceDataRowType(typeof(Group));
        }

        protected void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if ((string)Session["AppId"] == e.Parameters) return;
            Session["AppId"] = e.Parameters;
            gridView.DataBind();
        }

        protected void gridView_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["AppId"] = Session["AppId"].ToString();
        }

        protected void gridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["AppId"] = Session["AppId"].ToString();
        }

        protected void gridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "ShowInLeftMenu")
            {
                (e.Editor as ASPxCheckBox).Checked = true;
            }
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

        protected void modulesGridView_Init(object sender, EventArgs e)
        {
            var grid = sender as ASPxGridView;
            grid.ForceDataRowType(typeof(Module));
        }

        protected void modulesGridView_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "ModuleType")
            {
                var comboBox = e.Editor as ASPxComboBox;
                comboBox.DataSource = Enum.GetNames(typeof(ModuleType));
                comboBox.DataBind();
                foreach (ListEditItem item in comboBox.Items)
                {
                    if (item.Value.ToString() == ModuleType.CustomUrl.ToString())
                    {
                        comboBox.SelectedItem = item;
                        comboBox.Enabled = false;
                        break;
                    }
                }
            }
            else if (e.Column.FieldName == "GroupId")
            {
                var comboBox = e.Editor as ASPxComboBox;
                object masterKey = (sender as ASPxGridView).GetMasterRowKeyValue();
                foreach (ListEditItem item in comboBox.Items)
                {
                    if (item.Value.ToString() == masterKey.ToString())
                    {
                        comboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else if (e.Column.FieldName == "ShowInLeftMenu")
            {
                var checkBox = e.Editor as ASPxCheckBox;
                checkBox.Checked = true;
            }
        }

        protected void modulesGridView_BeforePerformDataSelect(object sender, EventArgs e)
        {
            Session["GroupId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }

        protected void modulesGridView_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
        }

        protected void modulesGridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
        }

    }
}
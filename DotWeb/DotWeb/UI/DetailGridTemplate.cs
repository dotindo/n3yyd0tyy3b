using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;

namespace DotWeb.UI
{
    /// <summary>
    /// This class is an implementation of ITemplate. Instance of this class can be placed in grid view's DetailRow template.
    /// </summary>
    public class DetailGridTemplate : ITemplate
    {
        private Control parent;
        private object masterKey;
        private TableMetaRelation detailTable;
        private TableMeta masterTableMeta;
        private string connectionString;
        private List<PermissionType> permissions;
        public ASPxButton btnExport;
        private ASPxGridViewExporter masterGridExport;

        /// <summary>
        /// Parameterized constructor of <see cref="DetailGridTemplate"/>.
        /// </summary>
        /// <param name="masterTableMeta">Meta data of master table.</param>
        /// <param name="connectionString">The connection string to underlying database.</param>
        public DetailGridTemplate(TableMeta masterTableMeta, string connectionString, List<PermissionType> permissions)
        {
            this.masterTableMeta = masterTableMeta;
            if (masterTableMeta.Children.Where(c => c.IsRendered).Count() != 1)
                throw new ArgumentException(string.Format("Master table {0} has no child table or more than 1 child tables.", masterTableMeta.Name));
            this.detailTable = masterTableMeta.Children[0];
            this.connectionString = connectionString;
            this.permissions = permissions;
        }

        /// <summary>
        /// Creates detail grid view and its header (H3) in a container.
        /// </summary>
        /// <param name="container">The container control in which this template is instantiated.</param>
        public void InstantiateIn(Control container)
        {
            parent = container;
            masterKey = ((GridViewDetailRowTemplateContainer)parent).KeyValue;
            var gridCreator = new DetailGridCreator(detailTable, masterTableMeta, masterKey, connectionString, permissions);

            btnExport = buttonExportGrid();
            masterGridExport = CreateGridExport();
            if (masterTableMeta.Caption.ToLower() == "alterations" || masterTableMeta.Caption.ToLower() == "vehicleorders" ||
                masterTableMeta.Caption.ToLower() == "packinglists" || masterTableMeta.Caption.ToLower() == "dialogs" || masterTableMeta.Caption.ToLower() == "productionsequence")
            {
                parent.Controls.Add(btnExport);
            }
            parent.Controls.Add(new LiteralControl(string.Format("<h3>{0}</h3>", detailTable.Caption)));
            parent.Controls.Add(gridCreator.CreateDetailGrid());
            parent.Controls.Add(masterGridExport);

        }
        public ASPxButton buttonExportGrid()
        {
            btnExport = new ASPxButton();
            btnExport.ID = "btnExportGrid";
            btnExport.Text = "Export to excel";
            btnExport.Click += btnExport_click;
            return btnExport;
        }
        public ASPxGridViewExporter CreateGridExport()
        {

            masterGridExport = new ASPxGridViewExporter();

            masterGridExport.GridViewID = string.Concat(masterTableMeta.Children[0], "GridView");
            masterGridExport.ID = "GridExport";
            masterGridExport.ExportedRowType = GridViewExportedRowType.All;

            return masterGridExport;
        }
        void btnExport_click(object sender, EventArgs e)
        {
            //masterGridExport.WriteXlsToResponse(new de);
            masterGridExport.WriteXlsxToResponse();

        }
    }
}

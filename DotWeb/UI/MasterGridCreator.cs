﻿using DevExpress.Web;
using DotWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace DotWeb.UI
{
    /// <summary>
    /// <para>This is the creator of master grid in a grid view page, whether grid view is used in one table scenario,
    /// master-detail scenario, and master-multiple details scenario.</para>
    /// <para>The creation of grid view depends heavily on meta data (see <see cref="TableMeta"/> created from the generator
    /// in inspection phase.</para>
    /// </summary>
    public class MasterGridCreator
    {
        private TableMeta tableMeta;
        private string connectionString;
        List<PermissionType> permissions;

        /// <summary>
        /// Custom constructor accepting an instance of <see cref="TableMeta"/> and connection string.
        /// </summary>
        /// <param name="tableMeta">An instance of <see cref="TableMeta"/>.</param>
        /// <param name="connectionString">String, a connection string to the database being rendered.</param>
        /// <param name="permissions"></param>
        public MasterGridCreator(TableMeta tableMeta, string connectionString, List<PermissionType> permissions)
        {
            this.tableMeta = tableMeta;
            this.connectionString = connectionString;
            this.permissions = permissions;
        }

        /// <summary>
        /// Entry point to create master grid view.
        /// </summary>
        /// <returns>An instance of <see cref="ASPxGridView"/>.</returns>
        public ASPxGridView CreateMasterGrid()
        {
            var masterGrid = new ASPxGridView();
            masterGrid.ID = tableMeta.Name.ToCamelCase() + "Grid";
            masterGrid.ClientInstanceName = "masterGrid";
            masterGrid.CssClass = "gridView";
            masterGrid.AutoGenerateColumns = false;
            masterGrid.SettingsPager.PageSize = tableMeta.App.PageSize;
            masterGrid.Paddings.Padding = new Unit("0px");
            masterGrid.Border.BorderWidth = new Unit("0px");
            masterGrid.BorderBottom.BorderWidth = new Unit("1px");
            masterGrid.Settings.ShowGroupPanel = true;
            masterGrid.AutoGenerateColumns = false;
            masterGrid.SettingsBehavior.ConfirmDelete = true;
            masterGrid.Columns.Add(GridViewHelper.AddGridViewCommandColumns(
                permissions.Contains(PermissionType.Insert), permissions.Contains(PermissionType.Update), permissions.Contains(PermissionType.Delete)
            ));
            masterGrid.CustomColumnDisplayText += masterGrid_CustomColumnDisplayText;
            masterGrid.CellEditorInitialize += masterGrid_CellEditorInitialize;

            // Create grid view columns
            foreach (var column in tableMeta.Columns.OrderBy(c => c.OrderNo))
            {
                if (!column.DisplayInGrid)
                    continue;

                var dataColumn = GridViewHelper.AddGridViewDataColumn(column, connectionString);
                if (dataColumn != null)
                    masterGrid.Columns.Add(dataColumn);
            }
            masterGrid.KeyFieldName = string.Join(";", tableMeta.PrimaryKeys.Select(x => x.Name));
            masterGrid.DataSource = GridViewHelper.GetGridViewDataSource(tableMeta, connectionString);

            // Master-detail scenario
            if (tableMeta.Children.Where(c => c.IsRendered).Count() == 1)
            {
                masterGrid.SettingsDetail.ShowDetailRow = true;
                masterGrid.Templates.DetailRow = new DetailGridTemplate(tableMeta, connectionString, permissions);
            }
            // Master-multiple details scenario
            else if (tableMeta.Children.Where(c => c.IsRendered).Count() > 1)
            {
                masterGrid.SettingsDetail.ShowDetailRow = true;
                masterGrid.Templates.DetailRow = new MultipleDetailGridTemplate(tableMeta, connectionString, permissions);
            }

            return masterGrid;
        }

        /// <summary>
        /// This is for truncation of long text in a text-column.
        /// </summary>
        /// <param name="sender">The sender is an instance of <see cref="ASPxGridView"/>.</param>
        /// <param name="e">Event args, an instance of <see cref="ASPxGridViewColumnDisplayTextEventArgs"/></param>
        void masterGrid_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            GridViewHelper.gridView_CustomColumnDisplayText(sender, e, tableMeta.App.GridTextColumnMaxLength);
        }

        void masterGrid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            GridViewHelper.gridView_CellEditorInitialize(tableMeta, e);
        }

    }
}

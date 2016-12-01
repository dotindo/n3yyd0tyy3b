﻿using DevExpress.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace DotWeb.UI
{
    /// <summary>
    /// <para>This class is an implementation of ITemplate, just like <see cref="DetailGridTemplate"/>. 
    /// Instance of this class can be placed in grid view's DetailRow template.</para>
    /// <para>The only difference is the amount of detail grid that it can display. As its name implied, this
    /// template displays multiple detail grid views in a <see cref="TabControl"/>.</para>
    /// </summary>
    public class MultipleDetailGridTemplate : ITemplate
    {
        private Control parent;
        private object masterKey;
        private TableMeta masterTableMeta;
        private string connectionString;
        private List<PermissionType> permissions;

        /// <summary>
        /// Paramterized constructor of <see cref="MultipleDetailGridTemplate"/>.
        /// </summary>
        /// <param name="masterTableMeta">Meta data of master table.</param>
        /// <param name="connectionString">The connection string to underlying database.</param>
        /// <param name="permissions"></param>
        public MultipleDetailGridTemplate(TableMeta masterTableMeta, string connectionString, List<PermissionType> permissions)
        {
            this.masterTableMeta = masterTableMeta;
            this.connectionString = connectionString;
            this.permissions = permissions;
        }

        /// <summary>
        /// Creates detail grid view in a container.
        /// </summary>
        /// <param name="container">The container control in which this template is instantiated.</param>
        public void InstantiateIn(Control container)
        {
            parent = container;
            masterKey = ((GridViewDetailRowTemplateContainer)parent).KeyValue;

            var pageControl = new ASPxPageControl();
            foreach (var childTableMeta in masterTableMeta.Children.Where(c => c.IsRendered == true))
            {
                var tabPage = new TabPage(childTableMeta.Caption);
                var gridCreator = new DetailGridCreator(childTableMeta, masterTableMeta, masterKey, connectionString, permissions);
                tabPage.Controls.Add(gridCreator.CreateDetailGrid());
                pageControl.TabPages.Add(tabPage);
            }
            parent.Controls.Add(pageControl);
        }
    }
}

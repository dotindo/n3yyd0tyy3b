﻿using DevExpress.Web;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;

namespace DotWeb.UI
{
    /// <summary>
    /// Descendant of <see cref="ASPxNavBar"/> for menu navigation.
    /// </summary>
    [ToolboxData("<{0}:LeftMenu runat=server></{0}:LeftMenu>")]
    public class LeftMenu : ASPxNavBar
    {
        /// <summary>
        /// Renders left menu based on <see cref="ModuleGroup"/> and <see cref="Module"/> stored in admin database.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (ConfigurationManager.AppSettings["appId"] == null)
                throw new ArgumentException("appId must be specified in configuration file.");
            int appId = int.Parse(ConfigurationManager.AppSettings["appId"]);
            var dotWebDb = new DotWebDb();
            var groups = dotWebDb.ModuleGroups
                .Include(g => g.App)
                .Include(g => g.Modules)
                .Where(g => g.App.Id == appId && g.ShowInLeftMenu == true)
                .OrderBy(o => o.OrderNo).ToList();

            this.Groups.Clear();
            foreach (var group in groups)
            {
                var navBarGroup = new DevExpress.Web.NavBarGroup(group.Title);
                var modules = group.Modules.Where(m => m.ShowInLeftMenu == true).OrderBy(m => m.OrderNo);
                foreach (var module in modules)
                {
                    var moduleUrl = module.Url;
                    if (module.ModuleType == ModuleType.AutoGenerated)
                        moduleUrl = "~/" + module.TableName + "/list";
                    navBarGroup.Items.Add(new DevExpress.Web.NavBarItem(module.Title, module.Title, null, moduleUrl));
                }
                this.Groups.Add(navBarGroup);
            }
            dotWebDb.Dispose();
        }
    }
}
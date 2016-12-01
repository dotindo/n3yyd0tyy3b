using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace DotWeb.UI
{
    /// <summary>
    /// <para>Ancestor page for List page type.</para>
    /// <para>There are several page types in auto-generated pages: List page type, Edit page type, and Detail page type.</para>
    /// <para>List page type displays several data in grid view, read-write or read only. Edit page type displays single data in
    /// form view, read-write. Detail page type is just like Edit page type, but read-only.</para>
    /// </summary>
    public class ListPage : Page
    {
        protected TableMeta tableMeta;
        protected ASPxGridView masterGrid;
        protected string connectionString;
        protected List<PermissionType> permissions;

        public ListPage() : base()
        {
            if (ConfigurationManager.ConnectionStrings["AppDb"] == null)
                throw new ArgumentException("You have to specify AppDb connection string name in web.config.");
            connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ToString();

            Page.Init += Page_Init;
            Page.Load += Page_Load;
        }

        /// <summary>
        /// Page Init event; creates all required controls in a page.
        /// </summary>
        /// <param name="sender">The page sending the event.</param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            var tableName = RouteData.Values["module"] == null ? "" : RouteData.Values["module"].ToString();
            var schemaInfo = Application["SchemaInfo"] as SchemaInfo;

            tableMeta = schemaInfo.Tables.Where(s => s.Name.Equals(tableName, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
            if (tableMeta == null)
                Response.Redirect("~/404.aspx");

            var user = HttpContext.Current.Session["user"] as User;
            //var sessionName = string.Format("accessRights_{0}_{1}", user == null ? "default" : user.UserName, HttpContext.Current.Request.RawUrl);
            var sessionName = string.Format("accessRights_{0}_{1}", user == null ? "reader" : user.UserName, HttpContext.Current.Request.RawUrl);
            permissions = Session[sessionName] as List<PermissionType>;
            if (permissions == null)
            {
                //set default to view only permission
                permissions = new List<PermissionType>();
                permissions.Add(PermissionType.Read);
            }
            var gridCreator = new MasterGridCreator(tableMeta, connectionString, permissions);
            masterGrid = gridCreator.CreateMasterGrid();
            var masterPage = this.Controls[0] as IMainMaster;
            if (masterPage == null)
                Response.Write("<p>Your master page must implement IMainMaster interface.</p>");
            else
            {
                var panel = new System.Web.UI.WebControls.Panel();
                panel.CssClass = "mainContent";
                panel.Controls.Add(new LiteralControl(string.Format("<h2>{0}</h2>", tableMeta.Caption)));
                panel.Controls.Add(masterGrid);

                masterPage.MainContent.Controls.Add(panel);
                masterPage.PageTitle.Controls.Add(new LiteralControl(tableMeta.Caption));
            }
        }

        /// <summary>
        /// Page Load event; bind the grid view.
        /// </summary>
        /// <param name="sender">The page sending the event.</param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            masterGrid.DataBind();
        }

    }
}

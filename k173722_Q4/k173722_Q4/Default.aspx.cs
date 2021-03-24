using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace k173722_Q4
{
    public partial class Default : System.Web.UI.Page
    {
        string rootPath = ConfigurationManager.AppSettings["rootFolder"];
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack) {
                loadDropDown();
                loadSelection();
            }
        }

        protected void loadAll() {
            ds.Clear();
            ds = new DataSet();
            string[] folders = Directory.GetDirectories(rootPath);

            foreach (string folder in folders)
            {
                FileInfo newestFile = GetNewestFile(new DirectoryInfo(folder));
                ds.ReadXml(folder + @"\" + newestFile.ToString());
            }

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        // function to return latest filename
        public static FileInfo GetNewestFile(DirectoryInfo directory)
        {
            return directory.GetFiles()
                .Union(directory.GetDirectories().Select(d => GetNewestFile(d)))
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault();
        }

        protected void loadDropDown() {
            string[] folders = Directory.GetDirectories(rootPath);

            DropDownList1.Items.Add(string.Empty);
            foreach (string name in folders)
            {
                DropDownList1.Items.Add(Path.GetFileName(name));
            }
        }

        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (DropDownList1.SelectedItem.ToString().Equals(string.Empty))
            {
                this.loadAll();
            }
            else {
                this.loadSelection();
            }
        }

        protected void loadSelection() {

            if (DropDownList1.SelectedItem.ToString().Equals(string.Empty))
            {
                this.loadAll();
            }
            else {
                ds.Clear();
                ds = new DataSet();

                string folder = rootPath + @"\" + DropDownList1.SelectedItem.ToString();

                FileInfo newestFile = GetNewestFile(new DirectoryInfo(folder));
                ds.ReadXml(folder + @"\" + newestFile.ToString());

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            this.loadAll();
            DropDownList1.SelectedIndex = 0;
            GridView1.PageIndex = 0;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            loadSelection();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string rootPath;
        
        public Form1()
        {
            InitializeComponent();
            InitializeInterface();
            loadAllData();
        }

        public void loadAllData() {

            try
            {
                DataSet dataSet = new DataSet();
                
                string[] folders = Directory.GetDirectories(rootPath);

                foreach (string name in folders)
                {
                    comboBox1.Items.Add(Path.GetFileName(name));
                }

                foreach (string folder in folders)
                {
                    FileInfo newestFile = GetNewestFile(new DirectoryInfo(folder));
                    dataSet.ReadXml(folder + @"\" + newestFile.ToString());
                }
                dataGridView1.DataSource = dataSet.Tables[0];
                dataGridView1.DataBindingComplete += (o, e) =>
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        dataGridView1.Rows[i].HeaderCell.Value = (dataGridView1.Rows[i].Index + 1).ToString();
                };

                // setting data gridview length
                dataGridView1.RowHeadersWidth = 60;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        // function to return latest filename
        public static FileInfo GetNewestFile(DirectoryInfo directory)
        {
            return directory.GetFiles()
                .Union(directory.GetDirectories().Select(d => GetNewestFile(d)))
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadAllData();
            dataGridView1.Update();
            dataGridView1.Refresh();

            try
            {
                comboBox1.SelectedIndex = -1;
            }
            catch (NullReferenceException ex) { 
                
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Configures the appearance and behavior of a DataGridView control.
        private void InitializeInterface()
        {
            this.rootPath = ConfigurationManager.AppSettings["rootFolder"];
            
            // Initialize basic DataGridView properties.
            //dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.BackgroundColor = Color.LightGray;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            // Set property values appropriate for read-only display and 
            // limited interactivity. 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Set the selection background color for all the cells.
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Set RowHeadersDefaultCellStyle.SelectionBackColor so that its default
            // value won't override DataGridView.DefaultCellStyle.SelectionBackColor.
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Empty;

            // Set the background color for all rows and for alternating rows. 
            // The value for alternating rows overrides the value for all rows. 
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.DarkGray;

            // Set the row and column header styles.
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;

            dataGridView1.DefaultCellStyle.Font = new Font("Tahoma", 9F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10F, FontStyle.Bold);

            // editing for combox box
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet dataSet = new DataSet();

            string folder = rootPath + @"\" + comboBox1.SelectedItem.ToString();

            FileInfo newestFile = GetNewestFile(new DirectoryInfo(folder));
            dataSet.ReadXml(folder + @"\" + newestFile.ToString());
            
            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.DataBindingComplete += (o, e1) =>
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].HeaderCell.Value = (dataGridView1.Rows[i].Index + 1).ToString();
            };

            // setting data gridview length
            dataGridView1.RowHeadersWidth = 60;
        }
    }
}

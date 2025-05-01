using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PhoneList
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
        SqlDataAdapter adapter;
        DataTable table;
        public Form1()
        {
            InitializeComponent();
            DropBoxItems();
            ListPeople();
            this.dataGridView1.Font = new Font("Arial", 12);

            //ChangeCellContext();
            if (dep_box_combo.Text.Equals(""))
            {
                ListPeople();
            }
            if (textEdit3.Text.Equals(""))
            {
                ListPeople();
            }

            //Databaseden gelen başlık isimlerini değiştirme
            /*
            dataGridView1.Columns[3].HeaderText = "Table Phone";
            dataGridView1.Columns[4].HeaderText = "Cellular Phone";
            */
            //Databaseden gelen başlık isimlerini şekillendirme   
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].HeaderCell.Style.Font = new Font("Arial", 12);
            }

            //id gizleme
            this.dataGridView1.Columns[2].Visible = false;

            //Satırların arka plan renklerini ayarlama
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            //Mavi satırlardaki yazı rengi
            //dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;

            //Enter tuşunu engelleme
            textEdit3.KeyPress += new KeyPressEventHandler(keypressed);

            Configuration config = ConfigurationManager.OpenExeConfiguration("PhoneList.exe");
            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");

        }
        public void ListPeople()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            connection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM telephoneList", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[2].Visible = false;
            dep_box_combo.Text = "";
            textEdit3.Text = "";
            connection.Close();
            int rowCount = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                rowCount++;
            }
            for (int i = 0; i < rowCount; i++)
            {
                string item = dataGridView1.Rows[i].Cells[3].Value.ToString();
                if (dep_box_combo.Items.Contains(item))
                {
                    continue;
                }
                else
                {
                    dep_box_combo.Items.Add(item);
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    string loc = dataGridView1.Rows[j].Cells[0].Value.ToString();
                    if (dataGridView1.Rows[i].Cells[0].Value.Equals(loc))
                    { }
                    else
                    {
                        dep_box_combo.Items.Remove(loc);
                        dep_box_combo.Refresh();
                    }
                }
            }
        }

        private void keypressed(Object o, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListPeople();
        }

        private void adminToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form3 admin = new Form3();
            admin.Show();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dep_box_combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            connection.Open();
            adapter = new SqlDataAdapter("select * from telephoneList where Departmant like '" + dep_box_combo.Text + "%' ", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[2].Visible = false;
            connection.Close();
        }
        //Arama methodu
        private void textEdit3_TextChanged(object sender, EventArgs e)
        {
            table = new DataTable();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            connection.Open();
            adapter = new SqlDataAdapter("select * from telephoneList where Name like '" + textEdit3.Text + "%' " +
            "OR Surname like '" + textEdit3.Text + "%' OR Departmant like '" + textEdit3.Text +
            "%' OR TablePhone like '" + textEdit3.Text + "%'" + "OR CellularPhone like '" + textEdit3.Text + "%'", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
        }
        private void DropBoxItems()
        {
            //Comboboxların içindeki itemler
            dep_box_combo.Items.Add("Quality control");
            dep_box_combo.Items.Add("Maintenance");
            dep_box_combo.Items.Add("Marketing Department");
            dep_box_combo.Items.Add("Merchandising department");
            dep_box_combo.Items.Add("Engineering");
            dep_box_combo.Items.Add("Human resource and Administration");
            dep_box_combo.Items.Add("Manufacturing");
            dep_box_combo.Items.Add("Design department");
            dep_box_combo.Items.Add("Production");
            dep_box_combo.Items.Add("Research and development");
            dep_box_combo.Items.Add("Shipping and documentation");
            dep_box_combo.Items.Add("Accounting");
            dep_box_combo.Items.Add("Assembler");
            dep_box_combo.Items.Add("Cost management");
            dep_box_combo.Items.Add("Cutting department");
            dep_box_combo.Items.Add("Textile");
            dep_box_combo.Items.Add("Finance");
            dep_box_combo.Items.Add("Management");

        }
        private void f5Refresh(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ListPeople();
            }
        }

        private void dOSYAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

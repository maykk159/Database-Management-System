using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PhoneList
{
    public partial class Form2 : Form
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
        SqlDataAdapter adapter;
        SqlCommand command;
        DataTable table;

        public Form2()
        {
            InitializeComponent();
            listPeople();
            cep_telefonu_box.Text.TrimStart();
            cep_telefonu_box.Text.TrimEnd();
        }
        public void listPeople()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            connection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM telephoneList", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();

        }
        private void Form2_Load(object sender, EventArgs e)
        {
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
            listPeople();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            cep_telefonu_box.Text.TrimStart();
            id_box.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            name_box.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            surname_box.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            num_box.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            cep_telefonu_box.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            dep_box_combo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            //Farklı dosya veya kontrollerde hata çıkarsa
            //loc.contains veya loc. startswith yapısını dene            
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (name_box.Text != "" && surname_box.Text != "" && dep_box_combo.Text != "")
            {
                string sorgu = "INSERT INTO telephoneList(Name,Surname,TablePhone, ID, CellularPhone, Departmant) VALUES " +
                    "(@Name,@Surname,@TablePhone,@ID,@CellularPhone, @Departmant)";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@Name", name_box.Text);
                command.Parameters.AddWithValue("@Surname", surname_box.Text);
                command.Parameters.AddWithValue("@TablePhone", num_box.Text);
                command.Parameters.AddWithValue("@ID", id_box.Text);
                command.Parameters.AddWithValue("@CellularPhone", cep_telefonu_box.Text);
                command.Parameters.AddWithValue("@Departmant", dep_box_combo.Text);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Added successfully!");
                id_box.Text = "";
                name_box.Text = "";
                surname_box.Text = "";
                num_box.Text = "";
                cep_telefonu_box.Text = "";
                dep_box_combo.Text = "";
                listPeople();
            }

            else
                MessageBox.Show("Lütfen boş alan bırakmayınız!");
        }

        private void btn_del_Click(object sender, EventArgs e)
        {

            int numara = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
            deleteRow(numara);

            MessageBox.Show("Silme işlemi başarılı!");
            id_box.Text = "";
            name_box.Text = "";
            surname_box.Text = "";
            num_box.Text = "";
            cep_telefonu_box.Text = "";
            dep_box_combo.Text = "";
            listPeople();
        }
        private void deleteRow(int numara)
        {
            string sql = "DELETE FROM telephoneList WHERE ID=@id";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", numara);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            if (name_box.Text != "" && surname_box.Text != "" && dep_box_combo.Text != "")
            {
                string sorgu = "UPDATE telephoneList SET Name=@Name,Surname=@Surname,CellularPhone=@CellularPhone," +
                    "TablePhone=@TablePhone,Departmant=@Departmant WHERE ID=@id";
                command = new SqlCommand(sorgu, connection);
                command.Parameters.AddWithValue("@id", id_box.Text);
                command.Parameters.AddWithValue("@Name", name_box.Text);
                command.Parameters.AddWithValue("@Surname", surname_box.Text);
                command.Parameters.AddWithValue("@TablePhone", num_box.Text);
                command.Parameters.AddWithValue("@CellularPhone", cep_telefonu_box.Text);
                command.Parameters.AddWithValue("@Departmant", dep_box_combo.Text);
                //Farklı dosya veya kontrollerde hata çıkarsa
                //loc.contains veya loc. startswith yapısını dene
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Update Completed!");
                id_box.Text = "";
                name_box.Text = "";
                surname_box.Text = "";
                num_box.Text = "";
                cep_telefonu_box.Text = "";
                listPeople();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Arama methodu
        private void textEdit3_TextChanged_1(object sender, EventArgs e)
        {
            table = new DataTable();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);
            connection.Open();
            adapter = new SqlDataAdapter("select * from telephoneList where Name like '" + textEdit3.Text + "%' " +
            "OR Surname like '" + textEdit3.Text + "%' OR Departmant like '" + textEdit3.Text +
            "%' OR CellularPhone like '" + textEdit3.Text + "%'" + "OR TablePhone like '" + textEdit3.Text + "%'", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
        }
    }
}

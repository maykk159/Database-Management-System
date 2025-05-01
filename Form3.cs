using PhoneList;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PhoneList
{
    public partial class Form3 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dr;
        public Form3()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBConnectionString"].ConnectionString);

            //string user = username_box.Text;
            //string pass = password_box.Text;

            command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM adminTable where admin='" + username_box.Text + "' AND password='" + password_box.Text + "'";
            dr = command.ExecuteReader();
            if (dr.Read())
            {
                Form2 yönetim = new Form2();
                yönetim.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please check your username and password!!!");
            }
            connection.Close();
        }
    }
}

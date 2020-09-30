using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SuperMarket
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int i;
            string user = textBox1.Text;
            string password = textBox2.Text;
            if (radioButton2.Checked)
            {
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "SELECT * FROM loginadmin WHERE admin = '" + user + "' AND password = '" + password + "'";
                cmd.ExecuteNonQuery();
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable(); //this is creating a virtual table  
                sda.Fill(dt);
                i = Int32.Parse(dt.Rows.Count.ToString());
                if (i == 1)
                {
                    this.Hide();
                    Form1 f1 = new Form1();
                    MessageBox.Show("Login Successfull");
                    MessageBox.Show("he is george");
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }               
                

                con.Close();


            }
            if (radioButton1.Checked)
            {
                OleDbCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "SELECT * FROM login WHERE user = '" + user + "' AND password = '" + password + "'";
                cmd.ExecuteNonQuery();
                OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable(); //this is creating a virtual table  
                sda.Fill(dt);
                i = Int32.Parse(dt.Rows.Count.ToString());
                if (i == 1)
                {
                    this.Hide();
                    Form1 f1 = new Form1();
                    MessageBox.Show("Login Successfull");
                    MessageBox.Show("user");
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
                
                con.Close();
            }
        }
    }
}

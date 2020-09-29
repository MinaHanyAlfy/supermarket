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
    public partial class Search : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
        public Search()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            con.Open();
           
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "select * from product ;";
            cmd.Connection = con;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "select * from product where prodname like'"+ this.textBox1.Text +"%';";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            DataTable dtblBook = new DataTable();
            dtblBook.Load(dr);
            dataGridView1.DataSource = dtblBook;
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "select * from product where ID like'"+this.textBox2.Text+"%';";
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            DataTable dtblBook = new DataTable();
            dtblBook.Load(dr);
            dataGridView1.DataSource = dtblBook;
            con.Close();
        }
    }
}

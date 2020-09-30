using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class store : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");

        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        BindingSource bs;
        DataTable dt;
        OleDbDataReader dr;
        public store()
        {
            InitializeComponent();
        }
        public void refresh()
        {
            try
            {
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "select* from store";
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bills_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                refresh();
            }
            else
            {
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = cmd.CommandText = "select * from store where prod_id like @id ";


                cmd.Parameters.AddWithValue("@id", Int16.Parse(textBox1.Text));

                cmd.ExecuteNonQuery();
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
        }
    }
}

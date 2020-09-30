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
    public partial class bill_table : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
     
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        BindingSource bs;
        DataTable dt;
        OleDbDataReader dr;
        public bill_table()
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
                cmd.CommandText = "select* from bill";
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

        private void bill_table_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "select* from bill";
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                DataTable dt = new DataTable();

                da.Fill(dt);
                //for (int i = 0; i <= dt.Rows.Count; i++) {
                //    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0];
                //    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1];
                //    dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2];
                //    dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3];
                //    dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4];
                //    dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5];
                //    dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][6];
                //}
                dataGridView1.DataSource = dt;
                con.Close();
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                cmd.CommandText = cmd.CommandText = "select * from bill where ID like @id ";


                cmd.Parameters.AddWithValue("@id", Int16.Parse(textBox1.Text));

                cmd.ExecuteNonQuery();
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Hide();
                bill_update ob = new bill_update();
                con.Open();

                int bill_id=Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd = new  OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = " select * from bill where ID=" + bill_id + " ;";
                da = new  OleDbDataAdapter(cmd);
                dt = new  DataTable();

                da.Fill(dt);
                ob.dataGridView1.DataSource = dt;
                ob.ShowDialog();
                con.Close();
        }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ في البرنامج يرجي اعادة التشغيل");
            }
}
    }
}

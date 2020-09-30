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
    public partial class Delete : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
        string code;
        int count;
        double price;
        string kind;
        string name;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        BindingSource bs;
        DataTable dt;
        OleDbDataReader dr;
        public Delete()
        {
            InitializeComponent();
        }

        private void Delete_Load(object sender, EventArgs e)
        {
            con.Open();
            cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from product";
            cmd.ExecuteNonQuery();
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            bs = new BindingSource();
            bs.DataSource = ds.Tables[0];
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "prodname";
            comboBox1.ValueMember = "priceofprod";
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            cmd.Connection = con;

            cmd.CommandText = " select * from product where prodname= @name";
            cmd.Parameters.AddWithValue("@name", comboBox1.Text);
            try
            {
                con.Open();

                using (OleDbDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        textBox5.Text = (read["ID"].ToString());
                        textBox3.Text = (read["priceofprod"].ToString());
                        textBox2.Text = (read["numofprod"].ToString());
                        textBox1.Text = (read["prodname"].ToString());
                        textBox4.Text = (read["kindofprod"].ToString());
                        dateTimePicker1.Text = (read["date"].ToString());

                    }
                }
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                code = textBox5.Text;
                name = textBox1.Text;
                count = int.Parse(textBox2.Text);
                kind = textBox4.Text;
                price = double.Parse(textBox3.Text);
                con.Open();
                cmd = new OleDbCommand("delete from product where ID= @id", con);
                cmd.Parameters.AddWithValue("@id", code);
                cmd.ExecuteNonQuery();
                MessageBox.Show("تم الحذف بنجاح", "Congrats");
                con.Close();
            }
            catch (Exception) { MessageBox.Show("حدث خطأ في عملية الحذف يرجي اعادة تشغيل البرنامج"); }
        }
    }
}

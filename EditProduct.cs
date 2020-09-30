using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SuperMarket
{

    public partial class EditProduct : Form
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
        public EditProduct()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            con.Close();
            try
            {
                if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox4.Text) || String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("قيم مفقودة ارجو التأكد من القيم");
            }
            else{ 
                    code = textBox5.Text;
                    name = textBox1.Text;
                    count = int.Parse(textBox2.Text);
                    kind = textBox4.Text;
                    price = double.Parse(textBox3.Text);
                if (price <= 0 || count <= 0)
                {
                    MessageBox.Show("خطأ في قيم السعر او العدد يرجي التعديل ");
                }
                else
                {

                    con.Open();
                    cmd = new OleDbCommand("update product set [prodname]=@name, [numofprod]=@count, [priceofprod]=@price , [kindofprod]=@kind , [date]=@date where [ID]= @id", con);
                    //cmd.Connection = con;
                    //cmd.CommandText = ;
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@count", count);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@kind", kind);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text.ToString());
                    cmd.Parameters.AddWithValue("@id", code);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم التعديل بنجاح", "Congrats");
                    con.Close();
                }
            }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                con.Close();
            }

        }

        private void EditProduct_Load(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button1_Click_1(object sender, EventArgs e)
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
    }
}

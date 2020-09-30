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
    public partial class bill_update : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
       
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        BindingSource bs;
        DataTable dt;
        OleDbDataReader dr;
        public bill_update()
        {
            InitializeComponent();
        }

        double qty()//return exist value of number in bill 
        {
            cmd = new  OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from bill where ID=@billid";
            //cmd.Parameters.AddWithValue("@prodcode", textBox5.Text);
            cmd.Parameters.AddWithValue("@billid", textBox6.Text);
            dt = new DataTable();

            dr = cmd.ExecuteReader();
            dt.Load(dr);
            double x = double.Parse(dt.Rows[0][3].ToString());

            return x;


        }
        double price_bill()//return total price in bill table
        {

             cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandText = "select total_price from bill where ID=@bill2id";
            cmd.Parameters.AddWithValue("@bill2id", textBox6.Text);
            dt = new DataTable();

            dr = cmd.ExecuteReader();
            dt.Load(dr);
            double x = double.Parse(dt.Rows[0][0].ToString());


            return x;





        }


        int qty2()//return the value of number in product table
        {

             OleDbCommand cmd4 = new OleDbCommand();
            cmd4.Connection = con;
            cmd4.CommandText = "select numofprod from product where ID='" + textBox5.Text + "'";
            dt = new DataTable();

            dr = cmd4.ExecuteReader();
            dt.Load(dr);
            int x = Int32.Parse(dt.Rows[0][0].ToString());


            return x;





        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{


                con.Open();
                double last_price = 0;

                cmd = new OleDbCommand();
                cmd.Connection = con;

            double num_bill_befor_update = qty();

                double num = double.Parse(textBox2.Text);
                double total_price = double.Parse(textBox4.Text);
                double price = double.Parse(textBox3.Text);
                if (num <= 0)
                {
                    MessageBox.Show("يوجد خطأ في قيم السعر او العدد");



                }
                else
                {
                    if (num < qty())
                    {
                        total_price = num * price;

                        ////////////// update in bill2 //////////
                        //cmd.CommandText = "update bill2 set  number=@c ,Total_price =@z  where product_code = @code and id=@id";

                        //cmd.Parameters.AddWithValue("@code", textBox5.Text);
                        //cmd.Parameters.AddWithValue("@id", textBox6.Text);
                        //cmd.Parameters.AddWithValue("@z", total_price);
                        //cmd.Parameters.AddWithValue("@c", num);



                        //cmd.ExecuteNonQuery();
                        //MessageBox.Show("تم التعديل بنجاح في جدول التفاصيل ");
                        con.Close();

                        /////////////// update in bill //////////////////
                        con.Open();
                        OleDbCommand cmd2 = new OleDbCommand();
                        double bill_price_update = (num_bill_befor_update - num) * price;
                        cmd2.Connection = con;

                        last_price = price_bill() - bill_price_update;
                    MessageBox.Show(textBox6.Text);
                    MessageBox.Show(num.ToString());
                    MessageBox.Show(last_price.ToString());
                        cmd2.CommandText = "update bill set  [total_price] =?, [prod_num]=?  where [ID] = ?";
                    cmd2.Parameters.AddWithValue("@price", last_price);
                 
                    cmd2.Parameters.AddWithValue("@num", num);
                    cmd2.Parameters.AddWithValue("@id2", textBox6.Text);

                    cmd2.ExecuteNonQuery();
                        MessageBox.Show("تم التعديل بنجاح في الفاتورة ");
                        con.Close();

                        //////////////////////////////// update in product /////////////////////
                        con.Open();
                        double product_num;
                        OleDbCommand cmd3 = new OleDbCommand();
                        product_num = qty2() + num;
                        cmd3.Connection = con;


                        cmd3.CommandText = "update product set  [numofprod] =@number  where [ID] = @procode";

                    cmd3.Parameters.AddWithValue("@number", product_num);
                    cmd3.Parameters.AddWithValue("@procode", textBox5.Text);
                       




                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("تم التعديل بنجاح في السلعة ");
                        con.Close();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";


                    }
                }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["prod_name"].FormattedValue.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["prod_num"].FormattedValue.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["prod_price"].FormattedValue.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["total_price"].FormattedValue.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["prod_id"].FormattedValue.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString();
        }
    }
}

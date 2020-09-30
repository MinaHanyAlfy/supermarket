using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SuperMarket
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
        string code;
        int count;
        double price;
        string kind;
        string name;
        //OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        BindingSource bs;
        DataTable dt;
        OleDbDataReader dr;


        public Form1()
        {
            InitializeComponent();
        }
        public void func_load()
        {
            con.Close();
            try
            {
                textBox3.Focus();
                textBox3.Select();
                textBox3.SelectAll();

                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from product";
                cmd.ExecuteNonQuery();
                da = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                bs = new BindingSource();
                bs.DataSource = ds.Tables[0];
                comboBox2.DataSource = bs;
                comboBox2.DisplayMember = "prodname";
                con.Close();




                OleDbCommand cmd4 = new OleDbCommand();
                con.Open();
                cmd4.Connection = con;
                cmd4.CommandText = "select * from product where prodname='" + comboBox2.Text + "'";
                dt = new DataTable();

                dr = cmd4.ExecuteReader();
                dt.Load(dr);
                textBox3.Text = dt.Rows[0][0].ToString();
                textBox4.Text = dt.Rows[0][3].ToString();
                con.Close();

                // load number of Bill//

                //OleDbCommand cmd11 = new OleDbCommand("select * from bill order by ID DESC LIMIT 1", con);
                ////cmd11.Connection = con;
                ////cmd11.CommandText = "select * from bill order by ID DESC LIMIT 1";
                //con.Open();
                //dr = cmd11.ExecuteReader();
                //dt = new DataTable();
                //dt.Load(dr);
                //int n = Int32.Parse(dt.Rows.Count.ToString());
                //if (n > 1)
                //{

                //    int m = int.Parse(dt.Rows[0][0].ToString());
                //    m = m + 1;
                //    textBox1.Text = m.ToString();
                //    con.Close();
                //}
                //else
                //{
                //    textBox1.Text = "1";
                //    con.Close();
                //}
                // End load number of Bill//


            }

            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                MessageBox.Show("حدث خطأ في تشغيل البرنامج اعد تشغيله من فضلك");
            }
            finally
            {
                con.Close();
            }



        }
        // Add Function ////////////// 
        double qty;
        public void fun_add()
        {
            try
            {

                string nprod = comboBox2.Text;
                qty = double.Parse(textBox5.Text);
                double price = Convert.ToDouble(textBox4.Text);
                string procode = textBox3.Text;

                if (qty <= qyt())
                {
                    double subtoT = qty * price;

                    object[] row = { procode, nprod, qty, price, subtoT };
                    dataGridView1.Rows.Add(row);

                    double total = (Convert.ToDouble(textBox6.Text) + subtoT);

                    textBox6.Text = total.ToString();


                    MessageBox.Show("تم اضافة الصنف للفاتورة بنجاح ");

                }
                else
                {
                    MessageBox.Show("الكمية المطلوبة غير موجودة ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        double extranum;
        double qyt()//number of product
        {
            con.Open();
            OleDbCommand cmd4 = new OleDbCommand();
            cmd4.Connection = con;
            cmd4.CommandText = "select numofprod from product where ID='" + textBox3.Text + "'";
            
            dt = new DataTable();
            dr = cmd4.ExecuteReader();
            dt.Load(dr);
            double x = double.Parse(dt.Rows[0][0].ToString());
            con.Close();
            MessageBox.Show(x.ToString());
            return x;


        }

        // Refresh //////////
        public void refresh()
        {


            OleDbCommand cmd = new OleDbCommand();
            con.Close();
            cmd.Connection = con;

            cmd.CommandText = " select * from product where prodname= @name";
            cmd.Parameters.AddWithValue("@name", comboBox2.Text);
            try
            {

                con.Open();
                using (OleDbDataReader read = cmd.ExecuteReader())
                {

                    while (read.Read())
                    {
                        textBox3.Text = (read["ID"].ToString());
                        textBox4.Text = (read["priceofprod"].ToString());


                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("حدث خطأ في التحميل اعد تشغيل البرنامج");
            }
            finally
            {
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            func_load();
            dateTimePicker2.Value = DateTime.Now;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Adding a1 = new Adding();
            a1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            EditProduct e1 = new EditProduct();
            e1.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //this.();
            Search s1 = new Search();
            s1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            store b1 = new store();
            b1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //store b1 = new store();
            //b1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Delete d1 = new Delete();
            d1.Show();

        }

        private void button11_Click(object sender, EventArgs e)
        {


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fun_add();

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
                comboBox2.Focus();

            }
        }

        double qyt2()//number with update
        {
            con.Close();
            con.Open();
            OleDbCommand cmd4 = new OleDbCommand();
            cmd4.Connection = con;
            cmd4.CommandText = "select numofprod from product where ID='" + textBox3.Text + "'";
            dt = new DataTable();

            dr = cmd4.ExecuteReader();
            dt.Load(dr);
            double x = double.Parse(dt.Rows[0][0].ToString());
            extranum = x - qty;
            con.Close();
            return extranum;





        }

        private void button9_Click(object sender, EventArgs e)
        {

            ///////////////// insert in store /////////////////

            try
            {
                OleDbCommand cmd = new OleDbCommand();
                con.Close();
                cmd.Connection = con;

                cmd.CommandText = "update product set numofprod=@c  where ID = @code";

                cmd.Parameters.AddWithValue("@c", qyt2());

                cmd.Parameters.AddWithValue("@code", textBox3.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                OleDbCommand cmd2 = new OleDbCommand();
                con.Open();
                cmd2.Connection = con;
                cmd2.CommandText = "select * from product where ID=@code2";
                cmd2.Parameters.AddWithValue("@code2", textBox3.Text);
                dr = cmd2.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                con.Close();


                int x = Int32.Parse(dt.Rows[0][2].ToString());
                if (x == 0)
                {
                    OleDbCommand cmd3 = new OleDbCommand();
                    con.Open();
                    cmd3.Connection = con;




                    cmd3.Parameters.AddWithValue("@name", dt.Rows[0][1]);
                    cmd3.Parameters.AddWithValue("@code", dt.Rows[0][0]);
                    cmd3.Parameters.AddWithValue("@num", dt.Rows[0][2]);
                    cmd3.Parameters.AddWithValue("@price", dt.Rows[0][3]);
                    cmd3.Parameters.AddWithValue("@kind", dt.Rows[0][4]);
                    cmd3.Parameters.AddWithValue("@status", "انتهاء الكمية");
                    cmd3.Parameters.AddWithValue("@date", dt.Rows[0][5]);


                    cmd3.CommandText = "insert into store values(@code,@name,@num,@price,@kind,@status,@date) ";

                    cmd3.ExecuteNonQuery();
                    con.Close();
                    OleDbCommand cmd5 = new OleDbCommand();
                    con.Open();
                    cmd5.Connection = con;
                    cmd5.CommandText = "delete from product where ID='" + dt.Rows[0][0] + "' ";

                    cmd5.ExecuteNonQuery();
                    con.Close();


                }





                /////////////////////////// insert into bill  table ///////////////////////

                OleDbCommand cmd7 = new OleDbCommand();
                con.Open();
                cmd7.Connection = con;


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    double num = double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    double price = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    double total_price = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());

                    cmd7.CommandText = "insert into bill(prod_id,prod_name,prod_num,prod_price,prod_kind,total_price,date_sell) values('" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + num + "','" + price + "','" + dt.Rows[0][4].ToString() + "','" + total_price + "','" + dateTimePicker2.Text.ToString() + "') ";


                    cmd7.ExecuteNonQuery();


                }
                con.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("يوجد خطأ في بيانات الفاتورة او في تحميل البرنامج ,اعد تشغيل البرنامج من فضلك");
            }
            finally
            {



                  dataGridView1.Rows.Clear();



                //}



            }

          

           
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            bill_table bl = new bill_table();
            bl.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            func_load();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }  }
    


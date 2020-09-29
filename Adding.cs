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
using System.Data.Common;

namespace SuperMarket
{
    public partial class Adding : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataMarket.mdb");
        public Adding()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Adding_Load(object sender, EventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }
        string code;
        string name;
        double price;
        string kind;
        int count;

        private void button1_Click(object sender, EventArgs e)
        {

            name = textBox1.Text;
            price = double.Parse(textBox3.Text);
            count = int.Parse(textBox2.Text);
            code = textBox5.Text;
            kind = textBox4.Text;
            
            OleDbCommand cmd = new OleDbCommand("insert into product values(@id,@name,@num,@price,@kind,@date)",con);
            con.Open();
                    
            cmd.Parameters.AddWithValue("@id", code);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@num", count);
            cmd.Parameters.AddWithValue("@price",price);
            cmd.Parameters.AddWithValue("@kind", kind);
            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text.ToString());
            cmd.ExecuteNonQuery();
           
            con.Close();
         //   Db.ExecuteNonQuery("Insert into product (ID,prodname,numofprod,priceofprod,kindofprod,code)Values(?)", new object[] { code, name, count,price,kind });
            MessageBox.Show("تم الايداع بنجاح");

        }
    }
}

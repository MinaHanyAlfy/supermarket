using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
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

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bills b1 = new Bills();
            b1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Delete d1 = new Delete();
            d1.Show();

        }
    }
}

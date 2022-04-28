using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet11.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet11.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet11.Products);
            ComboxSelet();
        }

        private void ComboxSelet()
        {
            var q = from o in this.nwDataSet11.Orders
                    select o.OrderDate.Year;

            this.comboBox1.DataSource = q.Distinct().ToList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

            int take = int.Parse(textBox1.Text);
            var q = from p in nwDataSet11.Products
                    select p;

            skip += take;

            this.dataGridView2.DataSource = q.Skip(skip).Take(take).ToList();

            take_old = take;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            var l = from i in files
                    where i.Extension == ".log"
                    select i;

            this.dataGridView1.DataSource = l.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from i in files
                    where i.CreationTime.Year == 2019
                    orderby i.CreationTime ascending
                    select i;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from i in files
                    where i.Length >= 5000
                    orderby i.Length ascending
                    select i;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet11.Orders;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var q = from o in this.nwDataSet11.Orders
                    where !o.IsEmployeeIDNull() && o.OrderDate.Year == (int)comboBox1.SelectedItem
                    select o;

            this.dataGridView1.DataSource = q.ToList();

            var i = from l in this.nwDataSet11.Orders
                    join ld in nwDataSet11.Order_Details
                    on l.OrderID equals ld.OrderID
                    where l.OrderDate.Year.ToString() == comboBox1.Text
                    select l;

            this.dataGridView2.DataSource = i.ToList();

        }
        int take_old =0;
        int skip = 0;
        private void button12_Click(object sender, EventArgs e)
        {
            int take = int.Parse(textBox1.Text);
            var q = from p in nwDataSet11.Products
                    select p;

            skip -= take;

            this.dataGridView2.DataSource = q.Skip(skip).Take(take).ToList();

            take_old = take;
        
        }
    }
}

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
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            
            this.productPhotoTableAdapter1.Fill(this.awDataSet11.ProductPhoto);
            loacomboxdata();
        }

        private void loacomboxdata()
        {
            var q = this.awDataSet11.ProductPhoto.OrderBy(p => p.ModifiedDate.Year).Select(p => p.ModifiedDate.Year);

            this.comboBox3.Text = "選擇年份";
            this.comboBox2.Text = "請選擇季節";
            foreach (int i in q.ToList().Distinct())
            {
                this.comboBox3.Items.Add(i);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.pictureBox1.DataBindings.Clear();
            this.bindingSource1.DataSource = awDataSet11.ProductPhoto;
            this.dataGridView1.DataSource = bindingSource1;
            this.pictureBox1.DataBindings.Add("Image", bindingSource1,"LargePhoto", true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.DataBindings.Clear();

            var q = from da in this.awDataSet11.ProductPhoto
                    where da.ModifiedDate >= this.dateTimePicker1.Value &&
                          da.ModifiedDate <= this.dateTimePicker2.Value
                    select da;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = bindingSource1;
            this.pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.pictureBox1.DataBindings.Clear();
            int year = int.Parse(comboBox3.Text);

            var q = from da in this.awDataSet11.ProductPhoto
                    where da.ModifiedDate.Year == year
                    select da;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = bindingSource1;
            this.pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.pictureBox1.DataBindings.Clear();
            
            if (comboBox2.Text == "請選擇季節" || comboBox3.Text == "選擇年份")
            {
                MessageBox.Show("請選擇年與季節");
            }
            else
            {
                var q = this.awDataSet11.ProductPhoto.Where(m => m.ModifiedDate.Year == int.Parse(comboBox3.Text) && m.ModifiedDate.Month / 4 == comboBox2.SelectedIndex);

                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = bindingSource1;
                this.pictureBox1.DataBindings.Add("Image", bindingSource1, "LargePhoto", true);
            }
        }
    }
}

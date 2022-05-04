using _36_Linq;
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

namespace MyHomeWork
{
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }
        NorthwindEntities dbContext = new NorthwindEntities();

        private void button4_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 15, 11 };

            foreach(int i in nums)
            {
                string n = Mynums(i);

                if (treeView1.Nodes[n] == null)
                {
                    TreeNode node = null;
                    node = treeView1.Nodes.Add(n, n);
                    node.Nodes.Add(i.ToString());
                }
                else
                {
                    treeView1.Nodes[n].Nodes.Add(i.ToString());
                }
            }        
        }

        private string Mynums(int i)
        {
            if (i < 5) return "Small";
            else if (i < 10) return "Medium";
            else return "Large";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\Windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = false;

            var q = from p in files
                    group p by MyKey(p) into g
                    orderby g.Count()
                    select new
                    {
                        Size = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };
            this.dataGridView1.DataSource = q.ToList();

            foreach(var f in q)
            {
                string s = $"{f.Size}({f.Count})";
                TreeNode x = this.treeView1.Nodes.Add(f.Size.ToString(), s);
                foreach(var i in f.MyGroup)
                {
                    x.Nodes.Add(i.ToString() + "  Size-" + i.Length);
                }
            }
        }

        private object MyKey(FileInfo p)
        {
            if (p.Length > 1000000)
            {
                return "Large";
            }
            else return "Small";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\Windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.dataGridView1.DataSource = false;

            var q = from p in files
                    group p by p.CreationTime.Year into g
                    orderby g.Count()
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        MyGroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            foreach (var f in q)
            {
                string s = $"{f.Year}({f.Count})";
                TreeNode x = this.treeView1.Nodes.Add(f.Year.ToString(), s);
                foreach (var i in f.MyGroup)
                {
                    x.Nodes.Add(i.ToString());
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products.AsEnumerable()
                    orderby p.UnitsInStock descending, p.ProductID descending
                    select new
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice,
                        p.UnitsInStock,
                        TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
                    };

            this.dataGridView1.DataSource = q.ToList();

            var q2 = this.dbContext.Products.OrderByDescending(p => p.UnitsInStock).ThenByDescending(p => p.ProductID);
            this.dataGridView2.DataSource = q2.ToList();
        }
    }
}

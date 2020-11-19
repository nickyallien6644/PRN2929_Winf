using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PRN292_LapTopSaleSystemWF_Group4.Model;

namespace PRN292_LapTopSaleSystemWF_Group4.View
{
    public partial class frmHistory : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int id = -1;
        frmManegement main;
        public frmHistory(frmManegement main)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.main = main;
            load();
            getValue();
        }

        public void getValue()
        {
            cbbUser.DataSource = db.Users.ToList();
            cbbUser.DisplayMember = "Fullname";

            cbbProduct.DataSource = db.Products.ToList();
            cbbProduct.DisplayMember = "Name";

            List<int> day = new List<int>();
            for (int i = 1; i <= 31; i++)
            {
                day.Add(i);
            }
            cbbDay.DataSource = day.ToList();

            List<int> month = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                month.Add(i);
            }
            cbbMonth.DataSource = month.ToList();

            var year = from order in db.Orders
                       select order.date.Year;
            cbbYear.DataSource = year.ToList();

        }

        public void load()
        {
            var list =
                from orderdetail in db.OrderDetails
                join order in db.Orders on
                orderdetail.OrderID equals order.ID
                join user in db.Users on
                order.UserID equals user.ID
                join product in db.Products on
                orderdetail.ProductID equals product.ID
                select new
                {
                    ID = orderdetail.ID,
                    Quantity = orderdetail.Quantity,
                    Price = orderdetail.Price,
                    Product = product.Name,
                    User = user.Fullname,
                    Date = order.date
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ((User)cbbUser.SelectedValue).ID;
            var list =
                from orderdetail in db.OrderDetails
                join order in db.Orders on
                orderdetail.OrderID equals order.ID
                join user in db.Users on
                order.UserID equals user.ID
                join product in db.Products on
                orderdetail.ProductID equals product.ID
                where user.ID == id
                select new
                {
                    ID = orderdetail.ID,
                    Quantity = orderdetail.Quantity,
                    Price = orderdetail.Price,
                    Product = product.Name,
                    User = user.Fullname,
                    Date = order.date
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ((Product)cbbProduct.SelectedValue).ID;
            var list =
                from orderdetail in db.OrderDetails
                join order in db.Orders on
                orderdetail.OrderID equals order.ID
                join user in db.Users on
                order.UserID equals user.ID
                join product in db.Products on
                orderdetail.ProductID equals product.ID
                where product.ID == id
                select new
                {
                    ID = orderdetail.ID,
                    Quantity = orderdetail.Quantity,
                    Price = orderdetail.Price,
                    Product = product.Name,
                    User = user.Fullname,
                    Date = order.date
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            int day = (int)cbbDay.SelectedValue;
            var list =
                from orderdetail in db.OrderDetails
                join order in db.Orders on
                orderdetail.OrderID equals order.ID
                join user in db.Users on
                order.UserID equals user.ID
                join product in db.Products on
                orderdetail.ProductID equals product.ID
                where order.date.Day == day
                select new
                {
                    ID = orderdetail.ID,
                    Quantity = orderdetail.Quantity,
                    Price = orderdetail.Price,
                    Product = product.Name,
                    User = user.Fullname,
                    Date = order.date
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = (int)cbbMonth.SelectedValue;
            var list =
                from orderdetail in db.OrderDetails
                join order in db.Orders on
                orderdetail.OrderID equals order.ID
                join user in db.Users on
                order.UserID equals user.ID
                join product in db.Products on
                orderdetail.ProductID equals product.ID
                where order.date.Month == month
                select new
                {
                    ID = orderdetail.ID,
                    Quantity = orderdetail.Quantity,
                    Price = orderdetail.Price,
                    Product = product.Name,
                    User = user.Fullname,
                    Date = order.date
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = (int)cbbYear.SelectedValue;
            var list =
                from orderdetail in db.OrderDetails
                join order in db.Orders on
                orderdetail.OrderID equals order.ID
                join user in db.Users on
                order.UserID equals user.ID
                join product in db.Products on
                orderdetail.ProductID equals product.ID
                where order.date.Year == year
                select new
                {
                    ID = orderdetail.ID,
                    Quantity = orderdetail.Quantity,
                    Price = orderdetail.Price,
                    Product = product.Name,
                    User = user.Fullname,
                    Date = order.date
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dtTableBrand.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dtTableBrand.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dtTableBrand.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dtTableBrand.Rows.Count; i++)
                {
                    for (int j = 0; j < dtTableBrand.Columns.Count; j++)
                    {
                        excelApp.Cells[i + 2, j + 1] = dtTableBrand.Rows[i].Cells[j].Value.ToString();
                    }
                }
                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
        }
    }
}
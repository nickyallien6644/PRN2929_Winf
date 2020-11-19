﻿using System;
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
    public partial class frmFeedback : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        frmManegement main;
        Comment comment;
        public frmFeedback(frmManegement main)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.main = main;

            load();
            getValue();          

        }

        public void load()
        {
            var list =
                   from comment in db.Comments
                   join user in db.Users on
                   comment.UserID equals user.ID
                   join product in db.Products on
                   comment.ProductID equals product.ID
                   select new
                   {
                       ID = comment.ID,
                       Content = comment.Content,
                       Date = comment.date,
                       Accept = comment.Accept,
                       Active = comment.Active,
                       User = user.Fullname,
                       Product = product.Name
                   };
            dtTableBrand.DataSource = db.Comments.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

        private void cbbActive_CheckedChanged(object sender, EventArgs e)
        {
            var list =
                   from comment in db.Comments
                   join user in db.Users on
                   comment.UserID equals user.ID
                   join product in db.Products on
                   comment.ProductID equals product.ID
                   select new
                   {
                       ID = comment.ID,
                       Content = comment.Content,
                       Date = comment.date,
                       Accept = comment.Accept,
                       Active = comment.Active,
                       User = user.Fullname,
                       Product = product.Name
                   };
            if (cbbActive.Checked)
            {
                dtTableBrand.DataSource = list.Where(x => x.Active == true).ToList();
                dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            else
            {
                dtTableBrand.DataSource = list.Where(x => x.Active == false).ToList();
                dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new InsertUpFeedback(true, null, main).Show();
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            new InsertUpFeedback(false, comment, main).Show();
            this.Dispose();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            load();
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

        private void cbbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            String userName = ((User)cbbUser.SelectedValue).Fullname;
            var list =
                   from comment in db.Comments
                   join user in db.Users on
                   comment.UserID equals user.ID
                   where user.Fullname == userName
                   join product in db.Products on
                   comment.ProductID equals product.ID
                   select new
                   {
                       ID = comment.ID,
                       Content = comment.Content,
                       Date = comment.date,
                       Accept = comment.Accept,
                       Active = comment.Active,
                       User = user.Fullname,
                       Product = product.Name
                   };
            dtTableBrand.DataSource = db.Comments.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            String productName = ((Product)cbbUser.SelectedValue).Name;
            var list =
                   from comment in db.Comments
                   join user in db.Users on
                   comment.UserID equals user.ID
                   join product in db.Products on
                   comment.ProductID equals product.ID
                   where product.Name == productName
                   select new
                   {
                       ID = comment.ID,
                       Content = comment.Content,
                       Date = comment.date,
                       Accept = comment.Accept,
                       Active = comment.Active,
                       User = user.Fullname,
                       Product = product.Name
                   };
            dtTableBrand.DataSource = db.Comments.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void dtTableBrand_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
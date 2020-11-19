using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN292_LapTopSaleSystemWF_Group4.Model;

namespace PRN292_LapTopSaleSystemWF_Group4.View
{   
    public partial class HistoryControl : UserControl
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int id = -1;
        public HistoryControl()
        {
            InitializeComponent();
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
            for(int i = 1; i <= 31; i++)
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
            dtHistory.DataSource = list.ToList();
            dtHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            dtHistory.DataSource = list.ToList();
            dtHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            dtHistory.DataSource = list.ToList();
            dtHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            dtHistory.DataSource = list.ToList();
            dtHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            dtHistory.DataSource = list.ToList();
            dtHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            dtHistory.DataSource = list.ToList();
            dtHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}

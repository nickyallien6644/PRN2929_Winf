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
    public partial class frmProductView : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        frmManegement main;
        int id = -1;
        Product product;
        public frmProductView(frmManegement main)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.LookAndFeel.SetSkinStyle("Dark Side");

            load();
            getValue();

            product = new Product();
            this.main = main;
        }

        public void load()
        {
            var list = from product in db.Products
                       join brand in db.Brands on
                       product.BrandID equals brand.ID
                       join category in db.Categories on
                       product.CategoryID equals category.ID
                       select new
                       {
                           ID = product.ID,
                           Name = product.Name,
                           Price = product.Price,
                           Discount = product.Discount,
                           Discription = product.Description,
                           Features = product.Features,
                           Active = product.Active,
                           Brand = brand.Name,
                           Category = category.Name,
                           ProductDetail = product.ProductDetailID
                       };

            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        public void getValue()
        {
            cbbBrand.DataSource = db.Brands.ToList();
            cbbBrand.DisplayMember = "Name";

            cbbCate.DataSource = db.Categories.ToList();
            cbbCate.DisplayMember = "Name";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String text = txtSearch.Text.Trim();
            if (text == "")
            {
                load();
            }
            var list = from product in db.Products
                       join brand in db.Brands on
                       product.BrandID equals brand.ID
                       join category in db.Categories on
                       product.CategoryID equals category.ID
                       where product.Name.Contains(text)
                       select new
                       {
                           ID = product.ID,
                           Name = product.Name,
                           Price = product.Price,
                           Discount = product.Discount,
                           Discription = product.Description,
                           Features = product.Features,
                           Active = product.Active,
                           Brand = brand.Name,
                           Category = category.Name,
                           ProductDetail = product.ProductDetailID

                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ((Brand)cbbBrand.SelectedItem).ID;
            var list = from product in db.Products

                       join brand in db.Brands on
                       product.BrandID equals brand.ID
                       join category in db.Categories on
                       product.CategoryID equals category.ID
                       where product.BrandID == id
                       select new
                       {
                           ID = product.ID,
                           Name = product.Name,
                           Price = product.Price,
                           Discount = product.Discount,
                           Discription = product.Description,
                           Features = product.Features,
                           Active = product.Active,
                           Brand = brand.Name,
                           Category = category.Name,
                           ProductDetail = product.ProductDetailID

                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ((Category)cbbCate.SelectedItem).ID;
            var list = from product in db.Products

                       join brand in db.Brands on
                       product.BrandID equals brand.ID
                       join category in db.Categories on
                       product.CategoryID equals category.ID
                       where product.CategoryID == id
                       select new
                       {
                           ID = product.ID,
                           Name = product.Name,
                           Price = product.Price,
                           Discount = product.Discount,
                           Discription = product.Description,
                           Features = product.Features,
                           Active = product.Active,
                           Brand = brand.Name,
                           Category = category.Name,
                           ProductDetail = product.ProductDetailID

                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new ProductInsAndUp(true, null, main).Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.id < 0)
            {
                MessageBox.Show("Please choice product to update");
            }
            else
            {
                new ProductInsAndUp(false, this.product, main).Show();
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.id < 0)
            {
                MessageBox.Show("Please choice product to delete");
            }
            else
            {
                db.Products.Remove(this.product);
                db.SaveChanges();
                load();
            }
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

        private void dtTableBrand_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.id = Convert.ToInt32(dtTableBrand.Rows[dtTableBrand.CurrentCell.RowIndex].Cells[0].Value);
            this.product = db.Products.FirstOrDefault(c => c.ID == this.id);
        }

        private void cbbActive_CheckedChanged(object sender, EventArgs e)
        {
            var list = 
                from product in db.Products
                       join brand in db.Brands on
                       product.BrandID equals brand.ID
                       join category in db.Categories on
                       product.CategoryID equals category.ID
                       select new
                       {
                           ID = product.ID,
                           Name = product.Name,
                           Price = product.Price,
                           Discount = product.Discount,
                           Discription = product.Description,
                           Features = product.Features,
                           Active = product.Active,
                           Brand = brand.Name,
                           Category = category.Name,
                           ProductDetail = product.ProductDetailID

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
    }
}
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
    public partial class ProductInsAndUp : DevExpress.XtraEditors.XtraForm
    {
        Product product;
        frmManegement main;
        bool isIsert;
        bool isClick = false;
        int idDetail = 0;
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        
        public ProductInsAndUp(bool isInsert, Product product, frmManegement main)
        {
            InitializeComponent();
            this.CenterToScreen();

            this.product = product;
            this.isIsert = isInsert;
            this.main = main;
            getValue();

            if (isIsert)
            {
                title.Text = "Insert";
                btnView.Hide();
                btnDetail.Show();
                clear();
            }
            else
            {
                clear();
                btnView.Show();
                btnDetail.Hide();
                title.Text = "Update";
                isClick = true;
                txtName.Text = product.Name;
                txtDescription.Text = product.Description;
                txtFeatures.Text = product.Features;
                txtPrice.Text = product.Price.ToString();
                txtDiscount.Text = product.Discount.ToString();
                cbActive.Checked = product.Active == true ? true : false;
            }
        }

        public void setisClick(bool isClick)
        {
            this.isClick = isClick;
        }

        public void setIdDetail(int idDetail)
        {
            this.idDetail = idDetail;
        }
            

        public void getValue()
        {
            cbbBrand.DataSource = db.Brands.ToList();
            cbbBrand.DisplayMember = "Name";
            cbbBrand.ValueMember = "ID";

           cbbCategory.DataSource = db.Categories.ToList();
            cbbCategory.DisplayMember = "Name";
            cbbCategory.ValueMember = "ID";
        }

        public void clear()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtFeatures.Text = "";
            txtPrice.Text = "0";
            txtDiscount.Text = "0";
            cbActive.Checked = false;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            new ProductDetailInsAndUp(true, null, this).Show();
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String name = txtName.Text.Trim();
            String description = txtDescription.Text.Trim();
            String features = txtFeatures.Text.Trim();
            String price = txtPrice.Text.Trim();
            String discount = txtDiscount.Text.Trim();


            if (name == "" || description == "" || features == "" || txtPrice.Text == "" || txtDiscount.Text == "")
                MessageBox.Show("Please input textfield");

            else
            {
                int brandid = Convert.ToInt32(cbbBrand.SelectedValue);
                int cateId = Convert.ToInt32(cbbCategory.SelectedValue);

                if(isClick == false)
                {
                    MessageBox.Show("Please add product detail");
                }
                else
                {
                    if (isIsert)
                    {
                        db.Products.Add(new Product(name,
                            Convert.ToDouble(price), Convert.ToDouble(discount), description, features, cbActive.Checked == true ?
                            true : false, brandid, cateId, idDetail));
                    db.SaveChanges();
                    }
                    else
                    {
                        int id = Convert.ToInt32(this.product.ID);
                        Product EProduct = db.Products.FirstOrDefault(c => c.ID == id);
                        EProduct.Name = txtName.Text;
                        EProduct.Price = Convert.ToDouble(txtPrice.Text);
                        EProduct.Discount = Convert.ToDouble(txtDiscount.Text);
                        EProduct.Description = txtDescription.Text;
                        EProduct.Features = txtFeatures.Text;
                        EProduct.Active = cbActive.Checked == true ? true : false;
                        EProduct.BrandID = Convert.ToInt32(cbbBrand.SelectedValue);
                        EProduct.CategoryID = Convert.ToInt32(cbbCategory.SelectedValue);
                        EProduct.ProductDetailID = product.ProductDetailID;
                        db.SaveChanges();
                    }
                    frmProductView form = new frmProductView(main);
                    form.MdiParent = main;
                    form.WindowState = FormWindowState.Maximized;
                    form.Show();
                    form.load();
                    this.Dispose();
                }              
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmProductView form = new frmProductView(main);
            form.MdiParent = main;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            form.load();
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            new DetailView((int)this.product.ProductDetailID, this).Show();
            this.Hide();
        }
    }
}
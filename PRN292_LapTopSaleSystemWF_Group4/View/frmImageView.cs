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
using Image = PRN292_LapTopSaleSystemWF_Group4.Model.Image;

namespace PRN292_LapTopSaleSystemWF_Group4.View
{
    public partial class frmImageView : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        frmManegement main;
        int id = -1;
        Image image;
        public frmImageView(frmManegement main)
        {
            InitializeComponent();
            load();
            getValue();
        }

        public void getValue()
        {
            cbbProduct.DataSource = db.Products.ToList();
            cbbProduct.DisplayMember = "Name";

        }

        public void load()
        {
            var list =
                from images in db.Images
                join product in db.Products on
                images.ProductID equals product.ID
                select new
                {
                    ID = images.ID,
                    Image = images.image1,
                    Product = product.Name
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = (Product)cbbProduct.SelectedValue;
            var list =
                from images in db.Images
                join product1 in db.Products on
                images.ProductID equals product1.ID
                where product.Name == product1.Name
                select new
                {
                    ID = images.ID,
                    Image = images.image1,
                    Product = product.Name
                };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
        }

        private void dtTableBrand_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.id = Convert.ToInt32(dtTableBrand.Rows[dtTableBrand.CurrentCell.RowIndex].Cells[0].Value);
            //this.image = db.Images.FirstOrDefault(x => x.ID == this.id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new InsertUpImage(true, 0, main).Show();
            this.Dispose();
        }
    }
}
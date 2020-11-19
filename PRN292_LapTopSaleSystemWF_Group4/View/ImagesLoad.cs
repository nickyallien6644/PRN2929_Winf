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
    public partial class ImagesLoad : UserControl
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int id = -1;
        public ImagesLoad()
        {
            InitializeComponent();
            load();
            getValue();
        }

        public void getValue()
        {
            cbbProduct.DataSource = db.Products.ToList();
            cbbProduct.DisplayMember = "Name";

            cbbImgage.DataSource = db.Products.ToList();
            cbbImgage.DisplayMember = "Name";
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
            dtTableImage.DataSource = list.ToList();
            dtTableImage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableImage.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            dtTableImage.DataSource = list.ToList();
            dtTableImage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableImage.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            load();
        }

        private void dtTableImage_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.id = Convert.ToInt32(dtTableImage.Rows[dtTableImage.CurrentCell.RowIndex].Cells[0].Value);

            txtUpload.Text = db.Images.FirstOrDefault(x => x.ID == this.id).image1.ToString();
        }
    }
}

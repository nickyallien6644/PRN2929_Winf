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
    public partial class InsertUpImage : DevExpress.XtraEditors.XtraForm
    {
        frmManegement main;
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int id = 0;
        Image img;
        bool isIsert;
        public InsertUpImage(bool isIsert, int id, frmManegement main)
        {
            InitializeComponent();
            this.CenterToScreen();

            this.id = id;
            this.img = db.Images.FirstOrDefault(x => x.ID == this.id);           
            this.main = main;
            this.isIsert = isIsert;

            if (isIsert)
            {
                lbltitle.Text = "Insert";
            }
            else
            {
                lbltitle.Text = "Update";
                getValue();
            }
        }

        public void getValue()
        {
            cbbProduct.DataSource = db.Products.ToList();
            cbbProduct.DisplayMember = "Name";

            txtImage.Text = this.img.image1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
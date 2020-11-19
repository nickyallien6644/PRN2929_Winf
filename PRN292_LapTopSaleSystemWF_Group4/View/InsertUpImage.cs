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
        //Image img;
        int id = 0;
        bool isIsert;
        public InsertUpImage(bool isIsert, int id, frmManegement main)
        {
            InitializeComponent();
            this.CenterToScreen();
            getValue();

            this.id = id;
            this.main = main;
            this.isIsert = isIsert;

            if (isIsert)
            {
                lbltitle.Text = "Insert";
            }
            /*else
            {
                lbltitle.Text = "Update";
                txtImage.Text = this.img.image1;
            }*/
        }

        public void getValue()
        {
            cbbProduct.DataSource = db.Products.ToList();
            cbbProduct.DisplayMember = "Name";           
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmImageView frmImage = new frmImageView(main);
            frmImage.MdiParent = main;
            frmImage.WindowState = FormWindowState.Maximized;
            frmImage.Show();
            frmImage.load();
            this.Dispose();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtImage.Text = openFileDialog.SafeFileName;
                ///File.Copy(openFileDialog.SafeFileName, @"Desktop" + openFileDialog.SafeFileName, true);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isIsert)
            {
                db.Images.Add(new Image("..\\SaleLaptopSystem\\SaleLaptopSystem\\SaleLaptopSystem\\img\\Brands_img\\" + txtImage.Text, ((Product)cbbProduct.SelectedValue).ID));
                db.SaveChanges();
            }
            else
            {

            }

            frmImageView frmImage = new frmImageView(main);
            frmImage.MdiParent = main;
            frmImage.WindowState = FormWindowState.Maximized;
            frmImage.Show();
            frmImage.load();
            this.Dispose();
        }

        private void cbbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
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
    public partial class BrandInsAndUp : DevExpress.XtraEditors.XtraForm
    {
        Brand brand;
        frmManegement main;
        bool isIsert;
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        public BrandInsAndUp(bool isIsert, Brand brand, frmManegement main)
        {
            InitializeComponent();
            this.CenterToScreen();

            this.brand = brand;
            this.isIsert = isIsert;
            this.main = main;

            if (isIsert)
            {
                lbltitle.Text = "Insert";
                clear();
            }
            else
            {
                clear();
                lbltitle.Text = "Update";
                txtName.Text = brand.Name;
                txtImage.Text = brand.Image;
                cbbActive.Checked = brand.Active == true ? true : false;
            }
        }

        public void clear()
        {
            txtName.Text = "";
            txtImage.Text = "";
            cbbActive.Checked = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            String name = txtName.Text.Trim();
            String image = txtImage.Text.Trim();

            if (name == "" || image == "")
                MessageBox.Show("Please input textfield");
            else
            {
                if (isIsert)
                {
                    db.Brands.Add(new Brand(name, "..\\SaleLaptopSystem\\SaleLaptopSystem\\SaleLaptopSystem\\img\\Brands_img\\" + image, cbbActive.Checked==true?true:false));
                    db.SaveChanges();
                }
                else
                {
                    int id = Convert.ToInt32(this.brand.ID);
                    Brand EBrand = db.Brands.FirstOrDefault(b => b.ID == id);
                    EBrand.Name = name;
                    EBrand.Image = "..\\SaleLaptopSystem\\SaleLaptopSystem\\SaleLaptopSystem\\img\\Brands_img\\" + image;
                    EBrand.Active = cbbActive.Checked == true ? true : false;
                    db.SaveChanges();
                }
                frmBrandView form = new frmBrandView(main);
                form.MdiParent = main;
                form.Show();
                form.WindowState = FormWindowState.Maximized;
                form.load();
                this.Dispose();
            }
                        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmBrandView form = new frmBrandView(main);
            form.MdiParent = main;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
            form.load();
            this.Dispose();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            txtImage.Text = openFileDialog.SafeFileName;
        }
    }
}
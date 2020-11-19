using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using PRN292_LapTopSaleSystemWF_Group4.Model;
using DevExpress.LookAndFeel;

namespace PRN292_LapTopSaleSystemWF_Group4.View
{
    public partial class frmManegement : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        User user;
        frmBrandView brand;
        frmCateView category;
        frmProductView product;
        frmUserView userView;

        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        public frmManegement(User user)
        {
            InitializeComponent();
            this.user = user;
            this.WindowState = FormWindowState.Maximized;
            this.LookAndFeel.SetSkinStyle("Dark Side");


            this.btnLogout.Caption = "Logout, " + user.Fullname;
        }

        private void btnBrand_ItemClick(object sender, ItemClickEventArgs e)
        {
            brand = new frmBrandView(this);
            brand.MdiParent = this;
            brand.WindowState = FormWindowState.Maximized;
            brand.Show();
        }

        private void btnLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmLogin frmLogin = new frmLogin(null);
            frmLogin.Show();
            this.Dispose();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            category = new frmCateView(this);
            category.MdiParent = this;
            category.WindowState = FormWindowState.Maximized;
            category.Show();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            product = new frmProductView(this);
            product.MdiParent = this;
            product.WindowState = FormWindowState.Maximized;
            product.Show();
        }

        private void btnUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            userView = new frmUserView(this);
            userView.MdiParent = this;
            userView.WindowState = FormWindowState.Maximized;
            userView.Show();
        }
    }
}
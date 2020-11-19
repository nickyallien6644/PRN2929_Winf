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
    public partial class InsertUpFeedback : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int idAcc;
        frmManegement main;
        Product product;
        public InsertUpFeedback(Product product, int idAcc, frmManegement main)
        {
            InitializeComponent();
            this.CenterToScreen();

            this.main = main;
            this.idAcc = idAcc;
            this.product = product;

            lbltitle.Text = "Feedback";
            txtProduct.Text = product.Name;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            String content = txtContent.Text.Trim();

            db.Comments.Add(new Comment(content, DateTime.Now, true, true, this.idAcc, this.product.ID));
            db.SaveChanges();

            frmFeedback feedback = new frmFeedback(main);
            feedback.MdiParent = main;
            feedback.Show();
            feedback.load();
            feedback.WindowState = FormWindowState.Maximized;
            this.Dispose();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmFeedback feedback = new frmFeedback(main);
            feedback.MdiParent = main;
            feedback.Show();
            feedback.load();
            feedback.WindowState = FormWindowState.Maximized;
            this.Dispose();
        }
    }
}
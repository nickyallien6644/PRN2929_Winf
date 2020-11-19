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
        Comment comment; 
        frmManegement main;
        bool isInsert;
        public InsertUpFeedback(bool isInsert, Comment comment, frmManegement main)
        {
            InitializeComponent();
            this.CenterToScreen();
            getValue();

            this.main = main;
            this.comment = comment;
            this.isInsert = isInsert;

            if (isInsert)
            {
                clear();
                lbltitle.Text = "Insert";
            }
            else
            {
                clear();
                lbltitle.Text = "Update";
                txtContent.Text = comment.Content;
                dateDate.Value = comment.date;
                cbAccept.Checked = comment.Accept == true ? true : false;
                cbActive.Checked = comment.Active == true ? true : false;

                User userpicked = db.Users.FirstOrDefault(x => x.ID == comment.ID);
                cbbUser.Text = userpicked.Fullname;

                Product productpicked = db.Products.FirstOrDefault(x => x.ID == comment.ProductID);
                cbbProduct.Text = productpicked.Name;
            }
            
        }

        public void getValue()
        {
            cbbUser.DataSource = db.Users.ToList();
            cbbUser.DisplayMember = "FullName";

            cbbProduct.DataSource = db.Products.ToList();
            cbbProduct.DisplayMember = "Name";
        }

        public void clear()
        {
            txtContent.Text = "";
            cbActive.Checked = false;
            cbAccept.Checked = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            String content = txtContent.Text.Trim();
            DateTime date = dateDate.Value;
            bool accept = cbAccept.Checked == true ? true : false;
            bool active = cbActive.Checked == true ? true : false;
            int user = ((User)cbbUser.SelectedValue).ID;
            int product = ((Product)cbbProduct.SelectedValue).ID;

            if (isInsert)
            {
                db.Comments.Add(new Comment(content, date, accept, active, user, product));
                db.SaveChanges();
            }
            else
            {
                Comment cmtEdit = db.Comments.FirstOrDefault(x => x.ID == comment.ID);
                cmtEdit.Content = content;
                cmtEdit.date = date;
                cmtEdit.Accept = accept;
                cmtEdit.Active = active;
                cmtEdit.UserID = user;
                cmtEdit.ProductID = product;
                db.SaveChanges();
            }
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
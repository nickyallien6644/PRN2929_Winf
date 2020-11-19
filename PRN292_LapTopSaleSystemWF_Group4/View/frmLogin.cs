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
using PRN292_LapTopSaleSystemWF_Group4.DAO;
using PRN292_LapTopSaleSystemWF_Group4.Model;
using DevExpress.LookAndFeel;

namespace PRN292_LapTopSaleSystemWF_Group4.View
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        UserDAO uDAO = new UserDAO();
        User user;
        public frmLogin(User user)
        {
            InitializeComponent();
            this.CenterToScreen();


            UserLookAndFeel.Default.SetDefaultStyle();
            this.user = user;
            txtPass.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text.Trim() == "" || txtPass.Text.Trim() == "")
            {
                MessageBox.Show("Please input the fill", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                String email = txtEmail.Text.Trim();
                String password = txtPass.Text.Trim();

                this.user = uDAO.login(email, password);
                if (this.user == null)
                {
                    MessageBox.Show("Incorrect email or password", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    frmManegement frmMain = new frmManegement(this.user);
                    frmMain.Show();
                    this.Hide();
                }
            }         
        }

        private void cbPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void linkRegis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegis frmRegis = new frmRegis();
            frmRegis.Show();
            this.Visible = false;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void lblPass_Click(object sender, EventArgs e)
        {

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }
    }
}
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
using PRN292_LapTopSaleSystemWF_Group4.Validate;
using System.Security.Cryptography;
using PRN292_LapTopSaleSystemWF_Group4.Model;
using PRN292_LapTopSaleSystemWF_Group4.DAO;
using System.IO;
using System.Text.RegularExpressions;

namespace PRN292_LapTopSaleSystemWF_Group4.View
{
    public partial class frmRegis : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        bool isName = false;
        bool isEmail = false;
        bool isPhone = false;
        bool isPass = false;
        bool isConfirm = false;
        bool isAddress = false;
        public frmRegis()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.lblcheckPass.Hide();
            this.lblConf.Hide();
            this.lblEmailCheck.Hide();
            this.lblPhonecheck.Hide();
            this.lblFullname.Hide();
            this.lblCheckAddress.Hide();

            cbbRole.Text = "Admin";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin(null);
            frmLogin.Show();
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            AuthenticationValidate validate = new AuthenticationValidate();
            SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
            UserDAO uDAO = new UserDAO();

            String fullname = txtFullname.Text.Trim();
            String email = txtEmail.Text.Trim();
            String password = txtPass.Text.Trim();
            String confirm = txtConfirm.Text.Trim();
            String phone = txtPhone.Text.Trim();
            String address = txtAddress.Text.Trim();
            String img = txtImage.Text.Trim();
            String role = cbbRole.SelectedItem.ToString();

            if (fullname == "" || email == "" || password == "" || confirm == "" || phone == "" || address == "")
            {
                MessageBox.Show("Input the fill", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if(isAddress && isConfirm && isEmail && isName && isPass && isPhone)
                {
                    if (img == "")
                    {
                        img = "user.png";
                    }
                    if (!uDAO.registration(fullname, password, email, phone, address, "..\\SaleLaptopSystem\\SaleLaptopSystem\\SaleLaptopSystem\\img\\Brands_img\\" + img, role))
                    {
                        MessageBox.Show("Registration error", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        frmLogin frmLogin = new frmLogin(null);
                        frmLogin.Show();
                        this.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid register", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            Regex rex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,})");
            if (rex.IsMatch(txtPass.Text.Trim()))
            {
                lblcheckPass.Show();
                isPass = true;
            }
            else
            {
                lblcheckPass.Hide();
                isPass = false;
            }
            
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {
            String password = txtPass.Text.Trim();
            String confirm = txtConfirm.Text.Trim();
            if (password != confirm)
            {
                this.lblConf.Hide();
                isConfirm = false;
            }
            else
            {
                this.lblConf.Show();
                isConfirm = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            AuthenticationValidate validate = new AuthenticationValidate();
            String email = txtEmail.Text.Trim();
            Regex rex = new Regex(@"[a-zA-Z0-9]+([.-_][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([.-_][a-zA-Z0-9]+)*([.-_][a-zA-Z0-9]{2,})+");
            if (!rex.IsMatch(email))
            {
                this.lblEmailCheck.Visible = false;
                isEmail = false;
            }
            else
            {
                if(validate.checkUserExist(email) == null)
                {
                    this.lblEmailCheck.Show();
                    isEmail = true;
                }
                else
                {
                    this.lblEmailCheck.Hide();
                    isEmail = false;
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtImage.Text = openFileDialog.SafeFileName;
                ///File.Copy(openFileDialog.SafeFileName, @"Desktop" + openFileDialog.SafeFileName, true);
            }


            
        }

        private void txtFullname_TextChanged(object sender, EventArgs e)
        {            
            String fullname = txtFullname.Text.Trim();
            Regex rex = new Regex(@"^[A-Za-z]+(( )?[A-Za-z]*)+(( )?[A-Za-z]*)");
            if (!rex.IsMatch(fullname))
            {
                this.lblFullname.Hide();
                isName = false;
            }
            else
            {
                this.lblFullname.Show();
                isName = true;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            String address = txtAddress.Text.Trim();
            if(address == "")
            {
                this.lblCheckAddress.Hide();
                isAddress = false;
            }
            else
            {
                this.lblCheckAddress.Show();
                isAddress = true;
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            String phone = txtPhone.Text.Trim();
            Regex rex = new Regex(@"(09|01[2|6|8|9])+([0-9]{8})\b");
            if (!rex.IsMatch(phone))
            {
                this.lblPhonecheck.Hide();
                isPhone = false;
            }
            else
            {
                this.lblPhonecheck.Show();
                isPhone = true;
            }
        }
    }
}
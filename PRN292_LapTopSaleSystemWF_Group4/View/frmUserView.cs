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
    public partial class frmUserView : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int id = -1;
        User user;
        frmManegement main;
        public frmUserView(frmManegement main)
        {
            InitializeComponent();
            this.LookAndFeel.SetSkinStyle("Dark Side");
            this.WindowState = FormWindowState.Maximized;
            dtTableBrand.ForeColor = Color.Black;
            this.main = main;
            load();
        }

        public void load()
        {
            var list = from user in db.Users
                       orderby user.Fullname ascending
                       select new
                       {
                           ID = user.ID,
                           Fullname = user.Fullname,
                           Email = user.Email,
                           Phone = user.Phone,
                           Address = user.Address,
                           Role = user.Role,
                           Active = user.Active
                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String text = txtSearch.Text.Trim();
            if (text == "")
            {
                load();
            }
            var list = from user in db.Users
                       where user.Fullname.Contains(text)
                       select new
                       {
                           ID = user.ID,
                           Fullname = user.Fullname,
                           Email = user.Email,
                           Phone = user.Phone,
                           Address = user.Address,
                           Role = user.Role,
                           Active = user.Active
                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void cbbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            String value = cbbRole.SelectedItem.ToString();
            var list = from user in db.Users
                       orderby user.Fullname ascending
                       where user.Role == value
                       select new
                       {
                           ID = user.ID,
                           Fullname = user.Fullname,
                           Email = user.Email,
                           Phone = user.Phone,
                           Address = user.Address,
                           Role = user.Role,
                           Active = user.Active
                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.id < 0)
            {
                MessageBox.Show("Please select user to update");
            }
            else
            {
                User userU = db.Users.FirstOrDefault(x => x.ID == this.id);
                userU.Active = user.Active == true ? false : true;
                db.SaveChanges();
                load();
            }
        }

        private void cbbActive_CheckedChanged(object sender, EventArgs e)
        {
            var list = from user in db.Users
                       orderby user.Fullname ascending
                       select new
                       {
                           ID = user.ID,
                           Fullname = user.Fullname,
                           Email = user.Email,
                           Phone = user.Phone,
                           Address = user.Address,
                           Role = user.Role,
                           Active = user.Active
                       };
            if (cbbActive.Checked)
            {
                dtTableBrand.DataSource = list.Where(x => x.Active == true).ToList();
                dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
            else
            {
                dtTableBrand.DataSource = list.Where(x => x.Active == false).ToList();
                dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            User uChange = db.Users.FirstOrDefault(x => x.ID == this.id);
            if (uChange.Role == "Admin")
            {
                uChange.Role = "User";
                db.SaveChanges();
            }
            else
            {
                uChange.Role = "Admin";
                db.SaveChanges();
            }
            load();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dtTableBrand.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dtTableBrand.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dtTableBrand.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dtTableBrand.Rows.Count; i++)
                {
                    for (int j = 0; j < dtTableBrand.Columns.Count; j++)
                    {
                        excelApp.Cells[i + 2, j + 1] = dtTableBrand.Rows[i].Cells[j].Value.ToString();
                    }
                }
                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
        }

        private void dtTableBrand_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.id = Convert.ToInt32(dtTableBrand.Rows[dtTableBrand.CurrentCell.RowIndex].Cells[0].Value);
            this.user = (User)db.Users.FirstOrDefault(x => x.ID == this.id);
        }
    }
}
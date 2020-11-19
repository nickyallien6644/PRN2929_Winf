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
    public partial class frmBrandView : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        frmManegement main;
        int id = -1;
        Brand brand;
        public frmBrandView(frmManegement main)
        {
            InitializeComponent();
            load();
            this.WindowState = FormWindowState.Maximized;
            this.LookAndFeel.SetSkinStyle("Dark Side");
            this.main = main;
        }

        public void load()
        {
            var list = from brand in db.Brands
                       orderby brand.Name ascending
                       select new
                       {
                           ID = brand.ID,
                           Name = brand.Name,
                           Image = brand.Image,
                           Active = brand.Active
                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void dtTableBrand_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.id = Convert.ToInt32(dtTableBrand.Rows[dtTableBrand.CurrentCell.RowIndex].Cells[0].Value);
            this.brand = db.Brands.FirstOrDefault(b => b.ID == this.id);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String text = txtSearch.Text.Trim();
            if (text == "")
            {
                load();
            }
            var list = from brand in db.Brands
                       where brand.Name.Contains(text)
                       select new
                       {
                           ID = brand.ID,
                           Name = brand.Name,
                           Image = brand.Image,
                           Active = brand.Active
                       };
            dtTableBrand.DataSource = list.ToList();
            dtTableBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtTableBrand.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new BrandInsAndUp(true, null, main).Show();
            this.Close();
        }

        private void cbbActive_CheckedChanged(object sender, EventArgs e)
        {
            var list = from brand in db.Brands
                       orderby brand.Name ascending
                       select new
                       {
                           ID = brand.ID,
                           Name = brand.Name,
                           Image = brand.Image,
                           Active = brand.Active
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.id < 0)
            {
                MessageBox.Show("Please choice brand to update");
            }
            else
            {
                new BrandInsAndUp(false, this.brand, main).Show();
                this.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.id < 0)
            {
                MessageBox.Show("Please choice brand to delete");
            }
            else
            {
                db.Brands.Remove(this.brand);
                db.SaveChanges();
                load();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
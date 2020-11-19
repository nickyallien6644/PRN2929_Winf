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
    public partial class DetailView : DevExpress.XtraEditors.XtraForm
    {
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        int id = 0;
        ProductInsAndUp form;
        public DetailView(int id, ProductInsAndUp form)
        {
            InitializeComponent();
            this.CenterToScreen();           

            this.id = id;
            this.form = form;

            load();
        }

        public void load()
        {
            var list = from detail in db.ProductDetails
                       where detail.ID == this.id
                       select detail;
            dtDetail.DataSource = list.ToList();
            dtDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ProductDetail detail = db.ProductDetails.FirstOrDefault(x => x.ID == this.id);
            new ProductDetailInsAndUp(false, detail, form).Show();
            this.Hide();
        }
    }
}
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
    public partial class ProductDetailInsAndUp : DevExpress.XtraEditors.XtraForm
    {
        ProductDetail detail;
        bool isIsert;
        ProductInsAndUp form;
        SaleLaptopSystemEntities db = new SaleLaptopSystemEntities();
        public ProductDetailInsAndUp(bool isIsert, ProductDetail detail, ProductInsAndUp form)
        {
            InitializeComponent();
            this.CenterToScreen();

            this.detail = detail;
            this.isIsert = isIsert;
            this.form = form;

            if (isIsert)
            {
                title.Text = "Insert";
                clear();
            }
            else
            {
                title.Text = "Update";
                txtProcess.Text = detail.Processor;
                txtRAM.Text = detail.RAM;
                txtScreen.Text = detail.Screen;
                txtStorage.Text = detail.Storage;
                txtGraphic.Text = detail.Graphic;
                txtSize.Text = detail.Size;
                txtOS.Text = detail.OS;
                txtVideo.Text = detail.Video;
                txtConnection.Text = detail.Connection;
                txtKeyboard.Text = detail.Keyboard;
                txtBattery.Text = detail.Battery;
            }
        }

        public void clear()
        {
            txtProcess.Text = "";
            txtRAM.Text = "";
            txtScreen.Text = "";
            txtStorage.Text = "";
            txtGraphic.Text = "";
            txtSize.Text = "";
            txtOS.Text = "";
            txtVideo.Text = "";
            txtConnection.Text = "";
            txtBattery.Text = "";
            txtKeyboard.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String processor = txtProcess.Text.Trim();
            String RAM = txtRAM.Text.Trim();
            String screen = txtScreen.Text.Trim();
            String storage = txtStorage.Text.Trim();
            String graphic = txtGraphic.Text.Trim();
            String size = txtSize.Text.Trim();
            String os = txtOS.Text.Trim();
            String video = txtVideo.Text.Trim();
            String connection = txtConnection.Text.Trim();
            String battery = txtBattery.Text.Trim();
            String keyboard = txtKeyboard.Text.Trim();
            if(processor == "" || RAM == "" || screen == "" || storage == "" || graphic == "" || size == "" || os == "" || video =="" || connection == "" || battery == "" || keyboard == "")
                    MessageBox.Show("Please input textfield");
            else
            {
                ProductDetail detail = null;
                if (isIsert)
                {
                    detail = new ProductDetail(processor, RAM, screen, storage, graphic, size, os, video, connection, keyboard, battery);
                    db.ProductDetails.Add(detail);
                    db.SaveChanges();
                    form.Show();
                    form.setisClick(true);
                    form.setIdDetail(detail.ID);
                    this.Dispose();
                }
                else
                {
                    int id = Convert.ToInt32(this.detail.ID);
                    ProductDetail productDetail = db.ProductDetails.FirstOrDefault(b => b.ID == id);
                    productDetail.Processor = processor;
                    productDetail.RAM = RAM;
                    productDetail.Screen = screen;
                    productDetail.Storage = storage;
                    productDetail.Graphic = graphic;
                    productDetail.Size = size;
                    productDetail.OS = os;
                    productDetail.Video = video;
                    productDetail.Connection = connection;
                    productDetail.Keyboard = keyboard;
                    productDetail.Battery = battery;
                    db.SaveChanges();
                    form.Show();                   
                    this.Dispose();
                }
                
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
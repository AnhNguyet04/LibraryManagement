using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYTHUVIEN
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form f = new FrmDangnhap();
            f.Show();
        }

        private void menuThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void menuThongtin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new FrmCapnhat();
            f.Show();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void thốngKêDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new ThongKe();
            f.Show();
        }
    }
}

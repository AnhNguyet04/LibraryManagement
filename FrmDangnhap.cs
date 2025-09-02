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
    public partial class FrmDangnhap : Form
    {
        public FrmDangnhap()
        {
            InitializeComponent();
        }
        string pass = "DHM";
        string user = "Admin";
        private void btDangnhap_Click(object sender, EventArgs e)
        {
            if (txtDangnhap.Text == "" || txtMatkhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu!", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDangnhap.Focus();
            }
            else if (pass.Equals(txtMatkhau.Text) && (user.Equals(txtDangnhap.Text)))
            {
                MessageBox.Show("Đăng nhập thành công!", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form f = new FrmMain();
                f.Show();
            }
            else { MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmDangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtDangnhap.Text == "" || txtMatkhau.Text == "")
                Application.Exit();
        }

        private void FrmDangnhap_Load(object sender, EventArgs e)
        {

        }
    }
}

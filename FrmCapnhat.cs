using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using Microsoft.Win32;


namespace QUANLYTHUVIEN
{
    public partial class FrmCapnhat : Form
    {
        string Status = "";
        int index = -1;
        int IndexSearch = -1;
        public FrmCapnhat()
        {
            InitializeComponent();
            txtTimkiem.Enabled = false;
        }
        private void importExcel()
        {
            List<ThongTinSach> sach = new List<ThongTinSach>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã sách");
            dt.Columns.Add("Tên sách");
            dt.Columns.Add("Năm xuất bản");
            dt.Columns.Add("Tên tác giả");
            dt.Columns.Add("Kho lưu trữ");
            dt.Columns.Add("Số lượng");
            try
            {
                var package = new ExcelPackage(new FileInfo("Danhsach.xlsx"));
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                for (int i = worksheet.Dimension.Start.Row + 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        int j = 1;
                        string masach = worksheet.Cells[i, j++].Value.ToString();
                        string tensach = worksheet.Cells[i, j++].Value.ToString();
                        string namxb = worksheet.Cells[i, j++].Value.ToString();
                        string tentg = worksheet.Cells[i, j++].Value.ToString();
                        string kholuu = worksheet.Cells[i, j++].Value.ToString();
                        string soluong = worksheet.Cells[i, j++].Value.ToString();
                        dt.Rows.Add(masach, tensach, namxb, tentg, kholuu, soluong);
                        ThongTinSach list = new ThongTinSach(masach, tensach, namxb, tentg, kholuu, soluong);
                        sach.Add(list);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi chuyển dữ liệu");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nhập file thất bại!\n");
            }
            ListSachcs.Instance.Listsach = sach;
            LoadListSach();
        }
        private void btDoc_Click(object sender, EventArgs e)
        {
            string filePath = "";   //Biến lưu đường dẫn file
            OpenFileDialog dialog = new OpenFileDialog();   //để mở file excel

            //nếu mở và lưu file thành công thì lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //tạo danh sách rỗng để hứng dữ liệu
            List<ThongTinSach> sach = new List<ThongTinSach>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã sách");
            dt.Columns.Add("Tên sách");
            dt.Columns.Add("Năm xuất bản");
            dt.Columns.Add("Tên tác giả");
            dt.Columns.Add("Kho lưu trữ");
            dt.Columns.Add("Số lượng");
            try
            {
                //mở file excel
                var package = new ExcelPackage(new FileInfo(filePath));
                //sd mục đích phi thương mại
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                //lấy sheet đầu tiên để lưu
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //duyệt tuần tự từ dòng thứ 3 cho đến hết file
                for (int i = worksheet.Dimension.Start.Row + 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        int j = 1;
                        string masach = worksheet.Cells[i, j++].Value.ToString();
                        string tensach = worksheet.Cells[i, j++].Value.ToString();
                        string namxb = worksheet.Cells[i, j++].Value.ToString();
                        string tentg = worksheet.Cells[i, j++].Value.ToString();
                        string kholuu = worksheet.Cells[i, j++].Value.ToString();
                        string soluong = worksheet.Cells[i, j++].Value.ToString();
                        dt.Rows.Add(masach, tensach, namxb, tentg, kholuu, soluong);
                        ThongTinSach list = new ThongTinSach(masach, tensach, namxb, tentg, kholuu, soluong);
                        sach.Add(list);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi chuyển dữ liệu");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nhập file thất bại!\n");
            }
            ListSachcs.Instance.Listsach = sach;
            LoadListSach();
        }

        private void Thoát_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form fm = new FrmMain();
            fm.Show();
        }

        private void FrmCapnhat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private int DaChon(string Masach)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value + "" == Masach)
                { return i; }
            }
            return -1;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            string masach = txtMasach.Text;
            string tensach = txtTensach.Text;
            string namxb = txtNamXB.Text;
            string tentg = txtTenTG.Text;
            string kholuu = cbKho.SelectedItem.ToString();
            string soluong = txtSL.Text;
            if (masach == "" || tensach == "" || soluong == "" || namxb == "" || tentg == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin cho sách.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int trung = DaChon(masach);
                if (trung == -1)
                    ListSachcs.Instance.Listsach.Add(new ThongTinSach(masach, tensach, namxb, tentg, kholuu, soluong));
                else
                    MessageBox.Show("Trùng mã sách! Hãy kiểm tra lại hoặc chọn cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListSach();
                ClearTextBox();
            }
        }
        void CreateColumnForDataGridView()
        {
            var colMaSach = new DataGridViewTextBoxColumn();
            var colTenSach = new DataGridViewTextBoxColumn();
            var colNXB = new DataGridViewTextBoxColumn();
            var colTenTG = new DataGridViewTextBoxColumn();
            var colKhoLuu = new DataGridViewTextBoxColumn();
            var colSoLuong = new DataGridViewTextBoxColumn();

            colMaSach.HeaderText = "Mã sách";
            colTenSach.HeaderText = "Tên sách";
            colNXB.HeaderText = "Năm xuất bản";
            colTenTG.HeaderText = "Tên tác giả";
            colKhoLuu.HeaderText = "Kho lưu trữ";
            colSoLuong.HeaderText = "Số lượng";

            colMaSach.DataPropertyName = "masach";
            colTenSach.DataPropertyName = "tensach";
            colNXB.DataPropertyName = "namxb";
            colTenTG.DataPropertyName = "tentg";
            colKhoLuu.DataPropertyName = "kholuu";
            colSoLuong.DataPropertyName = "soluong";

            colMaSach.Width = 100;
            colTenSach.Width = 250;
            colNXB.Width = 205;
            colTenTG.Width = 250;
            colKhoLuu.Width = 130;
            colSoLuong.Width = 100;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[] {colMaSach,colTenSach,colNXB,colTenTG,colKhoLuu,colSoLuong});
        }
        public void ClearTextBox()
        {
            txtMasach.Text = "";
            txtTensach.Text = "";
            txtNamXB.Text = "";
            txtTenTG.Text = "";
            cbKho.SelectedIndex =0;
            txtSL.Text = "";
            txtMasach.Focus();
        }
        public void LoadListSach()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ListSachcs.Instance.Listsach; 
            dataGridView1.Refresh();
        }

        private void FrmCapnhat_Load(object sender, EventArgs e)
        {
            CreateColumnForDataGridView();
            LoadListSach();
            cbKho.SelectedIndex = 0;
            importExcel();
        }
    
        private void btLuu_Click(object sender, EventArgs e)
        {
            string masach = txtMasach.Text;
            string tensach = txtTensach.Text;
            string namxb = txtNamXB.Text;
            string tentg = txtTenTG.Text;
            string kholuu = cbKho.SelectedItem.ToString();
            if (Status =="Sửa")
            {
                ListSachcs.Instance.Listsach[index].MaSach = txtMasach.Text;
                ListSachcs.Instance.Listsach[index].TenSach = txtTensach.Text;
                ListSachcs.Instance.Listsach[index].NamXB= txtNamXB.Text;
                ListSachcs.Instance.Listsach[index].TenTG = txtTenTG.Text;
                ListSachcs.Instance.Listsach[index].Kholuu = cbKho.SelectedItem.ToString();
                ListSachcs.Instance.Listsach[index].SoLuong = txtSL.Text;
            }
            LoadListSach();
            ClearTextBox();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (index <0)
            {
                MessageBox.Show("Hãy chọn sách cần cập nhật","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtMasach.Text = ListSachcs.Instance.Listsach[index].MaSach;
            txtTensach.Text = ListSachcs.Instance.Listsach[index].TenSach;
            txtNamXB.Text = ListSachcs.Instance.Listsach[index].NamXB;
            txtTenTG.Text = ListSachcs.Instance.Listsach[index].TenTG;
            cbKho.SelectedItem = ListSachcs.Instance.Listsach[index].Kholuu;
            txtSL.Text = ListSachcs.Instance.Listsach[index].SoLuong;
            Status = "Sửa";
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Status == "Search")
            {
                IndexSearch = e.RowIndex;
                for (int i = 0; i < ListSachcs.Instance.Listsach.Count; i++)
                {
                    if (ListSachcs.Instance.Listsach[i].MaSach == dataGridView1.Rows[IndexSearch].Cells[0].Value.ToString())
                        index = i;
                }
            }
            else
            { index = e.RowIndex; }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (index < 0)
            {
                MessageBox.Show("Hãy chọn sách cần xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ListSachcs.Instance.Listsach.RemoveAt(index);
            LoadListSach();
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)))
            {
                MessageBox.Show("Vui lòng nhập số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        private void txtNamXB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)))
            {
                MessageBox.Show("Vui lòng nhập số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }  
        }
        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string search = txtTimkiem.Text;
            if (search != "")
            {
                List<ThongTinSach> listSearch = new List<ThongTinSach>();
                foreach (var item in ListSachcs.Instance.Listsach)
                {
                    if (rdMa.Checked)
                    {
                        if (item.MaSach.ToLower().Contains(search.ToLower()))
                        {
                            listSearch.Add(item);
                        }
                    }
                    else
                    {
                        if (item.TenSach.ToLower().Contains(search.ToLower()))
                        {
                            listSearch.Add(item);
                        }
                    }
                }
                dataGridView1.DataSource = null;
                CreateColumnForDataGridView();
                dataGridView1.DataSource = listSearch;
            }
            else
            {
                dataGridView1.DataSource= null;
                LoadListSach();
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            txtTimkiem.Enabled = true;
            Status = "Search";
        }

    }
}

using OfficeOpenXml.DataValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.VisualStyles;

namespace QUANLYTHUVIEN
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
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

            dataGridView.Columns.AddRange(new DataGridViewColumn[] { colMaSach, colTenSach, colNXB, colTenTG, colKhoLuu, colSoLuong });
        }
            
        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form fm= new FrmMain();
            fm.Show();
        }

        private void ThongKe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btThongKe_Click(object sender, EventArgs e)
        {
            List<ThongTinSach> listSearch = new List<ThongTinSach>();
            foreach (var item in ListSachcs.Instance.Listsach)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    listSearch.Add(item);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    if (item.Kholuu.ToLower().CompareTo("Nhà Bè".ToLower()) == 0)
                    {
                        listSearch.Add(item);
                    }
                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    if (item.Kholuu.ToLower().CompareTo("Võ Văn Tần".ToLower()) == 0)
                    {
                        listSearch.Add(item);
                    }
                }
                else if (comboBox1.SelectedIndex == 3)
                {
                    if (item.Kholuu.ToLower().CompareTo("Mai Thị Lựu".ToLower()) == 0)
                    {
                        listSearch.Add(item);
                    }
                }
            }    
            dataGridView.DataSource = null;
            CreateColumnForDataGridView();
            dataGridView.DataSource = listSearch;
        }
        private void xuatfile()
        {
            string filepath = "C:\\LTGD\\QUANLYTHUVIEN\\bin\\Debug\\Danhsach.xlsx";
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook workbook = excelApp.Workbooks.Open(filepath);
            Excel.Worksheet worksheet = workbook.Sheets[1];
            try
            {
                // Lấy dữ liệu từ DataGridView và cập nhật vào tệp Excel
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 3, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }
                workbook.Save();
                workbook.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }
        private void exportExceḷ(string path)
        {
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            for (int i=0; i<dataGridView.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            } 
            for (int i=0; i<dataGridView.Rows.Count;i++)
            {
                for (int j=0; j<dataGridView.Columns.Count;j++)
                {
                    application.Cells[i + 2f, j + 1] = dataGridView.Rows[i].Cells[j].Value; 
                }    
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(path);
            application.ActiveWorkbook.Saved = true;

        }
        private void btXuat_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                xuatfile();
            else
            { 
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Xuất dữ liệu";
                saveFileDialog.Filter = "Excel(*.xlsx)|*.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        exportExceḷ(saveFileDialog.FileName);
                        MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xuất file thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}

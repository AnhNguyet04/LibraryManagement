using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYTHUVIEN
{
    public class ThongTinSach
    {
        private string masach;
        private string tensach;
        private string namxb;
        private string tentg;
        private string kholuu;
        private string soluong;
        public string MaSach
        {
            get { return masach; } set {  masach = value; }
        }
        public string TenSach
        {
            get { return tensach; }
            set { tensach = value; }
        }
        public string NamXB
        {
            get { return namxb; } set { namxb = value; }
        }
        public string TenTG
        {
            get { return tentg; } set { tentg = value; }
        }
        public string Kholuu
        {
            get { return kholuu; }  set { kholuu = value; }
        }
        public string SoLuong
        {
            get { return soluong; } set { soluong = value; }
        }
        public ThongTinSach(string masach, string tensach, string namxb, string tentg, string kholuu, string soluong)
        {
            MaSach = masach;
            TenSach = tensach;
            NamXB = namxb;
            TenTG = tentg;
            Kholuu = kholuu;
            SoLuong = soluong;
        }
    }
}

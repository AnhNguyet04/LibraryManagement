using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUANLYTHUVIEN
{
    public class ListSachcs
    {
        private static ListSachcs instance;
        List<ThongTinSach> listsach;
        public List<ThongTinSach> Listsach { get { return listsach; } set { listsach= value; } }
        public static ListSachcs Instance
        {
            get 
            { 
                if (instance == null)
                    instance = new ListSachcs();
                return instance; 
            }
            set { instance = value; }
        }
        private ListSachcs()
        {
            listsach = new List<ThongTinSach>();
            //listsach.Add(new ThongTinSach("M01", "Lập trình giao diện", "2010", "Mai Trang", "Nhà Bè", "2"));
            //listsach.Add(new ThongTinSach("M02", "Mạng máy tính", "2015", "Tô Oai Hùng", "Võ Văn Tần", "5"));
        }
    }
}

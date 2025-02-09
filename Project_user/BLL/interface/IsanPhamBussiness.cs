using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IsanPhamBussiness
    {
        sanPhamModel GetDatabyID(string id);

        // lay ra mot danh sach cac san pham
        List<sanPhamModel> GetDataAll();

        bool Update(sanPhamModel model);
        // tim kiem san pham
        List<sanPhamModel> Search(int pageIndex, int pageSize, out long total, string sanPham_name);
    }
}

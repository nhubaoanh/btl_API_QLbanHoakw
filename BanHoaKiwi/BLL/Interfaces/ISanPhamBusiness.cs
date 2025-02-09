using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISanPhamBusiness
    {
        bool Create(SanPhamModel model);

        bool UpDate(SanPhamModel model);

        bool Delete(string id);

        SanPhamModel GetDatabyID(string id);

        // lay ra mot danh sach cac san pham
        List<SanPhamModel> GetDataAll();

        // tim kiem san pham
        List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string sanPham_name);


    }
}

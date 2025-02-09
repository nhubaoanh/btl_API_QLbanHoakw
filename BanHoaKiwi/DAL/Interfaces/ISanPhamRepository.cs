using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISanPhamRepository
    {
        // cai nay la hoach dinh nhung thu minh can lam
        bool Create(SanPhamModel model);

        bool Update(SanPhamModel model);

        bool Delete(string id);

        SanPhamModel GetDatabyID(string id);

        // lay ra mot danh sach cac san pham
        List<SanPhamModel> GetDataAll();

        // tim kiem san pham
        List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string sanPham_name);
    }
}

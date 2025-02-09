using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class sanPhamBussiness : IsanPhamBussiness
    {
        private readonly IsanPhamResponsitory _res;

        public sanPhamBussiness(IsanPhamResponsitory sanPhamResponsitory)
        {
            _res = sanPhamResponsitory;
        }
        public List<sanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public sanPhamModel GetDatabyID(string id)
        {
           return _res.GetDatabyID(id);
        }

        public List<sanPhamModel> Search(int pageIndex, int pageSize, out long total, string sanPham_name)
        {
            return _res.Search(pageIndex, pageSize, out total, sanPham_name);
        }

        public bool Update(sanPhamModel model)
        {
            return _res.Update(model);
        }
    }
}

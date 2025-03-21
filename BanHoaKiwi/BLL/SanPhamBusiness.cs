﻿using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SanPhamBusiness : ISanPhamBusiness
    {
        private ISanPhamRepository _res;

        public SanPhamBusiness(ISanPhamRepository res)
        {
            _res = res;
        }
        public bool Create(SanPhamModel model)
        {
            return _res.Create(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }

        public List<SanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }

        public SanPhamModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }

        public List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string sanPham_name)
        {
            return _res.Search(pageIndex, pageSize, out total, sanPham_name);
        }

        public bool UpDate(SanPhamModel model)
        {
            return _res.Update(model);
        }
    }
}

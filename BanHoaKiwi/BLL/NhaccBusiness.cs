using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NhaccBusiness : INhaccBusiness
    {
        private INhaCCResponsitory _nhacc;

        public NhaccBusiness(INhaCCResponsitory nhaCCReponsitory)
        {
            _nhacc = nhaCCReponsitory;
        }
        public bool Create(nhaCungCapModel model)
        {
            return _nhacc.Create(model);
        }

        public bool Delete(string id)
        {
            return _nhacc.Delete(id);
        }

        public nhaCungCapModel Get(string id)
        {
            return _nhacc.Get(id);
        }

        public List<nhaCungCapModel> GetAll()
        {
            return _nhacc.GetAll();
        }

        public bool Update(nhaCungCapModel model)
        {
            return _nhacc.Update(model);
        }
    }
}

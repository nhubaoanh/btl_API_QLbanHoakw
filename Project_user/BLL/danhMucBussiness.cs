using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class danhMucBussiness : IdanhMucBussiness
    {
        private readonly IdanhMucResponsitory _res;

        public danhMucBussiness(IdanhMucResponsitory idanhMuc)
        {
            _res = idanhMuc;
        }
        public List<danhMucModel> GetAll()
        {
            return _res.GetAll();
        }

        public danhMucModel GetByID(string id)
        {
            return _res.GetByID(id);
        }
    }
}

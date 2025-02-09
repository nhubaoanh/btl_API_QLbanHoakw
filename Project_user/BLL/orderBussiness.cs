using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class orderBussiness : IorderBussiness
    {
        private readonly IorderResponsitory _res;

        public orderBussiness(IorderResponsitory orderResponsitory)
        {
            _res = orderResponsitory;
        }
        public bool create(orderModel model)
        {
            return _res.create(model);
        }

        public bool delete(string id)
        {
            return _res.delete(id);
        }

        public List<orderModel> getAll()
        {
            return _res.getAll();
        }
    }
}

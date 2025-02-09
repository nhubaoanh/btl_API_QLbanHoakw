using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrdersBusiness : IOrdersBusiness
    {
        private readonly IOrdersResponsitory _res;

        public OrdersBusiness(IOrdersResponsitory orders)
        {
            _res = orders;
        }
        public bool create(orderModel model)
        {
            model.order_id = Guid.NewGuid().ToString();
            if(model.listjson_chitiet != null)
            {
                foreach(var item in model.listjson_chitiet)
                {
                    item.order_id = model.order_id;
                    item.chitiet_id = Guid.NewGuid().ToString();

                }
            }
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

        public orderModel getId(string id)
        {
            return _res.getId(id);
        }

        public List<orderModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi)
        {
            return _res.Search(pageIndex, pageSize, out total, hoten, diachi);
        }

        public bool update(orderModel model)
        {
            return _res.update(model);
        }
    }
}

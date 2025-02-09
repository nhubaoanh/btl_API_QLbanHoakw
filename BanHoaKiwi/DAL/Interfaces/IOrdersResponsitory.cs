using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrdersResponsitory
    {
        bool create(orderModel model);

        bool update(orderModel model);

        bool delete(string id);

        orderModel getId(string id);

        List<orderModel> getAll();

        List<orderModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi);
    }
}

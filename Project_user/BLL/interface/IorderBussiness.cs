using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IorderBussiness
    {
        bool create(orderModel model);
        bool delete(string id);
        List<orderModel> getAll();
    }
}

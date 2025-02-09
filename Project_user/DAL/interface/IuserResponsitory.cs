using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IuserResponsitory
    {
        userModel GetUser(string name, string password);

        userModel GetByID(string id);

        bool Create(userModel users);

        bool Update(userModel users);

        bool Delete(string id);
    }
}

using DAL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class danhMucResponsitory : IdanhMucResponsitory
    {
        private IDataBaseHelper _helper;
        public danhMucResponsitory(IDataBaseHelper dataBase)
        {
            _helper = dataBase;
        }
        public List<danhMucModel> GetAll()
        {
            string mgs = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_danhmuc_getAll");
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                return dt.ConvertTo<danhMucModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public danhMucModel GetByID(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_danhmuc_getId",
                    "@danhmuc_id", id);
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                return dt.ConvertTo<danhMucModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using DAL.helper;
using DAL.helper.interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KhachHangResponsitory : IkhachhangResponsitory
    {
        private readonly IDataBaseHelper _helper;

        public KhachHangResponsitory(IDataBaseHelper dataBase)
        {
            _helper = dataBase;
        }
        public bool create(khachHangModel khachHangModel)
        {
            string mgs = "";
            try
            {
                var reslut = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_khachhang_create",
                    "@khachhang_id", khachHangModel.khachhang_id,
                    "@users_id", khachHangModel.users_id,
                    "@khachhang_email", khachHangModel.khachhang_email,
                    "@khachhang_password", khachHangModel.khachhang_password
                    );
                if ((reslut != null) && (!string.IsNullOrEmpty(reslut.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception(Convert.ToString(reslut) + mgs);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool delete(string id)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_khachhang_delete", "@khachhang_id", id);
                if ((!string.IsNullOrEmpty(mgs) || result != null && !string.IsNullOrEmpty(result.ToString())))
                {
                    throw new Exception($"ERROR is SERVER {Convert.ToString(result)}");
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<khachHangModel> getAll()
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_khachhang_getAll");
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<khachHangModel>().ToList();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public khachHangModel getId(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_khachhang_getId", "@khachhang_id", id);
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<khachHangModel>().FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool update(khachHangModel khachHangModel)
        {
            string mgs = "";
            try
            {
                var reslut = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_khachhang_update",
                    "@khachhang_id", khachHangModel.khachhang_id,              
                    "@khachhang_email", khachHangModel.khachhang_email,
                    "@khachhang_password", khachHangModel.khachhang_password
                    );
                if ((reslut != null) && (!string.IsNullOrEmpty(reslut.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception(Convert.ToString(reslut) + mgs);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

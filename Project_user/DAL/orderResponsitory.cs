using DAL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class orderResponsitory : IorderResponsitory
    {
        private readonly IDataBaseHelper _helper;

        public orderResponsitory(IDataBaseHelper helper)
        {
            _helper = helper;
        }
        public bool create(orderModel model)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_orders_create",
                    "@order_id", model.order_id,
                    "@ngaymua", model.ngaymua,
                    "@khachhang_name", model.khachhang_name,
                    "@nhanvien_id", model.nhanvien_id,
                    "@trangthai", model.trangthai,
                    "@chietkhau", model.chietkhau,
                    "@tongtien", model.tongtien,
                    "@sodienthoai", model.sodienthoai,
                    "@diachi", model.diachi,
                    "@listjson_chitiet", model.listjson_chitiet != null ? MessageConvert.SerializeObject(model.listjson_chitiet) : null
                );
                if ((result != null) && (!string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception($"Error is Database :  {Convert.ToString(result)}");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool delete(string id)
        {
            string msgError = "";
            try
            {
                var ressult = _helper.ExcuteScalarSprocedureWithTransaction(out msgError, "sp_order_delete", "@order_id", id);
                if ((ressult != null && !string.IsNullOrEmpty(ressult.ToString()) || !string.IsNullOrEmpty(msgError)))
                {
                    // cái này để thông ba lỗi
                    throw new Exception($"lỗi trong quá trình xóa : {Convert.ToString(ressult)} {msgError}");
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<orderModel> getAll()
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_order_getAll");
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<orderModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

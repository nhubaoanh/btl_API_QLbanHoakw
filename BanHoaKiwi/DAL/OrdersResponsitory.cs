using DAL.helper;
using DAL.helper.interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL
{
    public class OrdersResponsitory : IOrdersResponsitory
    {
        private readonly IDataBaseHelper _helper;

        public OrdersResponsitory(IDataBaseHelper helper)
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
                if((result != null) && (!string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception($"Error is Database :  {Convert.ToString(result)}");
                }
                return true;
            }catch (Exception ex)
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

        public orderModel getId(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_order_getId_json", "@order_id", id);

                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<orderModel>().FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<orderModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi)
        {
            string mgs = "";
            total = 0;
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_hoadon_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@hoten", hoten,
                    "@diachi", diachi
                    );
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                if (dt.Rows.Count > 0 && dt.Rows[0]["RecordCount"] != DBNull.Value)
                {
                    total = Convert.ToInt64(dt.Rows[0]["RecordCount"]); // Chuyển đổi an toàn
                }

                return dt.ConvertTo<orderModel>().ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool update(orderModel model)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_orders_update",
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
                if (result != null || !string.IsNullOrEmpty(mgs))
                {
                    // Nếu có lỗi từ result hoặc mgs, ném lỗi với thông tin từ cả hai
                    throw new Exception($"Error from Database: {Convert.ToString(result)}. Message: {mgs}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

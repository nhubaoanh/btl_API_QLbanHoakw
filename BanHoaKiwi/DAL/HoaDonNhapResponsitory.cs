using DAL.helper;
using DAL.helper.interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HoaDonNhapResponsitory : IHoaDonNhapResponsitori
    {
        private readonly IDataBaseHelper _helper;

        public HoaDonNhapResponsitory(IDataBaseHelper helper)
        {
            _helper = helper;
        }

        //? MessageConvert.SerializeObject có tác dụng chuyển một object thành một json
        public bool create(hoaDonNhapModel hoaDonNhapModel)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_hoadonnhap_create",
                "@hoadonnhap_id", hoaDonNhapModel.hoadonnhap_id,
                "@nhacc_id", hoaDonNhapModel.nhacc_id,
                "@nhacc_name", hoaDonNhapModel.nhacc_name,
                "@manhanvien", hoaDonNhapModel.manhanvien,
                "@tennhanvien", hoaDonNhapModel.tennhanvien,
                "@ngaynhap", hoaDonNhapModel.ngaynhap,
                "@sotien", hoaDonNhapModel.sotien,
                "@trangthai", hoaDonNhapModel.trangthai,
                "@listjson_chitiet", hoaDonNhapModel.listjson_chitietnhap != null ? MessageConvert.SerializeObject(hoaDonNhapModel.listjson_chitietnhap) : null);
                if ((result != null && !string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception($"Lỗi trong quá trình tạo hóa đơn:{Convert.ToString(result)}  {mgs}");
                }
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }

        }

        public bool delete(string id)
        {
            string msgError = "";
            try
            {
                var ressult = _helper.ExcuteScalarSprocedureWithTransaction(out msgError, "sp_hoadonnhap_delete", "@hoadonnhap_id", id);
                if((ressult != null && !string.IsNullOrEmpty(ressult.ToString()) || !string.IsNullOrEmpty(msgError)))
                {
                    // cái này để thông ba lỗi
                    throw new Exception($"lỗi trong quá trình xóa : {Convert.ToString(ressult)} {msgError}");
                }
                return true;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<hoaDonNhapModel> getAll()
        {
            string mgs = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_hoadonnhap_getAll");
                if(!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                return dt.ConvertTo<hoaDonNhapModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public hoaDonNhapModel getId(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_hoadonnhap_getId_json", "@hoadonnhap_id", id);

                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<hoaDonNhapModel>().FirstOrDefault();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<hoaDonNhapModel> Search(int pageIndex, int pageSize, out long total, string hoten)
        {
            string mgs = "";
            total = 0;
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_hoadonnhap_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@hoten", hoten
                    );
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                if (dt.Rows.Count > 0 && dt.Rows[0]["RecordCount"] != DBNull.Value)
                {
                    total = Convert.ToInt64(dt.Rows[0]["RecordCount"]); // Chuyển đổi an toàn
                }
                return dt.ConvertTo<hoaDonNhapModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(hoaDonNhapModel hoaDonNhapModel)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_hoadonnhap_update",
                "@hoadonnhap_id", hoaDonNhapModel.hoadonnhap_id,
                "@nhacc_id", hoaDonNhapModel.nhacc_id,
                "@nhacc_name", hoaDonNhapModel.nhacc_name,
                "@manhanvien", hoaDonNhapModel.manhanvien,
                "@tennhanvien", hoaDonNhapModel.tennhanvien,
                "@ngaynhap", hoaDonNhapModel.ngaynhap,
                "@sotien", hoaDonNhapModel.sotien,
                "@trangthai", hoaDonNhapModel.trangthai,
                "@listjson_chitiet", hoaDonNhapModel.listjson_chitietnhap != null ? MessageConvert.SerializeObject(hoaDonNhapModel.listjson_chitietnhap) : null);
                if ((result != null && !string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception($"Lỗi trong quá trình sửa hóa đơn:{Convert.ToString(result)}  {mgs}");
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

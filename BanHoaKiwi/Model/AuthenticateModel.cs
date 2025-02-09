using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AuthenticateModel
    {
        // tạo cái này ra chỉ để lưu và chung chuyển dữ liệu tk và mk 
        [Required]
        public string UserName {  get; set; }

        [Required]

        public string Password { get; set; }
    }
}

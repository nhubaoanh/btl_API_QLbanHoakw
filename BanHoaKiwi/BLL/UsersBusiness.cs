using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsersBusiness : IUserBusiness
    {
        private IUser _user;
        private string Secret;

        public UsersBusiness(IUser user, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _user = user;
        }
        public bool Create(usersModel users)
        {
            return _user.Create(users);
        }

        public bool Delete(string id)
        {
            return _user.Delete(id);
        }


        /// <summary>
        /// chỗ này là xác thực người dùng
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        

        // hàm này sẽ đc truyền vào tài khoản, mk sau đó kt nếu nó có thì bắt đầy tạo chuỗi
        public usersModel Authenticate(string username, string password)
        {
            // đoạn này dùng api getuser để lấy lên mật khẩu và pasword
            // nó dùng cái interface này của responsitory
            var user = _user.GetUser(username, password);
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            // chuyển đổi
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // đoạn này là lấy ra hoten của người dùng
                    new Claim(ClaimTypes.Name, user.hoten.ToString()),
                    new Claim(ClaimTypes.StreetAddress, user.diachi)
                }),
                // giữ trong 7 ngày 
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // bắt đầy tạo chuỗi nè
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            // trả ra kết quỷ chuỗi token
            return user;

        }




        public usersModel GetByID(string id)
        {
            return _user.GetByID(id);
        }

     

        public List<usersModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan)
        {
            return _user.Search(pageIndex, pageSize, out  total, hoten, taikhoan);
        }

        public bool Update(usersModel users)
        {
            return _user.Update(users);
        }
    }
}

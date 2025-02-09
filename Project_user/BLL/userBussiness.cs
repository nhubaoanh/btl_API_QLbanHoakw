using DAL;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class userBussiness : IuserBussiness
    {
        private readonly IuserResponsitory _res;
        private string Secret;
        public userBussiness(IuserResponsitory user)
        {
            _res = user;
        }

        public userModel Authenticate(string username, string password)
        {
            // đoạn này dùng api getuser để lấy lên mật khẩu và pasword
            // nó dùng cái interface này của responsitory
            var user = _res.GetUser(username, password);
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

        public bool Create(userModel users)
        {
            return _res.Create(users);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }

        public userModel GetByID(string id)
        {
            return _res.GetByID(id);
        }

        public userModel GetUser(string name, string password)
        {
            return _res.GetUser(name, password);
        }

        public bool Update(userModel users)
        {
            return _res.Update(users);
        }
    }
}

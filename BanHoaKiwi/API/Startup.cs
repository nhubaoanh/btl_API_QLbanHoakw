using BLL;
using BLL.Interfaces;
using DAL;
using DAL.helper;
using DAL.helper.interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model;
using System.Text;

namespace API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QLBanHoa", Version = "v1" });
            });

          
            // chõ này confi để lấy ra chuỗi token
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // lấy class Appsetting ra
            var appSettings = appSettingsSection.Get<AppSettings>();

            // cofig continue chuyển đổi mã
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            // thì có khởi tạo ra để chạy các hàm truy vấn dữ liệu và cái xác thực
            services.AddControllers();
            services.AddTransient<IDataBaseHelper, DatabaseHelper>();

            services.AddTransient<ISanPhamRepository, SanPhamRepository>();
            services.AddTransient<ISanPhamBusiness, SanPhamBusiness>();

            // gọi nó ra
            services.AddTransient<IDanhMuc, DanhMucReponsitory>();
            services.AddTransient<IDanhMucBusiness, DanhMucBusiness>();

            // dng ki no
            services.AddTransient<IUser, UsersResponsitory>();
            services.AddTransient<IUserBusiness, UsersBusiness>();

            // nhacc
            services.AddTransient<INhaCCResponsitory, NhaCCReponsitory>();
            services.AddTransient<INhaccBusiness, NhaccBusiness>();


            //hoa don nhap
            services.AddTransient<IHoaDonNhapResponsitori, HoaDonNhapResponsitory>();
            services.AddTransient<IHoaDonNhapBusiness, HoaDonNhapBusiness>();

            // hoa don ban
            services.AddTransient<IOrdersResponsitory, OrdersResponsitory>();
            services.AddTransient<IOrdersBusiness, OrdersBusiness>();

            // nhan vien
            services.AddTransient<INhanVienResponsitory, NhanvienResponsitory>();
            services.AddTransient<INhanVienBusiness, NhanVienBusiness>();

            // khachhang
            services.AddTransient<IkhachhangResponsitory, KhachHangResponsitory>();
            services.AddTransient<IKhachHangBusiness, KhachHangBusiness>();


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "vidu2 v1"));
            });

        }
    }
}

using BLL;
using DAL;
using DAL.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            services.AddTransient<IDataBaseHelper, DataBaseHelper>();

            services.AddTransient<IsanPhamResponsitory, sanPhamResponsitory>();
            services.AddTransient<IsanPhamBussiness, sanPhamBussiness>();


            services.AddTransient<IdanhMucResponsitory, danhMucResponsitory>();
            services.AddTransient<IdanhMucBussiness, danhMucBussiness>();


            services.AddTransient<IorderResponsitory, orderResponsitory>();
            services.AddTransient<IorderBussiness, orderBussiness>();



            services.AddTransient<IuserResponsitory, userResponsitory>();
            services.AddTransient<IuserBussiness, userBussiness>();
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
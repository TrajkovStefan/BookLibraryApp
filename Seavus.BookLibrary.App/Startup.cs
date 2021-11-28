using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Seavus.BookLibrary.Helpers;
using Seavus.BookLibrary.Shared;
using System.Text;

namespace Seavus.BookLibrary.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //CORS
            //1 WAY
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            //2 WAY

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("Access-Control-Allow-Origin", builder =>
            //        builder.AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowCredentials());
            //});

            //3 WAY

            //services.AddCors(o =>
            //{
            //    o.AddPolicy("Access-Control-Allow-Origin", builder =>
            //     builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //});

            services.AddControllers();

            var appSettingsConfiugration = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfiugration);

            AppSettings appSettings = appSettingsConfiugration.Get<AppSettings>();

            DependencyInjectionHelper.InjectDbContext(services, appSettings.DbConnectionString);
            DependencyInjectionHelper.InjectRepositories(services);
            DependencyInjectionHelper.InjectServices(services);

            //Configure JWT
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
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.SecretKey))//secret key
                        //RoleClaimType = "Role"
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
using api_target_desafio.Middlewares;
using api_target_desafio.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;





namespace api_target_desafio
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api_target_desafio", Version = "v1" });
            });
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            services.AddHttpContextAccessor();
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            });
            //services.AddDbContext<api_target_desafioContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("api_target_desafioContext")));

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_target_desafio v1"));
            }
            //app.Map("/user", UserMiddleware);



            app.ApplyUserKeyValidation();

            app.UseHttpsRedirection();
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder ApplyUserKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();

            return app;
        }
    }
}

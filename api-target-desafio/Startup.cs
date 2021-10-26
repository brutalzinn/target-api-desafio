using api_target_desafio.Config;
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
using System;
using System.IO;
using System.Reflection;

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


           

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<Swagger.Filters.SwaggerAuthentication>();
                c.OperationFilter<Swagger.Filters.ReApplyOptionalRouteParameter>();

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Target Desafio", Version = "v1",
                    Description = "Api que desenvolvi para o desafio da target pagamentos. <3",
                    Contact = new OpenApiContact
                    {
                        Name = "Roberto Caneiro Paes",
                        Url = new System.Uri("https://github.com/brutalzinn")
                    }

                });;

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddHttpContextAccessor();
            services.AddControllers()
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.Converters.Add(new Config.CustomJsonSerializer());
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc - Api Target Desafio v1"));
            }
            //app.Map("/user", UserMiddleware);
         

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.ApplyApiKeyMiddleware();

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
        public static IApplicationBuilder ApplyApiKeyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();

            return app;
        }
    }

}

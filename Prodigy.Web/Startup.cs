using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prodigy.Logica.Implementacion;
using Prodigy.Logica.Interface;
using Prodigy.Modelo.examSkillify;
using Prodigy.Persistencia;
using Prodigy.Persistencia.Implementacion;
using Prodigy.Persistencia.Interface;
using Prodigy.Utils.AppSettings;
using Prodigy.Utils.Log.Excepciones;
using Prodigy.Utils.Log.Excepciones.Importacion;
using Prodigy.Web.BitacoraApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Prodigy.Web
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

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperExamSkillify());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region Swagger

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "API examSkillify",
                    Version = "V1",
                    Description = "Servicio Web API que contiene todos los metodos del modulo de usuarios"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

            });

            #endregion

            #region Conexion a BD
            services.AddDbContext<exam_skillifyContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConexionDb"))
            .UseLazyLoadingProxies());
            #endregion

            #region Inyeccion dependencias

            #region logica
            services.AddTransient<IUsuariosLogic, UsuariosLogic>();
            #endregion

            #region modelo

            #endregion

            #region Persistencia
            services.AddTransient<IUsuariosData, UsuariosData>();
            #endregion

            #region Servicios

            #endregion

            #region Utils
            services.AddTransient<ILogging, Logging>();
            #endregion

            services.AddTransient<ApiAsistenteMiddleware>();

            #endregion

            #region Configuracion Parametros 

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            var config = configBuilder.Build();

            Setting.ConnectionString = Configuration.GetSection("ConnectionStrings:ConexionDb").Value;

            #endregion

            #region Cors

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("https://localhost:44315", "http://localhost:4200");
            }));

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            #region Swagger

            app.UseStaticFiles();
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseRedirectValidation();
            app.UseXContentTypeOptions();
            app.UseXfo(xfo => xfo.SameOrigin());
            app.UseNoCacheHttpHeaders();

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/V1/swagger.json", "API CORE V1");
                config.RoutePrefix = string.Empty;
            });

            #endregion

            #region Log
            loggerFactory.AddFile("Logs/examSkillify-{Date}.txt", LogLevel.Error);
            loggerFactory.AddFile("Logs/examSkillify-{Date}.txt", LogLevel.Warning);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            #endregion

            # region Cors
            app.UseCors("CorsPolicy");
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ApiAsistenteMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

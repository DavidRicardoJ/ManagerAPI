using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Service.Interfaces;
using Manager.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Manager.Infra.Repositories;
using AutoMapper;
using Manager.Domain.Entities;
using Manager.Service.DTO;
using Manager.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Manager.API.Token;
using System;
using Manager.Service.Cryptography.Interfaces;
using Manager.Service.Cryptography;

namespace Manager.API
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

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Manager API",
                    Version = "v1",
                    Description = "API construída na serie de vídeos no canal Lucas Eschechola.",
                    Contact = new OpenApiContact
                    {
                        Name = "J.Ricardo David",
                        Email = "david_ricardo@live.com",
                        Url = new Uri("https://github.com/DavidRicardoJ/")
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor utilize Bearer <TOKEN>",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });

            #endregion

            #region DI
            services.AddDbContext<ManagerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBCrypt, CryptographyBCrypt>();
            services.AddSingleton(d => Configuration);
            #endregion

            #region AutoMapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());
            #endregion

            #region Jwt
            var secretKey = Configuration["Jwt:Key"];
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
            #endregion

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


using Domain.Model.IdentityMedule;
using Domain.RepoInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Peresistance;
using Peresistance.Context;
using Peresistance.Identity;
using Peresistance.Repository;
using Service;
using Service.MappingProfile;
using ServiceAbestraction;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddCors(option =>{
                option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            builder.Services.AddHttpClient();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Please enter Bearer Followed by Space and your Token",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ExpenceContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<IdContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
             builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IManagerService, ManagerService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IAuthenticationsService,AuthenticationService>();
            builder.Services.AddScoped<IDataSeed, DataSeed>();

            builder.Services.AddScoped<Func<ICategoryService>>(provider =>
    () => provider.GetRequiredService<ICategoryService>());
            builder.Services.AddScoped<Func<IExpenseService>>(provider =>
            () => provider.GetRequiredService<IExpenseService>());
            builder.Services.AddScoped<Func<IReportService>>(provider =>
            () => provider.GetRequiredService<IReportService>());
            builder.Services.AddScoped<Func<IAuthenticationsService>>(provider =>
            ()=>provider.GetRequiredService<IAuthenticationsService>());



            builder.Services.AddAutoMapper(X=>X.AddProfile(new ProfileMap()));

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<IdContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(option=>
            {
                option.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWToptions:ValidIssuer"],
                    ValidAudience = builder.Configuration["JWToptions:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWToptions:SecretKey"]))
                };
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            using var scope = app.Services.CreateScope();
            var opj = scope.ServiceProvider.GetRequiredService<IDataSeed>();
            await opj.IdentityDataSeed();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

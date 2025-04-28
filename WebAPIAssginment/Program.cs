using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPIAssginment.Models;
using WebAPIAssginment.Models.Data;
using WebAPIAssginment.Models.Repository;

namespace WebAPIAssginment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options
                => options.SuppressModelStateInvalidFilter = true
            );

            builder.Services.AddDbContext<WebAPIContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WebAPIContext>()
                .AddDefaultTokenProviders();


            //JWT Authentication Settings--------------------------------------------------------
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";  //Change Authentication From Cookie to To JWT
                options.DefaultChallengeScheme = "Bearer";     //if Unauthorized=>Return UnAuthorized | Not Redirect to Account/Login Action
                options.DefaultScheme = "Bearer"; //Default Scheme
            }).AddJwtBearer("Bearer", options => //AddJwtBearer Verify And validate JWT tokens when someone sends a request.
            {
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false; //true : https Requests Only

                //Verify And validate JWT tokens when someone sends a request
                //if valid and trusted Decode it using key
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, //Not Expired
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:IssuerIP"],
                    ValidAudience = builder.Configuration["JWT:AudienceIP"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                };
            });
            //-------------------------------------------------------------------------
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            #region Swagger Setting
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 9 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                    });
            });
            #endregion

            builder.Services.AddScoped<IProductRepository , ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // Enable Swagger only in development (or as needed)
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

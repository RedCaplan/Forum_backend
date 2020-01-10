using System;
using System.Text;
using Forum.Data;
using Forum.Data.Repository;
using Forum.Model;
using Forum.Model.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;

namespace Forum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ServiceProvider Services { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add db Context and use the connection string found in appsettings.json
            services.AddDbContext<ApplicationDbContext>(options=>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //scope data initializer
            services.AddScoped<DataInitializer>();

            //scope the repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IGroupRepository, GroupRepository>()
                .AddScoped<IPostRepository, PostRepository>()
                .AddScoped<IThreadRepository, ThreadRepository>()
                .AddScoped<IVotesRepository, VotesRepository>();

            //add api documentation
            services.AddOpenApiDocument(d => {
                d.Description = "The cutest API for the cutest forum";
                d.Version = "Alpha";
                d.Title = "Forum API";
                d.DocumentName = "Forum API";
                d.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new SwaggerSecurityScheme
                {
                    Type = SwaggerSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = SwaggerSecurityApiKeyLocation.Header,
                    Description = "Copy 'Bearer' + valid JWT token into field"
                }));
                d.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            });

            //add identity options
            services.AddDefaultIdentity<UserAccount>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 10;
                    options.Password.RequiredUniqueChars = 4;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();

            
            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Services = services.BuildServiceProvider();

            //add jwt token authentication
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true //Ensure token hasn't expired
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataInitializer di)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                di.Seed();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseCors("AllowAllOrigins");

            app.UseSwaggerUi3();
            app.UseOpenApi();
        }
    }
}

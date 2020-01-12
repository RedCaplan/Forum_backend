using System.Text;
using Forum.Core.Interfaces;
using Forum.Root;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;

namespace Forum.Web
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
            CompositionRoot.InjectDependencies(services,Configuration);

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDataInitizalizer dataInitizalizer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                dataInitizalizer.Seed();
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

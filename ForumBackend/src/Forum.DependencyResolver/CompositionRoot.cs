using System;
using Forum.Core.Interfaces;
using Forum.DAL.EF;
using Forum.DAL.EF.Context;
using Forum.DAL.Models.Entities;
using Forum.DAL.Repository;
using Forum.DAL.Repository.Interfaces;
using Forum.Services.BusinessServices;
using Forum.Services.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.DependencyResolver
{
    public class CompositionRoot
    {
        public CompositionRoot()
        {
        }

        public static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            //Add db Context and use the connection string found in appsettings.json
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //scope data initializer
            services.AddScoped<IDataInitizalizer, DataInitializer>();

            //scope the repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IPostRepository, PostRepository>()
                .AddScoped<IThreadRepository, ThreadRepository>()
                .AddScoped<IVotesRepository, VotesRepository>()
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}

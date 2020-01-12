﻿using System;
using System.Collections.Generic;
using System.Text;
using Forum.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.IntegrationTests.Data
{
    public abstract class BaseEfRepoTestFixture
    {
        protected ApplicationDbContext _dbContext;

        protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("ForumTestInMemoryDatabase")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected ApplicationDbContext GetRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new ApplicationDbContext(options);
            return _dbContext;  
        }
    }
}

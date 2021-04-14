using Library.Infrastructure.Application;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.Entrusts;
using Library.Persistence.EF.Members;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Entrusts;
using Library.Services.Entrusts.Contracts;
using Library.Services.Members;
using Library.Services.Members.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi
{
    public class Application
    {
        public Application(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<EFDataContext>
                (
                options =>
                {
                    options.UseSqlServer("Server=.;Database=Library;Trusted_Connection=True;");
                    });


            services.AddScoped<UnitOfWork, EFUnitOfWork>();


            services.AddScoped<BookCategoryService, BookCategoryAppService>();
            services.AddScoped<BookCategoryRepository, EFBookCategoryRepository>();

            services.AddScoped<BookService, BookAppService>();
            services.AddScoped<BookRepository, EFBookRepository>();

            services.AddScoped<MemberService, MemberAppService>();
            services.AddScoped<MemberRepository, EFMemberRepository>();

            services.AddScoped<EntrustService, EntrustAppService>();
            services.AddScoped<EntrustRepository, EFEntrustRepository>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

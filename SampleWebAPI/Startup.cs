using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SampleWebAPI
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
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Employee"));

            //set the swagger generator
            services.AddSwaggerGen(setupAction=> {
                setupAction.SwaggerDoc("v1",new Swashbuckle.AspNetCore.Swagger.Info {
                    Title = "SampleSwagger"
                });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //configure swagger  and UI
            app.UseSwagger();
            app.UseSwaggerUI(
                setupAction =>
                {
                    setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger API");
                }
                );
            app.UseMvc();
        }
    }
}

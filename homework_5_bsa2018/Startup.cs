using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using homework_5_bsa2018.BLL.Services;
using homework_5_bsa2018.BLL.Interfaces;
using homework_5_bsa2018.DAL.Interfaces;
using homework_5_bsa2018.DAL;
using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using homework_5_bsa2018.BLL;
using homework_5_bsa2018.Shared;

namespace homework_5_bsa2018
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
            services.AddMvc();
            services.AddAutoMapper();
            services.AddScoped<IService<CrewDTO>, CrewService>();
            services.AddScoped<IService<PilotDTO>, PilotService>();
            services.AddScoped<IService<StewardessDTO>, StewardessService>();
            services.AddScoped<IService<PlaneTypeDTO>, PlaneTypeService>();
            services.AddScoped<IService<PlaneDTO>, PlaneService>();
            services.AddScoped<IService<TicketDTO>, TicketService>();
            services.AddScoped<IService<FlightDTO>, FlightService>();
            services.AddScoped<IService<DepartureDTO>, DepartureService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SeedData>();
            var connection = ConnectionString.Value;
            services.AddDbContext<AirportContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

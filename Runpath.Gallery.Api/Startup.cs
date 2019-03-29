using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Runpath.Gallery.Api.Infrastructure;
using Runpath.Gallery.Api.Models;
using Runpath.Gallery.Api.Repository;
using Runpath.Gallery.Domain;
using Runpath.Gallery.Domain.Entities;

namespace Runpath.Gallery.Api
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
            services.AddDbContext<GalleryContext>(options => options.UseInMemoryDatabase("Runpath.Gallery"));            
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddScoped<GalleryDataService>();
            services.AddTransient<Seeder>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            try
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap<Album, AlbumDto>().ReverseMap();
                    config.CreateMap<Photo, PhotoDto>().ReverseMap();
                    config.CreateMap<User, UserDto>().ReverseMap();
                });
            }
            catch (Exception)
            {
                // Ignore for xUnit as this Configuration has bein re-used for Test project.
                // TODO : Create Startup configuration for Runpath.Gallery.Tests
            }

            seeder.EnsureCreated();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            Mapper.Initialize(config =>
            {
                config.CreateMap<Album, AlbumDto>().ReverseMap();
                config.CreateMap<Photo, PhotoDto>().ReverseMap();
                config.CreateMap<User, UserDto>().ReverseMap();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

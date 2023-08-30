using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Library.Mapping;

namespace Library.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositoryServices();
            services.AddSingleton(Assembly.GetExecutingAssembly());

            //Add mapper
            var MapperCfg = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BookProfile());
                mc.AddProfile(new ChapterProfile());
            });
            IMapper Mapper = MapperCfg.CreateMapper();
            services.AddSingleton(Mapper);

            //Add Context
            var conn = configuration.GetConnectionString("Default");
            services.AddDbContext<LibraryContext>(builder => builder.UseSqlServer(conn));

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });
        }
    }
}

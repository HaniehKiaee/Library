using Infrastructure.Data.Repositories.Contracts;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IChapterRepository, ChapterRepository>();
            services.AddTransient<ILogErrorRepository, LogErrorRepository>();
            services.AddTransient<ILogInformationRepository, LogInformationRepository>();
        }
    }
}

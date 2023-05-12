using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DbOperations;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Books.AddRange(
                     new Book
                     {
                         Id = 1,
                         Title = "Böyle Söyledi Zerdüst",
                         GenreId = 1,
                         PageCount = 350,
                         PublishDate = new DateTime(2001, 06, 12)
                     },
                     new Book
                     {
                         Id = 2,
                         Title = "Savas Sanatı",
                         GenreId = 2,
                         PageCount = 150,
                         PublishDate = new DateTime(2000, 06, 12)
                     },
                     new Book
                     {
                         Id = 3,
                         Title = "Yer Altindan Notlar",
                         GenreId = 2,
                         PageCount = 250,
                         PublishDate = new DateTime(2003, 06, 12)
                     }
                 );
                 context.SaveChanges();
            }

        }
    }
}
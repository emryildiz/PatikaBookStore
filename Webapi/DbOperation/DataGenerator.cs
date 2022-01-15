using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Webapi.DbOperation
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
                        Title = "Learn Startup",
                        GenreId = 1, //Personal Growth,
                        PageCount = 200,
                        PublishDate = new System.DateTime(2001, 06, 12)

                    },
                    new Book
                    {
                        Title = "HerLand",
                        GenreId = 2, //Science Fiction,
                        PageCount = 300,
                        PublishDate = new System.DateTime(2004, 02, 11)

                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, //Science Fiction,
                        PageCount = 520,
                        PublishDate = new System.DateTime(2006, 06, 23)

                    }); 
                    context.SaveChanges();

            }

        }



    }
}
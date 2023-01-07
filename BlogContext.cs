using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class BlogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-3BJ5GK9;Initial Catalog=OptimizeMePlease;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? Country { get; set;}
        public int BooksCount { get; set; }
        public string? NickName { get; set;}
        public int UserId { get; set; }
        public ICollection<Book> Books { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime Published { get; set;}
        public string ISBN { get; set; }
        public int PublisherId { get; set; }
    }
}

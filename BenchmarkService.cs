using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [ShortRunJob, Config(typeof(Config))]
    public class BenchmarkService
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                SummaryStyle = BenchmarkDotNet.Reports.SummaryStyle.Default.WithRatioStyle(BenchmarkDotNet.Columns.RatioStyle.Trend);
            }
        }        

        
        [Benchmark(Baseline = true)]
        public IList<Author> GetAuthorsWithInclude()
        {
            BlogContext blogContext = new();
            var result = blogContext.Authors.Take(10).Include("Books").ToList();
            return result;
        }

        [Benchmark]
        public IList<Author> GetAuthorsWithJoin()
        {
            BlogContext blogContext = new();
            var result = (from author in blogContext.Authors.Take(10)
                         select new Author
                         {
                             Id = author.Id,
                             Age = author.Age,
                             Books = blogContext.Books.Where(p => p.AuthorId == author.Id).ToList(),
                             BooksCount = author.BooksCount,
                             Country = author.Country,
                             NickName = author.NickName,
                             UserId = author.UserId
                         }).ToList();

            return result;
        }
        

        [Benchmark]
        public async Task GetAuthorsWithSqlQuery()
        {
            BlogContext blogContext = new();
            blogContext.Database.ExecuteSqlInterpolatedAsync(
                $"select * from Authors au left join Books bo on bo.AuthorId = au.Id");
        }


    }
}

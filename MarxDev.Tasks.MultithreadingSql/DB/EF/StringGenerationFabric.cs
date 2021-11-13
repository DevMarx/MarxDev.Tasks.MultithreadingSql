
using Microsoft.EntityFrameworkCore;

namespace MarxDev.Tasks.MultithreadingSql
{
    public class StringGenerationFabric
    {
        private readonly string _connectionString;

        public DbContextOptions<StringGenerationsContext> GetDbContextOptions =>
            new DbContextOptionsBuilder<StringGenerationsContext>().UseSqlServer(_connectionString).Options;
        public StringGenerationFabric(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Įtrerpia sugeneruotą reiksme
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="stringGeneration"></param>
        /// <returns>StringGeneration su Id</returns>
        public StringGeneration InsertStringGeneration(StringGenerationsContext dbContext, StringGeneration stringGeneration)
        {
           
            dbContext.StringGenerations.Add(stringGeneration);
            dbContext.SaveChanges();
            return stringGeneration;
        }



    }
}

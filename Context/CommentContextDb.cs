using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CommentService.Context
{
    public class CommentContextDb : DbContext
    {
        public DbSet<Comment> Resources { get; set; }

        public CommentContextDb(DbContextOptions<CommentContextDb> option) : base(option)
        {
            try
            {
                if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator dbCreator)
                {
                    if (!dbCreator.CanConnect()) dbCreator.Create();
                    if (!dbCreator.HasTables()) dbCreator.CreateTables();
                }
            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}


using CommentService.Context;
using CommentService.Interfaces;

namespace CommentService.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private CommentContextDb Db { get; init; }

        public IRepository<Comment> Comments { get; private set; }

        public UnitOfWorkRepository(CommentContextDb db)
        {
            Db = db;
            Comments = new Repository<Comment>(Db);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}
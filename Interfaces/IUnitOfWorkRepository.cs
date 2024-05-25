namespace CommentService.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        public IRepository<Comment> Comments { get; }
        public void Save();
    }
}

namespace CommentService.DTOs
{
    public class EditCommentDto
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required Guid OrganizationId { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ParentId { get; set; }
        public Guid? ReplyId { get; set; }
        public required string Content { get; set; }
    }
}

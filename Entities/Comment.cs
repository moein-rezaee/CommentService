using CommentService.Enums;

namespace CommentService
{
    public class Comment
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required Guid OrganizationId { get; set; }
        public required Guid UserId { get; set; }
        public required Guid TargetId { get; set; }
        public Guid? ParentId { get; set; }
        public required string Content { get; set; }
        public required CommentStatus Status { get; set; }
        // public required int? Rate { get; set; }
        // public required int? LikeCount { get; set; }
    }
}
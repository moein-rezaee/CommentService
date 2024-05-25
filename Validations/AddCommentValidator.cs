using CommentService.DTOs;
using FluentValidation;

namespace CommentService.Validations
{
    public class AddCommentValidator : AbstractValidator<AddCommentDto>
{
    public AddCommentValidator()
    {
        RuleFor(i => i.UserId)
            .NotEmpty()
            .NotNull();
    }

}
}
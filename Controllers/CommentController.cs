using CommentService.Common;
using CommentService.DTOs;
using CommentService.Interfaces;
using CustomResponse.Models;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace CommentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController(
        IUnitOfWorkRepository db,
        ILogger<CommentController> logger,
        IValidator<AddCommentDto> addValidator,
        IValidator<EditCommentDto> editValidator) : ControllerBase
    {
        private IUnitOfWorkRepository _db { get; init; } = db;
        private ILogger<CommentController> _logger { get; init; } = logger;
        private IValidator<AddCommentDto> _addValidator { get; init; } = addValidator;
        private IValidator<EditCommentDto> _editValidator { get; init; } = editValidator;


        [HttpGet]
        public IActionResult Get()
        {
            Result result;
            try
            {
                var found = _db.Comments.ToList();
                result = CustomResults.GetRecordsOk(found);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
                result = CustomErrors.GetRecordsFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Result result;
            try
            {
                var found = _db.Comments.Find(id);
                if (found is null)
                {
                    result = CustomErrors.RecordNotFaound();
                }
                else
                {
                    result = CustomResults.GetRecordOk(found);
                }
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
                result = CustomErrors.GetRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPost]
        public IActionResult Add(AddCommentDto dto)
        {
            Result result;
            try
            {
                var check = _addValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                    return StatusCode(result.StatusCode, result);
                }

                Comment item = dto.Adapt<Comment>();
                _db.Comments.Add(item);
                _db.Save();

                result = CustomResults.AddRecordOk(item.Id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
                result = CustomErrors.AddRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }

        [HttpPut]
        public IActionResult Edit(EditCommentDto dto)
        {
            Result result;
            try
            {
                var check = _editValidator.Validate(dto);
                if (!check.IsValid)
                {
                    result = CustomErrors.InvalidData(check.Errors);
                    return StatusCode(result.StatusCode, result);
                }

                bool isOk = _db.Comments.Edit(dto);
                _db.Save();

                if (isOk)
                {
                    result = CustomResults.EditRecordOk();
                }
                else
                {
                    result = CustomErrors.RecordNotFaound();
                }

                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
                result = CustomErrors.EditRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }


        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            Result result;
            try
            {
                var isOk = _db.Comments.Delete(id);
                _db.Save();

                if (isOk)
                {
                    result = CustomResults.DeleteRecordOk();
                }
                else
                {
                    result = CustomErrors.RecordNotFaound();
                }
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToJson());
                result = CustomErrors.DeleteRecordFailed();
                return StatusCode(result.StatusCode, result);
            }
        }
    }
}

using Pulse.Core.DTOs;
using Pulse.Core.Extensions;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Interfaces.Validations;
using Pulse.Core.Models;

namespace Pulse.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IValidation<CreateCommentDto> _validation;

        public CommentService(ICommentRepository commentRepository,
            IPostRepository postRepository, IValidation<CreateCommentDto> validation)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _validation = validation;
        }

        public async ValueTask<ServiceResult<ReadPostDto>> AddComment(CreateCommentDto comment)
        {
            _ = comment ?? throw new ArgumentNullException(nameof(comment));
            ServiceResult<ReadPostDto> result = new();

            if (!_validation.IsValid(comment, out List<ErrorMessage> errors))
            {
                result.Errors = errors;
                return result;
            }

            Post post = await _postRepository.GetByIdAsync(comment.PostId!);

            if (post == null)
            {
                result.Errors.Add(new ErrorMessage(nameof(comment.PostId), $"Post not avialable with {comment.PostId}"));
                return result;
            }

            Comment saved = await _commentRepository.CreateAsync(comment.CreateCommentDtoToComment());

            if (saved == null)
            {
                result.Errors.Add(new ErrorMessage(nameof(comment), $"Comment cannot be saved"));
                return result;
            }

            if (comment.ReplyId != null)
            {
                Comment reply = await _commentRepository.GetByIdAsync(comment.ReplyId);

                if (reply == null)
                {
                    result.Errors.Add(new ErrorMessage(nameof(comment), $"Comment not avialable with {comment.ReplyId}"));
                    return result;
                }

                reply.Replies.Add(saved.Id);
                await _commentRepository.UpdateAsync(reply);
            }
            else
            {
                post.Comments.Add(saved.Id);
                await _postRepository.UpdateAsync(post);
            }

            result.Data = post.PostToReadPostDto();

            return result;
        }
    }
}

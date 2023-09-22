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

        public async ValueTask<Response> AddComment(CreateCommentDto comment)
        {
            _ = comment ?? throw new ArgumentNullException(nameof(comment));
            Response response = new();

            if(!_validation.IsValid(comment, ref response))
            {
                return response;
            }

            Post post = await _postRepository.GetByIdAsync(comment.PostId!);

            if (post == null)
            {
                response.Messages.Add($"Post not avialable with {comment.PostId}");
                return response;
            }

            Comment saved = await _commentRepository.CreateAsync(comment.CreateCommentDtoToComment());

            if (saved == null)
            {
                response.Messages.Add($"Comment cannot be saved");
                return response;
            }

            if(comment.ReplyId != null)
            {
                Comment reply = await _commentRepository.GetByIdAsync(comment.ReplyId);

                if ( reply == null)
                {
                    response.Messages.Add($"Comment not avialable with {comment.ReplyId}");
                    return response;
                }

                reply.Replies.Add(saved.Id);
                await _commentRepository.UpdateAsync(reply);
            } 
            else
            {
                post.Comments.Add(saved.Id);
                await _postRepository.UpdateAsync(post);
            }

            response.Content = post.PostToReadPostDto();

            return response;
        }
    }
}

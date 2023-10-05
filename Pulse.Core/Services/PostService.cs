using Pulse.Core.DTOs;
using Pulse.Core.Extensions;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Infrastructures;
using Pulse.Core.Interfaces.Services;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IValidation<CreatePostDto> _validation;

        public PostService(IPostRepository repository, IValidation<CreatePostDto> validation)
        {
            _repository = repository;
            _validation = validation;
        }

        public async ValueTask<ServiceResult<ReadPostDto>> CreatePostAsync(CreatePostDto post)
        {
            _ = post ?? throw new ArgumentNullException(nameof(post));
            ServiceResult<ReadPostDto> result = new();

            if (!_validation.IsValid(post, out List<ErrorMessage> errors))
            {
                result.Errors = errors;
                return result;
            }

            var savedPost = await _repository.CreateAsync(post.CreatePostDtoToPost());

            result.Data = savedPost.PostToReadPostDto();

            return result;
        }

        public async ValueTask<ServiceResult<bool>> DeletePostAsync(string id, string creatorId)
        {
            _ = id ?? throw new ArgumentNullException(nameof(id));
            _ = creatorId ?? throw new ArgumentNullException(nameof(creatorId));
            ServiceResult<bool> result = new();

            var post = await _repository.GetByIdAsync(id);

            if (post == null)
            {
                result.Errors.Add(new ErrorMessage(string.Empty, $"No post found with id of {id}"));
                return result;
            }

            if (post.CreatorId == creatorId)
            {
                result.Errors.Add(new ErrorMessage(string.Empty, $"Post is not access for the user to delete."));
                return result;
            }

            await _repository.DeleteAsync(post.Id);
            result.Data = true;

            return result;
        }

        public async ValueTask<ServiceResult<ReadPostDto>> GetPostByIdAsync(string id)
        {
            _ = id ?? throw new ArgumentNullException(nameof(id));
            ServiceResult<ReadPostDto> result = new();

            var post = await _repository.GetByIdAsync(id);

            if(post == null) 
            {
                result.Errors.Add(new ErrorMessage(string.Empty, $"No post found with id of {id}"));
                return result;
            }

            result.Data = post.PostToReadPostDto();
            return result;
        }
    }
}

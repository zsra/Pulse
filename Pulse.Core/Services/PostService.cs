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

        public async ValueTask<Response> CreatePostAsync(CreatePostDto post)
        {
            _ = post ?? throw new ArgumentNullException(nameof(post));
            Response response = new();

            if (!_validation.IsValid(post, ref response))
            {
                return response;
            }

            var savedPost = await _repository.CreateAsync(post.CreatePostDtoToPost());

            response.Content = savedPost.PostToReadPostDto();

            return response;
        }

        public async ValueTask<Response> DeletePostAsync(string id, string creatorId)
        {
            _ = id ?? throw new ArgumentNullException(nameof(id));
            _ = creatorId ?? throw new ArgumentNullException(nameof(creatorId));
            Response response = new();

            var post = await _repository.GetByIdAsync(id);

            if (post == null)
            {
                response.Messages.Add($"No post found with id of {id}");
                return response;
            }

            if (post.CreatorId == creatorId)
            {
                response.Messages.Add($"Post is not access for the user to delete.");
                return response;
            }

            if (await _repository.DeleteAsync(post.Id)) 
            {
                response.Messages.Add($"Message deleted");
                return response;
            }

            response.Messages.Add($"Message couldn't delete");
            return response;
        }

        public async ValueTask<Response> GetPostByIdAsync(string id)
        {
            _ = id ?? throw new ArgumentNullException(nameof(id));
            Response response = new();

            var post = await _repository.GetByIdAsync(id);

            if(post == null) 
            {
                response.Messages.Add($"No post found with id of {id}");
                return response;
            }

            response.Content = post.PostToReadPostDto();
            return response;
        }
    }
}

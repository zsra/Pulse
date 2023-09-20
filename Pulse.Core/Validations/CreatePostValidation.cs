using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations
{
    internal class CreatePostValidation : IValidation<CreatePostDto>
    {
        public bool IsValid(CreatePostDto item, ref Response response)
        {
            if (string.IsNullOrEmpty(item.CreatorId))
            {
                response.Messages.Add($"{item.CreatorId} canot be null");
            }

            if (string.IsNullOrEmpty(item.Content)) 
            {
                response.Messages.Add($"{item.Content} canot be null");
            }

            return !response.Messages.Any();
        }
    }
}

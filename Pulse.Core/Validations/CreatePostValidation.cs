using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

public class CreatePostValidation : IValidation<CreatePostDto>
{
    public bool IsValid(CreatePostDto item, out List<ErrorMessage> errors)
    {
        errors = new List<ErrorMessage>();

        if (string.IsNullOrEmpty(item.CreatorId))
        {
            errors.Add(new ErrorMessage(nameof(item.CreatorId), $"{item.CreatorId} canot be null"));
        }

        if (string.IsNullOrEmpty(item.Content))
        {
            errors.Add(new ErrorMessage(nameof(item.Content), $"{item.Content} canot be null"));
        }

        return !errors.Any();
    }
}

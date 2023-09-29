using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

public class CreateCommentValidation : IValidation<CreateCommentDto>
{
    public bool IsValid(CreateCommentDto item, ref Response response)
    {
        if (string.IsNullOrEmpty(item.PostId))
        {
            response.Messages.Add($"{item.PostId} canot be null");
        }

        if (string.IsNullOrEmpty(item.Content))
        {
            response.Messages.Add($"{item.Content} canot be null");
        }

        return !response.Messages.Any();
    }
}

﻿using Pulse.Core.DTOs;
using Pulse.Core.Feedback;
using Pulse.Core.Interfaces.Validations;

namespace Pulse.Core.Validations;

public class CreateCommentValidation : IValidation<CreateCommentDto>
{
    public bool IsValid(CreateCommentDto item, out List<ErrorMessage> errors)
    {
        errors = new List<ErrorMessage>();

        if (string.IsNullOrEmpty(item.PostId))
        {
            errors.Add(new ErrorMessage(nameof(item.PostId), $"{item.PostId} canot be null"));
        }

        if (string.IsNullOrEmpty(item.Content))
        {
            errors.Add(new ErrorMessage(nameof(item.Content), $"{item.Content} canot be null"));
        }

        return !errors.Any();
    }
}

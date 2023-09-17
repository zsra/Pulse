using Pulse.Core.Enums;
using Pulse.Core.Interfaces;

namespace Pulse.Core.Feedback;

public class Response
{
    public ResponseTypes ResponseType {  get; set; }

    public IResponseContent? Content { get; set; } 

    public IEnumerable<string> Messages { get; set; } 
        = new List<string>();
}

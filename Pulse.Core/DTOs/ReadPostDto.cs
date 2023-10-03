using Pulse.Core.Interfaces;

namespace Pulse.Core.DTOs
{
    public class ReadPostDto
    {
        public string? Id { get; set; }

        public string? CreatorId { get; set; }

        public string? Content { get; set; }

        public DateTime? PostedAt { get; set; }

        public int Likes { get; set; }

    }
}

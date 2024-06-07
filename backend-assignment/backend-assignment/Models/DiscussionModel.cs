using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;

namespace backend_assignment.Models
{
    public class DiscussionModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string HashTags { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Creator { get; set; }
    }
}

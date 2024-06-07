using backend_assignment.Models;

namespace backend_assignment.Interfaces
{
    public interface IDiscussionRepository
    {
        public Task<string> Create(DiscussionModel post);
        public Task<string> Update(DiscussionModel post);
        public Task<List<DiscussionModel>> SearchHt(string tags);
        public Task<List<DiscussionModel>> SearchText(string tags);
    }
}

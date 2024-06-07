using backend_assignment.Interfaces;
using backend_assignment.Models;
using backend_assignment.Repositories;
using Microsoft.Extensions.Hosting;

namespace backend_assignment.Core
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _repository;

        public DiscussionService(IDiscussionRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Create(DiscussionModel post)
        {
            try
            {
                post.HashTags=SortTags(post.HashTags);
                post.CreatedOn = DateTime.Now;
                return await _repository.Create(post);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Update(DiscussionModel post)
        {
            try
            {
                post.HashTags = SortTags(post.HashTags);
                return await _repository.Update(post);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<DiscussionModel>> SearchHt(string tags)
        {
            tags = SortTags(tags);
            return await _repository.SearchHt(tags);
        }

        public async Task<List<DiscussionModel>> SearchText(string text)
        {
            return await _repository.SearchText(text);
        }

        private string SortTags(string tags) {
            var list = tags.Split("#").ToList();
            list = list.OrderBy(x => x).ToList();
            string res = "";
            foreach (var tag in list)
            {
                res += " #" + tag;
            }
            return res;
        }
    }
}

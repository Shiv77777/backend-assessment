using backend_assignment.Interfaces;
using backend_assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_assignment.Controllers
{
    [Route("/posts")]
    public class DiscussionController : Controller
    {
        private readonly IDiscussionService _discussionService;
        public DiscussionController(IDiscussionService discussionService)
        {
            _discussionService = discussionService;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<string> Create([FromBody]DiscussionModel discussion)
        {
            return await _discussionService.Create(discussion);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<string> Update([FromBody] DiscussionModel discussion)
        {
            return await _discussionService.Update(discussion);
        }

        [Route("SearchTags")]
        [HttpGet]
        public async Task<List<DiscussionModel>> SearchHt([FromQuery] string tags)
        {
            return await _discussionService.SearchHt(tags);
        }

        [Route("SearchText")]
        [HttpGet]
        public async Task<List<DiscussionModel>> SearchText([FromQuery] string text)
        {
            return await _discussionService.SearchText(text);
        }
    }
}

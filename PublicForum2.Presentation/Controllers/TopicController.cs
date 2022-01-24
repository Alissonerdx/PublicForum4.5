using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PublicForum2.Application.Interfaces;
using PublicForum2.Application.ViewModels;
using PublicForum2.Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PublicForum2.Presentation.Controllers
{
    [Authorize]
    public class TopicController : BaseController
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetTableData(
           int page,
           int size,
           List<Dictionary<string, string>> filter)
        {
            var userId = User.Identity.GetUserId();
            var result = await _topicService.GetPaginated(page, size, filter, userId);
            return JsonContent(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetail(Guid id)
        {
            var result = await _topicService.GetById(id);
            return JsonContent(new
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                OwnerId = result.OwnerId,
                Owner = $"{result.Owner.FirstName} {result.Owner.LastName}",
                Date = result.Created.ToLongDateString(),
                Info = $"Posted on {result.Created.ToLongDateString()} by {result.Owner.FirstName} {result.Owner.LastName}"
            });
        }

        [HttpPost]
        public ActionResult Create(TopicViewModel viewModel)
        {
            viewModel.OwnerId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
                return JsonContent(new { Success = false, Message = "Fill in all mandatory fields" });

            var result = _topicService.Create(viewModel);
            if (result.Validation != null && !result.Validation.IsValid)
                return JsonContent(new { Success = false, Message = String.Join("<br/>", result.Validation.Errors.Select(e => e.ErrorMessage)) });

            return JsonContent(new { Success = true, Message = "Topic created successfully" });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TopicViewModel viewModel)
        {
            viewModel.OwnerId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
                return JsonContent(new { Success = false, Message = "Fill in all mandatory fields" });

            var result = await _topicService.Update(viewModel);
            if (result.Validation != null && !result.Validation.IsValid)
                return JsonContent(new { Success = false, Message = String.Join("<br/>", result.Validation.Errors.Select(e => e.ErrorMessage)) });

            return JsonContent(new { Success = true, Message = "Topic updated successfully" });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var userLogged = User.Identity.GetUserId();
            var result = await _topicService.Delete(Guid.Parse(id), Guid.Parse(userLogged));
            if (result != null)
                return JsonContent(new { Success = true, Message = "Topic deleted successfully" });

            return JsonContent(new { Success = false, Message = "Topic not found" });
        }
    }
}
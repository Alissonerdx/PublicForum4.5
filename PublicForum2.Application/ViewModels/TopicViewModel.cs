using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PublicForum2.Application.ViewModels
{
    public class TopicViewModel : BaseViewModel
    {
        public TopicViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string OwnerId { get; set; }
        public UserViewModel Owner { get; set; }
        public bool IsDeleted { get; set; }
    }
}

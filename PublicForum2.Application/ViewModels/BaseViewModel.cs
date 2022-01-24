using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Application.ViewModels
{
    public class BaseViewModel
    {
        public ValidationResult Validation { get; set; }
    }
}

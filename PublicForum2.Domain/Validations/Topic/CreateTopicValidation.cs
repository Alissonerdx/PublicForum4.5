using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Domain.Validations.Topic
{
    public class CreateTopicValidation : TopicValidation
    {
        public CreateTopicValidation()
        {
            ValidateTitle();
            ValidateDescription();
            ValidateCreateDate();
        }
    }
}

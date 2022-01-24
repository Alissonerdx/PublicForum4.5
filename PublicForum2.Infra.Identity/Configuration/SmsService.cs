using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Infra.Identity.Configuration
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            //TWILIO SMS Provider.
            // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

            const string accountSid = "ID";
            const string authToken = "TOKEN";

            //var client = new TwilioRestClient(accountSid, authToken);

            //client.SendMessage("Phone Number", message.Destination, message.Body);

            return Task.FromResult(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAP.Presentation.Models;
using OpenPop.Mime;
using OpenPop.Pop3;


namespace MAP.Presentation.Controllers
{
    public class POPEmailController : Controller
    {
        // GET: POPEmail

        // GET: POPEmail
        public ActionResult Index()
        {
            OpenPop.Pop3.Pop3Client pop3Client = new OpenPop.Pop3.Pop3Client();
            pop3Client.Connect("pop.gmail.com", 995, true);
            pop3Client.Authenticate("cloud.army2019@gmail.com", "Cloudarmy19.", OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
            int count = pop3Client.GetMessageCount(); //total count of email in MessageBox  
            var Emails = new List<POPEmail>(); //POPEmail type 
            int counter = 0;


            for (int i = count; i >= 1; i--)
            {
                Message message = pop3Client.GetMessage(i);
                POPEmail email = new POPEmail()
                {
                    MessageNumber = i,
                    Subject = message.Headers.Subject,
                    DateSent = message.Headers.DateSent,
                    From = string.Format("<a href = 'mailto:{1}'>{1}</a>", message.Headers.From.DisplayName, message.Headers.From.Address),
                };
                MessagePart body = message.FindFirstHtmlVersion();
                if (body != null)
                {
                    email.Body = body.GetBodyAsText();
                }
                else
                {
                    body = message.FindFirstPlainTextVersion();
                    if (body != null)
                    {
                        email.Body = body.GetBodyAsText();
                    }
                }
                List<MessagePart> attachments = message.FindAllAttachments();

                foreach (MessagePart attachment in attachments)
                {
                    email.Attachments.Add(new Attachment
                    {
                        FileName = attachment.FileName,
                        ContentType = attachment.ContentType.MediaType,
                        Content = attachment.Body
                    });
                }
                Emails.Add(email);
                counter++;

                ViewBag.COUNT = counter;//return list of emails
                ViewBag.MAILID = email.MessageNumber;
            }

            var emails = Emails;

            return View(emails);


        }
    }
}
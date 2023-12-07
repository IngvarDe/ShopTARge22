using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.Dto;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Models.Emails;

namespace ShopTARge22.Controllers
{
    public class EmailsController : Controller
    {
        private readonly IEmailsServices _emailServices;

        public EmailsController(IEmailsServices emailsServices)
        {
            _emailServices = emailsServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm) 
        {
            var dto = new EmailDto()
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = vm.Body,
            };

            _emailServices.SendEmail(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}

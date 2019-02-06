using BucketList.Entities;
using BucketList.Site.Database;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BucketList.Site.Controllers
{
    [Route("ResetPassword")]
    public class ResetPasswordController : Controller
    {
        private BucketListContext _context;

        private static Guid guid;

        public ResetPasswordController()
        {
            _context = new BucketListContext();
        }

        [HttpGet]
        [Route("RequestPasswordReset")]
        public async Task<string> RequestPasswordReset(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            if (user != null)
            {
                var guid = user.ID;
                var response = await SendResetEmail(email, guid);
                return response.StatusCode.ToString();
            }
            return "User not found";
        }

        [HttpGet]
        [Route("change/{guid}")]
        public ActionResult Change(Guid guid)
        {
            ResetPasswordController.guid = guid;
            return View("ChangePassword");
        }

        [HttpGet]
        [Route("resetpassword")]
        public ActionResult ResetPassword(ChangePasswordInfo info)
        {
            var user = _context.Users.SingleOrDefault(x => x.ID == guid);
            var userChanges = user;
            string hash = SecurePasswordHasher.Hash(info.Password);
            userChanges.PasswordHash = hash;
            _context.Entry(user).CurrentValues.SetValues(userChanges);
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return View("PasswordChanged");   
        }

        private async Task<Response> SendResetEmail(string recipient, Guid guid)
        {
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_APIKEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("help@acebucketlist.azurewebsites.net", "Ace Bucket List");
            var subject = "Reset Password";
            var to = new EmailAddress(recipient);
            var plainTextContent = "Click this link to reset your password";
            var htmlContent = "<h2>" + plainTextContent + " http://acebucketlist.azurewebsites.net/resetpassword/change?guid=" + guid + "</h2>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }
    }
}
using PersonalSiteMVC.UI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PersonalSiteMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Links()
        { 
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                //Model state was invalid. Send the user back to the view to try again.
                return View(cvm);
            }

            //Build the message
            string message = $"From: {cvm.Name}<br />Subject: {cvm.Subject}<br />From Email: {cvm.Email}<br /><br />{cvm.Message}";

            //MailMessage - what sends the email
            //Arguments for this method were defined in the Web.config at the project level.
            MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailTo"].ToString(), cvm.Subject, message);

            //MailMessage Properties
            //Allow HTML formatting
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;

            //Respond to the sender and not the address the message was sent from
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client credentials
            client.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString()
                );

            //Try to send the email (POTENTIALLY DANGEROUS)
            try
            {
                //Attempt to send email
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, your request could not be completed. Please try again later.<br /><br />" +
                    $"Error Message:<br /> {ex.StackTrace}";
                return View(cvm);
            }

            //Email was sent successfully, route the user to a confirmation view.
            return View("EmailConfirmation", cvm);
        }

        public ActionResult Projects()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }
    }
}
using BiomedicClinic.Core;
using BiomedicClinic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BiomedicClinic.Controllers
{
    [RoutePrefix("Leads")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public string SendMessage(FormCollection collection)
        {
            var model = new Lead();
            model.FirstName = collection["FirstName"];
            model.LastName = collection["LastName"];
            model.PhoneNumber = collection["PhoneNumber"];
            model.EmailAddress = collection["EmailAddress"];
            model.Note = collection["Note"];
            model.Date = DateTime.Now;

            _unitOfWork.Leads.AddNewLead(model);
            _unitOfWork.Complete();

            string HostMail = "postmaster@struix.co";
            string HostPassword = "StruiXTeaM12#$";
            string DisplayName = "Biomedic Clinic Notifications";
            MailMessage eMailMessage = new MailMessage();
            eMailMessage.To.Add("inquiry@biomedic.co.uk");
            eMailMessage.From = new MailAddress(HostMail, DisplayName);

            string subject = "New Lead - biomedic.co.uk";
            string body = "<p>Name: <b>" + model.FirstName + " " + model.LastName + "</b></p><p>Email Address: <b>" + model.EmailAddress + "</b></p><p>Phone Number: <b>" + model.PhoneNumber + "</b></p><p>Note: <b>" + model.Note + "</b></p>";

            eMailMessage.Subject = subject;
            eMailMessage.Body = body;
            eMailMessage.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.EnableSsl = false;
                smtp.Host = "mail.struix.co";
                smtp.Port = 8889;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(HostMail, HostPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                smtp.Send(eMailMessage);
            };

            string message = "<div class='col-12 py-5'><p class='text-white text-center my-5'>Thank you for your message! We'll get back to you as soon as possible.</p></div>";

            return message;
        }

        [HttpPost]
        public string Subscribe(FormCollection collection)
        {
            var model = new Lead();
            model.EmailAddress = collection["EmailAddress2"];
            model.Note = collection["Note2"];
            model.Date = DateTime.Now;

            _unitOfWork.Leads.AddNewLead(model);
            _unitOfWork.Complete();

            string HostMail = "postmaster@struix.co";
            string HostPassword = "StruiXTeaM12#$";
            string DisplayName = "Biomedic Clinic Notifications";
            MailMessage eMailMessage = new MailMessage();
            eMailMessage.To.Add("inquiry@biomedic.co.uk");
            eMailMessage.From = new MailAddress(HostMail, DisplayName);

            string subject = "New Newsletter Subscription - biomedic.co.uk";
            string body = "<p>Email Address: <b>" + model.EmailAddress + "</b></p>";

            eMailMessage.Subject = subject;
            eMailMessage.Body = body;
            eMailMessage.IsBodyHtml = true;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.EnableSsl = false;
                smtp.Host = "mail.struix.co";
                smtp.Port = 8889;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(HostMail, HostPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                smtp.Send(eMailMessage);
            };

            string message = "<div class='col-12 py-5'><p class='text-white text-center my-5'>Thank you for your message! We'll get back to you as soon as possible.</p></div>";

            return message;
        }
    }
}
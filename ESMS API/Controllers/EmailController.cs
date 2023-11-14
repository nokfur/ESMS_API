﻿using Business.Services.EmailService;
using ESMS_Data.Entities.EmailModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESMS_API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [Authorize(Roles = "Testing Admin")]
        [HttpPost]
        public async Task<IActionResult> SendMailToLecturerWhenDeleteSchedule(int idt, string subjectId, string room)
        {
            try
            {
                var examTime = await _emailService.GetExamTimeToInform(idt);

                MailRequest mailRequest = new MailRequest();
                mailRequest.Subject = "Changing in the exam schedule";
                mailRequest.Body = $"Your registration in {examTime.Date.ToString("dd/MM/yyyy")} ({examTime.Start.ToString(@"hh\:mm")} - {examTime.End.ToString(@"hh\:mm")}) is cancelled. Please view the web again for newest information";
                    
                await _emailService.SendEmailToProctorWhenDeleteSchedule(mailRequest, idt, subjectId, room);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
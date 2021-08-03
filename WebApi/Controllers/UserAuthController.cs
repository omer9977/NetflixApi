using Business.IServices;
using Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Common;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAuthController : ControllerBase
    {
        IUserAuthService _userAuthService;
        IMailService _mailService;

        public UserAuthController(IUserAuthService userAuthService, IMailService mailService)
        {
            _userAuthService = userAuthService;
            _mailService = mailService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserInfo oUser)
        {
            string sMessage = "";
            var user = await _userAuthService.SignUp(oUser);
            if (user == null) return BadRequest(new { message = Message.ErrorFound });
            if (user.Message == Message.VerifyMail)
            {
                MailClass oMailClass = _mailService.GetMailObject(user);
                await _mailService.SendMail(oMailClass);
                return BadRequest(new { message = Message.VerifyMail });
            }
            #region Send Confirmation Mail
            if (user.Message == Message.Success)
            {
                MailClass oMailClass = _mailService.GetMailObject(user);
                sMessage = await _mailService.SendMail(oMailClass);
            }
            if (sMessage != Message.MailSent) return BadRequest(new { message = sMessage });
            else return Ok(new { message = Message.UserCreatedVerifyMail });
            #endregion
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmMail(string Id)
        {
            string sMessage = await _userAuthService.ConfirmEmail(Id);
            return Ok(new { message = sMessage });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LostPassword([FromBody] UserInfo oUser)
        {
            string sMessage = "";
            var user = await _userAuthService.LostPassword(oUser);
            if (user == null) return BadRequest(new { message = Message.ErrorFound });
            if (user.Message == Message.VerifyMail)
            {
                MailClass oMailClass = _mailService.GetMailObject(user);
                await _mailService.SendMail(oMailClass);
                return BadRequest(new { message = Message.VerifyMail });
            }
            #region Send Confirmation Mail
            if (user.Message == Message.Success)
            {
                MailClass oMailClass = _mailService.GetMailObjectForLostPassword(user);
                sMessage = await _mailService.SendMail(oMailClass);
            }
            if (sMessage != Message.MailSent) return BadRequest(new { message = sMessage });
            else return Ok(new { message = Message.UserCreatedVerifyMail });
            #endregion
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreatePassword([FromBody] UserInfo userIdPassword/*burası aslında userId olacak ileride*/)
        {
            string sMessage = await _userAuthService.CreatePassword(userIdPassword);
            return Ok(new { message = sMessage });
        }
    }
}

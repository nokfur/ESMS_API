﻿using Business.Services.AuthService;
using ESMS_Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ESMS_API.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserModel userModel)
        {
            var res = await _authService.Authenticate(userModel);

            if (res.Data != null && res.StatusCode == 200)
            {
                var user = (UserModel) res.Data;
                res = _authService.GenerateToken(user);
            }

            return res.IsSuccess ? Ok(res) : BadRequest(res);
        }        
    }
}

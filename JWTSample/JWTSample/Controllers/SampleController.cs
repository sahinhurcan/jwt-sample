﻿using JWTSample.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTSample.Controllers
{
    //Authorize attribute ile bu sınıfı sadece yetkisi yani tokenı olan kişilerin girmesini söylüyorum.
    [Authorize]
    [ApiController]
    //Routing için mesela /Sample/GetSummaries olarak ayarladım.
    [Route("[controller]/[action]")]
    public class SampleController : ControllerBase
    {
        private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        private readonly IUserService _userService;
        public SampleController(IUserService userService) => _userService = userService;

        //Burada da AllowAnonymous attribute nü kullanarak bu seferde bu metoda herkesin erişebileceğini söylüyorum.
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] string username, string password)
        {
            var user = _userService.Authenticate(username, password);

            if (user == null)
                return BadRequest("Username or password incorrect!");

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetSummaries() => Ok(Summaries);
    }
}

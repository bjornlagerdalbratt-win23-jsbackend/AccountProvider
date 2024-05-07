using AccountProvider.Models;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace AccountProvider.Functions
{
    public class SignIn(ILogger<SignIn> logger, SignInManager<UserAccount> signInManager)
    {
        private readonly ILogger<SignIn> _logger = logger;
        private readonly SignInManager<UserAccount> _signInManager = signInManager;

        [Function("SignIn")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            string body = null!;

            try
            {
                body = await new StreamReader(req.Body).ReadToEndAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"StreamReader :: {ex.Message}");
            }

            if ( body != null)
            {
                UserSigninRequest usr = null!;

                try
                {
                    usr = JsonConvert.DeserializeObject<UserSigninRequest>(body)!;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"JsonConvert.DeserializeObject<UserSigninRequest> :: {ex.Message}");
                }
                if (usr != null && !string.IsNullOrEmpty(usr.Email) && !string.IsNullOrEmpty(usr.Password))
                {
                    try
                    {
                        var result = await _signInManager.PasswordSignInAsync(usr.Email, usr.Password, usr.RememberMe, false);
                        if (result.Succeeded)
                        {
                            //get token from tokenProvider

                            return new OkObjectResult("accesstoken");
                        }

                        return new UnauthorizedResult();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"signInManager.PasswordSignInAsync :: {ex.Message}");
                    }
                }
            }

            return new BadRequestResult();

        }
    }
}

using CoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CoreIdentity.Infstracter
{
    public class CostomUserValidator : IUserValidator<AplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AplicationUser> manager, AplicationUser user)
        {
            List<IdentityError> erorors = new List<IdentityError>();


            if (user.Email.ToLower().EndsWith("@gmail.com") || user.Email.ToLower().EndsWith("@hotmail.com"))
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {


              return  Task.FromResult(IdentityResult.Failed(new IdentityError()  
                {

                   Code = "EmailEroror",
                    Description = "gmail or hotmail use email adress"



                }));


            }








        }








    }
}

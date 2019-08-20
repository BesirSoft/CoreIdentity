using CoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Infstracter
{
    public class CustomPasswordValidetor : IPasswordValidator<AplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AplicationUser> manager, AplicationUser user, string password)
        {
            List<IdentityError> erorors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                erorors.Add(new IdentityError()
                {

                    Code = "PaswordContainsUserName",
                    Description = "pasword cannot user name"



                });
            }

            if (password.Contains("123"))
            {

                erorors.Add(new IdentityError()
                {

                    Code = "paswordcontainsequnce",
                    Description = "pasword cannot user name"



                });

            }


            return Task.FromResult(erorors.Count() == 0 ?

                IdentityResult.Success : IdentityResult.Failed(erorors.ToArray()));






        }
    }
}

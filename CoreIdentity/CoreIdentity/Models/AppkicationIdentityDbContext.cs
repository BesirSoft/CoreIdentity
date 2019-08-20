using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Models
{
    public class AppkicationIdentityDbContext:IdentityDbContext<AplicationUser>
    {


        public AppkicationIdentityDbContext(DbContextOptions<AppkicationIdentityDbContext> options):base(options)
        {

        }

    }
}

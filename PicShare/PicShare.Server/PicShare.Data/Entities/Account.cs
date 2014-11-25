using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PicShare.Data.Entities
{
    public class Account : IdentityUser
    {
        public DateTime JoinedOn { get; set; }

        public DateTime LastSignedInOn { get; set; }

        public virtual ICollection<Picture>  Pictures { get; set; }

    }
}

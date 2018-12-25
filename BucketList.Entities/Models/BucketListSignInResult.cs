using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Entities.Models
{
    public class BucketListSignInResult
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
    }
}
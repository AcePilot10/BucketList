using System.Collections.Generic;

namespace BucketList.Entities.Models
{
    public class BucketListSignInResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
}
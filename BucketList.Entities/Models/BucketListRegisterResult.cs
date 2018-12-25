using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Entities.Models
{
    public class BucketListRegisterResult
    {
        public class Error
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }

        public bool Succeeded { get; set; }
        public List<Error> Errors { get; set; }
    }
}
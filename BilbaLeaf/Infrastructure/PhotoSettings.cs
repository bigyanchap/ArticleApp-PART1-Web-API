using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicLync.Api.Infrastructure
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }
    }

    public class VideoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }
    }

    public class AuthenticationSettings
    {
        public string ClientId { get; set; }
        public string Issuer { get; set; }
    }
}

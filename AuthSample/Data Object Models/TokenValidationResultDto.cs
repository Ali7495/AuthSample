﻿using AuthSample.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthSample.Data_Object_Models
{
    public class TokenValidationResultDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Session { get; set; }
        public DateTime SessionExpirationDate { get; set; }
        public ValidationStatus Status { get; set; }
        public string Message { get; set; }
    }
}

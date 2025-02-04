﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AccountDomain.DTOs
{
    public class UpdateProfileWebRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        
        public int? Pin { get; set; }
    }
}

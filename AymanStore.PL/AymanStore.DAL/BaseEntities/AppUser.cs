using AymanStore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AymanStore.DAL.BaseEntity
{
    public class AppUser : IdentityUser
    {
        public int? CountryTBLId { get; set; }
        public int? GenderTBLId { get; set; }

        public virtual CountryTBL? CountryTBL { get; set; }
        public virtual GenderTBL? GenderTBL { get; set; }


        public DateTime DateOfJoin { get; set; } = DateTime.Now;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? ActivationCode { get; set; } = null!;

        public bool IsActivated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

    }
}

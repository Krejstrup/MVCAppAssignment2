﻿using System.ComponentModel.DataAnnotations;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class CreatePerson   // A ViewModel for passing data for one person
    {
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string FirstName { get; set; }
        [StringLength(30, MinimumLength = 4)]
        public string LastName { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(12, MinimumLength = 4)]
        public string City { get; set; }



    }
}

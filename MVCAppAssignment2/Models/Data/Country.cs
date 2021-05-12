﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCAppAssignment2.Models.Data
{
    public class Country
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }


        public int CityId { get; set; }


        [ForeignKey("CityId")]
        public List<City> Cities { get; set; }  // Many
    }
}

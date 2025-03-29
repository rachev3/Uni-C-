﻿using AirCompanySystem.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirCompanySystem.Models
{
    public class City : BaseModel
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public Country? Country { get; set; } = null!;
        public ICollection<Airport> Airports { get; set; } = new List<Airport>();
    }
}

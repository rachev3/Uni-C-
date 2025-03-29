﻿using AirCompanySystem.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirCompanySystem.Models
{
    public class Flight : BaseModel
    {
        public int FlightDuration { get; set; }
        public DateTimeOffset FlightDate { get; set; }
        public ushort PassengerCount { get; set; }

        public ICollection<Crew> Crews { get; set; } = new List<Crew>();
        public ICollection<FlightStatusChange> FlightStatusChanges { get; set; } = new List<FlightStatusChange>();
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
        public int? AirplaneId { get; set; }
        public Airplane? Airplane { get; set; }
    }
}

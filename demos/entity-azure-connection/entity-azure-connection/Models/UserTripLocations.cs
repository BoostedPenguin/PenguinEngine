﻿using System;
using System.Collections.Generic;

namespace entity_azure_connection.Models
{
    public partial class UserTripLocations
    {
        public int LocationId { get; set; }
        public int TripId { get; set; }
        public string Name { get; set; }
        public double Lang { get; set; }
        public double Long { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual UserTrips Trip { get; set; }
    }
}

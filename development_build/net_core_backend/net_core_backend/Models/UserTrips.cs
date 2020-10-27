﻿using System;
using System.Collections.Generic;

namespace net_core_backend.Models
{
    public partial class UserTrips : DefaultModel
    {
        public UserTrips()
        {
            UserTripLocations = new HashSet<UserTripLocations>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public Transportation Transportation { get; set; }
        public int? Distance { get; set; }
        public double? Duration { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<UserTripLocations> UserTripLocations { get; set; }
    }
}

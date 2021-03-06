﻿using net_core_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_backend.ViewModel
{
    public class LocationsViewModel
    {
        public int Id { get; set; }
        public int? WishlistId { get; set; }
        public int? TripId { get; set; }
        public string Name { get; set; }
        public string PlaceId { get; set; }
        public double Lang { get; set; }
        public double Long { get; set; }
        public string Origin_Destination { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

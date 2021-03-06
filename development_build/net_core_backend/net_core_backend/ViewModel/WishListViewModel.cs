﻿using net_core_backend.ViewModel;
using System;
using System.Collections.Generic;

namespace net_core_backend.Models
{
    public partial class WishListViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Transportation { get; set; }
        public int? Distance { get; set; }
        public int? Duration { get; set; }
        public ICollection<LocationsViewModel> Locations { get; set; }
    }
}

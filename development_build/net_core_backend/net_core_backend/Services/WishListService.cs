﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using net_core_backend.Context;
using net_core_backend.Models;
using net_core_backend.Services.Extensions;
using net_core_backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_backend.Services
{
    public class WishListService : DataService<WishList>, IWishListService
    {
        private readonly IContextFactory contextFactory;
        private readonly IHttpContextAccessor httpContext;

        public WishListService(IContextFactory _contextFactory, IHttpContextAccessor httpContextAccessor) : base(_contextFactory)
        {
            contextFactory = _contextFactory;
            httpContext = httpContextAccessor;
        }

        public async Task<Locations> AddLocation(Locations location)
        {
            if (await CurrentExtensions.RestrictAdministratorResource(contextFactory, httpContext))
            {
                throw new ArgumentException("Administrators cannot intact with their wishlist!");
            }

            using (var a = contextFactory.CreateDbContext())
            {
                var wishList = await a.WishList.Where(x => x.User.Auth == httpContext.GetCurrentAuth()).FirstOrDefaultAsync();

                if (wishList == null) throw new ArgumentException("Something went wrong! This user doesn't have a wishlist!");

                //Assign values to make a unique location
                location.WishlistId = wishList.Id;
                location.TripId = null;


                wishList.Locations.Add(location);

                await a.SaveChangesAsync();

                return location;
            }
        }

        public async Task<WishList> ClearWishlist()
        {
            if (await CurrentExtensions.RestrictAdministratorResource(contextFactory, httpContext))
            {
                throw new ArgumentException("Administrators cannot intact with their wishlist!");
            }

            using (var a = contextFactory.CreateDbContext())
            {
                var wishList = await a.WishList.Where(x => x.User.Auth == httpContext.GetCurrentAuth()).FirstOrDefaultAsync();

                if (wishList == null) throw new ArgumentException("Something went wrong! This user doesn't have a wishlist!");

                var locations = await a.Locations.Where(x => x.TripId == null && x.WishlistId == wishList.Id).ToListAsync();

                a.RemoveRange(locations);

                return wishList;
            }
        }

        public async Task<UserTrips> CreateTrip()
        {
            if (await CurrentExtensions.RestrictAdministratorResource(contextFactory, httpContext))
            {
                throw new ArgumentException("Administrators cannot intact with their wishlist!");
            }

            using (var a = contextFactory.CreateDbContext())
            {
                var wishList = await a.WishList.Where(x => x.User.Auth == httpContext.GetCurrentAuth()).FirstOrDefaultAsync();

                if (wishList == null) throw new ArgumentException("Something went wrong! This user doesn't have a wishlist!");

                var locations = await a.Locations.Where(x => x.TripId == null && x.WishlistId == wishList.Id).ToListAsync();

                var trip = new UserTrips() { Distance = wishList.Distance, Duration = wishList.Duration, Transportation = wishList.Transportation, Name = wishList.Name, UserId = wishList.UserId };
                foreach (var l in locations)
                {
                    l.TripId = trip.Id;
                    l.WishlistId = null;
                }

                await a.AddAsync(trip);
                await a.AddRangeAsync(locations);

                return trip;
            }
        }

        public async Task<WishList> GetWishlist()
        {
            if (await CurrentExtensions.RestrictAdministratorResource(contextFactory, httpContext))
            {
                throw new ArgumentException("Administrators cannot interact with their own wishlist!");
            }

            using (var a = contextFactory.CreateDbContext())
            {
                return await a.WishList
                    .Include(x => x.Locations)
                    .Where(x => x.User.Auth == httpContext.GetCurrentAuth())
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Locations> RemoveLocation(int trip_id, int location_id)
        {
            if (await CurrentExtensions.RestrictAdministratorResource(contextFactory, httpContext))
            {
                throw new ArgumentException("Administrators cannot create tickets!");
            }

            using (var a = contextFactory.CreateDbContext())
            {
                var wishList = await a.WishList.Where(x => x.Id == trip_id).Where(x => x.User.Auth == httpContext.GetCurrentAuth()).FirstOrDefaultAsync();

                if (wishList == null) throw new ArgumentException("Something went wrong! This user doesn't have a wishlist!");

                var location = await a.Locations.Where(x => x.Id == location_id && x.WishlistId == trip_id && x.TripId == null).FirstOrDefaultAsync();

                a.Remove(location);

                return location;
            }
        }
    }
}
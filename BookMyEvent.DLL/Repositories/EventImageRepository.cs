﻿using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class EventImageRepository:IEventImageRepository
    {
       private readonly EventManagementSystemTeamZealContext _context;

        public EventImageRepository(EventManagementSystemTeamZealContext context)
        {
            _context = context;
        }

        public async Task<List<EventImage>> GetImagesByEventId(Guid eventId)
        {
            try
            {
                List<EventImage> eventImages = await _context.EventImages.Where(i => i.EventId.Equals(eventId)).ToListAsync();
                return eventImages;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddMultipleImages(List<EventImage> eventImages)
        {
            try
            {
                
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                await _context.EventImages.AddRangeAsync(eventImages);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
               
                return false;
            }
        }

        public async Task<Byte[]> GetProfileImageByEventId(Guid eventId)
        {
            try
            {
                EventImage eventProfileImage = await _context.EventImages.Where(i => i.EventId.Equals(eventId) && i.ImgType.Equals("profile")).FirstOrDefaultAsync();
                return eventProfileImage.ImgBody;
            }
            catch
            {
                return null;
            }
        }


    }
}

using AutoMapper;
using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
using BookMyEvent.DLL.Repositories;
using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IEventCategoryRepository _eventCategoryRepository;
        private Mapper _mapper;
        public CategoryServices(IEventCategoryRepository eventCategoryRepository)
        {
            _eventCategoryRepository = eventCategoryRepository;
            _mapper = Automapper.InitializeAutomapper();
        }

        public async Task<BLEventCategory> AddEventCategory(BLEventCategory category)
        {
            try
            {

                var response = await _eventCategoryRepository.AddEventCategory(_mapper.Map<EventCategory>(category));
                return _mapper.Map<BLEventCategory>(response.eventCategory);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BLEventCategory>> GetAllEventCategories()
        {
            try
            {
                var categories =  await _eventCategoryRepository.GetAllEventCategories();
                var BLcategories = new List<BLEventCategory>();
                foreach(var category in categories)
                {
                    BLcategories.Add(new BLEventCategory()
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                    });
                }
                return BLcategories;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLEventCategory> UpdateEventCategory(BLEventCategory category)
        {
            try
            {
                var response = await _eventCategoryRepository.UpdateEventCategory(_mapper.Map<EventCategory>(category));
                return _mapper.Map<BLEventCategory>(response.category);
            }
            catch
            {
                return null;
            }
        }
    }
}

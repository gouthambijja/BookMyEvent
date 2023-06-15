using BookMyEvent.BLL.Contracts;
using BookMyEvent.BLL.Models;
using BookMyEvent.DLL.Contracts;
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
        public CategoryServices(IEventCategoryRepository eventCategoryRepository)
        {
            _eventCategoryRepository = eventCategoryRepository;
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
    }
}

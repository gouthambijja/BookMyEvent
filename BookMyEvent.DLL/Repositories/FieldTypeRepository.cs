using BookMyEvent.DLL.Contracts;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class FieldTypeRepository : IFieldTypeRepository
    {
        EventManagementSystemTeamZealContext context;
        public FieldTypeRepository(EventManagementSystemTeamZealContext context)
        {
            this.context = context;
        }
        public async Task<List<FieldType>> GetAllFieldTypes()
        {
            try
            {
                var formFields =  await context.FieldTypes.ToListAsync();
                return formFields;
            }
            catch
            {
                return null;
            }
        }
        public async Task<FieldType> GetFieldTypeById(Guid FieldTypeId)
        {
            if (!FieldTypeId.Equals(string.Empty))
            {
                return await context.FieldTypes.FirstOrDefaultAsync(e => e.FieldTypeId.Equals(FieldTypeId));
            }
            return null;
        }
    }
}

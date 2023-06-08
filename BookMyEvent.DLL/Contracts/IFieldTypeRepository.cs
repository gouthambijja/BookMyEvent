using db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    // This is for performing Operations on FieldType table//
    public interface IFieldTypeRepository
    {
        //This Method is used to Get all the Fieldtypes present in the table//
        public Task<List<FieldType>> GetAllFieldTypes();
        /// <summary>
        /// this method is used for returning single fieldtype by taking id as input
        /// </summary>
        /// <param name="FieldTypeId"></param>
        /// <returns>FieldType Object</returns>
        public Task<FieldType> GetFieldTypeById(Guid FieldTypeId);
    }
}

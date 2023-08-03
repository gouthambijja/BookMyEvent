using BookMyEvent.DLL.Contracts;
using BookMyEvent.DLL.Models;
using db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Repositories
{
    public class FileTypeRepository : IFileTypeRepository
    {
        private readonly EventManagementSystemTeamZealContext DBContext;
        public FileTypeRepository(EventManagementSystemTeamZealContext dBContext)
        {
            this.DBContext = dBContext;
        }
        public async Task<List<FileType>> GetAllFileTypes()
        {
            return await DBContext.FileTypes.ToListAsync();
        }
    }
}

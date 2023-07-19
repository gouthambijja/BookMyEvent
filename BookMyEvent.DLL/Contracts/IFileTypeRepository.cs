using BookMyEvent.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.DLL.Contracts
{
    public interface IFileTypeRepository
    {
        Task<List<FileType>> GetAllFileTypes();
    }
}

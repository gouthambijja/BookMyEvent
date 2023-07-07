using BookMyEvent.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.RequestModels
{
    public class AddTransaction
    {
        public BLTransaction? transaction { get; set; }
        public List<BLUserInputForm?>? ListOfUserInputForm { get; set; }
    }
}

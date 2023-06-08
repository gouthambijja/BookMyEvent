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
    public class UserInputFormService
    {
        private readonly IUserInputFormRepository _userInputForm;
        private readonly IUserInputFormFieldsRepository _userInputFormField;

        public UserInputFormService(IUserInputFormRepository userInputForm,IUserInputFormFieldsRepository userInputFormField)
        {
            _userInputForm = userInputForm;
            _userInputFormField = userInputFormField;
        }

       
    }
}

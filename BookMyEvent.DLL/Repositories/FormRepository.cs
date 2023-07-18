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

    public class FormRepository : IFormRepository
    {

        private readonly EventManagementSystemTeamZealContext _DBContext;
        public FormRepository(EventManagementSystemTeamZealContext dBContext) { _DBContext = dBContext; }

        public async Task<Form> Add(Form form)
        {
            try
            {
                
               await _DBContext.Forms.AddAsync(form);
                await _DBContext.SaveChangesAsync();
                return form;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Delete(Guid FormId)
        {
            try
            {
                var _Forms = await _DBContext.Forms.
                     Where(e => e.FormId == FormId).ToListAsync();
                if (_Forms.Count == 0) return false;
                var _Form = _Forms[0];
                _Form.IsActive = false;
                await _DBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Form> Get(Guid FormId)
        {
            try
            {
                var form = await _DBContext.Forms.Where(e => e.FormId == FormId && e.IsActive == true).FirstAsync();
                return form;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Form>> GetByOrganisationId(Guid OrganisationId)
        {
            try
            {
                return await _DBContext.Forms.Where(e => e.OrganisationId == OrganisationId).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Form>> GetByCreatorId(Guid CreatorId)
        {
            try
            {
                return await _DBContext.Forms.Where(e => e.CreatedBy == e.CreatedBy).ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Form> Update(Form Form)
        {
            try
            {
                var _Form = await _DBContext.Forms.
                Where(e => e.FormId == Form.FormId).FirstOrDefaultAsync();
                _Form.FormName = Form.FormName;
                _Form.CreatedBy = Form.CreatedBy;
                await _DBContext.SaveChangesAsync();
                return _Form;
            }
            catch
            {
                return null;
            }
        }

        public async Task<(bool IsActiveToggled, string Message)> UpdateIsActive(Guid formId)
        {
            try
            {
                var form = await  _DBContext.Forms.Where(e => e.FormId == formId).FirstOrDefaultAsync();
                if (form == null) return await Task.FromResult((false, "Form not found"));
                form.IsActive = !form.IsActive;
               await _DBContext.SaveChangesAsync();
                return await Task.FromResult((true, "Form updated"));
            }
            catch (Exception ex)
            {
                return await Task.FromResult((false, ex.Message));
            }
        }

        public async Task<bool> IsformNameTaken(string formName)
        {
            try
            {
               var form = await _DBContext.Forms.Where(e => e.FormName == formName).FirstOrDefaultAsync();
                if( form != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }
    }
}

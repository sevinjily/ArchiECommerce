using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public void Create(List<AddCategoryDTO> models)
        {
            _categoryDAL.CreateCategory(models); 
        }

        public void Delete(Guid id)
        {
            _categoryDAL.DeleteCategory(id);
        }

        public List<GetCategoryDTO> Get(Guid id)
        {
           var result= _categoryDAL.GetAllLanguages(id);
            return result;

        }

        public GetCategoryDTO GetByLang(Guid id, string LangCode)
        {
           var result=_categoryDAL.GetCategoryByLang(id, LangCode);
            return result;

        }

        public async Task Update(Guid id, List<UpdateCategoryDTO> models)
        {
           await _categoryDAL.UpdateCategoryAsync(id, models);
        }

        public async Task UpdateByLang(Guid id, string lang, UpdateCategoryDTO model)
        {
          await _categoryDAL.UpdateCategoryByLangAsync(id, lang, model);
        }
    }
}

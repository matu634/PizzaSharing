using System;
using DAL.App.DTO;
using Domain;

namespace DAL.App.EF.Mappers
{
    public class CategoryMapper
    {
        public static DALCategoryDTO FromDomain(Category category)
        {
            throw new NotImplementedException();
        }
        
        public static DALCategoryMinDTO FromDomainToMin(Category category)
        {
            return new DALCategoryMinDTO(category.Id, category.CategoryName){};
        }
        
        public static Category FromDAL(DALCategoryDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
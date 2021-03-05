using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, KlinzCarzContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (KlinzCarzContext context = new KlinzCarzContext())
            {
                var result = from ca in context.Cars
                             join b in context.Brands
                             on ca.BrandId equals b.BrandId
                             join co in context.Colors
                             on ca.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 CarName = ca.Description, BrandName = b.BrandName, ModelYear= ca.ModelYear,
                                 ColorName = co.ColorName, DailyPrice = ca.DailyPrice
                             };
                return result.ToList();
            }
        }
    }
}

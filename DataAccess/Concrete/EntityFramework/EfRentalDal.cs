using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, KlinzCarzContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (KlinzCarzContext context = new KlinzCarzContext())
            {
                var result = from r in context.Rentals
                             join ca in context.Cars
                             on r.CarId equals ca.CarId
                             join b in context.Brands
                             on ca.BrandId equals b.BrandId
                             join cu in context.Customers
                             on r.CustomerId equals cu.CustomerId
                             select new RentalDetailDto
                             {
                                 Id = r.Id,
                                 BrandName = b.BrandName,
                                 CustomerName = cu.CompanyName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}

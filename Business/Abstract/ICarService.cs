using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        void Add(Car car);
        void Delete(Car car);
        List<Car> GetAll();
        Car GetById(int CarId);
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);
        List<CarDetailDto> GetCarDetails();
        void Update(Car car);
    }
}

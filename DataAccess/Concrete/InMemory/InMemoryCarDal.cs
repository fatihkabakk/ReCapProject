using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{CarId=1, BrandId=1, ColorId=1, ModelYear=2015, DailyPrice=220, Description="Opel Corsa 1.2 Benzinli"},
                new Car{CarId=2, BrandId=1, ColorId=3, ModelYear=2018, DailyPrice=290, Description="Opel Astra 1.6 Dizel"},
                new Car{CarId=3, BrandId=2, ColorId=2, ModelYear=2019, DailyPrice=300, Description="Renault Symbol 1.5 Dizel"},
                new Car{CarId=4, BrandId=3, ColorId=4, ModelYear=2016, DailyPrice=260, Description="Dacia Duster 1.6 LPG'li"},
                new Car{CarId=5, BrandId=4, ColorId=4, ModelYear=2017, DailyPrice=340, Description="Audi A3 Sportback 1.6 Dizel"}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=>c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int carId)
        {
            return _cars.Find(c => c.CarId == carId);
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
        }
    }
}

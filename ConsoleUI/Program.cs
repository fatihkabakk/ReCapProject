using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main()
        {
            ICarService carManager = new CarManager(new InMemoryCarDal());
            Console.WriteLine("***************First Part***************");

            List<Car> cars1 = carManager.GetById(2);
            PrintCarInfo(cars1);
            carManager.Delete(cars1[0]);
            List<Car> cars2 = carManager.GetById(2);
            Console.WriteLine("Liste boyutu: " + cars2.ToArray().Length);
            PrintCarInfo(cars2);


            Console.WriteLine("\n***************Second Part***************");
            List<Car> allCars = carManager.GetAll();
            PrintCarInfo(allCars);


            Console.WriteLine("\n***************Third Part***************");
            Car newCar = new Car { CarId = 6, BrandId = 5, ColorId = 2, ModelYear = 2016, DailyPrice = 400, Description = "BMW 3.20i 1.6 Benzinli" };
            carManager.Add(newCar);
            List<Car> cars3 = carManager.GetById(6);
            PrintCarInfo(cars3);


            Console.WriteLine("\n***************Fourth Part***************");
            Car carToUpdate = carManager.GetById(5)[0];
            Console.WriteLine("Güncellenmemiş Fiyat: " + carToUpdate.DailyPrice);
            Car toUpdate = new Car{ CarId = 5, BrandId = 4, ColorId = 4, ModelYear = 2017, DailyPrice = 370, Description = "Audi A3 Sportback 1.6 Dizel"};
            carManager.Update(toUpdate);
            Car carAfterUpdate = carManager.GetById(5)[0];
            Console.WriteLine("Güncellenmiş Fiyat: " + carAfterUpdate.DailyPrice);

            Console.ReadKey();
        }

        private static void PrintCarInfo(List<Car> cars)
        {
            foreach (Car car in cars)
            {
                Console.WriteLine("Car Id: " + car.CarId + "\nBrand Id: " + car.BrandId + "\nColor Id: " + car.ColorId + "\nModel Year: " + car.ModelYear + "\nDaily Price: " + car.DailyPrice + "\nDescription: " + car.Description);
                Console.WriteLine("****************************************");
            }
        }
    }
}

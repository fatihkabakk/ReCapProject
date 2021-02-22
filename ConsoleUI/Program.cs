using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
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
            ICarService carManager = new CarManager(new EfCarDal());

            List<Car> carsByBrand = carManager.GetCarsByBrandId(1);

            foreach (Car car in carsByBrand)
            {
                PrintCarInfo(car);
            }

            List<Car> carsByColor = carManager.GetCarsByColorId(4);

            foreach (Car car in carsByColor)
            {
                PrintCarInfo(car);
            }

            //This Is For Testing The Add Rules.
            //AddRulesTest(carManager);

            //Creates The Database Objects -- Just Run Once.
            //DbAddTest(carManager);

            //FirstTest(carManager);

            //SecondTest(carManager);

            //ThirdTest(carManager);

            //FourthTest(carManager);

            Console.ReadKey();
        }

        private static void AddRulesTest(ICarService carManager)
        {
            //Successfull
            carManager.Add(new Car { BrandId = 3, ColorId = 2, DailyPrice = 250, ModelYear = 2015, Description = "Dacia Logan 1.5 Dizel" });

            //Unsuccessfull -- Just For Testing
            carManager.Add(new Car { BrandId = 1, ColorId = 2, DailyPrice = 0, ModelYear = 2019, Description = "a" });
        }

        private static void DbAddTest(ICarService carManager)
        {
            List<Car> _carsToAdd = new List<Car>
            {
                new Car{BrandId=1, ColorId=1, ModelYear=2015, DailyPrice=220, Description="Opel Corsa 1.2 Benzinli"},
                new Car{BrandId=1, ColorId=3, ModelYear=2018, DailyPrice=290, Description="Opel Astra 1.6 Dizel"},
                new Car{BrandId=2, ColorId=2, ModelYear=2019, DailyPrice=300, Description="Renault Symbol 1.5 Dizel"},
                new Car{BrandId=3, ColorId=4, ModelYear=2016, DailyPrice=270, Description="Dacia Duster 1.6 LPG'li"},
                new Car{BrandId=4, ColorId=4, ModelYear=2017, DailyPrice=340, Description="Audi A3 Sportback 1.6 Dizel"}
            };
            foreach (Car car in _carsToAdd)
            {
                carManager.Add(car);
            }
        }

        private static void FourthTest(ICarService carManager)
        {
            Console.WriteLine("\n***************Fourth Part***************");
            Car carToUpdate = carManager.GetById(5);
            Console.WriteLine("Araç: " + carToUpdate.Description + "\nEski Fiyat: " + carToUpdate.DailyPrice);
            Car toUpdate = new Car { BrandId = 4, ColorId = 4, ModelYear = 2017, DailyPrice = 370, Description = "Audi A3 Sportback 1.6 Dizel" };
            carManager.Update(toUpdate);
            Car carAfterUpdate = carManager.GetById(5);
            Console.WriteLine("Güncel Fiyat: " + carAfterUpdate.DailyPrice);
        }

        private static void ThirdTest(ICarService carManager)
        {
            Console.WriteLine("\n***************Third Part***************");
            Car newCar = new Car { BrandId = 5, ColorId = 2, ModelYear = 2016, DailyPrice = 400, Description = "BMW 3.20i 1.6 Benzinli" };
            carManager.Add(newCar);
            Car car3 = carManager.GetById(6);
            PrintCarInfo(car3);
        }

        private static void SecondTest(ICarService carManager)
        {
            Console.WriteLine("\n***************Second Part***************");
            List<Car> allCars = carManager.GetAll();
            foreach (var car in allCars)
            {
                PrintCarInfo(car);
            }
        }

        private static void FirstTest(ICarService carManager)
        {
            Console.WriteLine("***************First Part***************");

            Car car1 = carManager.GetById(2);
            PrintCarInfo(car1);
            carManager.Delete(car1);
            Car car2 = carManager.GetById(2);
            if (car2 != null)
            {
                Console.WriteLine("Silme Testi Başarısız");
            }
            else
            {
                Console.WriteLine("Silme Testi Başarılı");
            }
            //car2 = null
            //PrintCarInfo(car2); <-- Throws An Exception
        }

        private static void PrintCarInfo(Car car)
        {
            Console.WriteLine("Car Id: " + car.CarId + "\nBrand Id: " + car.BrandId + "\nColor Id: " + car.ColorId + "\nModel Year: " + car.ModelYear + "\nDaily Price: " + car.DailyPrice + "\nDescription: " + car.Description);
            Console.WriteLine("****************************************");
        }
    }
}

using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage carImage, IFormFile formFile);
        IResult Delete(CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int carImageId);
        IDataResult<List<CarImage>> GetImagesByCarId(int carId);
        IResult Update(CarImage carImage, IFormFile formFile);
    }
}

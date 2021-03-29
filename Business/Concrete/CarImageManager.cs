using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("carimage.add,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImage carImage, IFormFile formFile)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceeded(carImage));

            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.AddAsync(formFile);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("carimage.delete,admin")]
        public IResult Delete(CarImage carImage)
        {
            CarImage imageToDelete = _carImageDal.Get(ci => ci.Id == carImage.Id);
            var oldPath = $@"{Environment.CurrentDirectory}\wwwroot{imageToDelete.ImagePath}";
            FileHelper.DeleteAsync(oldPath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<CarImage> GetById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.Id == carImageId));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (!result.Any())
            {
                List<CarImage> carImages = new List<CarImage>() { new CarImage { CarId = carId, ImagePath = @"\Images\klinz.jpg" } };
                return new SuccessDataResult<List<CarImage>>(carImages);
            }
            return new SuccessDataResult<List<CarImage>>(result);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("carimage.update,admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage, IFormFile formFile)
        {
            CarImage imageToUpdate = _carImageDal.Get(ci => ci.Id == carImage.Id);
            var oldPath = $@"{Environment.CurrentDirectory}\wwwroot{imageToUpdate.ImagePath}";
            carImage.ImagePath = FileHelper.UpdateAsync(oldPath, formFile);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckIfImageLimitExceeded(CarImage carImage)
        {
            List<CarImage> carImages = _carImageDal.GetAll(ci => ci.CarId == carImage.CarId);
            if (carImages.Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}

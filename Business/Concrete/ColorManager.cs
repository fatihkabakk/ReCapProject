using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperation("color.add,admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            if (color.ColorName.Length >= 2)
            {
                _colorDal.Add(color);
                return new SuccessResult(Messages.ColorAdded);
            }
            return new ErrorResult(Messages.ColorDetailsInvalid);

        }

        [SecuredOperation("color.delete,admin")]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorsListed);
        }

        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId), Messages.ColorListed);
        }

        [SecuredOperation("color.update,admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}

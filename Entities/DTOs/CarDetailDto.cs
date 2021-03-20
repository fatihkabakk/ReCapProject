using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public int ModelYear { get; set; }
        public string ColorName { get; set; }
        public int DailyPrice { get; set; }
    }
}

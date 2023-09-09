using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDto _responseDto;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                _responseDto.Result = _dbContext.Coupons.ToList();
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;

            }
            return _responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                var coupon = _dbContext.Coupons.FirstOrDefault(x => x.CouponId == id);
                if(coupon == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No coupon found with this id!";
                }

                _responseDto.Result = coupon;
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;

            }
            return _responseDto;
        }
    }
}

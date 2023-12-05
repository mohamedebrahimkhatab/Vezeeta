using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CouponsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICouponService _couponService;

    public CouponsController(ICouponService couponService, IMapper mapper)
    {
        _mapper = mapper;
        _couponService = couponService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Coupon>> GetById(int id)
    {
        Coupon? coupon = await _couponService.GetById(id);
        if (coupon == null)
        {
            return NotFound();
        }
        return Ok(coupon);
    }

    [HttpGet("{discountCode}")]
    public async Task<ActionResult<CouponDto>> GetByDiscountCode(string discountCode)
    {
        Coupon? coupon = await _couponService.GetByDiscountCode(discountCode);
        if (coupon == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CouponDto>(coupon));
    }

    [HttpPost]
    public async Task<IActionResult> Add(CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            await _couponService.Create(coupon);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCouponDto couponDto)
    {
        try
        {
            Coupon? coupon = await _couponService.GetById(couponDto.Id);
            if(coupon == null)
            {
                return NotFound();
            }
            coupon.DiscountCode = couponDto.DiscountCode;
            coupon.NumOfRequests = couponDto.NumOfRequests;
            coupon.DiscountType = couponDto.DiscountType;
            coupon.Value = couponDto.Value;
            await _couponService.Update(coupon);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        Coupon? coupon = await _couponService.GetById(id);
        if (coupon == null)
        {
            return NotFound();
        }
        await _couponService.Deactivate(coupon);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Coupon? coupon = await _couponService.GetById(id);
        if (coupon == null)
        {
            return NotFound();
        }
        await _couponService.Delete(coupon);
        return NoContent();
    }
}

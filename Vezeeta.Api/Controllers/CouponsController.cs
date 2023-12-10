using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Api.Validators;
using Vezeeta.Core.Consts;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Core.Models;
using Vezeeta.Core.Services;

namespace Vezeeta.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class CouponsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICouponService _couponService;

    public CouponsController(ICouponService couponService, IMapper mapper)
    {
        _mapper = mapper;
        _couponService = couponService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CouponDto couponDto)
    {
        try
        {
            var validator = new CouponDtoValidator();
            var validate = await validator.ValidateAsync(couponDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            await _couponService.Create(coupon);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCouponDto couponDto)
    {
        try
        {
            var validator = new UpdateCouponDtoValidator();
            var validate = await validator.ValidateAsync(couponDto);
            if (!validate.IsValid)
            {
                return BadRequest(validate.Errors.Select(e => e.ErrorMessage));
            }
            Coupon? coupon = await _couponService.GetById(couponDto.Id);
            if (coupon == null)
            {
                return NotFound();
            }
            await _couponService.CheckIfCouponApplied(coupon.DiscountCode);
            _mapper.Map(couponDto, coupon);
            await _couponService.Update(coupon);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        try
        {
            Coupon? coupon = await _couponService.GetById(id);
            if (coupon == null)
            {
                return NotFound($"there is no activated coupon with id: {id}");
            }
            await _couponService.Deactivate(coupon);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            Coupon? coupon = await _couponService.GetById(id);
            if (coupon == null)
            {
                return NotFound();
            }
            await _couponService.Delete(coupon);
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}

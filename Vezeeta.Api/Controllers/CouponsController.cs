using AutoMapper;
using Vezeeta.Core.Consts;
using Vezeeta.Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vezeeta.Core.Contracts.CouponDtos;
using Vezeeta.Services.DomainServices.Interfaces;

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

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _couponService.GetAll();
        return StatusCode(result.StatusCode, result.Body);
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
            var result = await _couponService.Create(couponDto);
            return StatusCode(result.StatusCode, result.Body);
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
            await _couponService.Update(couponDto);
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
            var result = await _couponService.Deactivate(id);
            return StatusCode(result.StatusCode, result.Body);
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
            var result = await _couponService.Delete(id);
            return StatusCode(result.StatusCode, result.Body);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}

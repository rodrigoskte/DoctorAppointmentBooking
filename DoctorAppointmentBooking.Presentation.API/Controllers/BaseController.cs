using DoctorAppointmentBooking.Application.Constants;
using DoctorAppointmentBooking.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.Presentation.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected IActionResult Execute<T>(Func<T> func)
    {
        try
        {
            var result = func();
            if (result == null)
                return NotFound(new ResultViewModel<T>(MessageConstants.ContentNotFound, StatusCodes.Status404NotFound));
            
            return Ok(new ResultViewModel<T>(result, StatusCodes.Status200OK));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ResultViewModel<string>(ex.Message, StatusCodes.Status400BadRequest));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<T>($"{MessageConstants.InternalServerError} - {ex.Message} - {ex.InnerException}", StatusCodes.Status500InternalServerError));
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected IActionResult Execute(Func<object> func)
    {
        try
        {
            var result = func();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
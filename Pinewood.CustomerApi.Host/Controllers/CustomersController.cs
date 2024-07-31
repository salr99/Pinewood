using Microsoft.AspNetCore.Mvc;
using Pinewood.CustomerApi.Exceptions;
using Pinewood.CustomerApi.Handlers;
using Pinewood.CustomerApi.Models;
using Pinewood.CustomerApi.RequestsAndResponses;

namespace Pinewood.CustomerApi.Host.Controllers;

// TODO: I could move all the catches into one middleware class that handles all errors and only have the happy paths in the controller.

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerHandler _handler;

    public CustomersController(ICustomerHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Customer>> GetCustomers()
    {
        try
        {
            var response = _handler.GetAll();
            return Ok(response.Customers);
        }
        catch (Exception) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{customerId}")]
    public ActionResult<Customer> GetCustomer(Guid customerId)
    {
        try
        {
            var request = new GetCustomerRequest { CustomerId = customerId };
            var customer = _handler.GetById(request);
            return Ok(customer);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public ActionResult AddCustomer(CreateCustomerRequest request)
    {
        try
        {
            var createdCustomer = _handler.Add(request);
            return CreatedAtAction(nameof(GetSetCustomerResponse), new { id = createdCustomer.CustomerId }, request);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{customerId}")]
    public IActionResult UpdateCustomer([FromRoute] Guid customerId, [FromBody]UpdateCustomerRequest request)
    {
        try
        {
            request.CustomerId = customerId;
            _handler.Update(request);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{customerId}")]
    public IActionResult DeleteCustomer(Guid customerId)
    {
        try
        {
            var request = new DeleteCustomerRequest { CustomerId = customerId };
            _handler.Delete(request);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
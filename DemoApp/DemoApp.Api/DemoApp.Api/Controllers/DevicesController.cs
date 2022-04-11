using DemoApp.BusinessLogic;
using DemoApp.DataLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace DemoApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        // Fields
        private readonly IRepository _repository;
        private readonly ILogger<DeviceController> _logger;

        // Constructors
        public DevicesController(IRepository repository, ILogger<DeviceController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        // Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetAllDevicesAsyc()
        {
            IEnumerable<Device> devices;
            try
            {
                devices = await _repository.GetAllDevices();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error while getting devices.");
                return StatusCode(500);
            }
            return devices.ToList();
        }
    }
}

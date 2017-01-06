using Api.Cars.SL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        readonly ICarsWorkflowService carsWorkflowService;

        public CarsController(ICarsWorkflowService carsWorkflowService)
        {
            this.carsWorkflowService = carsWorkflowService;
        }

        // GET api/cars
        [HttpGet("")]
        public async Task<IEnumerable<CarVM>> GetAsync()
        {
            return await carsWorkflowService.GetAllCarsAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
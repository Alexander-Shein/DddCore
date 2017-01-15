using System;
using Api.Cars.SL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Cars.SL.Contracts;
using Api.Cars.SL.Contracts.Models;

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
        public IEnumerable<CarVm> Get()
        {
            return carsWorkflowService.GetAllCars();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public CarVm Post([FromBody]CarIm value)
        {
            return carsWorkflowService.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public CarVm Put(Guid id, [FromBody]CarIm value)
        {
            try
            {
                var vm = carsWorkflowService.Update(id, value);
                return vm;
            }
            catch (Exception e)
            {
                return new CarVm
                {
                    Color = e.Message
                };
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
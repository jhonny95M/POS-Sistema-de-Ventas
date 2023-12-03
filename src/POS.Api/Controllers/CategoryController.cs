using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Infraestructure.Commons.Bases.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            this.categoryApplication = categoryApplication;
        }
        [HttpPost]
        public async Task<IActionResult> ListCategories([FromBody]BaseFiltersRequest filters)
        {
            var response =await categoryApplication.ListCategories(filters);
            return Ok(response);
        }
        // GET: api/<CategoryController>
        [HttpGet("select")]
        public async Task<IActionResult> Get()
        {
            var response=await categoryApplication.ListSelectCategories();
            return Ok(response);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response=await categoryApplication.CategoryById(id);
            return Ok(response);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryRequestDto requestDto)
        {
            var response =await categoryApplication.RegisterCategory(requestDto);
            return Ok(response);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryRequestDto requestDto)
        {
            var response = await categoryApplication.EditCategory(id,requestDto);
            return Ok(response);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await categoryApplication.RemoveCategory(id);
            return Ok(response);
        }
    }
}

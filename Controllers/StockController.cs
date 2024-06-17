using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApi_sqlServer.Data;
using testApi_sqlServer.Dtos.Stock;
using testApi_sqlServer.Helpers;
using testApi_sqlServer.Interfaces;
using testApi_sqlServer.Mappers;

namespace testApi_sqlServer.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContex _context;
        private readonly IStockRepository _stockRepo;
        public StockController(AppDbContex context, IStockRepository repo)
        {
            _stockRepo = repo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]  QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(e => e.ToStockDto()).ToList();
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);
            return (stock == null) ? NotFound() : Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = stockDto.ToStockCreateDto();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequesDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }



            return Ok(stockModel.ToStockDto());
        }


        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
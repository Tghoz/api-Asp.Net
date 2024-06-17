

using System.Net.Sockets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using testApi_sqlServer.Dtos.Comment;
using testApi_sqlServer.Extensions;
using testApi_sqlServer.Interfaces;
using testApi_sqlServer.Mappers;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Controllers
{

#pragma warning disable CS8604
    
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            var comment = await _commentRepo.GetByIdAsync(id);
            return (comment == null) ? NotFound() : Ok(comment.ToCommentDto());
        }


        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);


            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("stock does not exist");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);


            var commentModel = commentDto.ToCreateCommentDto(stockId);

            commentModel.AppUserId = appUser.Id;
            
            await _commentRepo.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var comment = await _commentRepo.UpdateAsync(id, updateDto.ToUpdateCommentDto());
            if (comment == null)
            {
                NotFound("Not exist comment");
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                NotFound("Not exist comment");
            }

            return Ok("ok");

        }

    }
}
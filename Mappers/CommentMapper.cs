using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi_sqlServer.Dtos.Comment;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreateBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId,

            };
        }

        public static Comment ToCreateCommentDto(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId

            };
        }
        public static Comment ToUpdateCommentDto(this UpdateCommentRequestDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content
        

            };
        }
    }
}
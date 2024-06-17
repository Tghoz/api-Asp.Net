using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApi_sqlServer.Data;
using testApi_sqlServer.Dtos.Comment;
using testApi_sqlServer.Interfaces;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContex _contex;
        public CommentRepository(AppDbContex contex)
        {
            _contex = contex;
        }
        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _contex.Comments.AddAsync(commentModel);
            await _contex.SaveChangesAsync();
            return commentModel;
        }
        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _contex.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null )return null;
            _contex.Comments.Remove(commentModel);
            await _contex.SaveChangesAsync();
            return commentModel;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _contex.Comments.Include(a => a.AppUser ).ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _contex.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existComment = await _contex.Comments.FindAsync(id);
            if(existComment == null){
                return null;
            }
            existComment.Title = commentModel.Title;
            existComment.Content = commentModel.Content;
            await _contex.SaveChangesAsync();
            return existComment;
        }
    }
}
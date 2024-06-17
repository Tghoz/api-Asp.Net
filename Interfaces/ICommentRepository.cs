using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApi_sqlServer.Models;

namespace testApi_sqlServer.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}
using CaseMangementSystem.Contexts;
using CaseMangementSystem.Models;
using CaseMangementSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Services
{
    internal class CommentService: GenericService<CommentEntity>
    {
        private readonly DataContext _context = new();
        public async Task<CommentEntity> CreateCommentAsync(Comment comment)
        {
            var commentEntity = new CommentEntity()
            {
                CommentText = comment.CommentText,
                TicketId = comment.TicketId
            };

            await _context.Comments.AddAsync(commentEntity); 
            await _context.SaveChangesAsync();

            return commentEntity;
            
        }
    }
}

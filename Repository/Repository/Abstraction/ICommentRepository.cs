using DomainModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Abstraction
{
    public interface ICommentRepository:IRepository<Comment>
    {
        Task<List<Comment>> GetSomethingInclude();
        Task<bool> DeleteComment(Comment comment);
    }
}

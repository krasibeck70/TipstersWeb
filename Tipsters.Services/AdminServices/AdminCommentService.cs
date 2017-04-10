using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tipsters.Data.Interfaces;
using Tipsters.Models.Models;

namespace Tipsters.Services.AdminServices
{
    public class AdminCommentService : Service
    {
        public AdminCommentService(ITipstersData data) : base(data)
        {
        }

        public List<Comment> GetAllComments()
        {
            var comment = data.Comments.GetAll().ToList();
            return comment;
        }
    }
}

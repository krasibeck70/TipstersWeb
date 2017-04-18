using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tipsters.Data.Interfaces;
using Tipsters.Models.BindingModels;
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

        public Comment CommentsById(string id)
        {
            var comment = data.Comments.Find(x => x.Id == id).First();
            return comment;
        }
        public void RemoveComment(string id)
        {
            var comment = this.CommentsById(id);
            data.Comments.Delete(comment);
            data.SaveChanges();
        }

        public void EditComment(string id, PostCommentBindingModel pcbm)
        {
            var comment = this.CommentsById(id);
            comment.Message = pcbm.Comment;
            data.Comments.InsertOrUpdate(comment);
            data.SaveChanges();
        }
    }
}

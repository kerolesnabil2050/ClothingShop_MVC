using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repoitry
{
    public class CommentRepositry : Repositry<Comment>, ICommentRepositry
    {
        private readonly Context context;
        public CommentRepositry(Context context) : base(context)
        {
            this.context=context;
        }

        public List<Comment> GetAllcommentForitem(int id)
        {
            return context.comments.Where(e =>e.ProdectSellerId== id).Include(e => e.Customer).ThenInclude(e => e.ApplcationUser).ToList();
        }
    }
}

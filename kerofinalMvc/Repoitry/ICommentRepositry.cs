using Project.Models;

namespace Project.Repoitry
{
    public interface ICommentRepositry:Irepositry<Comment>
    {
        List<Comment> GetAllcommentForitem(int id);
    }
}

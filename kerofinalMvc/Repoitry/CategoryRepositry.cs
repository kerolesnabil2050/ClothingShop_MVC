using Project.Models;

namespace Project.Repoitry
{
    public class CategoryRepositry : Repositry<Category>,ICategoryRepositry
    {
        public CategoryRepositry(Context context) : base(context)
        {

        }
    }
}

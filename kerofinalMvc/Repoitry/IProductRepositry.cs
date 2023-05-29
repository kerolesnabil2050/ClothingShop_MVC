using Project.Models;

namespace Project.Repoitry
{
    public interface IProductRepositry:Irepositry<prodect>
    {
        public List<prodect> GetAllByCategoryName(string categoryName);
        prodect GetByIdWithInclude(int id,string include);
       List<prodect> GetAllwithinclude(Func<prodect, bool> predicate, string include);

    }
}

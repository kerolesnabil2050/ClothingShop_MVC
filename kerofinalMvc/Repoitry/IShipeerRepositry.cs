using Project.Models;

namespace Project.Repoitry
{
    public interface IShipeerRepositry:Irepositry<Shiper>
    {
        string GetShiper();
        Shiper GetssupplierById(string id);
       
        Shiper GETSpecificsShiper(string id);

        List<Shiper> GetAllWithInclude(string include);
        public void Updatesup(string id, Shiper s);
        

    }
}

using Project.Models;

namespace Project.Repoitry
{
    public class Repositry<T>: Irepositry<T> where T : class
    {
        private Context Context;
        public Repositry(Context context)
        {
            this.Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            T element = Context.Set<T>().Find(id);
            Context.Set<T>().Remove(element);
            Context.SaveChanges();
        }

        public List<T> GetAll(Func<T, bool> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }

        

        public T GetById(int id)
        {
            return Context.Find<T>(id);
        }

        public void Update(int id, T newElement)
        {
            Context.Set<T>().Update(newElement);
            Context.SaveChanges();
        }
    }
}

namespace Project.Repoitry
{
    public interface Irepositry<T> where T : class
    {
        List<T> GetAll(Func<T, bool> predicate);

        T GetById(int id);

        void Delete(int id);

        void Update(int id, T repNmae);

        void Add(T entity);
    }
}

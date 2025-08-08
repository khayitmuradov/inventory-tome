using inventory_tome.Core.Models;

namespace inventory_tome.Core.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Member? GetById(int id);
        IEnumerable<Member> GetAll();
        void Add(Member member);
        void Update(Member member);
    }
}

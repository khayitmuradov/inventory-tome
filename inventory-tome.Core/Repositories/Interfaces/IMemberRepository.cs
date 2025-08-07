using inventory_tome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_tome.Core.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Member GetById(int id);
        IEnumerable<Member> GetAll();
        void Add(Member member);
        void Update(Member member);
    }
}

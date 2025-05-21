using ConsoleApp1.Exceptions;
using ConsoleApp1.Models;

namespace ConsoleApp1.Repositories
{
    class EmployeeRepository : Repository<int, Employee>
    {
        public override ICollection<Employee> GetAll()
        {
            if (_items.Count() == 0)
            {
                throw new CollectionEmptyException("NO items found!");
            }
            return _items;
        }

        public override Employee GetById(int id)
        {
            Employee? e = _items.FirstOrDefault(i => i.Id == id);
            if (e == null)
            {
                throw new KeyNotFoundException("NO items found!");
            }
            return e;
        }

        protected override int GenerateID()
        {
            if (_items.Count() == 0) return 101;
            return _items.Max(i => i.Id) + 1;
        }
    }
}
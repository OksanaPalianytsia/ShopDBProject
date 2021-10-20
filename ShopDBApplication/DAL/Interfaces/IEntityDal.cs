using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEntityDal<T>
    {
        List<T> GetAllItems();
        T GetItemById(int itemId);
        void UpdateItem(T item);
        void CreateItem(T item);
        void DeleteItem(int itemId);
    }
}

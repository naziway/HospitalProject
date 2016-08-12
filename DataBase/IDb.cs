using System.Collections.Generic;

namespace DataBase
{
    public interface IDb<T>
    {
        List<T> GetData();
        void InsertData(T data);
        void UpdateData(T data);

    }
}
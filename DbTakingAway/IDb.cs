using System.Collections.Generic;

namespace Data
{
    public interface IDb<T>
    {
        List<T> GetData();
        bool InsertData(T data);
        bool UpdateData(T data);

    }
}
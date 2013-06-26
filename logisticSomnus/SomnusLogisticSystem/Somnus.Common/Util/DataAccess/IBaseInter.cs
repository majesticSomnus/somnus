using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Somnus.Common.Util.DataAccess
{
   public interface IBaseInter<TModel>
    {
        int Count(Hashtable param);
        int Insert(TModel model);
        void Delete(int ID);
        void Update(TModel model);
        TModel Get(int ID, bool isFull);
        IList<TModel> Select(Hashtable param, bool isFull);
        IList<TModel> SelectPager(Hashtable param, bool isFull, int start, int end);
    }
}

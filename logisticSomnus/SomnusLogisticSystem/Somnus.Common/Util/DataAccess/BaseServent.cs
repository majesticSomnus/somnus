using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Somnus.Common.Util.DataAccess
{
    public class BaseServent<TModel, TInter> where TInter : IBaseInter<TModel>
    {
        public TInter DAO = CastleContext.Instence.GetService<TInter>();

        public static int Count(Hashtable param)
        {
            return new BaseServent<TModel, TInter>().DAO.Count(param);
        }
        public static int Insert(TModel model)
        {
            return new BaseServent<TModel, TInter>().DAO.Insert(model);
        }
        public static void Delete(int ID)
        {
            new BaseServent<TModel, TInter>().DAO.Delete(ID);
        }
        public static void Update(TModel model)
        {
            new BaseServent<TModel, TInter>().DAO.Update(model);
        }
        public static TModel Get(int ID, bool isFull)
        {
            return new BaseServent<TModel, TInter>().DAO.Get(ID, isFull);
        }
        public static IList<TModel> Select(Hashtable param, bool isFull)
        {
            return new BaseServent<TModel, TInter>().DAO.Select(param, isFull);
        }
        public static IList<TModel> SelectPager(Hashtable param, bool isFull, int start, int end)
        {
            return new BaseServent<TModel, TInter>().DAO.SelectPager(param, isFull, start, end);
        }
    }
}

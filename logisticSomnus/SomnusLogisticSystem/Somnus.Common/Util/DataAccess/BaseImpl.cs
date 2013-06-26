using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;
using System.Collections;

namespace Somnus.Common.Util.DataAccess
{
   public class BaseImpl<TModel> 
    {
       private string PrefixMapName = string.Empty;
       private ISqlMapper Mapper = null;
       public BaseImpl()
       {  
       }
       public BaseImpl(string preFixMapName, ISqlMapper mapper)
       {
           this.PrefixMapName = preFixMapName;
           this.Mapper = mapper;
       }

       public string Map(string mapName)
       {
           return this.PrefixMapName +"."+ mapName;
       }

       public int Count(Hashtable param)
       {
           string statement = this.Map("Count");
           return Mapper.QueryForObject<int>(statement, param);
       }

       public int Insert(TModel model)
       {
           string statement = this.Map("Insert");
           return Mapper.QueryForObject<int>(statement, model);
       }

       public void Delete(int ID)
       {
           string statement = this.Map("Delete");
           Mapper.Update(statement, ID);
       }

       public void Update(TModel model)
       {
           string statement = this.Map("Update");
           Mapper.Update(statement, model);
       }

       public TModel Get(int ID, bool isFull)
       {
           string statement = string.Empty;
           if (isFull)
           {
               statement = this.Map("SelectFull");
           }
           else
           {
               statement = this.Map("Select");
           }
           Hashtable param = new Hashtable();
           param.Add("ID", ID);
           return Mapper.QueryForObject<TModel>(statement, param);
       }

       public IList<TModel> Select(Hashtable param, bool isFull)
       {
           string statement = string.Empty;
           if (isFull)
           {
               statement = this.Map("SelectFull");
           }
           else
           {
               statement = this.Map("Select");
           }
           return Mapper.QueryForList<TModel>(statement, param);
       }

       public IList<TModel> SelectPager(Hashtable param, bool isFull, int start, int end)
       {
           string statement = string.Empty;
           if (isFull)
           {
               statement = this.Map("SelectFullPager");
           }
           else
           {
               statement = this.Map("SelectPager");
           }
           if (param == null)
           {
               param = new Hashtable();
           }
           if (start > 0)
           {
               param.Add("BeginRow", start);
           }
           if (end > 0)
           {
               param.Add("EndRow", end);
           }
           return Mapper.QueryForList<TModel>(statement, param);
       }
    }
}

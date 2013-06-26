using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SomnusLogistic.Model;
using SomnusLogistic.Dao.MySql.Inter;
using Somnus.Common.Util.DataAccess;

namespace SomnusLogistic.Dao.MySql.Impl
{
   public class CustomerDao:BaseImpl<Customer>,ICustomerDao
    {
       public CustomerDao()
           : base("SomnusLogistic.Dao.Customer", Mapper.MySqlMapper)
        {
        }
    }
}

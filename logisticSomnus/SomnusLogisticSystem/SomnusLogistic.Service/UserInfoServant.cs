using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Somnus.Common.Util.DataAccess;
using SomnusLogistic.Model;
using SomnusLogistic.Dao.MySql.Inter;

namespace SomnusLogistic.Service
{
   public class UserInfoServant:BaseServent<UserInfo,IUserInfoDao>
    {
       public static void ForbidUser(Hashtable param)
       {
           new BaseServent<UserInfo, IUserInfoDao>().DAO.ForbidUser(param);
       }
    }
}

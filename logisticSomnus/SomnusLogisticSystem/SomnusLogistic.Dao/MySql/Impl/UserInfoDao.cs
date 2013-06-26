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
    public class UserInfoDao : BaseImpl<UserInfo>, IUserInfoDao
    {
        public UserInfoDao()
            : base("SomnusLogistic.Dao.UserInfo", Mapper.MySqlMapper)
        {
        }

        public void ForbidUser(Hashtable param)
        {
            string statment = base.Map("ForbidUser");
            Mapper.MySqlMapper.Update(statment,param);
        }
    }
}

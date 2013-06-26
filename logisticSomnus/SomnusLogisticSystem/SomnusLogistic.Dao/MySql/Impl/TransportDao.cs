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
    public class TransportDao : BaseImpl<TransPort>, ITransportDao
    {
        public TransportDao()
            : base("SomnusLogistic.Dao.Transport", Mapper.MySqlMapper)
        {
        }
    }
}

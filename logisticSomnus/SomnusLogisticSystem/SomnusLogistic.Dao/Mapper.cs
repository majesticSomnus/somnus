using System;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System.Reflection;
using System.IO;

namespace SomnusLogistic.Dao
{
    public class Mapper
    {
        private static volatile ISqlMapper _mySqlMapper = null;
        /// <summary>
        /// </summary>
        public static ISqlMapper MySqlMapper
        {
            get
            {
                if (_mySqlMapper == null)
                {
                    lock (typeof(ISqlMapper))
                    {
                        if (_mySqlMapper == null)
                        {
                            _mySqlMapper = ConfigWithFileName("Somnus.Loginstic.mysqlmap.config");
                        }
                    }
                }
                return _mySqlMapper;
            }
        }


        protected static ISqlMapper ConfigWithFileName(string fileName)
        {
            string embeddedName = "SomnusLogistic.Dao." + fileName;
            Assembly assembly = Assembly.GetAssembly(typeof(Mapper));
            Stream stream = assembly.GetManifestResourceStream(embeddedName);
            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            ISqlMapper _mapper = builder.Configure(stream);
            stream.Close();
            return _mapper;
        }



    }
}

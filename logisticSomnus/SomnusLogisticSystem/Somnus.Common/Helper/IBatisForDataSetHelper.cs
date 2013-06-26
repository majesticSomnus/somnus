using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Configuration.Statements;
using IBatisNet.DataMapper.Scope;


namespace Somnus.Common.Helper
{
   public class IBatisForDataSetHelper
    {

        /// <summary>
        ///  返回DataSet
        /// </summary>
        /// <param name="sqlMapper"></param>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public static  DataSet QueryForDataSet(ISqlMapper sqlMapper, string statementName, object paramObject)
        {
            DataSet ds = new DataSet();
            bool isSessionLocal = false;
            IDalSession session = sqlMapper.LocalSession;
            if (session == null)
            {
                session = new SqlMapSession(sqlMapper);
                session.OpenConnection();
                isSessionLocal = true;
            }

            IDbCommand cmd = GetDbCommand(sqlMapper, statementName, paramObject);//SQL text command

            try
            {
                cmd.Connection = session.Connection;
                IDbDataAdapter adapter = session.CreateDataAdapter(cmd);
                adapter.Fill(ds);
            }
            finally
            {
                if (isSessionLocal)
                {
                    session.CloseConnection();
                }
            }

            return ds;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sqlMapper"></param>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public static  IDbCommand GetDbCommand(ISqlMapper sqlMapper, string statementName, object paramObject)
        {
            IStatement statement = sqlMapper.GetMappedStatement(statementName).Statement;
            IMappedStatement mapStatement = sqlMapper.GetMappedStatement(statementName);
            ISqlMapSession session = new SqlMapSession(sqlMapper);

            if (sqlMapper.LocalSession != null)
            {
                session = sqlMapper.LocalSession;
            }
            else
            {
                session = sqlMapper.OpenConnection();
            }

            RequestScope request = statement.Sql.GetRequestScope(mapStatement, paramObject, session);
            mapStatement.PreparedCommand.Create(request, session as ISqlMapSession, statement, paramObject);
            IDbCommand cmd = session.CreateCommand(CommandType.Text);
            cmd.CommandText = request.IDbCommand.CommandText;
            IDataParameterCollection collection = request.IDbCommand.Parameters;
            if (paramObject != null)
            {
                Dictionary<string, string> table = paramObject as Dictionary<string, string>;
                int i = 0;
                foreach (KeyValuePair<string, string> item in table)
                {
                    string parmam = collection[i].ToString();
                    SqlParameter par = new SqlParameter(parmam, table[item.Key]);
                    cmd.Parameters.Add(par);
                    i++;
                }

            }
            return cmd;
        }


        /// <summary>
        /// 得到运行时ibatis.net动态生成的SQL
        /// </summary>
        /// <param name="sqlMapper"></param>
        /// <param name="statementName"></param>
        /// <param name="paramObject"></param>
        /// <returns></returns>
        public static string GetRuntimeSql(ISqlMapper sqlMapper, string statementName, object paramObject)
        {
            string result = string.Empty;
            try
            {
                IMappedStatement statement = sqlMapper.GetMappedStatement(statementName);
                if (!sqlMapper.IsSessionStarted)
                {
                    sqlMapper.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMapper.LocalSession);
                result = scope.PreparedStatement.PreparedSql;
            }
            catch (Exception ex)
            {
                result = "获取SQL语句出现异常:" + ex.Message;
            }
            return result;
        }
    }
}

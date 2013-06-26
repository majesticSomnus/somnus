using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;

namespace Somnus.Common
{
    public class ImportExcel
    {
        /// <summary> 
        /// 将一组对象导出成EXCEL 
        /// </summary> 
        /// <typeparam name="T">要导出对象的类型</typeparam> 
        /// <param name="objList">一组对象</param> 
        /// <param name="FileName">导出后的文件名</param> 
        /// <param name="columnInfo">列名信息</param> 
        public static string ExExcel<T>(List<T> objList, string FileName, Dictionary<string, string> columnInfo)
        {

            if (columnInfo.Count == 0) { return ""; }
            if (objList.Count == 0) { return ""; }
            //生成EXCEL的HTML 
            string excelStr = "";

            Type myType = objList[0].GetType();
            //根据反射从传递进来的属性名信息得到要显示的属性 
            List<PropertyInfo> myPro = new List<PropertyInfo>();
            foreach (string cName in columnInfo.Keys)
            {
                PropertyInfo p = myType.GetProperty(cName);
                if (p != null)
                {
                    myPro.Add(p);
                    excelStr += columnInfo[cName] + "\t";
                }
            }
            //如果没有找到可用的属性则结束 
            if (myPro.Count == 0) { return ""; }
            excelStr += "\n";

            foreach (T obj in objList)
            {
                foreach (PropertyInfo p in myPro)
                {
                    excelStr += p.GetValue(obj, null) + "\t";
                }
                excelStr += "\n";
            }
            return excelStr;
        }
    }
}

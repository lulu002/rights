using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common;
using System.Web.Mvc;
using System.Text;
using System.EnterpriseServices;
using System.Configuration;


using NPOI.HPSF;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web;

namespace Models
{
    //[SupportFilter]//此处如果去掉注释，则全部继承BaseController的Controller，都将执行SupportFilter过滤
    public class BaseController : Controller
    {
        /// <summary>
        /// 获取当前登陆人的名称
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPerson()
        {
            Account account = GetCurrentAccount();
            if (account != null && !string.IsNullOrWhiteSpace(account.PersonName))
            {
                return account.PersonName;
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取当前登陆人的账户信息
        /// </summary>
        /// <returns>账户信息</returns>
        public Account GetCurrentAccount()
        {
            if (Session["account"] != null)
            {
                Account account = (Account)Session["account"];
                return account;
            }
            return null;
        }
        /// <summary>
        /// 导出数据集到excle
        /// </summary>
        /// <param name="titles">第一行显示的标题名称</param>
        /// <param name="fields">字段</param>
        /// <param name="query">数据集</param>
        /// <param name="path">excle模版的位置</param>
        /// <param name="from">显示的标题默认行数为1</param>
        /// <returns></returns>
        public string WriteExcle(string[] titles, string[] fields, dynamic[] query, string path = @"~/up/b.xls", int from = 1)
        {
            HSSFWorkbook _book = new HSSFWorkbook();
            string xlsPath = System.Web.HttpContext.Current.Server.MapPath(path);

            FileStream file = new FileStream(xlsPath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");
            string guid = Guid.NewGuid().ToString();
            string saveFileName = xlsPath.Path(guid);

            Dictionary<string, string> propertyName;
            PropertyInfo[] properties;
            //标题行  
            HSSFRow dataRow = sheet.CreateRow(0) as HSSFRow;
            for (int i = 0; i < titles.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(titles[i]))
                {

                    dataRow.CreateCell(i).SetCellValue(titles[i]); //列值

                }
            }
            //内容行
            for (int i = 0; i < query.Length; i++)
            {
                propertyName = new Dictionary<string, string>();
                if (query[i] == null)
                {
                    continue;
                }
                Type type = query[i].GetType();
                properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    object o = property.GetValue(query[i], null);
                    if (!string.IsNullOrEmpty(property.Name) && o != null)
                    {
                        propertyName.Add(property.Name, o.ToString());
                    }
                }
                int j = 0;
                dataRow = sheet.CreateRow(i + from) as HSSFRow;
                fields.All(a =>
                {

                    if (propertyName.ContainsKey(a)) //列名
                    {

                        dataRow.CreateCell(j).SetCellValue(propertyName[a]);
                        //列值
                    }
                    j++;
                    return true;
                });
            }
            sheet.ForceFormulaRecalculation = true;
            using (FileStream fileWrite = new FileStream(saveFileName, FileMode.Create))
            {
                hssfworkbook.Write(fileWrite);
            }


            //一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet  
            return string.Format("../../up/{0}.xls", guid);
            //记录日志

        }
        public string ReadExcle(string path = @"~/up/a.xls", int from = 1)
        {
            HSSFWorkbook _book = new HSSFWorkbook();
            string xlsPath = System.Web.HttpContext.Current.Server.MapPath(path);

            FileStream file = new FileStream(xlsPath, FileMode.Open, FileAccess.Read);
            IWorkbook hssfworkbook = new HSSFWorkbook(file);
            ISheet sheet = hssfworkbook.GetSheet("Sheet1");
            string guid = Guid.NewGuid().ToString();
            string saveFileName = xlsPath.Path(guid);

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            string courty = "";

            string temp =  "overseaotherSchoolClass[{1}] = new Array({0});\n\r";
            //获取sheet的首行
            var headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            //for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            //{

            //}

            //overseaSchoolItemID[0] = new Array("38001", "38002", "38003", "38004", "38005", "38006", "38007", "38008", "38009", "38010", "380");
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;
            sb.Append(",");
            int f = 1;
            int guojia = 3;
            for (int i = (sheet.FirstRowNum); i < sheet.LastRowNum; i++)
            {

                var row = sheet.GetRow(i);

                if (!string.IsNullOrWhiteSpace(courty) && courty.Trim() != row.GetCell(0).ToString().Trim())
                {
                    sb.Append(string.Format(temp, sb2.ToString(), guojia));
                    sb2.Clear();
                    f = 1;
                    guojia++;
                }

                if (f != 1)
                {
                    sb2.Append(",");

                }
                f++;
                courty = row.GetCell(0).ToString();
                sb2.Append(string.Format("\"{0}\",\"{1}\"", row.GetCell(1).ToString(), row.GetCell(0).ToString()));



            }
            var da = sb.ToString();



            //string courty = "";

            //string temp = "overseaSchoolItemValue[{1}] = new Array({0});\n\r";
            ////获取sheet的首行
            //var headerRow = sheet.GetRow(0);

            ////一行最后一个方格的编号 即总的列数
            //int cellCount = headerRow.LastCellNum;

            ////for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            ////{

            ////}

            ////overseaSchoolItemID[0] = new Array("38001", "38002", "38003", "38004", "38005", "38006", "38007", "38008", "38009", "38010", "380");
            ////最后一列的标号  即总的行数
            //int rowCount = sheet.LastRowNum;
            //sb.Append(",");
            //int f = 1;
            //int guojia = 3;
            //for (int i = (sheet.FirstRowNum); i < sheet.LastRowNum; i++)
            //{

            //    var row = sheet.GetRow(i);

            //    if (!string.IsNullOrWhiteSpace(courty) && courty.Trim() != row.GetCell(1).ToString().Trim())
            //    {
            //        sb.Append(string.Format(temp, sb2.ToString(), guojia));
            //        sb2.Clear();
            //        f = 1;
            //        guojia++;
            //    }

            //    if (f != 1)
            //    {
            //        sb2.Append(",");

            //    }
            //    f++;
            //    courty = row.GetCell(1).ToString();
            //    sb2.Append(string.Format("\"{0}\"", row.GetCell(2).ToString()));



            //}
            //var da = sb.ToString();



            //string courty = "";

            //string temp = "overseaSchoolItemID[{1}] = new Array({0});\n\r";
            ////获取sheet的首行
            //var headerRow = sheet.GetRow(0);

            ////一行最后一个方格的编号 即总的列数
            //int cellCount = headerRow.LastCellNum;

            ////for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            ////{

            ////}

            ////overseaSchoolItemID[0] = new Array("38001", "38002", "38003", "38004", "38005", "38006", "38007", "38008", "38009", "38010", "380");
            ////最后一列的标号  即总的行数
            //int rowCount = sheet.LastRowNum;
            //sb.Append(",");
            //int f = 1;
            //int guojia = 3;
            //for (int i = (sheet.FirstRowNum ); i < sheet.LastRowNum; i++)
            //{

            //    var row = sheet.GetRow(i);

            //    if (!string.IsNullOrWhiteSpace(courty) && courty.Trim() != row.GetCell(1).ToString().Trim())
            //    {
            //        sb.Append(string.Format(temp, sb2.ToString(), guojia));
            //        sb2.Clear();
            //        f = 1;
            //        guojia++;
            //    }
                 
            //        if (f != 1)
            //        {
            //            sb2.Append(",");

            //        }
            //        f++;
            //        courty = row.GetCell(1).ToString();
            //        sb2.Append(string.Format("\"{0}\"", row.GetCell(0).ToString()));

 

            //}
            //var da = sb.ToString();
            hssfworkbook = null;
            sheet = null;




            //一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet  
            return string.Format("../../up/{0}.xls", guid);
            //记录日志

        }

        public BaseController()
        { }

    }
}

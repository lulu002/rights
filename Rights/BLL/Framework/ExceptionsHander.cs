using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Langben.DAL;
using Common;
using System.IO;

namespace Langben.BLL
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public static class ExceptionsHander
    {
        /// <summary>
        /// 将异常信息写入数据库，或者文本文件
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteExceptions(Exception ex)
        {
            bool exceptionEnabled = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["ExceptionEnabled"]);
            if (!exceptionEnabled)
            {
                return;
            }
            //此处实现异常的记录

            //SysException sysException = new SysException();
            //sysException.CreateTime = DateTime.Now;
            //sysException.Remark = ex.StackTrace;
            //sysException.Message = ex.Message;
            //sysException.LeiXing = "异常";
            //sysException.Result = (ex.InnerException == null) ? string.Empty : ex.InnerException.Message;
            //sysException.Id = Result.GetNewId();

            //using (SysExceptionBLL sysExceptionRepository = new SysExceptionBLL())
            //{
            //    ValidationErrors validationErrors = new ValidationErrors();
            //    sysExceptionRepository.Create(ref validationErrors, sysException);
            //    return;
            //}

        }
    }
}


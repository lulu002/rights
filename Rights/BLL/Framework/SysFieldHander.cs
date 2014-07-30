using System.Collections.Generic;
using System.Linq;
using System;
using Langben.DAL;
using Langben.IBLL;
namespace Langben.BLL
{
    public class SysFieldHander : IDisposable,  ISysFieldHander
    {
        protected SysEntities db = new SysEntities();
        /// <summary>
        /// 获取下拉框的数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="colum">列明</param>
        /// <param name="parentId">父节点编号</param>
        /// <returns></returns>
        public List<SysField> GetSysField(string table, string colum, string parentMyTexts)
        {

            return (from m in db.SysField
                    where m.MyTables == table && m.MyColums == colum && m.SysField2.MyTexts == parentMyTexts
                    orderby m.Sort
                    select m).ToList();

        }
        /// <summary>
        /// 获取下拉框的数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="colum">列明</param>
        /// <returns></returns>
        public List<SysField> GetSysField(string table, string colum)
        {

            return (from m in db.SysField
                    where m.MyTables == table && m.MyColums == colum
                    orderby m.Sort
                    select m).ToList();

        }
        /// <summary>
        /// 根据父亲的Id，获取数据字典
        /// </summary>
        /// <param name="id">父亲节点的主键</param>
        /// <returns></returns>
        public List<SysField> GetSysFieldByParentId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return (from m in db.SysField
                    where m.ParentId == id
                    orderby m.Sort
                    select m).ToList();

        }
        /// <summary>
        /// 根据父亲的Id，获取数据字典
        /// </summary>
        /// <param name="id">父亲节点的主键</param>
        /// <returns></returns>
        public List<SysField> GetSysFieldByParent(string id, string parentid, string value)
        {

            return (from m in db.SysField
                    where m.MyColums == id && m.SysField2.MyColums == parentid && m.SysField2.MyTexts == value
                    select m
                     ).ToList();

        }
        /// <summary>
        /// 根据主键id，获取数据字典的展示字段
        /// </summary>
        /// <param name="id">父亲节点的主键</param>
        /// <returns></returns>
        public string GetMyTextsById(string id)
        {
            return db.SysField.Where(s => s.Id == id).Select(s => s.MyTexts).FirstOrDefault();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}




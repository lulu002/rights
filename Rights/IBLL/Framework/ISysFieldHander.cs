using System;
using System.Collections.Generic;
using Langben.DAL;
namespace Langben.IBLL
{
    public interface ISysFieldHander
    {
        string GetMyTextsById(string id);
        List<SysField> GetSysField(string table, string colum);
        List<SysField> GetSysField(string table, string colum, string parentMyTexts);
        List<SysField> GetSysFieldByParent(string id, string parentid, string value);
        List<SysField> GetSysFieldByParentId(string id);
    }
}


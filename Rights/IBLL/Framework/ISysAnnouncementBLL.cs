using System;
using System.Collections.Generic;
using System.Linq;

using Common;
using Langben.DAL;
using System.ServiceModel;

namespace Langben.IBLL
{
    /// <summary>
    /// 公告管理 接口
    /// </summary>
    public partial interface ISysAnnouncementBLL
    {
        SysAnnouncement GetTop(int num);
    
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Langben.DAL;
using Common;

namespace Langben.BLL
{
    /// <summary>
    /// 公告管理 
    /// </summary>
    public partial class SysAnnouncementBLL 
    {
        public SysAnnouncement GetTop(int num)
        {
            return repository.GetTop(num);
        }
    }
}


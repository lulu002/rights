using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common
{
    /// <summary>
    /// 登录的用户信息
    /// </summary>
    [DataContract]
    public class Account
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 登录的用户名
        /// </summary>
        [DataMember]
        public string PersonName { get; set; }
        /// <summary>
        /// 角色的集合
        /// </summary>
        [DataMember]
        public List<string> RoleIds { get; set; }

        /// <summary>
        /// 菜单的集合
        /// </summary>
        [DataMember]
        public List<string> MenuIds { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPage.Models
{
    /// <summary>
    /// 树形节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 打开方式。_self（默认），_blank 或 其他指定窗口名称
        /// </summary>
        public string target { get; set; }

        public bool @checked { get; set; }
        public TreeNode()
        {
            target = "_self";
        }

    }
}
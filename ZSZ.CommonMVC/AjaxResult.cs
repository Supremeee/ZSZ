using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.CommonMVC
{
    /// <summary>
    /// 所有的Ajax都要返回这个类型的数据
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}

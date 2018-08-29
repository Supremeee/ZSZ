using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.CommonMVC
{
    public class RuPengPager
    {
        /// <summary>
        /// 每一页的数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 显示出来的页码的最多个数
        /// </summary>
        public int MaxPageCount { get; set; }
        /// <summary>
        /// 当前页的页码(从1开始算起始页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 链接的格式，约定其中页码用{pn}占位符
        /// </summary>
        public string UrlPattern { get; set; }
        /// <summary>
        /// 当前页的样式名
        /// </summary>
        public string CurrentPageClassName { get; set; }
        public string GetPagerHtml()
        {
            StringBuilder html = new StringBuilder();
            html.Append("<ul>");
            //总页数
            int pageCount = (int)Math.Ceiling(TotalCount * 1.0 / PageSize);
            //显示出来的页码的起始页码
            int startPageIndex = Math.Max(1, PageIndex - MaxPageCount / 2);
            //显示出来的页码的结束页码
            int endPageIndex = Math.Min(pageCount, startPageIndex + MaxPageCount);
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                if(i == PageIndex)
                {
                    html.Append("<li class='").Append(CurrentPageClassName).Append("'>").Append(i).Append("</li>");
                }
                else
                {
                    html.Append("<li><a href='").Append(UrlPattern.Replace("{pn}", i.ToString())).Append("'>").Append(i).Append("</a></li>");
                }
            }


            html.Append("</ul>");
            return html.ToString();
        }
    }
}

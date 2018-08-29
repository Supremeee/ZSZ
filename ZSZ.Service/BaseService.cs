using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    class BaseService<T> where T: BaseEntity
    {
        private ZSZDbContext ctx;
        public BaseService(ZSZDbContext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(u => u.IsDeleted == false);
        }
        /// <summary>
        /// 获取总数据条数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int startIndex, int count)
        {
            return GetAll().OrderBy(u => u.CreateDateTime).Skip(startIndex).Take(count);
        }
        /// <summary>
        /// 根据Id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(e => e.Id == id).SingleOrDefault();
        }
        /// <summary>
        /// 根据Id软删除
        /// </summary>
        /// <param name="id"></param>
        public void MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }
    }
}

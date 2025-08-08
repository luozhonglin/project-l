using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.DTO.Common
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 页号 1开始
        /// </summary>
        public int PageNumber { get; set; } = 1;
        /// <summary>
        /// 页长
        /// </summary>
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 排序方式
        /// </summary>
        public string? OrderBy { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public bool IsDescending { get; set; } = false;
    }

    /// <summary>
    /// 分页响应数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponse<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 页长
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}

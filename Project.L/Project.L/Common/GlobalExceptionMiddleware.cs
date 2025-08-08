using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Project.L.Common
{
    public class GlobalExceptionMiddleware : IExceptionFilter, IAsyncExceptionFilter
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// The env
        /// </summary>
        private readonly IWebHostEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionFilter"/> class.
        /// </summary>
        public GlobalExceptionMiddleware(IWebHostEnvironment env, ILogger<GlobalExceptionMiddleware> logger)
        {
            this.env = env;
            this.logger = logger;
        }


        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            if (!context.ExceptionHandled)
            { 
            
            
            }
        }

        /// <summary>
        /// 异常处理（异步）
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.
        /// </returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}

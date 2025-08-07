using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Impl
{
    public class ServiceModuleRegister : Autofac.Module
    {
        /// <summary>
        /// 注册注入
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces().PropertiesAutowired();

        }
    }
}

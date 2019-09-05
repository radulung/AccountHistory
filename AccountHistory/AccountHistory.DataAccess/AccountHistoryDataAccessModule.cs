using AccountHistory.DataAccess.Queries;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace AccountHistory.DataAccess
{
    public class AccountHistoryDataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           //base.Load(builder);

            RegisterQueryTypes(builder);
        }

        private void RegisterQueryTypes(ContainerBuilder builder)
        {
            var queryTypes = GetAllQueryTypes();

            foreach (var type in queryTypes)
            {
                builder.RegisterType(type).AsSelf();
            }
        }

        private IEnumerable<Type> GetAllQueryTypes()
        {
            Type queryType = typeof(IQuery);
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            return
                currentAssembly.GetTypes()
                    .Where(p => queryType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
        }
    }
}

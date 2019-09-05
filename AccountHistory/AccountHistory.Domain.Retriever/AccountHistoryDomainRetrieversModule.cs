using AccountHistory.Core;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace AccountHistory.Domain.Retriever
{
    public class AccountHistoryDomainRetrieversModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterAllRetrieverTypes(builder);
        }

        private void RegisterAllRetrieverTypes(ContainerBuilder builder)
        {
            var handlerCommandTypes = GetAllRetrieverTypes();

            foreach (var commandType in handlerCommandTypes)
            {
                builder.RegisterType(commandType).AsSelf();
            }
        }

        private IEnumerable<Type> GetAllRetrieverTypes()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            return
                currentAssembly.GetTypes()
                    .Where(type => DoesTypeSupportInterface(type, typeof(IRetriever)));
        }

        private bool DoesTypeSupportInterface(Type type, Type inter)
        {
            if (type.IsInterface)
                return false;

            if (inter.IsAssignableFrom(type))
                return true;

            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == inter);
        }
    }
}

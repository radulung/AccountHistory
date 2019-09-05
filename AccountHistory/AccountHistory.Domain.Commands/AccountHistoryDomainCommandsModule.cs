using Autofac;
using Module = Autofac.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AccountHistory.Core;

namespace AccountHistory.Domain.Commands
{
    public class AccountHistoryDomainCommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterAllCommandTypes(builder);
        }

        private void RegisterAllCommandTypes(ContainerBuilder builder)
        {
            var handlerCommandTypes = GetAllCommandTypes();

            foreach (var commandType in handlerCommandTypes)
            {
                builder.RegisterType(commandType).AsSelf();
            }
        }

        private IEnumerable<Type> GetAllCommandTypes()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            return
                currentAssembly.GetTypes()
                    .Where(type => DoesTypeSupportInterface(type, typeof(ICommand)));
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

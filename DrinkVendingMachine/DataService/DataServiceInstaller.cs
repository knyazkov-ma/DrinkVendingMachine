﻿using DrinkVendingMachine.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.RegistrationByConvention;

namespace DrinkVendingMachine.DataService
{
    public static class DataServiceInstaller
    {
        public static void Install(IUnityContainer container)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            
            container.AddNewExtension<Interception>();
            Assembly dataService = assemblies.
                SingleOrDefault(assembly => assembly.GetName().Name == "DrinkVendingMachine");
            foreach (Type t in AllClasses.FromAssemblies(dataService)
                .Where(t => t.Name.EndsWith("Service") && t.Name != "BaseService"))
            {
                Type interfaceType = dataService.GetType("DrinkVendingMachine.DataService.Interface.I" + t.Name);

                IList<MethodInfo> mInfo = t.GetType().GetMethods().ToList();

                Attribute a = Attribute.GetCustomAttribute(t, typeof(TransactionAttribute));

                if (a != null)
                    container.RegisterType(interfaceType, t,
                        new Interceptor<InterfaceInterceptor>(),
                        new InterceptionBehavior<TransactionBehavior>()
                    );
                else
                    container.RegisterType(interfaceType, t);
            }
            
                        
        }
    }
}

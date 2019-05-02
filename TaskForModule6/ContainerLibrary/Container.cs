using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ContainerLibrary.Attributes;

namespace ContainerLibrary
{
    public class Container : IContainer
    {
        private readonly List<RegisteredObject> _registeredObjects;

        public Container()
        {
            _registeredObjects = new List<RegisteredObject>();
            RegisterInjectedAttributes();
        }


        public void Register<TResolve, TTarget>(bool isSingleton = false)
        {
            Register(typeof(TResolve), typeof(TTarget), isSingleton);
        }

        private void Register(Type typeToResolve, Type targetType, bool isSingleton = false)
        {
            _registeredObjects.Add(new RegisteredObject
            {
                TypeToResolve = typeToResolve,
                TargetType = targetType,
                IsSingleton = isSingleton
            });
        }

        public void Register<TResolve>(bool isSingleton = false)
        {
            Register(typeof(TResolve), isSingleton);
        }

        private void Register(Type typeToResolve, bool isSingleton = false)
        {
            Register(typeToResolve, typeToResolve, isSingleton);
        }

        private void RegisterInjectedAttributes()
        {
            var definedTypes = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var definedType in definedTypes)
            {
                var isImportedConstructor = definedType.GetCustomAttribute<ImportConstructorAttribute>() != null;

                if (isImportedConstructor)
                {
                    Register(definedType);
                }

                var exportAttr = definedType.GetCustomAttribute<ExportAttribute>();

                if (exportAttr != null)
                {
                    if (exportAttr.TypeToResolve != null)
                    {
                        Register(exportAttr.TypeToResolve, definedType);
                    }
                    Register(definedType);
                }
            }
        }

        public TResolve Resolve<TResolve>()
        {
            return (TResolve)Resolve(typeof(TResolve));
        }

        private object Resolve(Type typeToResolve)
        {
            var registeredObject = _registeredObjects.First(t => t.TypeToResolve == typeToResolve);

            if (registeredObject != null)
            {
                if (registeredObject.IsSingleton)
                {
                    return registeredObject.SingletonInstance ?? (registeredObject.SingletonInstance =
                               CreateInstance(registeredObject.TargetType));
                }

                return CreateInstance(registeredObject.TargetType);
            }

            throw new ArgumentException($"You tried to resolve not registered type: {typeToResolve}");
        }

        private object CreateInstance(Type type)
        {
            var constructorParameters = GetConstructorParameters(type).ToArray();
            var instance = constructorParameters.Any() ? 
                Activator.CreateInstance(type, constructorParameters) : Activator.CreateInstance(type);
            ResolveImportedProperties(instance);
            return instance;
        }

        private void ResolveImportedProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();
            foreach (var property in properties)
            {
                var isImported = property.GetCustomAttribute<ImportAttribute>() != null;
                if (isImported)
                {
                    var propertyValue = Resolve(property.PropertyType);
                    property.SetValue(instance, propertyValue);
                }
            }
        }

        private IEnumerable<object> GetConstructorParameters(Type type)
        {
            var constructorInfo = type.GetConstructors().First();
            var parameters = constructorInfo.GetParameters();
            foreach (var parameter in parameters)
            {
                yield return Resolve(parameter.ParameterType);
            }
        }
    }
}

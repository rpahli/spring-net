#region License

/*
 * Copyright © 2010-2011 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spring.Core;
using Spring.Logging;
using Spring.Objects.Factory.Config;

namespace Spring.Objects.Factory.Attributes
{
    /// <summary>
    ///     <see cref="InitDestroyAttributeObjectPostProcessor" /> implementation
    ///     that invokes attributed init and destroy methods. Allows for an attributation
    ///     alternative to Spring's <see cref="IInitializingObject" /> and
    ///     <see cref="IDisposable" /> callback interfaces.
    ///     Invoke and destroy annotations may be applied to methods of any visibility:
    ///     public, protected, or private. Multiple such methods
    ///     may be annotated, but it is recommended to only annotate one single
    ///     init method and destroy method, respectively.
    /// </summary>
    public class InitDestroyAttributeObjectPostProcessor : IDestructionAwareObjectPostProcessor, IObjectFactoryAware,
        IOrdered
    {
        private static readonly ILogger logger = LogManager.GetLogger<InitDestroyAttributeObjectPostProcessor>();
        private readonly IDictionary<string, LifecycleLifecycleMetadata> lifecycleMetadataCache;

        private IConfigurableListableObjectFactory objectFactory;

        /// <summary>
        ///     Creates InitDestroy Post Processor with default attribute types of
        ///     <see cref="PostConstructAttribute" />
        /// </summary>
        public InitDestroyAttributeObjectPostProcessor()
        {
            InitAttributeType = typeof(PostConstructAttribute);
            DestroyAttributeType = typeof(PreDestroyAttribute);

            lifecycleMetadataCache = new Dictionary<string, LifecycleLifecycleMetadata>();
        }

        /// <summary>
        ///     Specify the init attribute to check for, indicating initialization
        ///     methods to call after configuration of an object.
        /// </summary>
        public Type InitAttributeType { get; set; }

        /// <summary>
        ///     Specify the destroy attribute to check for, indicating disposal
        ///     methods to call before object is destroyed
        /// </summary>
        public Type DestroyAttributeType { get; set; }

        /// <summary>
        ///     Applies PostConstruct init method initialisation if instance is attributed
        /// </summary>
        /// <param name="instance">
        ///     The new object instance.
        /// </param>
        /// <param name="name">
        ///     The name of the object.
        /// </param>
        /// <returns>
        ///     The object instance to use, either the original or a wrapped one.
        /// </returns>
        /// <exception cref="T:Spring.Objects.ObjectsException">
        ///     In case of errors.
        /// </exception>
        public object PostProcessBeforeInitialization(object instance, string name)
        {
            try
            {
                LifecycleLifecycleMetadata metadata = FindLifecycleMetadata(instance.GetType(), name);
                metadata.InvokeInitMethods(instance, name);
            }
            catch (Exception e)
            {
                throw new ObjectCreationException(name, "Couldn't invoke init method", e);
            }

            return instance;
        }

        /// <summary>
        ///     No special post processing after initialization
        /// </summary>
        public object PostProcessAfterInitialization(object instance, string objectName)
        {
            return instance;
        }

        /// <summary>
        ///     Executed PreDestroy methods in given order for provided instance
        /// </summary>
        /// <param name="instance">The new object instance.</param>
        /// <param name="name">The name of the object.</param>
        /// <exception cref="T:Spring.Objects.ObjectsException">
        ///     In case of errors.
        /// </exception>
        public void PostProcessBeforeDestruction(object instance, string name)
        {
            try
            {
                LifecycleLifecycleMetadata metadata = FindLifecycleMetadata(instance.GetType(), name);
                metadata.InvokeDestroyMethods(instance, name);
            }
            catch (Exception e)
            {
                throw new ObjectsException("Couldn't invoke destroy method", e);
            }
        }

        /// <summary>
        /// </summary>
        public IObjectFactory ObjectFactory
        {
            set { objectFactory = value as IConfigurableListableObjectFactory; }
        }

        /// <summary>
        ///     Return the order value of this object, where a higher value means greater in
        ///     terms of sorting.
        /// </summary>
        /// <remarks>
        ///     <p>
        ///         Normally starting with 0 or 1, with <see cref="F:System.Int32.MaxValue" /> indicating
        ///         greatest. Same order values will result in arbitrary positions for the affected
        ///         objects.
        ///     </p>
        ///     <p>
        ///         Higher value can be interpreted as lower priority, consequently the first object
        ///         has highest priority.
        ///     </p>
        /// </remarks>
        /// <returns>
        ///     The order value.
        /// </returns>
        public int Order { get; } = int.MaxValue;

        private LifecycleLifecycleMetadata FindLifecycleMetadata(Type instanceType, string name)
        {
            LifecycleLifecycleMetadata metadata;
            if (lifecycleMetadataCache.TryGetValue(name, out metadata))
            {
                return metadata;
            }

            lock (lifecycleMetadataCache)
            {
                if (!lifecycleMetadataCache.TryGetValue(name, out metadata))
                {
                    metadata = BuildLifecycleMetadata(instanceType, name);
                    lifecycleMetadataCache.Add(name, metadata);
                }
            }
            return metadata;
        }

        private LifecycleLifecycleMetadata BuildLifecycleMetadata(Type instanceType, string name)
        {
            List<LifecycleElement> initMethods = new List<LifecycleElement>();
            List<LifecycleElement> destroyMethods = new List<LifecycleElement>();

            do
            {
                List<LifecycleElement> curInitMethods = new List<LifecycleElement>();
                List<LifecycleElement> curDestroyMethods = new List<LifecycleElement>();
                MethodInfo[] methods = instanceType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                foreach (MethodInfo methodInfo in methods)
                {
                    PostConstructAttribute initAttribute =
                        Attribute.GetCustomAttribute(methodInfo, InitAttributeType) as PostConstructAttribute;
                    if (initAttribute != null && methodInfo.DeclaringType == instanceType)
                    {
                        logger.Debug($"Found init method on class [{instanceType.Name}]: {methodInfo.Name}");

                        curInitMethods.Add(new LifecycleElement(methodInfo, initAttribute.Order));
                    }

                    PreDestroyAttribute destroyAttribute =
                        Attribute.GetCustomAttribute(methodInfo, DestroyAttributeType) as PreDestroyAttribute;
                    if (destroyAttribute != null && methodInfo.DeclaringType == instanceType)
                    {
                        logger.Debug($"Found destroy method on class [{instanceType.Name}]: {methodInfo.Name}");

                        curDestroyMethods.Add(new LifecycleElement(methodInfo, destroyAttribute.Order));
                    }
                }

                initMethods.InsertRange(0, curInitMethods.OrderBy(e => e.Order));
                destroyMethods.InsertRange(0, curDestroyMethods.OrderBy(e => e.Order));
                instanceType = instanceType.BaseType;
            } while (instanceType != null && instanceType != typeof(object));

            IObjectDefinition objectDef = objectFactory.GetObjectDefinition(name);
            LifecycleLifecycleMetadata metadata = new LifecycleLifecycleMetadata(initMethods, destroyMethods);
            metadata.CheckConfigMembers(objectDef);

            return metadata;
        }

        private class LifecycleLifecycleMetadata
        {
            private readonly IList<LifecycleElement> destroyMethods;
            private readonly IList<LifecycleElement> initMethods;

            public LifecycleLifecycleMetadata(IList<LifecycleElement> initMethods,
                IList<LifecycleElement> destroyMethods)
            {
                this.initMethods = initMethods;
                this.destroyMethods = destroyMethods;
            }

            public void CheckConfigMembers(IObjectDefinition objectDef)
            {
                lock (initMethods)
                {
                    for (int i = 0; i < initMethods.Count; i++)
                        if (!initMethods[i].CheckConfig(objectDef))
                        {
                            initMethods.Remove(initMethods[i]);
                        }
                }
                lock (destroyMethods)
                {
                    for (int i = 0; i < destroyMethods.Count; i++)
                        if (!destroyMethods[i].CheckConfig(objectDef))
                        {
                            destroyMethods.Remove(destroyMethods[i]);
                        }
                }
            }

            public void InvokeInitMethods(object instance, string objectName)
            {
                foreach (LifecycleElement lifecycleElement in initMethods)
                    lifecycleElement.Invoke(instance, objectName);
            }

            public void InvokeDestroyMethods(object instance, string objectName)
            {
                foreach (LifecycleElement lifecycleElement in destroyMethods)
                    lifecycleElement.Invoke(instance, objectName);
            }
        }

        private class LifecycleElement
        {
            private readonly MethodInfo method;

            public LifecycleElement(MethodInfo method, int order)
            {
                this.method = method;
                Order = order;
            }

            public int Order { get; }

            public bool CheckConfig(IObjectDefinition objectDef)
            {
                if (method.Name == objectDef.InitMethodName || method.Name == objectDef.DestroyMethodName)
                {
                    return false;
                }

                return true;
            }

            public void Invoke(object instance, string objectName)
            {
                logger.Debug($"Invoking init method on object '{objectName}': {method.Name}");

                method.Invoke(instance, new object[] { });
            }
        }
    }
}
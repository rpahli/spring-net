#region License

/*
 * Copyright 2002-2010 the original author or authors.
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

#region Imports

using System;
using System.Collections.Generic;
using Spring.Objects.Factory.Config;

#endregion

namespace Spring.Objects.Factory.Support
{
    /// <summary>
    ///     Programmatic means of constructing a <see cref="IObjectDefinition" /> using the builder pattern.  Intended
    ///     primarily
    ///     for use when implementing custom namespace parsers.
    /// </summary>
    /// <remarks>
    ///     Set methods are used instead of properties, so that chaining of methods can be used to create
    ///     'one-liner'definitions that set multiple properties at one.
    /// </remarks>
    /// <author>Rod Johnson</author>
    /// <author>Rob Harrop</author>
    /// <author>Juergen Hoeller</author>
    /// <author>Mark Pollack (.NET)</author>
    public class ObjectDefinitionBuilder
    {
        #region Constructor(s)

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectDefinitionBuilder" /> class, private
        ///     to force use of factory methods.
        /// </summary>
        private ObjectDefinitionBuilder()
        {
        }

        #endregion

        #region Fields

        private IObjectDefinitionFactory objectDefinitionFactory;

        private int constructorArgIndex;

        #endregion

        #region Factory Methods

        /// <summary>
        ///     Creates a new <see cref="ObjectDefinitionBuilder" /> used to construct a
        ///     <see cref="Spring.Objects.Factory.Support.GenericObjectDefinition" />.
        /// </summary>
        public static ObjectDefinitionBuilder GenericObjectDefinition()
        {
            ObjectDefinitionBuilder builder = new ObjectDefinitionBuilder();
            builder.RawObjectDefinition = new GenericObjectDefinition();
            return builder;
        }

        /// <summary>
        ///     Creates a new <see cref="ObjectDefinitionBuilder" /> used to construct a
        ///     <see cref="Spring.Objects.Factory.Support.GenericObjectDefinition" />.
        /// </summary>
        /// <param name="objectType">the <see cref="Type" /> of the object that the definition is being created for</param>
        public static ObjectDefinitionBuilder GenericObjectDefinition(Type objectType)
        {
            ObjectDefinitionBuilder builder = new ObjectDefinitionBuilder();
            builder.RawObjectDefinition = new GenericObjectDefinition();
            builder.RawObjectDefinition.ObjectType = objectType;
            return builder;
        }

        /// <summary>
        ///     Creates a new <see cref="ObjectDefinitionBuilder" /> used to construct a
        ///     <see cref="Spring.Objects.Factory.Support.GenericObjectDefinition" />.
        /// </summary>
        /// <param name="objectTypeName">the name of the <see cref="Type" /> of the object that the definition is being created for</param>
        public static ObjectDefinitionBuilder GenericObjectDefinition(string objectTypeName)
        {
            ObjectDefinitionBuilder builder = new ObjectDefinitionBuilder();
            builder.RawObjectDefinition = new GenericObjectDefinition();
            builder.RawObjectDefinition.ObjectTypeName = objectTypeName;
            return builder;
        }

        /// <summary>
        ///     Create a new <code>ObjectDefinitionBuilder</code> used to construct a root object definition.
        /// </summary>
        /// <param name="objectDefinitionFactory">The object definition factory.</param>
        /// <param name="objectTypeName">The type name of the object.</param>
        /// <returns>A new <code>ObjectDefinitionBuilder</code> instance.</returns>
        public static ObjectDefinitionBuilder RootObjectDefinition(IObjectDefinitionFactory objectDefinitionFactory,
            string objectTypeName)
        {
            return RootObjectDefinition(objectDefinitionFactory, objectTypeName, null);
        }

        /// <summary>
        ///     Create a new <code>ObjectDefinitionBuilder</code> used to construct a root object definition.
        /// </summary>
        /// <param name="objectDefinitionFactory">The object definition factory.</param>
        /// <param name="objectTypeName">Name of the object type.</param>
        /// <param name="factoryMethodName">Name of the factory method.</param>
        /// <returns>A new <code>ObjectDefinitionBuilder</code> instance.</returns>
        public static ObjectDefinitionBuilder RootObjectDefinition(IObjectDefinitionFactory objectDefinitionFactory,
            string objectTypeName,
            string factoryMethodName)
        {
            ObjectDefinitionBuilder builder = new ObjectDefinitionBuilder();

            builder.objectDefinitionFactory = objectDefinitionFactory;

            // Pass in null for parent name and also AppDomain to force object definition to be register by name and not type.
            builder.RawObjectDefinition =
                objectDefinitionFactory.CreateObjectDefinition(objectTypeName, null, null);

            builder.RawObjectDefinition.FactoryMethodName = factoryMethodName;

            return builder;
        }

        /// <summary>
        ///     Create a new <code>ObjectDefinitionBuilder</code> used to construct a root object definition.
        /// </summary>
        /// <param name="objectDefinitionFactory">The object definition factory.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>A new <code>ObjectDefinitionBuilder</code> instance.</returns>
        public static ObjectDefinitionBuilder RootObjectDefinition(IObjectDefinitionFactory objectDefinitionFactory,
            Type objectType)
        {
            return RootObjectDefinition(objectDefinitionFactory, objectType, null);
        }

        /// <summary>
        ///     Create a new <code>ObjectDefinitionBuilder</code> used to construct a root object definition.
        /// </summary>
        /// <param name="objectDefinitionFactory">The object definition factory.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="factoryMethodName">Name of the factory method.</param>
        /// <returns>A new <code>ObjectDefinitionBuilder</code> instance.</returns>
        public static ObjectDefinitionBuilder RootObjectDefinition(IObjectDefinitionFactory objectDefinitionFactory,
            Type objectType, string factoryMethodName)
        {
            ObjectDefinitionBuilder builder = new ObjectDefinitionBuilder();

            builder.objectDefinitionFactory = objectDefinitionFactory;

            builder.RawObjectDefinition =
                objectDefinitionFactory.CreateObjectDefinition(objectType.FullName, null, AppDomain.CurrentDomain);

            builder.RawObjectDefinition.ObjectType = objectType;
            builder.RawObjectDefinition.FactoryMethodName = factoryMethodName;
            return builder;
        }

        /// <summary>
        ///     Create a new <code>ObjectDefinitionBuilder</code> used to construct a child object definition..
        /// </summary>
        /// <param name="objectDefinitionFactory">The object definition factory.</param>
        /// <param name="parentObjectName">Name of the parent object.</param>
        /// <returns></returns>
        public static ObjectDefinitionBuilder ChildObjectDefinition(IObjectDefinitionFactory objectDefinitionFactory,
            string parentObjectName)
        {
            ObjectDefinitionBuilder builder = new ObjectDefinitionBuilder();

            builder.objectDefinitionFactory = objectDefinitionFactory;

            builder.RawObjectDefinition =
                objectDefinitionFactory.CreateObjectDefinition(null, parentObjectName, AppDomain.CurrentDomain);

            return builder;
        }

        #endregion


        #region Properties

        /// <summary>
        ///     Gets the current object definition in its raw (unvalidated) form.
        /// </summary>
        /// <value>The raw object definition.</value>
        public AbstractObjectDefinition RawObjectDefinition { get; private set; }

        /// <summary>
        ///     Validate and gets the object definition.
        /// </summary>
        /// <value>The object definition.</value>
        public AbstractObjectDefinition ObjectDefinition
        {
            get
            {
                RawObjectDefinition.Validate();
                return RawObjectDefinition;
            }
        }

        #endregion

        #region Methods

        //TODO add expression support.

        /// <summary>
        ///     Adds the property value under the given name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder AddPropertyValue(string name, object value)
        {
            RawObjectDefinition.PropertyValues.Add(new PropertyValue(name, value));
            return this;
        }

        /// <summary>
        ///     Adds a reference to the specified object name under the property specified.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder AddPropertyReference(string name, string objectName)
        {
            RawObjectDefinition.PropertyValues.Add(new PropertyValue(name, new RuntimeObjectReference(objectName)));
            return this;
        }


        /// <summary>
        ///     Adds an index constructor arg value.  The current index is tracked internally and all addtions are
        ///     at the present point
        /// </summary>
        /// <param name="value">The constructor arg value.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder AddConstructorArg(object value)
        {
            RawObjectDefinition.ConstructorArgumentValues.AddIndexedArgumentValue(constructorArgIndex++, value);
            return this;
        }

        /// <summary>
        ///     Adds a reference to the named object as a constructor argument.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns></returns>
        public ObjectDefinitionBuilder AddConstructorArgReference(string objectName)
        {
            return AddConstructorArg(new RuntimeObjectReference(objectName));
        }


        /// <summary>
        ///     Sets the name of the factory method to use for this definition.
        /// </summary>
        /// <param name="factoryMethod">The factory method.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetFactoryMethod(string factoryMethod)
        {
            RawObjectDefinition.FactoryMethodName = factoryMethod;
            return this;
        }

        /// <summary>
        ///     Sets the name of the factory object to use for this definition.
        /// </summary>
        /// <param name="factoryObject">The factory object.</param>
        /// <param name="factoryMethod">The factory method.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetFactoryObject(string factoryObject, string factoryMethod)
        {
            RawObjectDefinition.FactoryObjectName = factoryObject;
            RawObjectDefinition.FactoryMethodName = factoryMethod;
            return this;
        }

        /// <summary>
        ///     Sets whether or not this definition describes a singleton object.
        /// </summary>
        /// <param name="singleton">if set to <c>true</c> [singleton].</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetSingleton(bool singleton)
        {
            RawObjectDefinition.IsSingleton = singleton;
            return this;
        }

        /// <summary>
        ///     Sets whether objects or not this definition is abstract.
        /// </summary>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetAbstract(bool flag)
        {
            RawObjectDefinition.IsAbstract = flag;
            return this;
        }

        /// <summary>
        ///     Sets whether objects for this definition should be lazily initialized or not.
        /// </summary>
        /// <param name="lazy">if set to <c>true</c> [lazy].</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetLazyInit(bool lazy)
        {
            RawObjectDefinition.IsLazyInit = lazy;
            return this;
        }

        /// <summary>
        ///     Sets the autowire mode for this definition.
        /// </summary>
        /// <param name="autowireMode">The autowire mode.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetAutowireMode(AutoWiringMode autowireMode)
        {
            RawObjectDefinition.AutowireMode = autowireMode;
            return this;
        }

        /// <summary>
        ///     Sets the autowire candidate value for this definition.
        /// </summary>
        /// <param name="autowireCandidate">The autowire candidate value</param>
        /// <returns></returns>
        public ObjectDefinitionBuilder SetAutowireCandidate(bool autowireCandidate)
        {
            RawObjectDefinition.IsAutowireCandidate = autowireCandidate;
            return this;
        }

        /// <summary>
        ///     Sets the primary value for this definition.
        /// </summary>
        /// <param name="primary">If object is primary</param>
        /// <returns></returns>
        public ObjectDefinitionBuilder SetPrimary(bool primary)
        {
            RawObjectDefinition.IsPrimary = primary;
            return this;
        }

        /// <summary>
        ///     Sets the dependency check mode for this definition.
        /// </summary>
        /// <param name="dependencyCheck">The dependency check.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetDependencyCheck(DependencyCheckingMode dependencyCheck)
        {
            RawObjectDefinition.DependencyCheck = dependencyCheck;
            return this;
        }


        /// <summary>
        ///     Sets the name of the destroy method for this definition.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetDestroyMethodName(string methodName)
        {
            RawObjectDefinition.DestroyMethodName = methodName;
            return this;
        }

        /// <summary>
        ///     Sets the name of the init method for this definition.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetInitMethodName(string methodName)
        {
            RawObjectDefinition.InitMethodName = methodName;
            return this;
        }

        /// <summary>
        ///     Sets the resource description for this definition.
        /// </summary>
        /// <param name="resourceDescription">The resource description.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder SetResourceDescription(string resourceDescription)
        {
            RawObjectDefinition.ResourceDescription = resourceDescription;
            return this;
        }

        /// <summary>
        ///     Adds the specified object name to the list of objects that this definition depends on.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>The current <code>ObjectDefinitionBuilder</code>.</returns>
        public ObjectDefinitionBuilder AddDependsOn(string objectName)
        {
            if (RawObjectDefinition.DependsOn == null)
            {
                RawObjectDefinition.DependsOn = new[] {objectName};
            }
            else
            {
                List<string> arrayList = new List<string>();
                arrayList.AddRange(RawObjectDefinition.DependsOn);
                arrayList.AddRange(new[] {objectName});
                RawObjectDefinition.DependsOn = arrayList;
            }
            return this;
        }

        #endregion
    }
}
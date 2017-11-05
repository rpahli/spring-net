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

using System.ComponentModel;
using System.Xml;
using Spring.Context.Attributes;
using Spring.Context.Attributes.TypeFilters;
using Spring.Logging;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;
using Spring.Objects.Factory.Xml;

namespace Spring.Context.Config
{
    /// <summary>
    ///     Parses ObjectDefinitions from classes identified by an <see cref="AssemblyObjectDefinitionScanner" />.
    /// </summary>
    public class ComponentScanObjectDefinitionParser : IObjectDefinitionParser
    {
        private const string ATTRIBUTE_CONFIG_ATTRIBUTE = "attribute-config";

        private const string NAME_GENERATOR_ATTRIBUTE = "name-generator";

        private const string BASE_ASSEMBLIES_ATTRIBUTE = "base-assemblies";

        private const string EXCLUDE_FILTER_ELEMENT = "exclude-filter";

        private const string INCLUDE_FILTER_ELEMENT = "include-filter";

        private static readonly ILogger Logger = LogManager.GetLogger<ComponentScanObjectDefinitionParser>();


        /// <summary>
        ///     Parse the specified XmlElement and register the resulting
        ///     ObjectDefinitions with the <see cref="P:Spring.Objects.Factory.Xml.ParserContext.Registry" />
        ///     IObjectDefinitionRegistry
        ///     embedded in the supplied <see cref="T:Spring.Objects.Factory.Xml.ParserContext" />
        /// </summary>
        /// <param name="element">The element to be parsed.</param>
        /// <param name="parserContext">
        ///     The object encapsulating the current state of the parsing process.
        ///     Provides access to a IObjectDefinitionRegistry
        /// </param>
        /// <returns>The primary object definition.</returns>
        /// <remarks>
        ///     <p>
        ///         This method is never invoked if the parser is namespace aware
        ///         and was called to process the root node.
        ///     </p>
        /// </remarks>
        public IObjectDefinition ParseElement(XmlElement element, ParserContext parserContext)
        {
            AssemblyObjectDefinitionScanner scanner = ConfigureScanner(parserContext, element);
            IObjectDefinitionRegistry registry = parserContext.Registry;

            // Actually scan for objects definitions and register them.
            scanner.ScanAndRegisterTypes(registry);
            RegisterComponents(element, registry);

            return null;
        }

        /// <summary>
        ///     Configures the scanner.
        /// </summary>
        /// <param name="parserContext">The parser context.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        protected virtual AssemblyObjectDefinitionScanner ConfigureScanner(ParserContext parserContext,
            XmlElement element)
        {
            AssemblyObjectDefinitionScanner scanner = new AssemblyObjectDefinitionScanner();

            ParseBaseAssembliesAttribute(scanner, element);
            ParseNameGeneratorAttribute(scanner, element);
            ParseTypeFilters(scanner, element);

            scanner.Defaults = parserContext.ParserHelper.Defaults;

            return scanner;
        }

        private void ParseBaseAssembliesAttribute(AssemblyObjectDefinitionScanner scanner, XmlElement element)
        {
            string baseAssemblies = element.GetAttribute(BASE_ASSEMBLIES_ATTRIBUTE);

            if (string.IsNullOrEmpty(baseAssemblies))
            {
                return;
            }

            foreach (string baseAssembly in baseAssemblies.Split(','))
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("Start With Assembly Filter: " + baseAssembly);
                }

                scanner.WithAssemblyFilter(assy => assy.FullName.StartsWith(baseAssembly));
            }
        }

        private void ParseNameGeneratorAttribute(AssemblyObjectDefinitionScanner scanner, XmlElement element)
        {
            string nameGeneratorString = element.GetAttribute(NAME_GENERATOR_ATTRIBUTE);
            IObjectNameGenerator nameGenerator = CustomTypeFactory.GetNameGenerator(nameGeneratorString);
            if (nameGenerator != null)
            {
                Logger.Debug($"Use NameTable Generator: {nameGeneratorString}");
                scanner.ObjectNameGenerator = nameGenerator;
            }
        }

        private void ParseTypeFilters(AssemblyObjectDefinitionScanner scanner, XmlElement element)
        {
            foreach (XmlNode node in element.ChildNodes)
                if (node.Name.Contains(INCLUDE_FILTER_ELEMENT))
                {
                    ITypeFilter filter = CreateTypeFilter(node);

                    Logger.Debug($"Inlude Filter: {filter}");
                    scanner.WithIncludeFilter(filter);
                }
                else if (node.Name.Contains(EXCLUDE_FILTER_ELEMENT))
                {
                    ITypeFilter filter = CreateTypeFilter(node);

                    Logger.Debug($"Exclude Filter: {filter}");
                    scanner.WithExcludeFilter(filter);
                }
        }

        private void RegisterComponents(XmlElement element, IObjectDefinitionRegistry registry)
        {
            bool attributeConfig = true;
            string attr = element.GetAttribute(ATTRIBUTE_CONFIG_ATTRIBUTE);
            if (attr != null)
            {
                bool.TryParse(attr, out attributeConfig);
            }
            if (attributeConfig)
            {
                AttributeConfigUtils.RegisterAttributeConfigProcessors(registry);
            }
        }

        private ITypeFilter CreateTypeFilter(XmlNode node)
        {
            string type = node.Attributes["type"].Value;
            string expression = node.Attributes["expression"].Value;

            switch (type)
            {
                case "regex":
                    return new RegexPatternTypeFilter(expression);
                case "attribute":
                    return new AttributeTypeFilter(expression);
                case "assignable":
                    return new AssignableTypeFilter(expression);
                case "custom":
                    return CustomTypeFactory.GetTypeFilter(expression);
                default:
                    throw new InvalidEnumArgumentException(string.Format("Filter type {0} is not defined", type));
            }
        }
    }
}
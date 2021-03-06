﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;
using System.Xml.XPath;

using NXKit.Composition;

namespace NXKit.XForms
{

    /// <summary>
    /// Provides the XForms 'delete' properties.
    /// </summary>
    [Extension(typeof(DeleteProperties), "{http://www.w3.org/2002/xforms}delete")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    public class DeleteProperties :
        ElementExtension
    {

        readonly DeleteAttributes attributes;
        readonly Extension<EvaluationContextResolver> context;
        readonly Lazy<XPathExpression> at;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributes"></param>
        /// <param name="context"></param>
        [ImportingConstructor]
        public DeleteProperties(
            XElement element,
            DeleteAttributes attributes,
            Extension<EvaluationContextResolver> context)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            Contract.Requires<ArgumentNullException>(attributes != null);
            Contract.Requires<ArgumentNullException>(context != null);

            this.attributes = attributes;
            this.context = context;

            this.at = new Lazy<XPathExpression>(() =>
                !string.IsNullOrEmpty(attributes.At) ? context.Value.Context.CompileXPath(element, attributes.At) : null);
        }

        public XPathExpression At
        {
            get { return at.Value; }
        }

    }

}
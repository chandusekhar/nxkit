﻿using System;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace NXKit.XForms
{

    /// <summary>
    /// Provides the standard XForms binding attributes.
    /// </summary>
    public class BindingAttributes :
        AttributeAccessor
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public BindingAttributes(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

        /// <summary>
        /// Gets the 'ref' attribute.
        /// </summary>
        public XAttribute RefAttribute
        {
            get { return GetAttribute("ref"); }
        }

        /// <summary>
        /// Gets the 'ref' attribute values.
        /// </summary>
        public string Ref
        {
            get { return GetAttributeValue("ref"); }
        }

        /// <summary>
        /// Gets the 'nodeset' attribute.
        /// </summary>
        public XAttribute NodeSetAttribute
        {
            get { return GetAttribute("nodeset"); }
        }

        /// <summary>
        /// Gets the 'nodeset' attribute values.
        /// </summary>
        public string NodeSet
        {
            get { return GetAttributeValue("nodeset"); }
        }

        /// <summary>
        /// Gets the 'bind' attribute value.
        /// </summary>
        public XAttribute BindAttribute
        {
            get { return GetAttribute("bind"); }
        }

        /// <summary>
        /// Gets the 'bind' attribute value.
        /// </summary>
        public string Bind
        {
            get { return GetAttributeValue("bind"); }
        }

    }

}
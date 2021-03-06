﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;
using NXKit.DOMEvents;
using NXKit.Scripting;
using NXKit.Xml;
using NXKit.XMLEvents;

namespace NXKit.XMLEvents
{

    /// <summary>
    /// XML Events 2 script tag.
    /// </summary>
    [Extension("{http://www.w3.org/2001/xml-events}script")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    [Remote]
    public class Script :
        ElementExtension,
        IEventHandler
    {

        readonly Lazy<IDocumentScript> documentScript;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        [ImportingConstructor]
        public Script(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);

            this.documentScript = new Lazy<IDocumentScript>(() => element.Document.Interface<IDocumentScript>());
        }

        public string Type
        {
            get { return (string)Element.Attribute("type"); }
        }

        public string Src
        {
            get { return (string)Element.Attribute("src"); }
        }

        public string Charset
        {
            get { return (string)Element.Attribute("charset"); }
        }

        public string Defer
        {
            get { return (string)Element.Attribute("defer"); }
        }

        public string Implements
        {
            get { return (string)Element.Attribute("implements "); }
        }

        public void Invoke()
        {
            try
            {
                documentScript.Value.Execute(Type, Element.Value);
            }
            catch (ScriptException e)
            {
                throw new DOMTargetEventException(Element, Events.Error, e);
            }
        }

        public void HandleEvent(Event evt)
        {
            Invoke();
        }

    }

}

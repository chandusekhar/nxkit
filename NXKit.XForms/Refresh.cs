﻿using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

using NXKit.Composition;
using NXKit.DOMEvents;
using NXKit.XMLEvents;

namespace NXKit.XForms
{

    [Extension("{http://www.w3.org/2002/xforms}refresh")]
    [PartMetadata(ScopeCatalog.ScopeMetadataKey, Scope.Object)]
    public class Refresh :
        ElementExtension,
        IEventHandler
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        [ImportingConstructor]
        public Refresh(XElement element)
            : base(element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
        }

        public void HandleEvent(Event ev)
        {
            throw new NotImplementedException();
        }

    }

}

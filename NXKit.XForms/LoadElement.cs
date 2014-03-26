﻿using System.Xml.Linq;
using NXKit.DOMEvents;

namespace NXKit.XForms
{

    [Element("load")]
    public class LoadElement : 
        SingleNodeBindingElement,
        IActionElement
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public LoadElement(XElement element)
            : base(element)
        {

        }

        public void Handle(Event ev)
        {
            Module.InvokeAction(this);
        }

        public void Invoke()
        {
            // ensure values are up to date
            Refresh();


        }

    }

}
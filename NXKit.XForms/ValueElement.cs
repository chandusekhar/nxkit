﻿using System;
using System.Xml.Linq;
using NXKit.Util;

namespace NXKit.XForms
{

    [Element("value")]
    public class ValueElement : 
        SingleNodeBindingElement, 
        ISelectableNode
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="element"></param>
        public ValueElement(XElement element)
            : base(element)
        {

        }

        /// <summary>
        /// Gets the text value associated with this value visual.
        /// </summary>
        public string InlineContent
        {
            get { return Xml.Value.TrimToNull(); }
        }

        /// <summary>
        /// Obtains the simple value to be set.
        /// </summary>
        /// <returns></returns>
        private string GetNewValue()
        {
            if (Binding != null &&
                Binding.Value != null)
                return Binding.Value;
            else if (InlineContent != null)
                return InlineContent;
            else
                return "";
        }

        public void Select(SingleNodeBindingElement visual)
        {
            if (visual.Binding == null ||
                visual.Binding.ModelItem == null)
                throw new InvalidOperationException();

            if (Binding != null &&
                Binding.Value == null)
                throw new InvalidOperationException();

            visual.Binding.SetValue(GetNewValue());
        }

        public void Deselect(SingleNodeBindingElement visual)
        {

        }

        public bool Selected(SingleNodeBindingElement visual)
        {
            if (visual.Binding == null ||
                visual.Binding.ModelItem == null ||
                visual.Binding.Value == null)
                return false;

            // our value matches the current value?
            return visual.Binding.Value == GetNewValue();
        }

        public override void Refresh()
        {
            base.Refresh();

            if (Binding != null &&
                Binding.Value == null)
            {
                // binding, if it exists, must point to a value node
                // DispatchEvent<XFormsBindingExceptionEvent>();
                return;
            }
        }

        public int GetValueHashCode()
        {
            if (Binding != null &&
                Binding.Value == null)
                return 0;

            return GetNewValue().GetHashCode();
        }
    }

}
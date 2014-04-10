﻿using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

using NXKit.DOMEvents;
using NXKit.Xml;

namespace NXKit.XForms
{

    /// <summary>
    /// Provides a wrapper for a model item that controls access to the underlying model item properties.
    /// </summary>
    public class ModelItem
    {

        readonly XObject xml;
        readonly Lazy<Model> model;
        readonly Lazy<Instance> instance;
        readonly Lazy<ModelItemState> state;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xml"></param>
        public ModelItem(XObject xml)
        {
            Contract.Requires<ArgumentNullException>(xml != null);

            this.xml = xml;
            this.model = new Lazy<Model>(() => xml.Document.Annotation<Model>());
            this.instance = new Lazy<Instance>(() => xml.Document.Annotation<Instance>());
            this.state = new Lazy<ModelItemState>(() => xml.AnnotationOrCreate<ModelItemState>());
        }

        /// <summary>
        /// Gets a reference to the underlying XML object.
        /// </summary>
        public XObject Xml
        {
            get { return xml; }
        }

        /// <summary>
        /// Gets the state structure for the given model item.
        /// </summary>
        /// <returns></returns>
        internal ModelItemState State
        {
            get { return state.Value; }
        }

        /// <summary>
        /// Gets the model element of the specified model item.
        /// </summary>
        /// <returns></returns>
        public Model Model
        {
            get { return model.Value; }
        }

        /// <summary>
        /// Gets the instance element of the specified model item.
        /// </summary>
        /// <returns></returns>
        public Instance Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// Gets or sets the type of the given model item.
        /// </summary>
        /// <returns></returns>
        public XName ItemType
        {
            get { return GetItemType(); }
            set { SetItemType(value); }
        }

        XName GetItemType()
        {
            return State.Type ?? NXKit.XmlSchemaConstants.XMLSchema + "string";
        }

        /// <summary>
        /// Sets the type of the given model item.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="item"></param>
        /// <param name="type"></param>
        public void SetItemType(XName type)
        {
            State.Type = type;
        }

        /// <summary>
        /// Gets whether the given model item is required.
        /// </summary>
        /// <returns></returns>
        public bool Required
        {
            get { return GetRequired(); }
        }

        bool GetRequired()
        {
            return State.Required ?? false;
        }

        /// <summary>
        /// Gets whether the given model item is read-only.
        /// </summary>
        /// <returns></returns>
        public bool ReadOnly
        {
            get { return GetReadOnly(); }
        }

        bool GetReadOnly()
        {
            return xml.AncestorsAndSelf().Any(i => i.AnnotationOrCreate<ModelItemState>().ReadOnly ?? false);
        }

        /// <summary>
        /// Gets whether the given model item is relevant.
        /// </summary>
        /// <returns></returns>
        public bool Relevant
        {
            get { return GetRelevant(); }
        }

        bool GetRelevant()
        {
            return xml.AncestorsAndSelf().All(i => i.AnnotationOrCreate<ModelItemState>().Relevant ?? true);
        }

        /// <summary>
        /// Gets whether the given model item's constraint value is currently valid.
        /// </summary>
        /// <returns></returns>
        public bool Constraint
        {
            get { return GetConstraint(); }
        }

        bool GetConstraint()
        {
            return State.Constraint ?? true;
        }

        /// <summary>
        /// Gets whether the given model item is currently valid.
        /// </summary>
        /// <returns></returns>
        public bool Valid
        {
            get { return GetValid(); }
        }

        bool GetValid()
        {
            return State.Valid ?? true;
        }

        /// <summary>
        /// Gets or sets the value of the simple node.
        /// </summary>
        public string Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        /// <summary>
        /// Implements the getter for Value.
        /// </summary>
        /// <returns></returns>
        string GetValue()
        {
            if (xml is XElement)
                return !((XElement)xml).HasElements ? ((XElement)xml).Value : null;
            else if (xml is XAttribute)
                return ((XAttribute)xml).Value;
            else
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Implements the setter for Value.
        /// </summary>
        /// <param name="newValue"></param>
        void SetValue(string newValue)
        {
            Contract.Requires<ArgumentNullException>(newValue != null);

            // nothing changed
            if (newValue == GetValue())
                return;

            // This action has no effect if the Single Item Binding does not select an instance data node or if a 
            // readonly instance data node is selected.
            if (ReadOnly)
                return;

            if (Xml is XElement)
            {
                // An xforms-binding-exception occurs if the Single Item Binding indicates a node whose content is not
                // simpleContent (i.e., a node that has element children).
                var target = (XElement)Xml;
                if (target.HasElements)
                    throw new DOMEventException(Events.BindingException);

                ((XElement)Xml).SetValue(newValue);
            }
            else if (Xml is XAttribute)
            {
                var target = (XAttribute)Xml;
                ((XAttribute)Xml).SetValue(newValue);
            }
            else if (Xml is XText)
            {
                var target = (XText)Xml;
                target.Value = newValue;
            }
            else
                throw new InvalidOperationException();

            // trigger recalculate event to collect new value
            Model.State.Recalculate = true;
            Model.State.Revalidate = true;
            Model.State.Refresh = true;
        }

        /// <summary>
        /// Gets the element value of the given model item.
        /// </summary>
        /// <param name="newElement"></param>
        public XElement Contents
        {
            get { return GetContents(); }
            set { SetContents(value); }
        }

        /// <summary>
        /// Implements the getter for Contents.
        /// </summary>
        /// <returns></returns>
        XElement GetContents()
        {
            Contract.Requires<ArgumentException>(Xml is XElement);

            if (xml is XElement)
                return ((XElement)xml).HasElements ? (XElement)((XElement)xml).FirstNode : null;
            else
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Implements the setter for Contents.
        /// </summary>
        /// <param name="newContents"></param>
        void SetContents(XElement newContents)
        {
            Contract.Requires<ArgumentException>(Xml is XElement);

            throw new NotImplementedException();

            // trigger recalculate event to collect new value
            //Model.State.RecalculateFlag = true;
            //Model.State.RevalidateFlag = true;
            //Model.State.RefreshFlag = true;
        }

        /// <summary>
        /// Applies all of the known values from the given model item to the current one.
        /// </summary>
        /// <param name="item"></param>
        public void Apply(ModelItem item)
        {
            State.Type = item.State.Type;
            State.Relevant = item.State.Relevant;
            State.ReadOnly = item.State.ReadOnly;
            State.Required = item.State.Required;
            State.Constraint = item.State.Constraint;
            State.Valid = item.State.Valid;
        }

        /// <summary>
        /// Creates a <see cref="XPathNavigator"/> for the given model item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal XPathNavigator CreateNavigator()
        {
            var attr = xml as XAttribute;
            if (attr != null)
            {
                // navigator needs to be created on parent, and navigated to attribute
                var nav = attr.Parent.CreateNavigator();
                nav.MoveToAttribute(attr.Name.LocalName, attr.Name.NamespaceName);
                return nav;
            }

            var element = xml as XElement;
            if (element != null)
                return element.CreateNavigator();

            throw new InvalidOperationException();
        }

    }

}

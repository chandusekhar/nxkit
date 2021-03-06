﻿using System;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace NXKit.DOMEvents
{

    /// <summary>
    /// Signifies a DOM event to be raised by the specified DOM element.
    /// </summary>
    public class DOMTargetEventException :
        DOMEventException
    {

        readonly XElement target;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <param name="contextInfo"></param>
        public DOMTargetEventException(XElement target, string type, object contextInfo)
            : base(type, contextInfo)
        {
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<ArgumentNullException>(type != null);

            this.target = target;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        public DOMTargetEventException(XElement target, string type)
            : base(type)
        {
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<ArgumentNullException>(type != null);

            this.target = target;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public DOMTargetEventException(XElement target, string type, string message)
            : base(type, message)
        {
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<ArgumentNullException>(type != null);

            this.target = target;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <param name="innerException"></param>
        public DOMTargetEventException(XElement target, string type, Exception innerException)
            : base(type, innerException)
        {
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(innerException != null);

            this.target = target;
        }

        /// <summary>
        /// Gets the target of the event to be raised.
        /// </summary>
        public XElement Target
        {
            get { return target; }
        }

    }

}

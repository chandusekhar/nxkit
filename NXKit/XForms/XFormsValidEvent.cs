﻿namespace NXKit.XForms
{

    public class XFormsValidEvent : XFormsEvent
    {

        public static readonly string Name = "xforms-valid";

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public XFormsValidEvent()
            : base(Name, true, false)
        {

        }

    }

}
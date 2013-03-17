﻿using NXKit.Layout;

namespace NXKit.XForms.Web.UI
{

    public class FormPage : FormNavigation
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="parent"></param>
        internal FormPage(FormSection parent, PageVisual visual)
            : base(parent, visual)
        {

        }

        public override bool Relevant
        {
            get { return Visual.Relevant; }
        }

    }

}
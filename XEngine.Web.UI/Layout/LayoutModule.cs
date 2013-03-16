﻿using System.ComponentModel.Composition;

using ISIS.Forms.Layout;
using ISIS.Forms.Web.UI.XForms;

namespace ISIS.Forms.Web.UI.Layout
{

    [FormModule]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LayoutModule : FormModule
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="view"></param>
        [ImportingConstructor]
        public LayoutModule([Import(FormModule.ViewParameter)] FormView view)
            : base(view)
        {
            View.VisualControlAdded += View_VisualControlAdded;
        }

        /// <summary>
        /// Invoked when a new <see cref="VisualControl"/> is added.
        /// </summary>
        /// <param name="args"></param>
        private void View_VisualControlAdded(VisualControlAddedEventArgs args)
        {
            var groupCtl = args.Control as GroupControl;
            if (groupCtl != null)
                groupCtl.BeginRender += group_BeginRender;
        }

        private void group_BeginRender(object sender, BeginRenderEventArgs args)
        {
            var groupCtl = sender as GroupControl;
            if (groupCtl == null)
                return;

            var annotation = groupCtl.Visual.Annotations.Get<ImportanceAnnotation>();
            if (annotation == null)
                return;

            switch (annotation.Importance)
            {
                case Importance.High:
                    args.CssClasses.Add("Layout__Importance_High");
                    break;
                case Importance.Low:
                    args.CssClasses.Add("Layout__Importance_Low");
                    break;
            }
        }

    }

}
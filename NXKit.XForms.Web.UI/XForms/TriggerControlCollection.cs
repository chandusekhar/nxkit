﻿using System.Collections.Generic;
using System.Linq;

using NXKit.XForms;

namespace NXKit.XForms.Web.UI.XForms
{

    public class TriggerControlCollection : VisualControlCollection
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="visual"></param>
        public TriggerControlCollection(FormView view, StructuralVisual visual)
            : base(view, visual)
        {

        }

        protected override IEnumerable<Visual> GetVisuals()
        {
            return Visual.OpaqueChildren().OfType<XFormsTriggerVisual>();
        }

    }

}
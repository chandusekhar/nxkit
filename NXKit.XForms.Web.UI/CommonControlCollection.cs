﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.UI;

using NXKit.Web.UI;

namespace NXKit.XForms.Web.UI
{

    /// <summary>
    /// Manages the common XForms visuals.
    /// </summary>
    public class CommonControlCollection : VisualControlCollection
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="visual"></param>
        public CommonControlCollection(View view, ContentVisual visual)
            : base(view, visual)
        {
            Contract.Requires<ArgumentNullException>(view != null);
            Contract.Requires<ArgumentNullException>(visual != null);
        }

        protected override IEnumerable<Visual> GetVisuals()
        {
            // our visuals
            if (LabelVisual != null)
                yield return LabelVisual;
            if (HelpVisual != null)
                yield return HelpVisual;
            if (HintVisual != null)
                yield return HintVisual;
            if (AlertVisual != null)
                yield return AlertVisual;
        }

        /// <summary>
        /// Gets a reference to the nested <see cref="XFormsLabelVisual"/>.
        /// </summary>
        public XFormsLabelVisual LabelVisual { get; private set; }

        /// <summary>
        /// Gets a reference to the label control.
        /// </summary>
        public LabelControl LabelControl
        {
            get { return LabelVisual != null ? (LabelControl)GetOrCreateControl(LabelVisual) : null; }
        }

        /// <summary>
        /// Gets a reference to the nested <see cref="XFormsHelpVisual"/>.
        /// </summary>
        public XFormsHelpVisual HelpVisual { get; private set; }

        /// <summary>
        /// Gets a reference to the help control.
        /// </summary>
        public Control HelpControl
        {
            get { return HelpVisual != null ? (Control)GetOrCreateControl(HelpVisual) : null; }
        }

        /// <summary>
        /// Gets a reference to the nested <see cref="XFormsHintVisual"/>.
        /// </summary>
        public XFormsHintVisual HintVisual { get; private set; }

        /// <summary>
        /// Gets a reference to the hint control.
        /// </summary>
        public Control HintControl
        {
            get { return HintVisual != null ? (Control)GetOrCreateControl(HintVisual) : null; }
        }

        /// <summary>
        /// Gets a reference to the nested <see cref="XFormsAlertVisual"/>.
        /// </summary>
        public XFormsAlertVisual AlertVisual { get; private set; }

        /// <summary>
        /// Gets a reference to the alert control.
        /// </summary>
        public Control AlertControl
        {
            get { return AlertVisual != null ? (Control)GetOrCreateControl(AlertVisual) : null; }
        }

        public override void Update()
        {
            // find visuals
            LabelVisual = Visual.Visuals.OfType<XFormsLabelVisual>().FirstOrDefault();
            HelpVisual = Visual.Visuals.OfType<XFormsHelpVisual>().FirstOrDefault();
            HintVisual = Visual.Visuals.OfType<XFormsHintVisual>().FirstOrDefault();
            AlertVisual = Visual.Visuals.OfType<XFormsAlertVisual>().FirstOrDefault();

            base.Update();
        }

    }


}

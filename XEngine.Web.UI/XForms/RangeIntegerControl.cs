﻿using System;
using System.Linq;
using System.Web.UI.WebControls;

using Telerik.Web.UI;

using ISIS.Forms.XForms;

namespace ISIS.Forms.Web.UI.XForms
{

    public class RangeIntegerControl : VisualControl<XFormsRangeVisual>
    {

        private RadNumericTextBox ctl;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="visual"></param>
        public RangeIntegerControl(FormView view, XFormsRangeVisual visual)
            : base(view, visual)
        {

        }

        /// <summary>
        /// Creates the child control hierarchy.
        /// </summary>
        protected override void CreateChildControls()
        {
            ctl = new RadNumericTextBox();
            ctl.ID = "ctl";
            ctl.Value = BindingUtil.Get<double?>(Visual.Binding);
            ctl.Type = NumericType.Percent;
            ctl.Width = Unit.Pixel(150);
            ctl.ShowSpinButtons = true;
            ctl.IncrementSettings.Step = 5;
            ctl.NumberFormat.DecimalDigits = 0;
            ctl.NumberFormat.NegativePattern = "-n";
            ctl.NumberFormat.PositivePattern = "n";
            ctl.NumberFormat.GroupSeparator = "";
            ctl.TextChanged += ctl_TextChanged;

            var labelVisual = Visual.Children.OfType<XFormsLabelVisual>().FirstOrDefault();
            if (labelVisual != null)
                ctl.Label = FormHelper.LabelToString(labelVisual);

            int start;
            if (int.TryParse(Visual.Start, out start))
                ctl.MinValue = start;

            int end;
            if (int.TryParse(Visual.End, out end))
                ctl.MaxValue = end;

            int step;
            if (int.TryParse(Visual.Step, out step))
                ctl.IncrementSettings.Step = step;

            Controls.Add(ctl);
        }

        private void ctl_TextChanged(object sender, EventArgs args)
        {
            BindingUtil.Set(Visual.Binding, ctl.Value);
        }

    }

}
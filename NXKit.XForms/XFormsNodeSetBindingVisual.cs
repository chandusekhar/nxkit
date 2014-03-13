﻿namespace NXKit.XForms
{

    /// <summary>
    /// Abstract base <see cref="Visual"/> implementation for node-set binding elements.
    /// </summary>
    public abstract class XFormsNodeSetBindingVisual :
        XFormsBindingVisual
    {

        /// <summary>
        /// Provides an evaluation context for children. Node-set bindings provide the first node result.
        /// </summary>
        /// <returns></returns>
        protected override XFormsEvaluationContext CreateEvaluationContext()
        {
            if (Binding != null &&
                Binding.Nodes != null &&
                Binding.Nodes.Length >= 1)
                return new XFormsEvaluationContext(Binding.Context.Model, Binding.Context.Instance, Binding.Nodes[0], 1, Binding.Nodes.Length);

            return null;
        }

        public override void Refresh()
        {
            // rebuild binding
            Binding = Module.ResolveNodeSetBinding(this);

            base.Refresh();

            // rebuild children
            base.InvalidateChildren();
        }

    }

}

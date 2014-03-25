﻿using System.Xml.Linq;

namespace NXKit.XForms
{

    /// <summary>
    /// Serves as a generated item within a repeat.
    /// </summary>
    public class XFormsRepeatItemVisual :
        XFormsVisual,
        IEvaluationContextScope,
        INamingScope,
        IRelevancyScope
    {

        XFormsEvaluationContext context;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xml"></param>
        public XFormsRepeatItemVisual(XElement xml)
            : base(xml)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override string Id
        {
            get { return "NODE" + Module.GetModelItemId(Context, Context.Node); }
        }

        /// <summary>
        /// Obtains the evaluation context for this visual.
        /// </summary>
        public XFormsEvaluationContext Context
        {
            get { return context; }
        }

        /// <summary>
        /// Sets the context to a new value, should only be used by the repeat container.
        /// </summary>
        /// <param name="ec"></param>
        internal void SetContext(XFormsEvaluationContext ec)
        {
            context = ec;

            // ensure module item id is initialized
            Module.GetModelItemId(Context, Context.Node);
        }

        public bool Relevant
        {
            get { return Module.GetModelItemRelevant(Context.Node); }
        }

    }

}

﻿using NXKit.DOMEvents;

namespace NXKit.XForms
{

    [Visual("recalculate")]
    public class XFormsRecalculateVisual : 
        XFormsVisual, 
        IActionVisual
    {

        public void Handle(IEvent ev)
        {
            Invoke();
        }

        public void Invoke()
        {
            var modelAttr = Module.GetAttributeValue(Element, "model");
            if (modelAttr != null)
            {
                var modelVisual = (XFormsModelVisual)ResolveId(modelAttr);
                if (modelVisual != null)
                    Module.RecalculateModel(modelVisual);
                else
                {
                    DispatchEvent<XFormsBindingExceptionEvent>();
                    return;
                }
            }
        }

    }

}

﻿using System.Linq;

namespace NXKit.XForms
{

    [Visual("select1")]
    public class XFormsSelect1Visual :
        XFormsSingleNodeBindingVisual,
        IUiCommon
    {

        bool selectedItemVisualCached;
        XFormsItemVisual selectedItemVisual;

        public bool Incremental
        {
            get { return Module.GetAttributeValue(Element, "incremental") == "true"; }
        }

        public XFormsSelection Selection
        {
            get
            {
                return Module.GetAttributeValue(Element, "selection") == "open" ? XFormsSelection.Open : XFormsSelection.Closed;
            }
        }

        /// <summary>
        /// Gets the currently selected item visual.
        /// </summary>
        public XFormsItemVisual SelectedItemVisual
        {
            get
            {
                if (!selectedItemVisualCached)
                {
                    foreach (var itemVisual in Descendants().OfType<XFormsItemVisual>())
                    {
                        // find selectable visuals underneath item
                        if (itemVisual.Selectable == null)
                            continue;

                        if (itemVisual.Selectable.Selected(this))
                        {
                            selectedItemVisual = itemVisual;
                            break;
                        }
                    }

                    selectedItemVisualCached = true;
                }

                return selectedItemVisual;
            }
        }

        /// <summary>
        /// Sets the selected item.
        /// </summary>
        /// <param name="visual"></param>
        public void SetSelectedItemVisual(XFormsItemVisual visual)
        {
            // deselect current visual
            if (SelectedItemVisual != null &&
                SelectedItemVisual.Selectable != null)
                SelectedItemVisual.Selectable.Deselect(this);

            if (visual.Selectable != null)
            {
                // pre-cache
                selectedItemVisualCached = true;
                selectedItemVisual = visual;

                // store selected item
                GetState<XFormsSelect1State>().SelectedVisualId = visual.UniqueId;

                // apply selection
                selectedItemVisual.Selectable.Select(this);
            }
        }

        /// <summary>
        /// Sets the selected value.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            // deselect current visual
            if (SelectedItemVisual != null &&
                SelectedItemVisual.Selectable != null)
                SelectedItemVisual.Selectable.Deselect(this);

            // clear selected visual state
            selectedItemVisualCached = true;
            selectedItemVisual = null;
            GetState<XFormsSelect1State>().SelectedVisualId = null;

            // set node value
            if (Binding != null && Binding.Node != null)
                Binding.SetValue(value);
        }

        public override void Refresh()
        {
            base.Refresh();

            // clear selected item cache
            selectedItemVisual = null;
            selectedItemVisualCached = false;

            // if no item is selected, attempt to find one
            var selectedItemId = GetState<XFormsSelect1State>().SelectedVisualId;
            if (selectedItemId == null)
            {
                // ensure descendant itemsets are refreshed, kind of a hack
                foreach (var itemSetVisual in Descendants().OfType<XFormsItemSetVisual>())
                    itemSetVisual.Refresh();

                foreach (var itemVisual in Descendants().OfType<XFormsItemVisual>())
                {
                    if (itemVisual.Selectable == null)
                        continue;

                    // is this the current selection?
                    if (itemVisual.Selectable.Selected(this))
                    {
                        // pre-cache
                        selectedItemVisualCached = true;
                        selectedItemVisual = itemVisual;

                        // store selected item
                        GetState<XFormsSelect1State>().SelectedVisualId = itemVisual.UniqueId;

                        break;
                    }
                }
            }
        }

    }

}

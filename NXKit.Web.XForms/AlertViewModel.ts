﻿/// <reference path="XFormsNodeViewModel.ts" />

module NXKit.Web.XForms {

    export class AlertViewModel
        extends NXKit.Web.XForms.XFormsNodeViewModel {

        constructor(context: KnockoutBindingContext, node: Node) {
            super(context, node);
        }

        get Text(): KnockoutComputed<string> {
            return this.ValueAsString;
        }

    }

}
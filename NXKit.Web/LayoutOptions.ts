﻿/// <reference path="Utils.ts" />

module NXKit.Web {

    export class LayoutOptions {

        /**
          * Gets the full set of currently applied layout option args for the given context.
          */
        public static GetArgs(bindingContext: KnockoutBindingContext): any {
            var a: any = {};
            var c = Utils.GetContextItems(bindingContext);
            for (var i = 0; i < c.length; i++)
                if (c[i] instanceof LayoutOptions)
                    a = ko.utils.extend(a, c[i]);

            return a;
        }

        private _args: any;

        constructor(args: any) {
            this._args = args;
        }

        public get Args(): any {
            return this._args;
        }

    }

}
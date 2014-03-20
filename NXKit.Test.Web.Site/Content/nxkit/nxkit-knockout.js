﻿ko.bindingHandlers.nxkit_template = {

    convert_value_accessor: function (valueAccessor, viewModel, bindingContext) {
        return ko.computed(function (_) {

            var data = NXKit.Web.Utils.GetTemplateViewModel(valueAccessor, viewModel, bindingContext);
            var opts = NXKit.Web.Utils.GetTemplateBinding(valueAccessor, viewModel, bindingContext);
            var name = NXKit.Web.Utils.GetTemplateName(bindingContext, opts);
             
            return {
                data: data,
                name: name,
            };
        });
    },

    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        return ko.bindingHandlers['template']['init'](
            element,
            ko.bindingHandlers.nxkit_template.convert_value_accessor(valueAccessor, viewModel, bindingContext),
            allBindings,
            viewModel,
            bindingContext);
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        return ko.bindingHandlers['template']['update'](
            element,
            ko.bindingHandlers.nxkit_template.convert_value_accessor(valueAccessor, viewModel, bindingContext),
            allBindings,
            viewModel,
            bindingContext);
    }
};

ko.virtualElements.allowedBindings.nxkit_template = true;

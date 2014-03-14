﻿Type.registerNamespace('_NXKit.XForms.Web.UI');

_NXKit.XForms.Web.UI.InputEditableString = function (element)
{
    _NXKit.XForms.Web.UI.InputEditableString.initializeBase(this, [element]);

    this._view = null;
    this._modelItemId = null;
    this._radTextBox = null;
};

_NXKit.XForms.Web.UI.InputEditableString.prototype =
{
    initialize: function ()
    {
        _NXKit.XForms.Web.UI.InputEditableString.callBaseMethod(this, 'initialize');
    },

    dispose: function ()
    {
        _NXKit.XForms.Web.UI.InputEditableString.callBaseMethod(this, 'dispose');
    },

    get_view: function ()
    {
        return this._view;
    },
    set_view: function (value)
    {
        if (this._view != value)
        {
            if (this._view != undefined)
                this._view.remove_valueChanged(Function.createDelegate(this, this._valueChangedHandler));

            this._view = value;
            this._view.add_valueChanged(Function.createDelegate(this, this._valueChangedHandler));
            this.raisePropertyChanged('view');
        }
    },

    get_modelItemId: function()
    {
        return this._modelItemId;
    },

    set_modelItemId: function(value)
    {
        if (this._modelItemId != value)
        {
            this._modelItemId = value;
            this.raisePropertyChanged('modelItemId');
        }
    },

    get_radTextBox: function()
    {
        return this._radTextBox;
    },

    set_radTextBox: function(value)
    {
        if (this._radTextBox != value)
        {
            if (this._radTextBox != null)
                this._radTextBox.remove_valueChanged(Function.createDelegate(this, this._onTextBoxValueChangedHandler));

            this._radTextBox = value;
            this.raisePropertyChanged('radTextBox');

            this._radTextBox.add_valueChanged(Function.createDelegate(this, this._onTextBoxValueChangedHandler));
        }
    },

    _valueChangedHandler: function (source, args)
    {
        // does this apply to us
        if (args.get_modelItemId() != this.get_modelItemId())
            return;

        if (args.get_newValue() != null)
            this.get_radTextBox().set_value(args.get_newValue());
    },
    
    _onTextBoxValueChangedHandler: function (source, args)
    {
        this.get_view().raiseValueChanged(new NXKit.Web.UI.ValueChangedEventArgs(self, this.get_modelItemId(), source.get_value(), null));
    },
};

_NXKit.XForms.Web.UI.InputEditableString.registerClass('_NXKit.XForms.Web.UI.InputEditableString', Sys.UI.Control);

if (typeof(Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();

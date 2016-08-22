define('app/bindings/selected-row', ['knockout', 'jquery'], function(ko) {
    ko.bindingHandlers.selectedRow = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called when the binding is first applied to an element
            // Set up any initial state, event handlers, etc. here
            var value = ko.unwrap(valueAccessor());
            $(element).find("tr").each(function(index, row) {

            });
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called once when the binding is first applied to an element,
            // and again whenever any observables/computeds that are accessed change
            // Update the DOM element based on the supplied values here.
        }
    };
});
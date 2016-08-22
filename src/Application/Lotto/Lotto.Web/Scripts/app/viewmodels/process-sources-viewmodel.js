define('app/viewmodels/process-sources-viewmodel', ['knockout', 'mapping', 'jquery'], function(ko, mapping) {

    function ProcessSourcesViewModel() {
        var self = this;

        self.primaryPrizes = ko.observableArray([]);
        self.selectedPrimaryPrize = ko.observable({size: '', lotteryName: '', connectionString: ''});

        self.loadData = function() {
            return $.get('api/processsources/get?selectNew=true').done(function (data) {
                if (data) {
                    var items = $.map(data, function(item) {
                        return mapping.fromJS(item);
                    });
                    self.primaryPrizes(items);
                    if (items.length > 0) {
                        self.selectedPrimaryPrize(items[0]);
                    }
                }
            });
        };

        self.save = function() {
            var itemsToSave = $.map(self.primaryPrizes(), function (item) {
                return mapping.toJS(item);
            });

            $.ajax({
                url: 'api/processsources/save',
                method: "POST",
                data: JSON.stringify(itemsToSave),
                dataType: "json",
                contentType: "application/json"
            }).done(self.loadData);
        };

        self.selectPrimaryPrize = function(item) {
            self.selectedPrimaryPrize(item);
        };
        
        self.destroy = function () {

        };
    }

    return ProcessSourcesViewModel;
});
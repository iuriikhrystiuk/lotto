define('app/viewmodels/lotteries-viewmodel', ['app/models/lottery-model', 'knockout', 'jquery'], function (LotteryModel, ko) {

    function processData(data) {
        var items = data ? $.map(data, function (value) {
            return new LotteryModel(value);
        }) : [];
        this.lotteries(items);
        if (items.length > 0) {
            this.select(items[0]);
        }
    }

    function LotteriesViewmodel() {
        var self = this;

        self.lotteries = ko.observableArray([]);
        self.selectedLottery = ko.observable(new LotteryModel());

        self.remove = function (item) {
            self.lotteries.remove(item);
            if (item.name() == self.selectedLottery().name() && self.lotteries().length > 0) {
                self.select(self.lotteries()[0]);
            }
        };

        self.select = function (item) {
            self.selectedLottery(item);
        };

        self.loadData = function () {
            return $.get('api/lotteries/get').done($.proxy(processData, self));
        };

        self.add = function () {
            var newLottery = new LotteryModel({ id: 0, name: '' });
            self.lotteries.push(newLottery);
            self.select(newLottery);
        };

        self.save = function () {
            var itemsToSave = $.map(self.lotteries(), function (item) {
                return item.getModel();
            });

            $.ajax({
                url: 'api/lotteries/save',
                method: "POST",
                data: JSON.stringify(itemsToSave),
                dataType: "json",
                contentType: "application/json"
            }).done($.proxy(self.loadData, self));
        };
        
        self.destroy = function() {

        };
    }

    return LotteriesViewmodel;
});
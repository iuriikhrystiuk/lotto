define('app/viewmodels/prize-map-viewmodel', ['app/models/prize-map-model', 'knockout', 'jquery'], function (prizeMapModel, ko) {
    function processLotteriesData(data) {
        var items = data ? data : [];
        this.lotteries(items);
        if (items.length > 0) {
            this.selectLottery(items[0]);
        }
    }

    function processPrizeMapData(data) {
        var viewModel = this;
        var items = data ? $.map(data, function (value) {
            var model = prizeMapModel(value);
            attachDisplayNextlotteryPrize(viewModel, model);
            return model;
        }) : [];
        viewModel.prizeMap(items);
        if (items.length > 0) {
            viewModel.selectPrizeMap(items[0]);
        }
    }

    function attachDisplayNextlotteryPrize(viewModel, model) {
        model.displayNextLotteryPrize = ko.computed(function () {
            var currentModel = this;
            var nextLotteryPrize = viewModel.prizeMap().filter(function (item) {
                return item.id() == currentModel.nextLotteryPrizeId() && currentModel.id() != currentModel.nextLotteryPrizeId();
            });
            if (nextLotteryPrize.length > 0) {
                currentModel.nextLotteryPrize(nextLotteryPrize[0]);
                return 'Size: ' + currentModel.nextLotteryPrize().size() + ' Prize: ' + currentModel.nextLotteryPrize().prize();
            }
            return '';
        }, model);
    }

    function loadLotteries() {
        return $.get('api/lotteries/get');
    }

    function loadPrizeMap(id) {
        return $.get('api/prizeMap/get/' + id);
    }

    var currentId = 0;

    function PrizeMapViewmodel() {
        var self = this;

        self.lotteries = ko.observableArray([]);
        self.prizeMap = ko.observableArray([]);
        self.selectedLottery = ko.observable({});
        self.selectedPrizeMap = ko.observable({ size: '', prize: '', nextLotteryPrize: ko.observable({}), nextLotteryPrizeId: ko.observable('') });
        self.availablePrizes = ko.computed(function () {
            return $.map(self.prizeMap(), function (value) {
                return $.extend(value, {
                    caption: ko.computed(function () {
                        return 'Size ' + value.size() + ' Prize ' + value.prize();
                    })
                });
            });
        }, self);

        self.selectLottery = function (item) {
            if (item.id !== self.selectedLottery().id) {
                self.selectedLottery(item);
                loadPrizeMap(item.id).done($.proxy(processPrizeMapData, self));
            }
        };

        self.selectPrizeMap = function (item) {
            self.selectedPrizeMap(item);
        };


        self.remove = function (item) {
            self.prizeMap.remove(item);
            if (self.prizeMap().length > 0) {
                self.selectPrizeMap(self.prizeMap()[0]);
            }
        };

        self.add = function () {
            var newItem = prizeMapModel({ id: currentId--, size: '', prize: '', lotteryId: self.selectedLottery().id, nextLotteryPrizeId: '', nextLotteryPrize: {} });
            attachDisplayNextlotteryPrize(self, newItem);
            self.prizeMap.push(newItem);
            self.selectPrizeMap(newItem);
        };

        self.loadData = function () {
            return loadLotteries().done($.proxy(processLotteriesData, self));
        };

        self.save = function () {
            var itemsToSave = $.map(self.prizeMap(), function (item) {
                return item.getModel();
            });

            $.ajax({
                url: 'api/prizemap/save',
                method: "POST",
                data: JSON.stringify(itemsToSave),
                dataType: "json",
                contentType: "application/json"
            }).done($.proxy(function () {
                loadPrizeMap(self.selectedLottery().id).done($.proxy(processPrizeMapData, self));
            }, self));
        };
        
        self.destroy = function () {

        };
    }

    return PrizeMapViewmodel;
});
define('app/viewmodels/process-results-viewmodel', ['knockout', 'jquery'], function (ko) {

    function ProcessResultsViewmodel() {
        var self = this;
        self.processSources = ko.observableArray([]);
        self.selectedProcessSource = ko.observable({});
        self.processResults = ko.observableArray([]);
        self.columns = ko.observableArray([]);
        self.weightColumns = ko.observableArray([]);
        self.weights = ko.observableArray([]);
        self.repeatsCount = ko.observable(0);

        self.loadData = function () {
            return self.loadProcessSources();
        };

        self.loadProcessSources = function () {
            return $.get('api/processsources/get?selectNew=false').then(function (data) {
                if (data) {
                    self.processSources(data);
                    self.selectProcessSource(data[0]);
                    return self.loadProcessResults(data[0]);
                }
                return $.Deferred().resolve();
            });
        };

        self.selectProcessSource = function (item) {
            self.selectedProcessSource(item);
            self.loadProcessResults(item);
        };

        self.loadResults = function () {
            var processSource = self.selectedProcessSource();
            $.get('api/processresults/loadresults/' + processSource.primaryLotteryPrizeId + '?repeatsCount=' + self.repeatsCount()).then(function () {
                return self.loadProcessResults(processSource);
            });
        };

        self.loadProcessResults = function (processSource) {
            return $.get('api/processresults/get/' + processSource.primaryLotteryPrizeId).done(function (data) {
                self.columns([]);
                self.columns.push({ displayName: 'Repeats Count', name: 'repeatsCount' });
                for (var i = 1; i <= processSource.size; i++) {
                    self.columns.push({ displayName: 'Number ' + i, name: 'number' + i });
                }
                if (data) {
                    var items = $.each(data, function (index, item) {
                        for (var j = 1; j <= item.numbers.length; j++) {
                            item['number' + j] = item.numbers[j - 1];
                        }
                        return item;
                    });
                    self.processResults(items);
                }
            });
        };

        self.calculateSimpleWeights = function () {
            var processSource = self.selectedProcessSource();
            return $.get('api/processresults/calculatesimpleweights/' + processSource.primaryLotteryPrizeId).done(function (data) {
                self.weightColumns([]);
                self.weightColumns.push({ displayName: 'Rating', name: 'rating' });
                for (var i = 1; i <= processSource.size; i++) {
                    self.weightColumns.push({ displayName: 'Number ' + i, name: 'number' + i });
                }
                if (data) {
                    var items = $.each(data, function (index, item) {
                        for (var j = 1; j <= item.combination.numbers.length; j++) {
                            item['number' + j] = item.combination.numbers[j - 1];
                        }
                        return item;
                    });
                    self.weights(items);
                }
            });
        };
        
        self.destroy = function () {

        };
    }

    return ProcessResultsViewmodel;
});
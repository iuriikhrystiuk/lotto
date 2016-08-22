define('app/viewmodels/process-statuses-viewmodel', ['knockout', 'jquery'], function (ko) {

    var currentTimeOut = null;

    function poll(viewModel, item) {
        if (currentTimeOut) {
            clearTimeout(currentTimeOut);
        }
        currentTimeOut = setTimeout(function () {
            viewModel.loadSummary(item).done(function (data) {
                viewModel.selectedProcessStatus(data);
                poll(viewModel, data);
            });
        }, 5000);
    }

    function ProcessStatusesViewmodel() {
        var self = this;
        self.processStatuses = ko.observableArray([]);
        self.selectedProcessStatus = ko.observable({ lotteryName: '', size: '', totalSteps: 0, currentStep: 0, averageDuration: 0, maxDuration: 0, estimatedTime: 0 });

        self.loadSummary = function (item) {
            return $.get('api/processstatuses/get/' + item.primaryPrizeId);
        };

        self.selectProcessStatus = function (item) {
            self.loadSummary(item).done(function (data) {
                if (data) {
                    self.selectedProcessStatus(data);
                    poll(self, data);
                } else {
                    self.selectedProcessStatus(item);
                }
            });
        };

        self.loadData = function () {
            return $.get('api/processstatuses/get').done(function (data) {
                if (data) {
                    self.processStatuses(data);
                    self.selectProcessStatus(data[0]);
                }
            });
        };

        self.queueProcess = function () {
            var processStatus = self.selectedProcessStatus();
            $.get('api/processstatuses/queueprocess?prizemapid=' + processStatus.primaryPrizeId).done(function () {
                self.selectProcessStatus(processStatus);
            });
        };

        self.stopProcess = function () {
            var processStatus = self.selectedProcessStatus();
            $.get('api/processstatuses/stopprocess?prizemapid=' + processStatus.primaryPrizeId).done(function () {
                self.selectProcessStatus(processStatus);
            });
        };

        self.cancelProcess = function () {
            var processStatus = self.selectedProcessStatus();
            $.get('api/processstatuses/cancelprocess?prizemapid=' + processStatus.primaryPrizeId).done(function () {
                self.selectProcessStatus(processStatus);
            });
        };

        self.continueProcess = function () {
            var processStatus = self.selectedProcessStatus();
            $.get('api/processstatuses/continueprocess?prizemapid=' + processStatus.primaryPrizeId).done(function () {
                self.selectProcessStatus(processStatus);
            });
        };

        self.destroy = function () {
            if (currentTimeOut) {
                clearTimeout(currentTimeOut);
            }
        };
    }

    return ProcessStatusesViewmodel;
});
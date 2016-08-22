define('app/viewmodels/viewmodel-factory', [
        'app/common/renderer',
        'app/viewmodels/lotteries-viewmodel',
        'app/viewmodels/prize-map-viewmodel',
        'app/viewmodels/process-sources-viewmodel',
        'app/viewmodels/process-results-viewmodel',
        'app/viewmodels/process-statuses-viewmodel',
        'knockout',
        'jquery'], function (renderer, lotteriesVm, prizeMapVm, processSourcesVm, processResultsVm, processStatusesVm, ko) {
    var containerId = '';
            var currentViewModel = null;

    function getViewmodel(route) {
        switch (route) {
            case 'lotteries':
                return new lotteriesVm();
            case 'prizemap':
                return new prizeMapVm();
            case 'processsources':
                return new processSourcesVm();
            case 'processresults':
                return new processResultsVm();
            case 'processstatuses':
                return new processStatusesVm();
            default:
                return {
                    loadData: function() {
                        return $.Deferred().resolve();
                    }
                };
        }
    }

    function buildViewModel(route) {
        var previousVm = currentViewModel;
        currentViewModel = getViewmodel(route);
        currentViewModel.loadData().done(function () {
            var node = $('#' + containerId)[0];
            if (previousVm) {
                previousVm.destroy();
            }
            ko.cleanNode(node);
            ko.applyBindings(currentViewModel, node);
        });
    }

    return {
        init: function (id) {
            containerId = id;
            renderer.rendered.add(buildViewModel);
        },
        buildViewmodel: buildViewModel
    };
});
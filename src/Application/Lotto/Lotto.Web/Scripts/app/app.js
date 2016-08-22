define('app/app',
    ['app/common/navigator', 'app/common/renderer', 'app/viewmodels/viewmodel-factory',
     'app/agents/navigation-agent', 'app/agents/spin-agent'],
    function (navigator, renderer, factory, navigationAgent, spinAgent) {

        return {
            start: function () {
                renderer.init('content');
                factory.init('content');
                spinAgent.init('content');
                navigationAgent.init('navigations');
                navigator.init();
                if (!navigator.initialized()) {
                    navigator.navigate("lotteries");
                }
            }
        };
    });
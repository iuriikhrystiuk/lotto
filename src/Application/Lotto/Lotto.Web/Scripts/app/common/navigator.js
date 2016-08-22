define('app/common/navigator', ['crossroads', 'hasher', 'jquery'], function (crossroads, hasher) {

    var initialized = false;

    function parseHash(newHash) {
        crossroads.parse(newHash);
    }

    function configureRoutes() {
        crossroads.addRoute('lotteries');
        crossroads.addRoute('prizemap');
        crossroads.addRoute('processsources');
        crossroads.addRoute('processstatuses');
        crossroads.addRoute('processresults');
    }

    function setInitialized(hash) {
        if (hash) {
            initialized = true;
        }
    }

    function configureHashes() {
        hasher.initialized.add(parseHash);
        hasher.initialized.add(setInitialized);
        hasher.changed.add(parseHash);
        hasher.init();
    }

    function init() {
        configureRoutes();
        configureHashes();
    }

    function route(hash) {
        hasher.setHash(hash);
    }

    return {
        init: init,
        routed: crossroads.routed,
        navigate: route,
        initialized: function() {
            return initialized;
        }
    };
});
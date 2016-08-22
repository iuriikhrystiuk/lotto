var requirejs = requirejs || {};

(function () {
    requirejs.config({
        baseUrl: 'Scripts',
        shim: {
            crossroads: ['signals'],
            knockout: ['jquery'],
            mapping: ['knockout'],
            hasher: ['signals'],
        },
        paths: {
            jquery: 'jquery-2.1.4',
            knockout: 'knockout-3.3.0',
            signals: 'signals',
            crossroads: 'crossroads',
            hasher: 'hasher',
            mapping: 'knockout.mapping-latest',
            spin: 'spin',
            app: './app'
        }
    });

    require(['app/app'], function(app) {
        app.start();
    });
}(requirejs));
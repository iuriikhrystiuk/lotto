define('app/common/renderer', ['app/common/navigator', 'signals', 'jquery'], function (navigator, signals) {
    var containerId = '';
    var rendered = new signals.Signal();

    function render(route) {
        $.get(route, function(data) {
            $('#' + containerId).html(data);
            rendered.dispatch(route);
        });
    }

    function init(id) {
        containerId = id;
        navigator.routed.add(render);
    }

    return {
        render: render,
        init: init,
        rendered: rendered
    };
});
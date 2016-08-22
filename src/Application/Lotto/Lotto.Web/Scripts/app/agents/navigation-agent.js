define('app/agents/navigation-agent', ['app/common/navigator', 'jquery'], function (navigator) {
    var navigationId = '';

    function manageLinks(route) {
        $('#' + navigationId + ' li.active').removeClass('active');
        $('#' + navigationId + ' li').each(function (id, el) {
            if ($(el).find('a').attr('href').indexOf(route) != -1) {
                $(el).addClass('active');
                return false;
            }
            return true;
        });
    }

    return {
        init: function (id) {
            navigationId = id;
            navigator.routed.add(manageLinks);
        }
    };
});
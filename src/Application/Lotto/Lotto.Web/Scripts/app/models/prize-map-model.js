define('app/models/prize-map-model', ['knockout', 'mapping', 'jquery'], function (ko, mapping) {

    function prizeMap(data) {

        var options = {
            'nextLotteryPrize': {
                create: function (option) {
                    return ko.observable(option.data ? mapping.fromJS(option.data) : option.data);
                },
                key: function(item) {
                    var id = ko.utils.unwrapObservable(item.id);
                    return id;
                }
            }
        };

        var mapped = mapping.fromJS(data, options);
        var self = mapped;

        mapped.getModel = function () {
            return mapping.toJS(self);
        };

        return mapped;
    }

    return prizeMap;
});
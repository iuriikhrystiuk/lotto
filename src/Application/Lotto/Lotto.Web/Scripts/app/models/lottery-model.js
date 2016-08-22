define('app/models/lottery-model', ['knockout', 'mapping'], function(ko, mapping) {

    function LotteryModel(lottery) {
        var self = this;

        mapping.fromJS(lottery, {}, self);

        self.getModel = function() {
            return mapping.toJS(self);
        };
    }

    return LotteryModel;
});
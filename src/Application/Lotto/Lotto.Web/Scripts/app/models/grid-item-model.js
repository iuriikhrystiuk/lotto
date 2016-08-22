define('app/models/grid-item-model', [], function() {

    function GridItem(data) {
        this.data = data;
        this.selected = false;
    };

    return GridItem;
});
ko.extenders.dirty = function (target, startDirty) {
    var mapping = { 'ignore': ['parent'] };
    var cleanValue = ko.observable(ko.mapping.toJSON(target, mapping));
    var dirtyOverride = ko.observable(ko.utils.unwrapObservable(startDirty));

    target.isDirty = ko.computed(function () {
        return dirtyOverride() || ko.mapping.toJSON(target, mapping) !== cleanValue();
    });

    target.cleanValue = cleanValue;
    target.markClean = function () {
        cleanValue(ko.mapping.toJSON(target, mapping));
        dirtyOverride(false);
    }
    target.markDirty = function () {
        dirtyOverride(true);
    }
    target.undo = function () {
        target(ko.mapping.fromJSON(target.cleanValue(), mapping)());
        dirtyOverride(false);
    }

    return target;
}
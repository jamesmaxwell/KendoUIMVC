define(function (require, exports) {
    var user = require('viewmodel/user');

    exports.init = function () {
        kendo.bind($('#mvvmForm'), user.user);
    };
});
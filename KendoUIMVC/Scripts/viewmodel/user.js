define(function (require, exports) {
    var uservm = kendo.observable({
        userName: 'junwei',
        password: '123',
        sex: 0,
        sexArray: [{ name: '男', value: 0 }, { name: '女', value: 1}],
        birthday: new Date(1954, 10, 9),
        weight: 66,
        email: 'junwei@xquant.com',

        confirm: function () {
            $('#result').html(
                '用户名：' + this.get('userName') + '<br/>' +
                '密码：' + this.get('password') + '<br/>' +
                '性别：' + this.get('sex') + '<br/>' +
                '生日：' + kendo.toString(this.get('birthday'), 'yyyy-MM-dd') + '<br/>' +
                '体重：' + this.get('weight')
            );
        }
    });

    exports.user = uservm;
});
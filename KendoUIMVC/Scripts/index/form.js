define(function (require, exports) {
    exports.init = function () {
        kendo.culture('zh-CHS');

        $('#sex').kendoDropDownList({
            dataTextField: 'text',
            dataValueField: 'value',
            dataSource: [
                { text: '男', value: 0 },
                { text: '女', value: 1 }
               ],
            index: 0
        });

        $('#birthday').kendoDatePicker({
            animation: false,
            footer: "今天- #= kendo.toString(data,'d')#",
            format: "yyyy-MM-dd",
            value: new Date(1954, 7, 5)
        });

        $('#weight').kendoNumericTextBox();

        var validator = $('#formMain').kendoValidator().data('kendoValidator');

        $('#confirm').click(function () {
            if (validator.validate()) {
                var sex = $('#sex').data('kendoDropDownList');
                var birthday = $('#birthday').data('kendoDatePicker');
                var weight = $('#weight').data('kendoNumericTextBox');

                $('#result').html(
                '用户名：' + $('#userName').val() + '<br/>' +
                '密码：' + $('#password').val() + '<br/>' +
                '性别：' + sex.text() + '<br/>' +
                '生日：' + kendo.toString(birthday.value(), 'yyyy-MM-dd') + '<br/>' +
                '体重：' + weight.value()
            );
            } else {
                $('#result').html('请输入合法的值');
            }
        });
    };
});
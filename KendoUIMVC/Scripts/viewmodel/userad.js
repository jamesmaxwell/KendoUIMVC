define(function (require, exports) {

    var uservm = kendo.observable({
        userSource: [],

        confirm: function () {
            $('#result').html(
                '用户名：' + this.userSource.Name + '<br/>' +
                '密码：' + this.userSource.Password + '<br/>' +
                '性别：' + this.userSource.Sex + '<br/>' +
                '生日：' + kendo.toString(this.userSource.Birthday, 'yyyy-MM-dd') + '<br/>' +
                '体重：' + this.userSource.Weight
            );
        },

        update: function () {
            userDataSource.sync();
        }
    });

    var userDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: '/Home/User/1',
                dataType: 'json'
            },
            update: {
                url: '/Home/UserUpdate',
                type: 'POST',
                /*data: { x: 1 },  uservm.userSource,*/
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                if (operation === "update") {
                    var d = new Date(options.Birthday);
                    options.Birthday = kendo.toString(new Date(options.Birthday), "yyyy-MM-dd");
                }

                return options;
            }
        },
        schema: {
            model: {
                id: "Id",
                fields: {    
                    Birthday: { type: "date" }
                }

            },
            data: function (res) {
                return [res];
            }
        },
        change: function (d) {
            var ds = this.view();
            uservm.userSource = ds[0];
            kendo.bind($('#mvvmForm'), uservm);
        }
    });

    userDataSource.read();

    //exports.user = uservm;
});
define(function (require, exports, module) {
    exports.init = function () {
        $('#horizontal').kendoSplitter({
            panes: [
                { collapsible: false, size: "200px", min: "100px", max: "300px" },
                { collapsible: false }
            ]
        });

        $('#treeview').kendoTreeView();

        $('#formDemo').click(function () {
            $('#contentFrame').attr('src', '/Home/FormDemo');
        }).click();

        $('#gridDemo').click(function () {
            $('#contentFrame').attr('src', '/Home/GridDemo');
        });

        $('#mvvmDemo').click(function () {
            $('#contentFrame').attr('src', '/Home/MvvmDemo');
        });

        $('#mvvmadDemo').click(function () {
            $('#contentFrame').attr('src', '/Home/MvvmDataSource');
        });
    };
});
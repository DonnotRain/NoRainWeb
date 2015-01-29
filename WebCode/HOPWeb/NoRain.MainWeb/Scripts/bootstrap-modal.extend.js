+function BootstrapModal() {
    makeModalDraggable();

    function makeModalDraggable() {
        //$(".modal-header").on('mousedown', function (event) {
        //    alert("鼠标放下了");
        //});

        $(".modal-header").each(function (e, t) {
            var draggableData = {
                isMouseDown: false,
                mouseOffset: {}
            };

            var parent = $(this).parent().parent(".modal");
            parent.data("draggableData", draggableData);
            if ((Boolean)(parent.attr("data-draggable"))) {
                $(this).css("cursor", "move");
                $(this).on('mousedown', { dialog: parent }, function (event) {
                    var dialog = event.data.dialog;
                    dialog.data("draggableData").isMouseDown = true;
                    var dialogOffset = dialog.offset();
                    dialog.data("draggableData").mouseOffset = {
                        top: event.clientY - dialogOffset.top,
                        left: event.clientX - dialogOffset.left
                    };
                });
                $(this).on('mouseup mouseleave', { dialog: parent }, function (event) {
                    event.data.dialog.data("draggableData").isMouseDown = false;
                });
                $('body').on('mousemove', { dialog: parent }, function (event) {
                    var dialog = event.data.dialog;
                    if (!dialog.data("draggableData").isMouseDown) {
                        return;
                    }
                    dialog.offset({
                        top: event.clientY - dialog.data("draggableData").mouseOffset.top,
                        left: event.clientX - dialog.data("draggableData").mouseOffset.left
                    });
                });
            }
        });

        return this;
    }
}()

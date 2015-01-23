+function BootstrapModal() {
	makeModalDraggable();
	var draggableData = {
		isMouseDown: false,
		mouseOffset: {}
	};

	function makeModalDraggable() {
		$(".modal-header").each(function (e, t) {
			var parent = $(this).parent(".modal");
			if ((Boolean)(parent.attr("data-draggable"))) {
				$(this).on('mousedown', function (event) {

					var dialog = event.data.dialog;
					dialog.draggableData.isMouseDown = true;
					var dialogOffset = dialog.getModalDialog().offset();
					dialog.draggableData.mouseOffset = {
						top: event.clientY - dialogOffset.top,
						left: event.clientX - dialogOffset.left
					};
				});
			}
		});

		//if (this.options.draggable) {
		//	this.getModalHeader().addClass(this.getNamespace('draggable')).on('mousedown', { dialog: this }, function (event) {
		//		var dialog = event.data.dialog;
		//		dialog.draggableData.isMouseDown = true;
		//		var dialogOffset = dialog.getModalDialog().offset();
		//		dialog.draggableData.mouseOffset = {
		//			top: event.clientY - dialogOffset.top,
		//			left: event.clientX - dialogOffset.left
		//		};
		//	});
		//	this.getModal().on('mouseup mouseleave', { dialog: this }, function (event) {
		//		event.data.dialog.draggableData.isMouseDown = false;
		//	});
		//	$('body').on('mousemove', { dialog: this }, function (event) {
		//		var dialog = event.data.dialog;
		//		if (!dialog.draggableData.isMouseDown) {
		//			return;
		//		}
		//		dialog.getModalDialog().offset({
		//			top: event.clientY - dialog.draggableData.mouseOffset.top,
		//			left: event.clientX - dialog.draggableData.mouseOffset.left
		//		});
		//	});
		//}

		return this;
	}
}()

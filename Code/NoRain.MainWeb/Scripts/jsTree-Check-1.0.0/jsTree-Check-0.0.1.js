(function ($) {
    var methods = (function () {
        var nextUid = (function () { var counter = 1; return function () { return counter++; }; }());

        var createContainer = function () {
            var ele = $(document.createElement("div")).attr({
                "class": "jsTreeCheck-container"
            }).css({ "display": "none", "z-index": 99999, "position": " absolute", "background-color": "#dff0d8", "border": "1px solid #d6e9c6" });

            return ele;
        }

        var createTree = function () {
            var ele = $(document.createElement("div")).attr({
                "class": "jsTreeCheck"
            });  //.css({ "display": "none", "z-index": 99999, "position": absolute });
            return ele;
        }
        function showMenu() {
            var $this = $(this);
            var tree = $this.data('tree');
            var container = $this.data('container');

            var parentOffSet = container.parent().offset();
            var cityOffset = $this.offset();
            //计算出父元素的偏移值
            container.css({ left: cityOffset.left - parentOffSet.left + 15 + "px", top: cityOffset.top - parentOffSet.top + $this.outerHeight() + 15 + "px" }).slideDown("fast");

            $("body").bind("mousedown", onBodyDown);
            $(window).resize(hideMenu);


            function hideMenu() {
                $(".jsTreeCheck-container").fadeOut("fast");
                $("body").unbind("mousedown", onBodyDown);
            }

            function onBodyDown(event) {
                if (!(event.target == container[0])) {
                    hideMenu();
                }
            }
        }

        function onCheck(e, data) {
            if (data.action != "select_node" && data.action != "deselect_node") return;
            var treeEle = data.instance.element;
            var $this = treeEle.data("target");

            //遍历选中项，显示选中值
            var selectedNodes = treeEle.jstree("get_selected", true);
            var selectText = [];
            for (var i = 0; i < selectedNodes.length; i++) {
                selectText.push(selectedNodes[i].text);
            }
            $this.val(selectText.join(","));

            $this.attr("SelectedValue", data.selected.join(","));
        }

        return {
            init: function (options, nodes) {
                var $this = $(this);
                var settings = $this.data('jsTreeCheck');
                var tree = $this.data('tree');
                var container = $this.data('container');

                if (tree) tree.jstree('destroy');
                if (container && container.length) container.remove();

                var defaults = $.jsTreeCheck.defaults;

                settings = $.extend(true, defaults, options);

                $this.prop("readonly", true);
                $this.click(showMenu);

                //创建容器并加入到页面中
                container = createContainer($this);
                tree = createTree($this);
                container.append(tree);
                if ($this.parents("form").length) {
                    container.appendTo($this.parents("form"));
                }
                else {
                    container.appendTo($("body"));
                }
                tree.data('target', $this);

                $this.data('container', container);
                $this.data('tree', tree);
                //设置数据
                if (nodes) settings.core.data = nodes;
                //选中回调
                tree.jstree(settings).on('changed.jstree', onCheck);

                $(container).bind("mousedown", function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                });

                return this;
            }
            , desdroy: function () {
                var $this = $(this);
                var settings = $this.data('jsTreeCheck');
                var tree = $this.data('tree');
                var container = $this.data('container');

                if (tree) tree.jstree('destroy');
                if (container && container.length) container.remove();
            }
            , destroyAll: function () {
                $.jsTree.destroy();
                $(".jsTreeCheck-container").remove();
            }
            , setValue: function (values) {

            }
            , getValue: function () {

            }
        };

    })();

    $.jsTreeCheck = {
        version: '0.0.1',
        'select_node': function () {
            alert("ashg");
        },
        defaults: {
            'plugins'
                : ['state', 'dnd', 'checkbox', 'unique'],
            'core': {
                'check_callback': true,
                "themes": {
                    "responsive": false
                },
                'data': [{
                    "text": "Same but with checkboxes",
                    "children": []
                }]
            }
        }
    };


    $.fn.jsTreeCheck = function () {
        var method = arguments[0];
        if (methods[method]) {
            method = methods[method];
            arguments = Array.prototype.slice.apply(arguments, 1);
        }
        else if (typeof (method) == "object" && method) {
            method = methods.init;
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.jsTreeCheck');
            return this;
        }
        this.each(function () {
            method.apply(this, arguments);
        });
        return this;
    }

})(jQuery);
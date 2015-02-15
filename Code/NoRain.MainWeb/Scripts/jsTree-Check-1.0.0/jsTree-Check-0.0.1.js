(function ($) {
    var methods = (function () {
        var nextUid = (function () { var counter = 1; return function () { return counter++; }; }());

        function indexOf(value, array) {
            var i = 0, l = array.length;
            for (; i < l; i = i + 1) {
                if (equal(value, array[i])) return i;
            }
            return -1;
        }
        /**
        * Compares equality of a and b
        * @param a
        * @param b
        */
        function equal(a, b) {
            if (a === b) return true;
            if (a === undefined || b === undefined) return false;
            if (a === null || b === null) return false;
            // Check whether 'a' or 'b' is a string (primitive or object).
            // The concatenation of an empty string (+'') converts its argument to a string's primitive.
            if (a.constructor === String) return a + '' === b + ''; // a+'' - in case 'a' is a String object
            if (b.constructor === String) return b + '' === a + ''; // b+'' - in case 'b' is a String object
            return false;
        }

        var createContainer = function () {
            var ele = $(document.createElement("div")).attr({
                "class": "jsTreeCheck-container"
            }).css({ "display": "none", "z-index": 99999, "position": " absolute", "background-color": "#dff0d8", "border": "1px solid #d6e9c6", "padding-right": "10px" });

            return ele;
        }

        var createTree = function () {
            var ele = $(document.createElement("div")).attr({
                "class": "jsTreeCheck"
            });  //.css({ "display": "none", "z-index": 99999, "position": absolute });
            return ele;
        }

        function hideMenu() {
            $(".jsTreeCheck-container").fadeOut("fast");
            $("body").unbind("mousedown", hideMenu);
        }

        function showMenu() {
            var $this = $(this);
            var tree = $this.data('tree');
            var container = $this.data('container');

            var parentOffSet = container.parent().offset();
            var cityOffset = $this.offset();
            //计算出父元素的偏移值
            container.css({ left: cityOffset.left - parentOffSet.left + 15 + "px", top: cityOffset.top - parentOffSet.top + $this.outerHeight() + 15 + "px" }).slideDown("fast");

            $("body").bind("mousedown", hideMenu);
            $(window).resize(hideMenu);
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

            //判断是否为多选，否则关闭选择框
            if (indexOf("checkbox", data.instance.settings.plugins) < 0) hideMenu.apply($this);
        }

        //判断是否为数组
        var isArray = function (v) {
            //  Object.prototype.toString.call(o) === ‘[object Array]‘;
            return Object.prototype.toString.apply(v) === '[object Array]';
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
            , setValue: function (ids) {
                var $this = $(this);
                var tree = $this.data('tree');
                var container = $this.data('container');

                
                if (typeof (ids) === "string") {
                    ids = ids.split(',');
                }
                else {
                    if (!isArray(ids)) {
                        ids = ids.toString().split(',');
                    }

                }

                tree.jstree("deselect_all");
                tree.jstree("select_node", ids);
                return $this;
            }
            , getValue: function () {
                var $this = $(this);
                var tree = $this.data('tree');
                var container = $this.data('container');
            }
        };
    })();

    $.jsTreeCheck = {
        version: '0.0.1',
        defaults: {
            'plugins'
                : ["wholerow"],
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
            arguments = Array.prototype.slice.call(arguments, 1);
        }
        else if (typeof (method) == "object" && method) {
            method = methods.init;
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.jsTreeCheck');
            return this;
        }
        var pArguments = arguments;
        this.each(function () {
            method.apply(this, pArguments);
        });
        return this;
    }

})(jQuery);
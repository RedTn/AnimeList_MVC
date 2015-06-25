(function () {
    // Private function
    function getColumnsForScaffolding(data) {
        if ((typeof data.length !== 'number') || data.length === 0) {
            return [];
        }
        var columns = [];
        for (var propertyName in data[0]) {
            columns.push({ headerText: propertyName, rowText: propertyName, rowImage: propertyName });
        }
        return columns;
    }

    ko.simpleGrid = {
        // Defines a view model class you can use to populate a grid
        viewModel: function (configuration) {
            var self = this;
            this.data = configuration.data;
            this.currentPageIndex = ko.observable(0);
            this.pageSize = configuration.pageSize || ko.observable(5);
            self.iconType = ko.observable("");
            self.currentColumn = ko.observable("");

            // If you don't specify columns configuration, we'll use scaffolding
            this.columns = configuration.columns || getColumnsForScaffolding(ko.unwrap(this.data));

            this.itemsOnCurrentPage = ko.computed(function () {
                var size = ko.unwrap(this.pageSize);
                var startIndex = size * this.currentPageIndex();
                return ko.unwrap(this.data).slice(startIndex, startIndex + size);
            }, this);

            this.maxPageIndex = ko.computed(function () {
                return Math.ceil(ko.unwrap(this.data).length / ko.unwrap(this.pageSize)) - 1;
            }, this);

            this.nextPage = function () {
                this.currentPageIndex(this.currentPageIndex() + 1);
            }

            this.previousPage = function () {
                this.currentPageIndex(this.currentPageIndex() - 1);
            }

            self.sortType = "ascending";
            self.sortTable = function (viewModel, e) {
                var orderProp = viewModel["sortTerm"];
                self.currentColumn(viewModel["headerText"]);
                self.data.sort(function (left, right) {
                    if ($.type(orderProp) == "array") {
                        $.each(orderProp[0], function (key, value) {
                            leftVal = left[key][value];
                            rightVal = right[key][value];
                        });
                    }
                    else {
                        leftVal = left[orderProp];
                        rightVal = right[orderProp];
                    }
                    if (self.sortType == "ascending") {
                        return leftVal < rightVal ? 1 : -1;
                    }
                    else {
                        return leftVal > rightVal ? 1 : -1;
                    }
                });
                self.sortType = (self.sortType == "ascending") ? "descending" : "ascending";
                self.iconType((self.sortType == "ascending") ? "glyphicon glyphicon-chevron-up" : "glyphicon glyphicon-chevron-down");
            };
        }
    };

    // Templates used to render the grid
    var templateEngine = new ko.nativeTemplateEngine();

    templateEngine.addTemplate = function (templateName, templateMarkup) {
        document.write("<script type='text/html' id='" + templateName + "'>" + templateMarkup + "<" + "/script>");
    };

    //PAGE LINKS
    templateEngine.addTemplate("ko_simpleGrid_pageLinks", "\
                    <div class=\"ko-grid-pageLinks\">\
                        <span data-bind='click: previousPage,visible:currentPageIndex() > 0' class='glyphicon glyphicon-circle-arrow-left pageChevrons'></span>\
                        <span>Page:</span>\
                        <!-- ko foreach: ko.utils.range(0, maxPageIndex) -->\
                               <a href=\"#\" data-bind=\"text: $data + 1, click: function() { $root.currentPageIndex($data) }, css: { selected: $data == $root.currentPageIndex() }\">\
                            </a>\
                        <!-- /ko -->\
                        <span data-bind='click: nextPage,visible:currentPageIndex() < maxPageIndex()' class='glyphicon glyphicon-circle-arrow-right pageChevrons'></span>\
                    </div>");
    //EXPLORE TEMPLATE
    templateEngine.addTemplate("anime_list_template", "\
                 <table class=\"table table-striped table-bordered table-condensed ko-grid\" cellspacing=\"0\">\
                      <thead>\
                          <tr data-bind=\"foreach: columns\" class=\"disableSelection\">\
                               <th>\
                                <!-- $parent header is required inside a foreach loop -->\
                               <a id=\"headerTitle\" data-bind=\"text: headerText, click: $parent.sortTable\"></a>\
                               <span data-bind=\"attr: { class: $parent.currentColumn() == headerText ? 'isVisible' : 'isHidden' }\">\
                               <i data-bind=\"attr: { class: $parent.iconType }\"></i>\
                               </span>\
                               </th>\
                          </tr>\
                      </thead>\
                      <tbody data-bind=\"foreach:itemsOnCurrentPage\">\
                      <tr data-bind=\"foreach: $parent.columns\">\
                                <!-- ko if: rowId-->\
                                    <td><a data-bind=\"attr: { href: rowAction + '/' + $parent[rowId]}\"><span data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></a></td>\
                                <!--/ko-->\
                                <!-- ko if: rowText && !rowId-->\
                                    <td><span data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></span></td>\
                                <!--/ko-->\
                                <!-- ko if: rowImage-->\
                                    <td align=\"center\"><img class=\"img-holder img-responsive\" data-bind=\"attr:{src: $parent[rowImage]}\" /></td>\
                                <!--/ko-->\
                      </tr>\
                      </tbody>\
                 </table>\
        ");
    //USER LIBRARY TEMPLATE 
    templateEngine.addTemplate("library_list_template", "\
                 <table class=\"table table-striped table-condensed ko-grid\" cellspacing=\"0\" id=\"tableLibrary\">\
                      <thead>\
                          <tr data-bind=\"foreach: columns\" class=\"disableSelection\">\
                               <th>\
                                <!-- $parent header is required inside a foreach loop -->\
                               <a id=\"headerTitle\" data-bind=\"text: headerText, click: $parent.sortTable\"></a>\
                               <span data-bind=\"attr: { class: $parent.currentColumn() == headerText ? 'isVisible' : 'isHidden' }\">\
                               <i data-bind=\"attr: { class: $parent.iconType }\"></i>\
                               </span>\
                               </th>\
                          </tr>\
                      </thead>\
                      <tbody data-bind=\"foreach:itemsOnCurrentPage\">\
                           <tr data-bind=\"foreach: $parent.columns\" onclick=\"$('.collapse-in').collapse('toggle');\">\
                                <!-- ko if: rowId-->\
                                    <td><a data-bind=\"attr: { href: rowAction + '/' + $parent[rowId]}\"><span data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></a></td>\
                                <!--/ko-->\
                                <!-- ko if: rowText && !rowId-->\
                                    <td><span data-bind=\"if: headerText == 'Progress'\">\
                                    <!--<button type=\"button\" class=\"btn btn-default\" aria-label=\"Left Align\">Test</button> -->\
                                    <input data-bind=\"numeric: rowEpisodes($parent), value: rowProgress($parent)\" type=\"text\" style=\"width: 40px;text-align:center;\"></input>\
                                    </span>\
                                    <span data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></span></td>\
                                <!--/ko-->\
                                <!-- ko if: rowImage-->\
                                    <td align=\"center\"><img class=\"img-holder img-responsive\" data-bind=\"attr:{src: $parent[rowImage]}\" /></td>\
                                <!--/ko-->\
                           </tr>\
                      </tbody>\
                 </table>\
        ");

    function inputOverMax(keyCode, curValue, maxValue) {
        var numCodes_1 = [48, 49, 50, 51, 52, 53, 54, 55, 56, 57];
        var numCodes_2 = [96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
        var check_1 = numCodes_1.indexOf(keyCode);
        var check_2 = numCodes_2.indexOf(keyCode);
        if (check_1 != -1) {
            return parseInt(curValue.toString().concat(check_1.toString())) > maxValue ? true : false;
        }
        else if (check_2 != -1) {
            return parseInt(curValue.toString().concat(check_2.toString())) > maxValue ? true : false;
        }
        return true;
    };

    //TODO: Highlight progress will not replace number if numebr normally would go over max
    ko.bindingHandlers.numeric = {
        update: function (element, valueAccessor) {
            $(element).on("keydown", function (event) {
                // Allow: backspace, delete, tab, escape, and enter
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
                    // Allow: Ctrl+A
                    (event.keyCode == 65 && event.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (event.keyCode == 35 || event.keyCode == 36 || event.keyCode == 37 || event.keyCode == 39))
                {
                    // let it happen, don't do anything
                    return;
                }
                else if (event.keyCode == 13) {
                    $(element).blur();
                }
                else {
                    // Ensure that it is a number and stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) ||
                        inputOverMax(event.keyCode, element.value, valueAccessor())) {
                        event.preventDefault();
                    }
                }
                //TODO: without timeout, gets old value; better method?
                setTimeout(function () {
                    if (isNaN(element.value)) {
                        element.value = 0;
                    }
                    //Get rid of leading zeros
                    element.value = parseInt(element.value);
                }, 1);
            });
            $(element).on("focusout", function (event) {
                if (isNaN(element.value) || element.value == "" || element.value == null) {
                    element.value = 0;
                }
                console.log(element.value);
            });
        },

    };

    // The "simpleGrid" binding
    ko.bindingHandlers.simpleGrid = {
        init: function () {
            return { 'controlsDescendantBindings': true };
        },
        // This method is called to initialize the node, and will also be called again if you change what the grid is bound to
        update: function (element, viewModelAccessor, allBindings) {
            var viewModel = viewModelAccessor();

            // Empty the element
            while (element.firstChild)
                ko.removeNode(element.firstChild);

            // Allow the default templates to be overridden
            var gridTemplateName = allBindings.get('simpleGridTemplate') || "ko_simpleGrid_grid",
                pageLinksTemplateName = allBindings.get('simpleGridPagerTemplate') || "ko_simpleGrid_pageLinks";

            // Render the main grid
            var gridContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(gridTemplateName, viewModel, { templateEngine: templateEngine }, gridContainer, "replaceNode");

            // Render the page links
            var pageLinksContainer = element.appendChild(document.createElement("DIV"));
            ko.renderTemplate(pageLinksTemplateName, viewModel, { templateEngine: templateEngine }, pageLinksContainer, "replaceNode");
        }
    };

    ko.extenders.numeric = function (target, precision) {
        console.log(ko.unwrap(target));
        //create a writable computed observable to intercept writes to our observable
        var result = ko.pureComputed({
            read: target,  //always return the original observables value
            write: function (newValue) {
                var current = target(),
                    roundingMultiplier = Math.pow(10, precision),
                    newValueAsNum = isNaN(newValue) ? 0 : parseFloat(+newValue),
                    valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;

                //only write if it changed
                if (valueToWrite !== current) {
                    target(valueToWrite);
                } else {
                    //if the rounded value is the same, but a different value was written, force a notification for the current field
                    if (newValue !== current) {
                        target.notifySubscribers(valueToWrite);
                    }
                }
            }
        }).extend({ notify: 'always' });

        //initialize with current value to make sure it is rounded appropriately
        result(target());

        //return the new computed observable
        return result;
    };
})();
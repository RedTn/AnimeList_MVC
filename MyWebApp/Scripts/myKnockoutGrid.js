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

            self.sortType = "ascending";
            self.sortTable = function (viewModel, e) {
                var orderProp = $(e.target).context.textContent;
                self.currentColumn(orderProp);
                self.data.sort(function (left, right) {
                    leftVal = left[orderProp];
                    rightVal = right[orderProp];
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

    templateEngine.addTemplate("ko_simpleGrid_grid", "\
                    <table class=\"ko-grid\" cellspacing=\"0\" >\
                        <thead>\
                            <tr data-bind=\"foreach: columns\">\
                               <th data-bind=\"text: headerText\"></th>\
                            </tr>\
                        </thead>\
                        <tbody data-bind=\"foreach: itemsOnCurrentPage\">\
                           <tr data-bind=\"foreach: $parent.columns\">\
                               <td data-bind=\"text: typeof rowText == 'function' ? rowText($parent) : $parent[rowText] \"></td>\
                            </tr>\
                        </tbody>\
                    </table>");
    templateEngine.addTemplate("ko_simpleGrid_pageLinks", "\
                    <div class=\"ko-grid-pageLinks\">\
                        <span>Page:</span>\
                        <!-- ko foreach: ko.utils.range(0, maxPageIndex) -->\
                               <a href=\"#\" data-bind=\"text: $data + 1, click: function() { $root.currentPageIndex($data) }, css: { selected: $data == $root.currentPageIndex() }\">\
                            </a>\
                        <!-- /ko -->\
                    </div>");
    templateEngine.addTemplate("anime_list_template", "\
               <div class = \"table-responsive\">\
                 <table class=\"table table-striped table-bordered table-condensed ko-grid\" cellspacing=\"0\">\
                      <thead>\
                          <tr data-bind=\"foreach: columns\" class=\"disableSelection\">\
                               <th>\
                                <!-- $parent header is required inside a foreach loop -->\
                               <a id=\"headerTitle\" data-bind=\"text: headerText, click: $parent.sortTable\"></a>\
                               <span data-bind=\"attr: { class: $parent.currentColumn() == headerText ? 'isVisible' : 'isHidden' }\">\
                               <i data-bind=\"attr: { class: $parent.iconType }\"></i>\
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
                                    <td align=\"center\"><img width=\"100\" height=\"100\" data-bind=\"attr:{src: $parent[rowImage]}\" /></td>\
                                <!--/ko-->\
                      </tr>\
                      </tbody>\
                 </table>\
               </div>\
        ");

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
})();
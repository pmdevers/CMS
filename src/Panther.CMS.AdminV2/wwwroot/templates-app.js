angular.module('templates-app', ['builder/builder.tpl.html', 'home/home.tpl.html', 'layout/header.tpl.html', 'layout/nav.tpl.html', 'navbar/navbar.tpl.html', 'sidebar/sidebar.tpl.html']);

angular.module("builder/builder.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("builder/builder.tpl.html",
    "<div id=\"builder\" ng-controller=\"builderCtrl\">\n" +
    "    <div id=\"tabs_container\">\n" +
    "        <ul class=\"tabs\" id=\"mbcode_tabs\">\n" +
    "            <li class=\"active mbcode_c_tab\" data-tab_i=\"0\" title=\"index.html\">index.html <span class=\"mbcode_c_tab_close\" href=\"#self\" ng-click=\"ToggleEditor\" data-tab_i=\"0\"><i class=\"fa fa-times\"></i></span></li></ul>\n" +
    "    </div>\n" +
    "    <div>\n" +
    "        <a href=\"\">code</a>\n" +
    "    </div>\n" +
    "    <div ng-show=\"editorVisible\" ui-ace=\"{\n" +
    "            useWrapMode : true,\n" +
    "            showGutter: true,\n" +
    "            theme:'chrome_razor',\n" +
    "            mode: 'razor',\n" +
    "            firstLineNumber: 1\n" +
    "        }\"></div>\n" +
    "</div>");
}]);

angular.module("home/home.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("home/home.tpl.html",
    "<h1>Hello</h1>");
}]);

angular.module("layout/header.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("layout/header.tpl.html",
    "<header class=\"clearfix\">\n" +
    "  <a href=\"#/\" class=\"toggle-min\" toggle-min-nav>\n" +
    "    <i class=\"fa fa-bars\"></i>\n" +
    "  </a>\n" +
    "\n" +
    "  <!-- Logo -->\n" +
    "  <div class=\"logo\">\n" +
    "    <a href=\"#/\">\n" +
    "      <span>PantherCMS</span>\n" +
    "    </a>\n" +
    "  </div>\n" +
    "\n" +
    "  <div class=\"menu-button\" toggle-off-convas>\n" +
    "    <span class=\"icon-bar\"></span>\n" +
    "    <span class=\"icon-bar\"></span>\n" +
    "    <span class=\"icon-bar\"></span>\n" +
    "  </div>\n" +
    "\n" +
    "  <div class=\"top-nav\">\n" +
    "    <ul class=\"nav-left list-unstyled\">\n" +
    "      <li class=\"dropdown\" dropdown is-open=\"isopenComment\">\n" +
    "        \n" +
    "      </li>\n" +
    "    </ul>\n" +
    "  </div>\n" +
    "</header>\n" +
    "");
}]);

angular.module("layout/nav.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("layout/nav.tpl.html",
    "<div id=\"nav-wrapper\">\n" +
    "    <div class=\"slimScrollDiv\">\n" +
    "        <ul id=\"nav\" ng-controller=\"NavCtrl\" collapse-nav slim-scroll highlight-active>\n" +
    "            <li>\n" +
    "                <a href=\"#/dashboard\">\n" +
    "                    <i class=\"fa fa-dashboard\">\n" +
    "                        <span class=\"icon-bg bg-danger\"></span>\n" +
    "                    </i>\n" +
    "                    <span i18n=\"Dashboard\"></span>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "            <li>\n" +
    "                <a href=\"#/ui\">\n" +
    "                    <i class=\"fa fa-magic\">\n" +
    "                        <span class=\"icon-bg bg-orange\"></span>\n" +
    "                    </i>\n" +
    "                    <span i18n=\"UI Kit\"></span>\n" +
    "                </a>\n" +
    "                <ul>\n" +
    "                    <li>\n" +
    "                        <a href=\"#/ui/buttons\">\n" +
    "                            <i class=\"fa fa-caret-right\"></i>\n" +
    "                            <span i18n=\"Buttons\"></span>\n" +
    "                        </a>\n" +
    "                    </li>\n" +
    "                </ul>\n" +
    "            </li>\n" +
    "        </ul>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("navbar/navbar.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("navbar/navbar.tpl.html",
    "<nav ng-Controller=\"NavbarCtrl\" class=\"navbar navbar-panther\" role=\"navigation\">\n" +
    "\n" +
    "    <div id=\"navbar-main-area\">\n" +
    "        <ul class=\"nav navbar-nav\">\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button ng-repeat=\"button in items\" id=\"mbcode_size_{{button.name}}\" type=\"button\" class=\"btn btn-default\" tooltip-placement=\"bottom\" tooltip-append-to-body=\"true\" tooltip-trigger=\"mouseenter\" tooltip-html-unsafe=\"{{button.tooltip}}<br /><small>(shortcut {{button.shortcut}})\"><i class=\"fa fa-{{button.icon}}\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "        </ul>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Collect the nav links, forms, and other content for toggling -->\n" +
    "    <div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">\n" +
    "\n" +
    "        <ul class=\"nav navbar-nav\">\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_id_select\" type=\"button\" class=\"btn btn-default mbcode_toolbar_button active\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-tool_name=\"mbcode_id_select\" data-original-title=\"Select tool\"><i class=\"fa fa-location-arrow l90\"></i></button>\n" +
    "                    <button id=\"mbcode_id_text_1\" type=\"button\" class=\"btn btn-default mbcode_toolbar_button\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-tool_name=\"mbcode_id_text_1\" data-original-title=\"Text mode\"><i class=\"fa fa-text-width\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "            <li id=\"mbcode_topmenu_texttools\" style=\"display: block;\">\n" +
    "                <div class=\"btn-group\">\n" +
    "\n" +
    "                    <button id=\"mbcode_id_text_bold\" type=\"button\" class=\"btn btn-default mbcode_toolbar_button\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Bold<br/><small>(shortcut cmd + b)</small>\"><i class=\"fa fa-bold\"></i></button>\n" +
    "                    <button id=\"mbcode_id_text_italic\" type=\"button\" class=\"btn btn-default mbcode_toolbar_button\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Italic<br/><small>(shortcut cmd + i)</small>\"><i class=\"fa fa-italic\"></i></button>\n" +
    "                    <button id=\"mbcode_id_text_underline\" type=\"button\" class=\"btn btn-default mbcode_toolbar_button\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Underline<br/><small>(shortcut cmd + u)</small>\"><i class=\"fa fa-underline\"></i></button>\n" +
    "\n" +
    "                </div>\n" +
    "            </li>\n" +
    "            \n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_s1_outline\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Outline mode<br/><small>(shortcut ctrl + o)</small>\"><i class=\"fa fa-minus-square-o\"></i></button>\n" +
    "\n" +
    "                    <button id=\"mbcode_s1_showhidden\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Show hidden<br/><small>(shortcut ctrl + h)</small>\"><i class=\"fa fa-plus-circle\"></i></button>\n" +
    "\n" +
    "                    <button id=\"mbcode_s1_dynoutline\" type=\"button\" class=\"btn btn-default hide\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Handle<br/><small>(shortcut ctrl + h)</small>\"><i class=\"fa fa-minus-square\"></i></button>\n" +
    "                    <button id=\"mbcode_s1_prevent\" type=\"button\" class=\"btn btn-default hide\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Prevent mode<br/><small>(shortcut ctrl + p)</small>\"><i class=\"fa fa-times-circle-o\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_s1_comments\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Show comments<br/><small></small>\"><i class=\"fa fa-comment\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_s1_zoom\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Zoom mode<br/><small></small>\"><i class=\"fa fa-search-minus\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_id_undo\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Undo<br/><small>(shortcut cmd + z)</small>\"><i class=\"fa fa-undo\"></i></button>\n" +
    "                    <button id=\"mbcode_id_redo\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Redo<br/><small>(shortcut cmd + shift + z)</small>\"><i class=\"fa fa-repeat\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "        </ul>\n" +
    "\n" +
    "        <a id=\"mbcode_s1_colog\" href=\"#self\" style=\"display: none;\">{c}</a>\n" +
    "\n" +
    "        <ul class=\"nav navbar-nav navbar-right\">\n" +
    "\n" +
    "            <li class=\"hide\" id=\"mbcode_sec_takeownership\">\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_takeownership\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Take ownership<br/><small></small>\">Register <b>FREE</b></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "            <!--\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_rate\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Rate app<br/><small></small>\" data-container=\"body\"><i class=\"fa fa-star-o\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "            -->\n" +
    "            <li><button id=\"mbcode_s1_test\" type=\"button\" class=\"btn btn-success\">Live Test</button></li>\n" +
    "\n" +
    "            <li>\n" +
    "\n" +
    "                <div class=\"btn-group\">\n" +
    "\n" +
    "                    <button id=\"mbcode_archive\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Download project<br/><small>(shortcut ctrl + d)</small>\"><i class=\"fa fa-cloud-download\"></i></button>\n" +
    "\n" +
    "                    <a href=\"#self\" id=\"header-avatar\" class=\"dropdown-toggle btn btn-default\" data-toggle=\"dropdown\" rel=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Share this project\"><i class=\"fa fa-share\"></i></a>\n" +
    "                    <ul class=\"dropdown-menu\">\n" +
    "                        <li>\n" +
    "                            <div class=\"input-group\">\n" +
    "                                <input type=\"text\" class=\"form-control\" id=\"mbcode_preview_link\">\n" +
    "                                <span class=\"input-group-btn\">\n" +
    "                                    <button class=\"btn btn-default\" id=\"mbcode_share_copy\" type=\"button\"><i class=\"fa fa-copy\"></i></button>\n" +
    "                                </span>\n" +
    "                            </div>\n" +
    "                        </li>\n" +
    "                        <li class=\"divider\"></li>\n" +
    "                        <li><a id=\"mbcode_share_facebook\" href=\"#self\"><i class=\"fa fa-facebook\"></i> Share on Facebook</a></li>\n" +
    "                        <li><a id=\"mbcode_share_twitter\" href=\"#self\"><i class=\"fa fa-twitter\"></i> Share on Twitter</a></li>\n" +
    "                        <li><a id=\"mbcode_share_linkedin\" href=\"#self\"><i class=\"fa fa-linkedin\"></i> Share on LinkedIn</a></li>\n" +
    "                        <!--\n" +
    "                         <li><a href=\"index.html#\"><i class=\"fa fa-envelope-o\"></i> Send to e-mail</a></li>\n" +
    "                         -->\n" +
    "                    </ul>\n" +
    "\n" +
    "                </div>\n" +
    "\n" +
    "            </li>\n" +
    "\n" +
    "            <!--\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_s1_save\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Save file<br/><small>(shortcut cmd + s)</small>\" data-container=\"body\"><i class=\"fa fa-floppy-o\"></i></button>\n" +
    "                    <button id=\"mbcode_archive\" type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Download project<br/><small>(shortcut ctrl + d)</small>\" data-container=\"body\"><i class=\"fa fa-cloud-download\"></i></button>\n" +
    "                    <button type=\"button\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Create iteration<br/><small>(shortcut cmd + alt + i)</small>\" data-container=\"body\"><i class=\"fa fa-archive\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "            -->\n" +
    "            <!--\n" +
    "             <li class=\"dropdown\">\n" +
    "             <a href=\"index.html#\" id=\"header-avatar\" class=\"dropdown-toggle btn btn-default\" data-toggle=\"dropdown\"  rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Contributors/<br/>Commentators\" data-container=\"body\"><i class=\"fa fa-users\"></i></a>\n" +
    "             <ul class=\"dropdown-menu\">\n" +
    "             <li class=\"title\">Contributors</li>\n" +
    "             <li><a href=\"index.html#\"><img src=\"assets/img/avatar.jpg\"> Lukasz Holeczek</a></li>\n" +
    "             <li><a href=\"index.html#\"><img src=\"assets/img/avatar.jpg\"> Lukasz Holeczek</a></li>\n" +
    "             <li class=\"divider\"></li>\n" +
    "             <li><a href=\"index.html#\"><i class=\"fa fa-plus\"></i> Add a contributor</a></li>\n" +
    "             <li class=\"title\">Commentators</li>\n" +
    "             <li><a href=\"index.html#\"><img src=\"assets/img/avatar.jpg\"> Lukasz Holeczek</a></li>\n" +
    "             <li><a href=\"index.html#\"><img src=\"assets/img/avatar.jpg\"> Lukasz Holeczek</a></li>\n" +
    "             <li><a href=\"index.html#\"><img src=\"assets/img/avatar.jpg\"> Lukasz Holeczek</a></li>\n" +
    "             <li class=\"divider\"></li>\n" +
    "             <li><a href=\"index.html#\"><i class=\"fa fa-plus\"></i> Add a commentator</a></li>\n" +
    "             </ul>\n" +
    "             </li>\n" +
    "             -->\n" +
    "\n" +
    "            <li>\n" +
    "                <div class=\"btn-group\">\n" +
    "                    <button id=\"mbcode_fullscreen\" type=\"button\" class=\"btn btn-default\" data-toggle=\"dropdown\" rel=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Toggle Fullscreen\"><i class=\"fa fa-arrows-alt\"></i></button>\n" +
    "                    <button id=\"mbcode_settings\" type=\"button\" class=\"btn btn-default\" data-toggle=\"dropdown\" rel=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Settings\"><i class=\"fa fa-cog\"></i></button>\n" +
    "\n" +
    "                    <!--<a href=\"https://www.youtube.com/watch?v=xmlSXeQIlQA\" target=\"_blank\" class=\"btn btn-default\" rel=\"tooltip\" data-toggle=\"tooltip\" data-placement=\"bottom\" title=\"Help Video\" data-container=\"body\"><i class=\"fa fa-youtube-play\"></i></a>-->\n" +
    "                    <button id=\"mbcode_help\" type=\"button\" class=\"btn btn-default\" data-toggle=\"dropdown\" rel=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Help\"><i class=\"fa fa-question\"></i></button>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "            <!--<li class=\"dropdown\">\n" +
    "                <a href=\"#self\" id=\"header-avatar\" class=\"dropdown-toggle btn btn-default user\" data-toggle=\"dropdown\" rel=\"tooltip\" data-placement=\"bottom\" title=\"\" data-container=\"body\" data-original-title=\"Account\"><img id=\"mbcode_user_avatar\" src=\"/assets/logo.png\"></a>\n" +
    "                <ul class=\"dropdown-menu\">\n" +
    "                    <li><a id=\"mbcode_myprojects\" href=\"#self\">My Account</a></li>\n" +
    "                    <li class=\"divider\"></li>\n" +
    "                    <li><a id=\"mbcode_logout\" href=\"#self\">Logout</a></li>\n" +
    "                </ul>\n" +
    "            </li>-->\n" +
    "\n" +
    "        </ul>\n" +
    "\n" +
    "\n" +
    "    </div><!-- /.navbar-collapse -->\n" +
    "\n" +
    "\n" +
    "\n" +
    "</nav>");
}]);

angular.module("sidebar/sidebar.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("sidebar/sidebar.tpl.html",
    "<div id=\"sidebar\" ng-controller=\"SidebarCtrl\">\n" +
    "    <div id=\"sidebar-menu\">\n" +
    "        <ul>\n" +
    "            <li>\n" +
    "                <a href=\"#self\">\n" +
    "                    <i class=\"fa fa-home\"></i>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "            <li ng-Repeat=\"item in sideBarItems\" >\n" +
    "                <a href=\"\" ng-Click=\"showSubMenu(item)\" target=\"#favourite-bricks\" tooltip-placement=\"right\" tooltip-append-to-body=\"true\" tooltip-html-unsafe=\"{{item.tooltip}}<br /><small>(shortcut {{item.shortcut}})\" original-title=\"\">\n" +
    "                    <i class=\"fa fa-{{item.icon}}\"></i>\n" +
    "                    <i class=\"fa fa-{{item.option}} option\" ng-Show=\"'hasOption(item)'\"></i>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "        </ul>\n" +
    "        <img id=\"logo\" src=\"assets/logo.png\" title=\"\">\n" +
    "    </div>\n" +
    "\n" +
    "    <div id=\"submenu\">\n" +
    "        <div id=\"newPage\" style=\"display: none\" class=\"blocks\">\n" +
    "            <div class=\"form-group\">\n" +
    "                <input type=\"text\" class=\"form-control\" placeholder=\"type to search element\"/>\n" +
    "                <i class=\"fa fa-times cancel\"></i>\n" +
    "            </div>\n" +
    "            <ul class=\"tools\" id=\"tools_fav\">\n" +
    "                <li class=\"title\">\n" +
    "                    <div class=\"tools-category\">\n" +
    "                        favourites\n" +
    "                        <i class=\"fa fa-angle-down\"></i>\n" +
    "                    </div>\n" +
    "                    <ul style=\"display: block\">\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                        <li class=\"toolbar_button\">\n" +
    "                            <div class=\"toolbar\">\n" +
    "                                <i class=\"fa fa-star\" title=\"add/remove from favourites\"></i>\n" +
    "                                <i class=\"fa fa-gear\" title=\"brick properties\"></i>\n" +
    "                            </div>\n" +
    "                            <img class=\"img-responsive\" src=\"/assets/tools/container.png\" draggable=\"true\"/>\n" +
    "                            <span>container</span>\n" +
    "                        </li>\n" +
    "                    </ul>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "        </div>\n" +
    "        <div id=\"site\" style=\"display: none;\" class=\"formmenu\">\n" +
    "            <ul class=\"tools\">\n" +
    "                <li class=\"title\">\n" +
    "                    <ul></ul>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "            <button type=\"button\" class=\"btn btn-success\">Add Page</button>\n" +
    "            <div class=\"form-group\">\n" +
    "                <label for=\"element\">Element</label>\n" +
    "                <input type=\"text\" class=\"form-control\"/>\n" +
    "            </div>\n" +
    "            <div id=\"content\">\n" +
    "                <div class=\"form-group\">\n" +
    "                    <label for=\"element\">Element</label>\n" +
    "                    <input type=\"text\" class=\"form-control\"/>\n" +
    "                </div>\n" +
    "                <div class=\"form-group\">\n" +
    "                    <label for=\"element\">Element</label>\n" +
    "                    <input type=\"text\" class=\"form-control\"/>\n" +
    "                </div>\n" +
    "                <div class=\"form-group\">\n" +
    "                    <label for=\"element\">Element</label>\n" +
    "                    <input type=\"text\" class=\"form-control\"/>\n" +
    "                </div>\n" +
    "                <div class=\"form-group\">\n" +
    "                    <div class=\"divider\"></div>\n" +
    "                </div>\n" +
    "                <div>\n" +
    "                    <div class=\"form-group\">\n" +
    "                        <label for=\"element\">Checkbox</label>\n" +
    "                        <div class=\"input-group\">\n" +
    "                            <input type=\"text\" class=\"form-control\"/>\n" +
    "                            <span class=\"input-group-btn\">\n" +
    "                                <button class=\"btn btn-default colorpicker-element\" type=\"button\">\n" +
    "                                    <i class=\"fa fa-dot-circle-o\"></i>\n" +
    "                                </button>\n" +
    "                            </span>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div class=\"form-group\">\n" +
    "                        <label for=\"element\">Element</label>\n" +
    "                        <select class=\"form-control\">\n" +
    "                            <option>Italic</option>\n" +
    "                            <option>Underlinde</option>\n" +
    "                            <option>Bold</option>\n" +
    "                        </select>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>");
}]);

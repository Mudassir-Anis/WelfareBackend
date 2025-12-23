/* eslint-disable no-extra-parens */

"use strict";

if (window.mApp === undefined) {
    try {
      window.mApp = KTApp;
    } catch (e) {
    
    }
}
if (window.Page === undefined) {
    window.Page = {};
}
if (Page.AJAXList === undefined) {
    Page.AJAXList = [];
}

(function ($) {
    $.fn.serializeAll = function () {
        var data = $(this).serializeArray();

        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        return data;
    }
})(jQuery);
var Utils = function () {
    this.BrowserDetect = {
        browserSpecs: function () {
            var ua = navigator.userAgent, tem,
                M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
            if (/trident/i.test(M[1])) {
                tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
                return { name: 'IE', version: (tem[1] || '') };
            }
            if (M[1] === 'Chrome') {
                tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
                if (tem !== null) return { name: tem[1].replace('OPR', 'Opera'), version: tem[2] };
            }
            M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
            if ((tem = ua.match(/version\/(\d+)/i)) !== null)
                M.splice(1, 1, tem[1]);
            return { name: M[0], version: M[1] };
        },
        // Opera 8.0+
        isOpera: function () { return (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0; },

        // Firefox 1.0+
        isFirefox: function () { return typeof InstallTrigger !== 'undefined'; },

        // Safari 3.0+ "[object HTMLElementConstructor]" 
        isSafari: function () { return /constructor/i.test(window.HTMLElement) || (function (p) { return p.toString() === "[object SafariRemoteNotification]"; })(!window['safari'] || (typeof safari !== 'undefined' && safari.pushNotification)); },

        // Internet Explorer 6-11
        isIE: function () { return /*@cc_on!@*/false || !!document.documentMode; },

        // Edge 20+
        isEdge: function () { return !isIE && !!window.StyleMedia; },

        // Chrome 1 - 76
        isChrome: function () { return /Chrome/.test(navigator.userAgent) && /Google Inc/.test(navigator.vendor); },

        // Blink engine detection
        isBlink: function () { return (isChrome || isOpera) && !!window.CSS; }
    },
        this.NLP = {
            Verbs: {
                GetInfinitive: function (word) {
                    var Conjugate = nlp(word).verbs().conjugate();
                    return Conjugate.length > 0 ? Conjugate[0].Infinitive : word[word.length - 1] === 'd' ? word.slice(0, -1) : word;
                }
            }
        },
        this.URL = {
            RemoveURLParameter: function (url, parameter) {
                //prefer to use l.search if you have a location/link object
                var urlparts = url.split('?');
                if (urlparts.length >= 2) {

                    var prefix = encodeURIComponent(parameter) + '=';
                    var pars = urlparts[1].split(/[&;]/g);

                    //reverse iteration as may be destructive
                    for (var i = pars.length; i-- > 0;) {
                        //idiom for string.startsWith
                        if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                            pars.splice(i, 1);
                        }
                    }

                    return urlparts[0] + (pars.length > 0 ? '?' + pars.join('&') : '');
                }
                return url;
            },
            RemoveQueryStringFromURL: function () {

                var uri = window.location.href.toString();
                if (uri.indexOf("?") > 0) {
                    var clean_uri = uri.substring(0, uri.indexOf("?"));
                    window.history.replaceState({}, document.title, clean_uri);
                }
            },
            HasQueryString: function (url) {

                return /[?&]q=/.test(url);
            },
            Open: function (URL) {
                let a = document.createElement('a');
                a.href = URL;
                //a.download = URL.split('/').pop();
                //a.target = "_blank";
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            },
            OpenNewTab: function (url, Data, NewURL = false) {
                if (!NewURL) {

                    if (Utilities.IsNotUndefinedOrNull(Data)) {
                        if (!Utilities.URL.HasQueryString(url)) {
                            url += '?';
                        }
                        url += $.param(Data);
                    }

                    if (!url.includes(APP.BasePath) && !url.includes("blob:")) {
                        url = APP.BasePath + url;
                    }
                }
                let a = document.createElement('a');
                a.href = url;
                //a.download = url.split('/').pop();
                a.target = "_blank";
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            },
            BlobInNewTab: function (url) {
                Utilities.HTML.Loader.Show();
                if (!url.includes(APP.BasePath)) {
                    url = APP.BasePath + url;
                }
                fetch(url)
                    .then(res => res.blob()) // Gets the response and returns it as a blob
                    .then(blob => {
                        // Here's where you get access to the blob
                        // And you can use it for whatever you want
                        // Like calling ref().put(blob)

                        // Here, I use it to make an image appear on the page
                        let objectURL = URL.createObjectURL(blob);
                        let blobUrl = objectURL;
                        Utilities.URL.OpenNewTab(blobUrl);
                        Utilities.HTML.Loader.Hide();

                    });

            },
            OpenNewPostTab: function (url, Data) {
                url = APP.BasePath + url;
                var _form = $("<form target='_blank' method='POST' style='display:none;'></form>").attr({
                    action: url
                }).appendTo(document.body);
                for (var i in Data) {
                    if (Data.hasOwnProperty(i)) {
                        $('<input type="hidden" />').attr({
                            name: i,
                            value: Data[i]
                        }).appendTo(_form);
                    }
                }
                _form.submit();
                _form.remove();
            },
            OpenNewBlobTab: function (url, Data) {

                url = APP.BasePath + url;
                $.ajax({
                    type: "POST",
                    url: url,
                    data: Data,
                    xhrFields: { responseType: 'blob' },
                    beforeSend: function () {
                        Utilities.HTML.Loader.Show();
                    },
                    success: function (response, status, xhr) {

                        Utilities.URL.OpenReport(response, status, xhr);
                    },
                    complete: function () {
                        Utilities.HTML.Loader.Hide();
                    },
                    error: function (jqXHR, error, errorThrown) {

                        toastr.error("Internal Server error");
                        //errors(jqXHR, error, errorThrown);
                    }
                });
            },
            Download: function (URL) {
                if (!URL.includes(APP.BasePath)) {
                    URL = APP.BasePath + URL;
                }
                let a = document.createElement('a');
                a.href = URL;
                a.download = URL.split('/').pop();
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            },
            OpenReport: function (response, status, xhr) {
                var filename = "";
                var disposition = xhr.getResponseHeader('Content-Disposition');
                if (disposition && disposition.indexOf('attachment') !== -1) {
                    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    var matches = filenameRegex.exec(disposition);
                    if (matches !== null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                }
                var type = xhr.getResponseHeader('Content-Type');
                var blob = new Blob([response], { type: type });
                if (typeof window.navigator.msSaveBlob !== 'undefined') {
                    /*IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."*/
                    window.navigator.msSaveBlob(blob, filename);
                } else {
                    var URL = window.URL || window.webkitURL;
                    var downloadUrl = URL.createObjectURL(blob);
                    if (filename) {
                        /*use HTML5 a[download] attribute to specify filename*/
                        var a = document.createElement("a");
                        /*safari doesn't support this yet*/
                        if (typeof a.download === 'undefined') {
                            window.location = downloadUrl;
                        } else {
                            a.href = downloadUrl;
                            a.download = filename;
                            document.body.appendChild(a);
                            a.click();
                        }
                    } else {
                        var windowVal = window.open(downloadUrl);/*openNewTabOrNewWindow(downloadUrl);*/
                        windowVal.opener = null;
                        if (!windowVal) {
                            mesgboxshow("error", "Please allow pop-ups for view report. <strong>How to allow pop-ups : <a target='_blank' href='https://support.mozilla.org/en-US/kb/pop-blocker-settings-exceptions-troubleshooting'><span>Click here<span></a></strong>");
                        }
                    }
                    setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); /*cleanup*/
                }
            },
            PrintPDF: function (URL) {
                if (!url.includes(APP.BasePath)) {
                    url = APP.BasePath + url;
                }
                var iFrameJQueryObject = $('<iframe id="iframe" src="' + URL + '" style="display:none"></iframe>');
                $('body').append(iFrameJQueryObject);
                iFrameJQueryObject.on('load', function () {
                    $(this).get(0).contentWindow.print();
                });
            },
            Print: function (url, PrintOnLoad = true) {
                if (!url.includes(APP.BasePath)) {
                    url = APP.BasePath + url;
                }
                if (!Utilities.BrowserDetect.isChrome()) {
                    Utilities.URL.OpenNewTab(url);
                    return false;
                }
                window.PrintPDF = function () {
                    window.frames["PrintFrame"].focus();
                    window.frames["PrintFrame"].print();
                };
                var Options = {
                    title: 'Printing',
                    message: '<p><div class="m-loader m-loader--brand" style="width: 30px; display: inline-block;"></div> Printing...<span class="PrintArea"></span></p>'
                };
                if (PrintOnLoad) {
                    //_.assign(Options, {
                    //    size: 'small'
                    //});
                    Utilities.HTML.Loader.Show();
                    $('.HiddenPrintArea').remove();
                    if ($('.HiddenPrintArea').length < 1) {
                        $('body').append('<div style="display:none" class="HiddenPrintArea"></div>');
                    }
                    Page.HiddenPrintAreaSelector = '.HiddenPrintArea';
                    Page.PrintURL = url;

                    fetch(url)
                        .then(res => res.blob()) // Gets the response and returns it as a blob
                        .then(blob => {
                            // Here's where you get access to the blob
                            // And you can use it for whatever you want
                            // Like calling ref().put(blob)

                            // Here, I use it to make an image appear on the page
                            let objectURL = URL.createObjectURL(blob);
                            let blobUrl = objectURL;
                            var $selected = $(Page.HiddenPrintAreaSelector);

                            $selected.html('<div  class="HiddenPrintBox" style="display:none;width:100%; height:100%"><a href="' + blobUrl + '" target="_blank" class="pull-right btn btn-outline-primary m-btn m-btn--icon m-btn--icon-only"><i class="la la-external-link"></i></a><iframe style="width:100%; height:100%" id="PrintFrame" name="PrintFrame" src="' + blobUrl + '" frameborder="0" allowtransparency="true"></iframe><div>');
                            PrintPDF();
                            Utilities.HTML.Loader.Hide();

                        });



                } else {
                    _.assign(Options, {
                        size: 'large',
                        buttons: {
                            Close: {
                                label: 'Close'
                                //className: 'btn-success'
                            },
                            Download: {
                                label: 'Download',
                                callback: function (e) {
                                    Utilities.URL.Download(URL);
                                }
                            },
                            Print: {
                                label: 'Print',
                                callback: function (e) {
                                    PrintPDF();

                                }
                            }
                        }
                    });
                    var PrintInModaldialog = bootbox.dialog(Options);

                    PrintInModaldialog.init(function () {
                        if ($('.HiddenPrintArea').length < 1) {
                            $('body').append('<div style="display:none" class="HiddenPrintArea"></div>');
                        }
                        var selector = '.HiddenPrintArea';
                        if (!PrintOnLoad) {
                            selector = '.bootbox-body, .HiddenPrintArea';
                        }
                        var $selected = $(selector);
                        $selected.html('<div  class="HiddenPrintBox" style="display:none;width:100%; height:100%"><a href="' + URL + '" target="_blank" class="pull-right btn btn-outline-primary m-btn m-btn--icon m-btn--icon-only"><i class="la la-external-link"></i></a><iframe style="width:100%; height:100%" id="PrintFrame" name="PrintFrame" src="' + URL + '" frameborder="0" allowtransparency="true"></iframe><div>');
                        if (!PrintOnLoad) {
                            $selected.css('height', '500px').find('.HiddenPrintBox').slideDown();
                        }
                        if (PrintOnLoad) {
                            PrintPDF();
                            setTimeout(function () {
                                //PrintInModaldialog.modal('hide');
                                bootbox.hideAll();
                            }, 1300);
                        }
                        setTimeout(function () {
                            //PrintInModaldialog.modal('hide');
                            $(".modal-footer button:contains('Print')").focus();
                        }, 700);
                        //if (CloseModelAfterMiliSeconds > 0) {
                        //}
                    });
                }

            }
        },
        this.String = {
            GetStringFromSquareBracets: function (input) {
                var matches = [];
                input.replace(/(\[+)([^\]]+)(]+)/g, function (orig, lb, txt, rb) {
                    if (lb.length === rb.length)
                        matches.push(txt);
                });
                return matches;
            }
        },
        this.Number = {
            isNumber: function (n) {
                return !isNaN(parseFloat(n)) && isFinite(n);
            },
            toFixed: function (val, decimalPlaces) {
                var multiplier = Math.pow(10, decimalPlaces);
                return (Math.round(val * multiplier) / multiplier).toFixed(decimalPlaces);
            },
            ABS: function (num) {
                num = isNaN(num) ? 0 : Number(num);
                return Utilities.Number.toFixed(num, 0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,").replace(/\,/g, '');
            }
        },
        this.Math = {
        },
        this.GetAppMessage = function (_code) {
            let _alertObj = AppCodes[_code];
            if (!_alertObj) {
                _alertObj = AppCodes[172];//Unknown Error
            }
            return _alertObj;
        },
        this.GetHTTPStatus = function (_code) {
            return _.find(HTTPStatusCodes, { 'code': _code });
        },
        this.HTML = {
            Form: {
                Populate: function (Form, JSON) {
                    $.each(JSON, function (key, value) {
                        var ctrl = $('[name=' + key + ']', Form);
                        switch (ctrl.prop("type")) {
                            case "radio": case "checkbox":
                                ctrl.each(function () {
                                    if ($(this).attr('value') === value || $(this).attr('value') === value.toString()) $(this).attr("checked", value);
                                });
                                break;
                            default:
                                ctrl.val(value);
                        }
                    });
                }
            },
            Event: {
                OnDoneTyping: function (FieldSelector, OnDoneFunction, WaitTime = 666.667, CallBackFunction, AfterHit) {
                    var ButtonKeys = { "EnterKey": 13 };
                    window.typingTimer = null;                //timer identifier
                    var doneTypingInterval = WaitTime;  //time in ms, 5 second for example
                    var $input = $(FieldSelector);

                    //on keyup, start the countdown

                    //                    if (!$input.hasClass('OnDoneTypingChangeBound')) {
                    let OnEvents = 'propertychange change keyup input paste';
                    var IgnoreKeys = ["Tab", "Shift", "Enter"];
                    $input.on(OnEvents, function (e) {
                        if (IgnoreKeys.indexOf(e.key) > -1) {
                            return;
                        }
                        //console.log(e.key);
                        if (Utilities.Isfunction(AfterHit)) {
                            AfterHit();
                        }
                        //if (e.which === Enumerators.KeyCodes.enter) {
                        //    doneTyping($(this));
                        //}
                        else {
                            clearTimeout(typingTimer);
                            typingTimer = setTimeout(doneTyping, doneTypingInterval, $(this));
                        }

                    });
                    //}

                    //on keydown, clear the countdown 
                    //if (!$input.hasClass('OnDoneTypingKeyDownBound')) {
                    $input.on('keydown', function (e) {

                        if (IgnoreKeys.indexOf(e.key) > -1) {
                            return;
                        }
                        clearTimeout(typingTimer);
                    });
                    //}

                    //user is "finished typing," do something
                    function doneTyping($input) {
                        var doneTypingPromise = new Promise((resolve, reject) => {
                            // We call resolve(...) when what we were doing async succeeded, and reject(...) when it failed.
                            // In this example, we use setTimeout(...) to simulate async code. 
                            // In reality, you will probably be using something like XHR or an HTML5 API.

                            OnDoneFunction($input);
                            resolve("Success!"); // Yay! Everything went well!
                        });

                        doneTypingPromise.then((successMessage) => {
                            // successMessage is whatever we passed in the resolve(...) function above.
                            // It doesn't have to be a string, but if it is only a succeed message, it probably will be.
                            try {
                                if (Utilities.Isfunction(CallBackFunction())) {
                                    CallbackFunction();
                                }
                            } catch (e) {
                                var a = 2 + 2; // Do Nothing
                            }
                        });

                    }
                }
            },
            Alert: {
                Show: function (_code, _message, options = {}, MesageMapJSON = {}) {
                    var _alertObj = AppCodes[_code];
                    debugger;
                    if (!_alertObj) {
                        _alertObj = AppCodes[172];//Unknown Error
                    }
                    if (Utilities.IsUndefinedOrNull(_message)) {
                        _message = _alertObj.Message;
                    }
                    
                    _message=Mustache.render(_message, MesageMapJSON);
                    let basicOptions = {
                        title: _alertObj.Type,
                        html: _message,
                        icon: _alertObj.Type.toLowerCase()==='blocked' ? 'error':_alertObj.Type.toLowerCase()
                    };
                    _.assign(basicOptions, options);
                    
                    swal.fire(basicOptions);
                }
            },
            Get: {
                Numbers: function (Selector) {
                    return $(Selector).map(function () { return parseFloat($(this).val()) || 0; }).get();
                }
            },
            Loader: {
                Show: function (selector, Message = 'Processing') {
                    if ((_.isObject(selector) && selector.tagName === 'BUTTON') || (_.isString(selector) && document.querySelector(selector) && document.querySelector(selector).tagName === 'BUTTON')) {
                        if ((_.isObject(selector) && !selector.id) || (_.isString(selector) && !document.querySelector(selector).id)) {
                            return false;
                        }
                        if (_.isString(selector)) {
                            selector = document.querySelector(selector);
                        } else
                            selector = selector;

                        if (!selector.disabled) {
                            selector.disabled = true;

                            if (!_.isArray(Page.Saving)) {
                                Page.Saving = [];
                            }
                            if (!_.isObject(Page.Saving[selector])) {
                                Page.Saving[selector] = {};
                            }
                            Page.Saving[selector].Icon = selector.querySelector('span.ButtonContent > i').outerHTML;
                            Page.Saving[selector].Text = selector.querySelector('span.ButtonText').innerText;
                            selector.querySelector('span.ButtonContent > i').remove();
                            selector.querySelector('span.ButtonContent').insertAdjacentHTML('afterbegin', '<img src="/Financial/Content/images/icons/Saving/index.glowing-rotate-ring.svg" alt="Saving" style="width: 26px" ">');


                            let verb = selector.querySelector('span.ButtonText').innerText;
                            let Text = Message;
                            try {
                                let gerund = nlp(verb).verbs().conjugate()[0].Gerund;
                                Text = nlp(gerund).toTitleCase().out();
                            } finally {
                                selector.querySelector('span.ButtonText').innerText = Text + '. . .';
                            }

                        }

                    } else if (Utilities.IsNotUndefinedOrNull(selector)) {
                        window.mApp.block(selector, {
                            overlayColor: '#000000',
                            type: 'loader',
                            state: 'primary',
                            message: Message + "...",
                            opacity: 0.3
                        });
                    } else {
                        window.mApp.blockPage({
                            overlayColor: '#000000',
                            type: 'loader',
                            state: 'primary',
                            message: Message + "...",
                            opacity: 0.3
                        });
                    }
                },
                Hide: function (selector) {
                    if ((_.isObject(selector) && selector.tagName === 'BUTTON') || (_.isString(selector) && document.querySelector(selector) && document.querySelector(selector).tagName === 'BUTTON')) {
                        if ((_.isObject(selector) && !selector.id) || (_.isString(selector) && !document.querySelector(selector).id)) {
                            return false;
                        }
                        if (_.isString(selector)) {
                            selector = document.querySelector(selector);
                        } else
                            selector = selector;
                        if (selector.disabled) {
                            selector.disabled = false;

                            if (_.isArray(Page.Saving)) {

                                if (_.isObject(Page.Saving[selector])) {
                                    selector.querySelector('span.ButtonContent img').remove();
                                    selector.querySelector('span.ButtonContent').insertAdjacentHTML('afterbegin', Page.Saving[selector].Icon);
                                    selector.querySelector('span.ButtonText').innerText = Page.Saving[selector].Text;
                                }
                            }
                        }
                    } else
                        if (Utilities.IsNotUndefinedOrNull(selector)) {
                            window.mApp.unblock(selector);
                        } else {
                            window.mApp.unblockPage();
                        }
                }
            },
            Load: {
                Stack: function (URLs, StackContainerSelector = '.StackContainer') {
                    Utilities.HTML.Loader.Show();
                    $.each(URLs, function (Index, Element) {
                        $.ajax({
                            async: false,
                            url: Element, success: function (result) {
                                $(".StackContainer").append(result);
                            }
                        });
                    });
                    Utilities.HTML.Loader.Hide();
                }
            },
            ScrollTo: function (Selector) {
                Page.BeforeScrollHeadLength = $('.m-portlet--sticky > .m-portlet__head').length;
                let top = $(Selector).offset().top - ($('#m_header').height()) - ($('.m-portlet--sticky > .m-portlet__head').height() || 0) - $('footer.m-footer').height();
                console.log(top);
                console.log($(Selector).offset().top);
                $([document.documentElement, document.body]).animate({
                    scrollTop: top
                }, 70, function (data) {
                    setTimeout(function () {
                        //($('.m-portlet--sticky > .m-portlet__head').height() || 0)
                        console.log("Before " + Page.BeforeScrollHeadLength);
                        let scrollMultiplier = 1;
                        if (Page.BeforeScrollHeadLength === 0) {
                            scrollMultiplier = 2;
                        }
                        $([document.documentElement, document.body]).animate({
                            scrollTop: $([document.documentElement, document.body]).scrollTop() - ($('.m-portlet--sticky > .m-portlet__head').height() * scrollMultiplier || 0)
                        }, 70);
                    }, 70);
                });
            },
            Toastr: {
                Show: function (_code) {
                    var _alertObj = AppCodes[_code];
                    if (!_alertObj) {
                        _alertObj = AppCodes[172];//Unknown Error
                    }
                    switch (_alertObj.Type) {
                        case 'Success':
                            toastr.success(_alertObj.Message);
                            break;
                        case 'Info':
                            toastr.info(_alertObj.Message);
                            break;
                        case 'Warning':
                            toastr.warning(_alertObj.Message);
                            break;
                        case 'Error':
                            toastr.error(_alertObj.Message);
                            break;
                        default:
                        // code block
                    }
                }
            },
            TypeAhead: function (params = {}) {
                let DefaultOptions = {
                    hint: true,
                    highlight: true,
                    minLength: 1
                };
                if (Page.substringMatcher === undefined) {
                    Page.substringMatcher = {};
                }
                if (params.name === undefined) {
                    params.name = Utilities.JavaScript.guid();
                }
                Page.substringMatcher[params.name] = function (strs) {
                    return function findMatches(q, cb) {
                        var matches, substrRegex;

                        // an array that will be populated with substring matches
                        matches = [];

                        // regex used to determine if a string contains the substring `q`
                        substrRegex = new RegExp(q, 'i');

                        // iterate through the pool of strings and for any string that
                        // contains the substring `q`, add it to the `matches` array
                        $.each(strs, function (i, str) {
                            if (substrRegex.test(str)) {
                                matches.push(str);
                            }
                        });

                        cb(matches);
                    };
                };
                if (params.options !== undefined) {
                    _.assign({}, DefaultOptions, params.options);
                }

                if (params.substringMatcher !== undefined && _.isFunction(params.substringMatcher)) {
                    Page.substringMatcher[params.name] = params.substringMatcher;
                }
                $(params.selector).typeahead(DefaultOptions, {
                    name: params.name,
                    source: Page.substringMatcher[params.name](params.source)
                });

            }
        },
        this.Select2 = {
            OldMatcherSearching: function (Selector) {///Remeber To Use Select2 Full With This
                function matchStart(term, text) {
                    if (text.toUpperCase().indexOf(term.toUpperCase()) === 0 || text.toUpperCase().indexOf(term.toUpperCase()) === '0') {
                        return true;
                    }

                    return false;
                }

                $.fn.select2.amd.require(['select2/compat/matcher'], function (oldMatcher) {
                    $(Selector).select2({
                        matcher: oldMatcher(matchStart)
                    });
                });
            },
            AJAXSelectList: function (Arguments) {///AKA Cascade

                ///Usage:
                //var CityArguments = { SourceSelector: '#StateOrProvinceId', TargetSelector: '#CityId', URL: "GetCities", ExtraParameters:{param1:val1}, SourceName: 'StateOrProvinceId', ReturnFieldNameText: 'Name', ReturnFieldNameId: 'Id', DefaultOption: '@ViewBag.CityName', SourceText: 'State Or Province', TargetText: 'City', SubArguments: null, CallBackFirstTime: function () { $('form#patient').bootstrapValidator('updateStatus', 'CityId', 'NOT_VALIDATED') } };
                //var StateOrProvinceArguments = { SourceSelector: '#CountryId', TargetSelector: '#StateOrProvinceId', URL: "GetStateOrProvinces", SourceName: 'CountryId', ReturnFieldNameText: 'Name', ReturnFieldNameId: 'Id', DefaultOption: '@ViewBag.StateName', SourceText: 'Country', TargetText: 'State or Province', SubArguments: CityArguments, CallBackFirstTime: function () { $('form#patient').bootstrapValidator('updateStatus', 'StateOrProvinceId', 'NOT_VALIDATED') } };

                //Utilities.Select2.AJAXSelectList(StateOrProvinceArguments)
                //Arguments.OldMatcherSearching = true 
                //
                var Select2Options = {};
                if (Utilities.IsNotUndefinedOrNull(window.defaultSelect2Options)) {
                    _.assign(Select2Options, window.defaultSelect2Options);

                }
                if (Arguments !== undefined && Arguments !== null && (Arguments.Select2Options !== undefined)) {
                    _.assign(Select2Options, Arguments.Select2Options);
                }


                if (Utilities.IsNotUndefinedOrNull(Arguments)) {


                    $('.modal').attr('tabindex', '');

                    if (Utilities.IsNotUndefinedOrNull(Arguments) && Utilities.Isfunction(Arguments.CallBackFirstTime)) {
                        if (Utilities.IsUndefinedOrNull(Page.CallBackFirstTime)) {
                            Page.CallBackFirstTime = [];
                        }
                        //Page.CallBackFirstTime.push(Arguments.CallBackFirstTime)
                    }

                    if (Utilities.IsABootstrapModalOpen()) {

                        $.fn.modal.Constructor.prototype.enforceFocus = function () { };

                    }

                    //$(Arguments.SourceSelector).find('option[value=""], option:not([value])').text("Please Select " + Arguments.SourceText).attr('value', '');
                    var Select2SourceOptions = _.cloneDeep(Select2Options);

                    if (Utilities.Isfunction(window.Select2CodeSearchTemplate)) {
                        _.assign(Select2SourceOptions, Select2CodeSearchTemplate(Arguments.SourceSelector, Select2SourceOptions));
                    }


                    if ($(Arguments.SourceSelector).attr('AppliedAJAXSelectList') !== 'true') {
                        $(Arguments.SourceSelector).attr('AppliedAJAXSelectList', 'true');

                        //if ($(Arguments.TargetSelector).hasClass("select2-hidden-accessible")) {
                        //    // Select2 has been initialized
                        //    //$(Arguments.SourceSelector).select2('destroy');
                        //}
                    }
                    Select2SourceOptions.placeholder = "Please Select " + Arguments.SourceText;
                    $(Arguments.SourceSelector).select2(Select2SourceOptions);

                    if (Utilities.IsNotUndefinedOrNull(Arguments.SubArguments) || (!$(Arguments.TargetSelector).prop('multiple'))) {

                        if (Utilities.IsUndefinedOrNull(Arguments.OldMatcherSearching) || (Utilities.IsUndefinedOrNull(Arguments.OldMatcherSearching) && Arguments.OldMatcherSearching !== false)) {
                            //Utilities.Select2.OldMatcherSearching(Arguments.SourceSelector);
                        }
                    }
                    $(Arguments.SourceSelector).off("select2:select");
                    $(Arguments.SourceSelector).on("select2:select", async function () {
                        if (Utilities.IsUndefinedOrNull(Page.FirstSelectedAJAXSelectList)) {
                            Page.FirstSelectedAJAXSelectList = $(this);
                        }

                        if (Utilities.IsNotUndefinedOrNull(Arguments) && Utilities.Isfunction(Arguments.CallBackFirstTime)) {

                            Page.CallBackFirstTime.push(Arguments.CallBackFirstTime);
                        }
                        if ($(Arguments.SourceSelector).val() !== '' || (Arguments.AllowAJAXOnNull === true || Arguments.AllowAJAXOnNull === 'true')) {
                            var SendData = {
                                [Arguments.SourceName]: $(Arguments.SourceSelector).val()
                            };
                            if (Utilities.IsUndefinedOrNull(Arguments.ExtraParameters)) {
                                Arguments.ExtraParameters = null;
                            } else {
                                jQuery.each(Arguments.ExtraParameters, function (i, val) {
                                    if (((typeof (val) === 'object') && val instanceof jQuery)) {
                                        if (Utilities.IsNotUndefinedOrNull(val.val())) {
                                            //Arguments.ExtraParameters[i] = val.val();
                                            SendData[i] = val.val();
                                        }
                                    } else if (_.isFunction(val)) {
                                        if (Utilities.IsNotUndefinedOrNull(val())) {
                                            //Arguments.ExtraParameters[i] = val();
                                            SendData[i] = val();
                                        }
                                    }
                                    else {
                                        if (Utilities.IsNotUndefinedOrNull(val)) {
                                            //Arguments.ExtraParameters[i] = val;
                                            SendData[i] = val;
                                        }
                                    }
                                });
                            }


                            Page.AJAXSelectListLastArguments = Arguments;
                            if (Arguments.ShowLoader) {
                                Utilities.HTML.Loader.Show($(Page.AJAXSelectListArguments.TargetSelector).closest('.m-form__group-sub,.form-group'));
                            }

                            let Select2Ajax = $.ajax({
                                async: (Utilities.IsNotUndefinedOrNull(Arguments.async) ? Arguments.async : true),///Default Async True Else Take 'async' Value From 'Arguments'
                                url: Arguments.URL,
                                type: 'POST',
                                data: SendData // { [Arguments.SourceName]: $(Arguments.SourceSelector).val(), ExtraParameters: Arguments.ExtraParameters  /*Arguments.SourceData*/ },
                                //async: false,
                            }).done(function (Data) {
                                ///Resolving Focus Start
                                if (Utilities.IsUndefinedOrNull(Page.AJAXSelectListLastArguments.SubArguments)) {

                                    if (Utilities.IsUndefinedOrNull(Page.AJAXSelectListCounter)) {
                                        Page.AJAXSelectListCounter = 0;
                                    } else {
                                        Page.AJAXSelectListCounter++;
                                    }
                                    if (Utilities.IsNotUndefinedOrNull(Page.AJAXSelectListCounter) && Page.AJAXSelectListCounter > 0) {
                                        if (Utilities.IsNotUndefinedOrNull(Page.FirstSelectedAJAXSelectList)) {
                                            if (Utilities.IsNotUndefinedOrNull(Page.FirstSelectedAJAXSelectList)) {
                                                console.log('u focus 1');
                                                //Page.FirstSelectedAJAXSelectList.select2('focus')
                                                Page.FirstSelectedAJAXSelectList = undefined;
                                            }
                                        }
                                    }
                                }
                                ///Resolving Focus End

                                if (Utilities.IsNotUndefinedOrNull(Arguments.SubArguments) || (!$(Arguments.TargetSelector).prop('multiple'))) {
                                    $(Arguments.TargetSelector).select2(Select2Options);

                                    if (Utilities.IsUndefinedOrNull(Arguments.OldMatcherSearching) || (Utilities.IsUndefinedOrNull(Arguments.OldMatcherSearching) && Arguments.OldMatcherSearching !== false)) {
                                        //Utilities.Select2.OldMatcherSearching(Arguments.TargetSelector);
                                    }
                                    $(Arguments.TargetSelector).select2('destroy');

                                }
                                $(Arguments.TargetSelector).empty();
                                var NewJSONString = {};
                                let ListInstance = null;
                                if (Utilities.IsNotUndefinedOrNull(Arguments.ReturnObjectName)) {
                                    ListInstance = Data[Arguments.ReturnObjectName];
                                } else
                                    ListInstance = Data.List;
                                if (ListInstance === undefined) {
                                    ListInstance = Data;
                                }
                                if (_.isObject(ListInstance)) {
                                    ListInstance = JSON.stringify(ListInstance);
                                }
                                NewJSONString = ListInstance.replace(new RegExp(",\"" + Arguments.ReturnFieldNameText + "\":", "g"), ',"text":').replace(new RegExp("\"" + Arguments.ReturnFieldNameId + "\":", "g"), '"id":');

                                var NewJSON = JSON.parse(NewJSONString);
                                NewJSON.unshift({ id: '', text: "Please Select " + Arguments.TargetText });
                                let Select2TargetOptions = _.cloneDeep(Select2Options);
                                _.assign(Select2TargetOptions, {
                                    data: NewJSON
                                });

                                $(Arguments.TargetSelector).select2(Select2TargetOptions);
                                if (Utilities.Isfunction(Arguments.OnTargetChange)) {
                                    $(Arguments.TargetSelector).on('change', function (e) {
                                        Arguments.OnTargetChange();
                                    });

                                }
                                $(Arguments.TargetSelector).on('select2:select', function (e) {
                                    $(e.currentTarget).next('.select2-container').find('.select2-selection__rendered').attr('title', $(e.currentTarget).find('option:selected').text());
                                });
                                if (Utilities.IsNotUndefinedOrNull(Arguments.SubArguments) || (!$(Arguments.TargetSelector).prop('multiple'))) {

                                    if (Utilities.IsUndefinedOrNull(Arguments.OldMatcherSearching) || (Utilities.IsUndefinedOrNull(Arguments.OldMatcherSearching) && Arguments.OldMatcherSearching !== false)) {
                                        //Utilities.Select2.OldMatcherSearching(Arguments.TargetSelector);
                                    }
                                }
                                $(Arguments.TargetSelector + ' option').first().attr('value', '');
                                toastr.clear();
                                if ($(Arguments.TargetSelector + ' option:not([value=""])').first().length > 0) {

                                    if (Utilities.IsNotUndefinedOrNull(Arguments.PreSelectedData) && $(Arguments.TargetSelector + ' option[value="' + Arguments.PreSelectedData[Arguments.PreSelectedDataIdField] + '"]:not([value=""])').length > 0) {
                                        $(Arguments.TargetSelector + ' option[value="' + Arguments.PreSelectedData[Arguments.PreSelectedDataIdField] + '"]:not([value=""])').first().prop('selected', true);

                                    } else {

                                        Utilities.SelectDropDownTextOptionElement(Arguments.TargetSelector, Arguments.DefaultOption);

                                    }
                                }
                                else {
                                    let itemName = Utilities.IsNotUndefinedOrNull(Arguments.TargetText) ? Arguments.TargetText : "Items";
                                    itemName = itemName.toLowerCase();
                                    try {
                                        itemName = nlp(itemName).nouns().toPlural().all().out();
                                    } catch (e) {
                                        _.noop();
                                    }
                                    toastr.info('There are no ' + itemName + ' Available for this ' + Arguments.SourceText + '');
                                }

                            }).fail(async function (Data) {
                                $(Arguments.TargetSelector).empty();
                                $(Arguments.TargetSelector).append('<option value=' + '' + '>' + 'Select ' + Arguments.SourceText + ' First' + '</option>');

                            }).always(async function (Data) {



                                if (Utilities.IsNotUndefinedOrNull(Arguments.SubArguments) || (!$(Arguments.TargetSelector).prop('multiple'))) {
                                    Utilities.Select2.AJAXSelectList(Arguments.SubArguments);
                                }
                                else if ($(Arguments.TargetSelector).prop('multiple')) {

                                    $(Arguments.TargetSelector).select2('destroy');
                                }
                                else {
                                    if (Utilities.IsUndefinedOrNull(Page.AJAXSelectListCounter) || (Page.AJAXSelectListCounter !== 0 || Page.AJAXSelectListCounter !== '0')) {
                                        Page.AJAXSelectListCounter = 0;
                                    }
                                    else
                                        Page.AJAXSelectListCounter++;
                                    $(Arguments.TargetSelector).trigger("select2:select");
                                }
                                $(Arguments.TargetSelector).change();
                                if (Utilities.IsNotUndefinedOrNull(Page.CallBackFirstTime)) {
                                    if (Page.CallBackFirstTime.length > 0) {

                                        Page.PoppedFunction = Page.CallBackFirstTime.shift();
                                        //if (Utilities.IsUndefinedOrNull(Arguments.SubArguments)) {
                                        //    Page.CallBackFirstTime.push(PoppedFunction)
                                        //}
                                        Page.PoppedFunction();
                                    }
                                }
                                //$(Arguments.SourceSelector).select2('focus');
                                if (Utilities.IsNotUndefinedOrNull(Arguments) && Utilities.Isfunction(Arguments.OnInit)) {
                                    Arguments.OnInit();
                                }
                                if (Arguments.ShowLoader) {
                                    Utilities.HTML.Loader.Hide($(Page.AJAXSelectListArguments.TargetSelector).closest('.m-form__group-sub,.form-group'));
                                }
                                let itemsCount = $(Arguments.TargetSelector).find('option[value!=""]').length;
                                let itemName = Utilities.IsNotUndefinedOrNull(Arguments.TargetText) ? Arguments.TargetText : "Items";
                                itemName = itemName.toLowerCase();

                                if (itemsCount !== 1 || itemsCount !== -1) {
                                    try {
                                        itemName = nlp(itemName).nouns().toPlural().all().out();
                                    } catch (e) {
                                        _.noop();
                                    }
                                }
                                $("#BankLocation").parent('div').notify(

                                    itemsCount + " " + (itemName),
                                    { position: "top right", className: 'base' }
                                );
                            });
                            Page.AJAXList.push(Select2Ajax);
                        }
                        else {

                            $(Arguments.TargetSelector).empty();
                            $(Arguments.TargetSelector).append('<option value=' + '' + '>' + 'Select ' + Arguments.SourceText + ' First' + '</option>');
                            $(Arguments.TargetSelector).change();
                            $(Arguments.TargetSelector).trigger("select2:select");

                            ///Resolving Focus Start
                            if (Utilities.IsNotUndefinedOrNull(Page.AJAXSelectListLastArguments) && Utilities.IsUndefinedOrNull(Page.AJAXSelectListLastArguments.SubArguments)) {

                                if (Utilities.IsUndefinedOrNull(Page.AJAXSelectListCounter)) {
                                    Page.AJAXSelectListCounter = 0;
                                } else {
                                    Page.AJAXSelectListCounter++;
                                }
                                if (Utilities.IsNotUndefinedOrNull(Page.AJAXSelectListCounter) && Page.AJAXSelectListCounter > 0) {
                                    if (Utilities.IsNotUndefinedOrNull(Page.FirstSelectedAJAXSelectList)) {
                                        setTimeout(function () {
                                            if (Utilities.IsNotUndefinedOrNull(Page.FirstSelectedAJAXSelectList)) {
                                                Page.FirstSelectedAJAXSelectList.select2('focus');
                                                Page.FirstSelectedAJAXSelectList = undefined;
                                            }
                                        }, 70);
                                    }
                                }
                            }
                            ///Resolving Focus End
                        }
                    });
                    $(Arguments.SourceSelector).trigger("select2:select");
                    if (Utilities.Isfunction(window.Select2CodeSearchTitleTemplate)) {
                        window.Select2CodeSearchTitleTemplate(Arguments.SourceSelector);
                    }
                    $(document).ready(function () {
                        if (Utilities.IsNotUndefinedOrNull(Arguments) && Utilities.Isfunction(Arguments.CallBackFirstTime)) {
                            Page.AJAXSelectListCallBackFirstTime = false;
                            Arguments.CallBackFirstTime();
                            Arguments.CallBackFirstTime = undefined;
                        }
                    });
                    ///Resolving Focus Start
                    if (Utilities.IsUndefinedOrNull(Arguments.SubArguments)) {
                        $(Arguments.TargetSelector).off("select2:close");
                        $(Arguments.TargetSelector).on("select2:close", function () {

                            if (Utilities.IsNotUndefinedOrNull(Page.AJAXSelectListCounter) && ++Page.AJAXSelectListCounter > 0) {
                                if (Utilities.IsNotUndefinedOrNull($(this))) {

                                    Page.LastLeafAJAXSelectList = $(this);

                                    setTimeout(function () {
                                        console.log('u focus 3');
                                        Page.LastLeafAJAXSelectList.select2('focus');
                                    }, 70);
                                }
                            }
                        });
                        $(Arguments.SourceSelector).off("select2:close");
                        $(Arguments.SourceSelector).on("select2:close", function () {
                            if (Utilities.IsNotUndefinedOrNull(Page.AJAXSelectListCounter) && ++Page.AJAXSelectListCounter > 0) {
                                if (Utilities.IsNotUndefinedOrNull($(this))) {
                                    Page.LastLeafAJAXSelectList = $(this);
                                    setTimeout(function () {
                                        Page.LastLeafAJAXSelectList.select2('focus');
                                    }, 70);
                                }
                            }
                        });
                    }
                    ///Resolving Focus End
                }
            },
        PopulateDropDown: function (selector, data, isAnother) {
                debugger
                $(selector).empty().trigger("change");
                $(selector).append('<option><option>');
                $.each(data, function (Index, Element) {
                    $(selector).append("<option  " + (Element.selected ? "selected" : "") + " code='" + Element.code + "' value='" + Element.id + "'>" + Element.text + "</option>");
                });
                if (isAnother === true) { /*Utilities.IsNotUndefinedOrNull(data) &&*/
                    $(selector).append("<option code='Other'>Other<option>");
                }
                $(selector).select2({
                    placeholder: 'Select an Option'
                });
            }
        },
        this.DataTable = {
            GetColumnValues: function (DataTable, ColumnName) {
                return _.reduce(DataTable.data(), function (result, value, key) {
                    (result || (result = [])).push(value[ColumnName]);
                    return result;
                }, []);
            },
            else:
            {
                return: true
            }
        },
        this.IsUndefinedOrNull = function (Value) {
            if (Value !== undefined && Value !== null && Value.length !== 0 && Value !== '') {
                return false;
            }
            else {
                return true;
            }
        },
        this.IsNotUndefinedOrNull = function (Value) {
            return !this.IsUndefinedOrNull(Value);
        },
        this.DataTable = {
            GetColumnValues: function (DataTable, ColumnName) {
                return _.reduce(DataTable.data(), function (result, value, key) {
                    (result || (result = [])).push(value[ColumnName]);
                    return result;
                }, []);
            }
        },
        this.Bootstrap = {
            DropDownEnableFunctionality: function () {
                $('a.dropdown-item').off().on('click', function () {
                    var currentElement = $(this)[0];
                    document.getElementById(currentElement.getAttribute('ToogleButtonId')).innerHTML = currentElement.innerHTML;
                    currentElement.classList.add('true');
                    $(currentElement).parent().children().removeClass('active');
                    $(currentElement).addClass('active');
                });
            }
        },
        this.cookies = {
            setCookie: function (cname, cvalue, exdays) {
                var d = new Date();
                var calc = exdays * 24 * 60 * 60 * 1000;
                d.setTime(d.getTime() + calc);
                var expires = "expires=" + d.toUTCString();
                document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
            },
            getCookie: function (cname, cookie = document.cookie) {
                var name = cname + "=";
                var ca = cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) === ' ') {
                        c = c.substring(1);
                    }
                    if (c.indexOf(name) === 0) {
                        return c.substring(name.length, c.length);
                    }
                }
                return "";
            }
        },
        this.Isfunction = function (ObjectReceived) {
            return typeof ObjectReceived === "function";
        },
        this.IsEven = function (n) {
            return n % 2 === 0;
        },
        this.timeSince = function (date) {
            let seconds = Math.floor((moment($('#LayoutClock').attr('lastplayedtime')) - date) / 1000);
            let months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
            if (seconds < 60) {
                return "just now";
            }
            else if (seconds < 3600) {
                let minutes = Math.floor(seconds / 60);
                if (minutes > 1)
                    return minutes + " minutes ago";
                else
                    return "1 minute ago";
            }
            else if (seconds < 86400) {
                let hours = Math.floor(seconds / 3600);
                if (hours > 1)
                    return hours + " hours ago";
                else
                    return "1 hour ago";
            }
            //2 days and no more
            else if (seconds < 604800) {
                let days = Math.floor(seconds / 86400);
                if (days > 1)
                    return days + " days ago";
                else
                    return "1 day ago";
            }
            else if (seconds < 2.592e+6) {
                let weeks = Math.floor(seconds / 604800);
                if (weeks > 1)
                    return weeks + " weeks ago";
                else
                    return "1 week ago";
            }
            else if (seconds < 3.154e+7) {
                let months = Math.floor(seconds / 2.592e+6);
                if (months > 1)
                    return months + " months ago";
                else
                    return "1 month ago";
            }
            else if (seconds > 3.154e+7) {
                let years = Math.floor(seconds / 3.154e+7);
                if (years > 1)
                    return years + " years ago";
                else
                    return "1 year ago";
            }
            else {

                //return new Date(time).toLocaleDateString();
                // return moment($('#LayoutClock').attr('lastplayedtime'))
                return "";
            }
        },
        this.PlayTime = async function (selector, attr = 'LastPlayedTime') {
            var $startMoment = moment($(selector).attr(attr));
            var ToFormat = "dddd D MMMM Y h:mm:ss A";
            if ($(selector).length > 0) {
                $(selector).text($startMoment.format(ToFormat));
                setInterval(async function () {
                    $startMoment = $startMoment.add(1, 'seconds');
                    $(selector).text($startMoment.format(ToFormat));
                    let ISOString = $startMoment.toISOString();
                    $(selector).attr(attr, ISOString);
                }, 1000);
            }
        },
        this.PlayTimeAttr = async function (selector, attr = 'LastPlayedTime') {
            var $startMoment = moment($(selector).attr(attr));
            if ($(selector).length > 0) {
                setInterval(async function () {
                    $startMoment = $startMoment.add(1, 'seconds');
                    let ISOString = $startMoment.toISOString();
                    $(selector).attr(attr, ISOString);
                }, 1000);
            }
        },
        //this.PlayFromNow = function (selector, OrginalDateTime ='OrginalDateTime') {
        //    $moment = moment($(selector));
        //    let DateTime = moment($moment).format(APP.Default.Date.Format + " hh:mm a");
        //    let FromNow = moment(moment($moment)).from($('#LayoutClock').attr('lastplayedtime'));

        //    return "<span realDateTime=" + $moment + " title='" + DateTime + "'><small style='color:#bebfc1' class='FromNow'>" + FromNow + "</small></span>";
        //},
        this.IsABootstrapModalOpen = function () {
            return document.querySelectorAll('.modal.in').length > 0;
        },
        this.SelectDropDownTextOptionElement = function (Selector, StringToSelectDropDownTextOptionElement) {
            if (Utilities.IsNotUndefinedOrNull(Selector) && Utilities.IsNotUndefinedOrNull(StringToSelectDropDownTextOptionElement)) {
                $(Selector).find('option').each(function (Index, Element) {
                    if ($(Element).text().trim() === StringToSelectDropDownTextOptionElement.trim()) {
                        //console.log($(Element).text().trim() + " " + $(Element).val().trim())
                        $(Selector).val($(Element).val());
                        //$(Selector).trigger('change').trigger('select2:select')
                        return false;
                    }
                });
            }
            return $(Selector);
        },
        this.DateTime = {
            CompareTwoDates: function (date1, date2) {
                if (date1.getDate() === date2.getDate() && date1.getMonth() === date1.getMonth() && date1.getYear() === date1.getYear()) {
                    return true;
                }
                return false;
            }
            //ToASPJSONDate: function ($moment) {
            //    return "/Date(" + moment($moment).valueOf() + moment($moment).format("ZZ") + ")/";
            //}
        },
        this.Json = {
            SwapJsonKeyValues: function (input) {
                var one, output = {};
                for (one in input) {
                    if (input.hasOwnProperty(one)) {
                        output[input[one]] = one;
                    }
                }
                return output;
            },
            IsJsonString: function (str) {
                try {
                    JSON.parse(str);
                } catch (e) {
                    return false;
                }
                return true;
            },
            GetFormData: function ($form, ConvertDateToISO = false) {
                var unindexed_array = $($form).serializeArray();
                var indexed_array = {};
                $.map(unindexed_array, function (n, i) {
                    indexed_array[n['name']] = n['value'];
                    let $selected = $('[name=' + n.name + ']');
                    if (ConvertDateToISO && $selected.hasClass('bootstrap-datepicker')) {
                        let value = $selected.datepicker('getDate');
                        if (Utilities.IsNotUndefinedOrNull(value)) {
                            indexed_array[n['name']] = value.toISOString();
                        }
                    }
                });
                return indexed_array;
            },
            GetDivData: function (divSelector) {
                var _divSelectorId = $(divSelector).attr('id');
                var _formSelectorId = 'fm_' + _divSelectorId;
                $(divSelector).children().unwrap().wrapAll("<form id='" + _formSelectorId + "' method='post'></form>");
                var _divData = Utilities.Json.GetFormData($('#' + _formSelectorId));
                $('#' + _formSelectorId).children().unwrap().wrapAll("<div id='" + _divSelectorId + "'></div>");
                return _divData;
            }
        },
        this.JavaScript = {
            Load: function ({ URL }) {
                if (URL) {
                    return $.ajax({ type: "GET", url: URL, async: false }).responseText; //No need to append  
                }
            },
            FormData: {
                ToJSON: function (FormData) {
                    return _.fromPairs(Array.from(FormData.entries()));
                }
            },
            uuidv4: function () {
                return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
                    (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
                );
            },
            guid: function () {
                return Utilities.JavaScript.uuidv4();
            }
        };
};
var Utilities = new Utils();
var U_ = new Utils();
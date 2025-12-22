/* eslint-disable no-extra-parens */
$(document).ready(function () {

    if (window.mUtil === undefined) {
        try {
        window.mUtil = KTUtil;
        } catch (e) {

        }
    }
    if (typeof GlobalSettings !== 'undefined') {
        window.BillGlobalSettings = new GlobalSettings().get();
        Object.freeze(window.BillGlobalSettings);
    }
    window.AppCodes =
        {
            ProscribedPersons: {
                SearchByIdentity: function (selector, url, blocked_AppCode, submit_selector) {
                    $(submit_selector).prop('disabled', true);
                    Utilities.HTML.Loader.Show();

                    GetRequest(url).done(function (responseResult) {
                        if (ComplexObjectNullCheck(responseResult)) {
                            var response = responseResult.response;
                            if (response.Code.equals(102)) {
                                if (Utilities.IsNotUndefinedOrNull(response.Data) && response.Data.length > 0) {
                                    //var BlockedPeopleData = response.Data;



                                    var errorMessage = '<div class="row pl-2"><h6 class="font-weight-bold mx-auto">Found In Proscribed Persons!</h6></div>' + '<div class="row pl-2"><p class="font-weight-bold mx-auto">Identity No: "' + selector.val() + '" is ' + ' blocked!</p>' + '<p class=" mx-auto">Transaction can not be performed!</p></div>';
                                    swal.fire({
                                        title: AppCodes[blocked_AppCode].Message,
                                        text: errorMessage,
                                        html: errorMessage,
                                        icon: 'error',
                                        confirmButtonText: 'Ok'
                                    }).then(function (result) {

                                    });
                                    toastr.remove();
                                    toastr.error(errorMessage, '', {
                                        onclick: function () {

                                        }
                                    });

                                    $(selector).val('').trigger('change');


                                } else {

                                    var successMessage = '<div class="row"><h6 class="font-weight-bold">Not Found In Proscribed Persons!</h6></div>' + '<div class="row"><p class="font-weight-bold">Identity No: "' + selector.val() + '"' + '</p></div>';
                                    toastr.remove();
                                    toastr.success(successMessage, '', {
                                        onclick: function () {

                                        }
                                    });

                                }

                            }
                            else {

                                Utilities.HTML.Toastr.Show(response.Code);
                                swal.fire({
                                    title: 'Proscribed Persons Search',
                                    text: AppCodes[response.Code].Message,
                                    icon: 'error',
                                    confirmButtonText: 'Ok'
                                }).then(function (result) {

                                });

                                $(selector).val('').trigger('change');
                            }
                        }
                    }).fail(function (response) {

                        Utilities.HTML.Toastr.Show(108);
                        swal.fire({
                            title: 'Proscribed Persons Search',
                            text: AppCodes[108].Message,
                            icon: 'error',
                            confirmButtonText: 'Ok'
                        }).then(function (result) {

                        });

                        $(selector).val('').trigger('change');
                    }).always(function (response) {
                        Utilities.HTML.Loader.Hide();
                        $(submit_selector).prop('disabled', false);

                    });

                }

        },
        Identity: {
            IsValidIdentityNo: function (IdentityNo) {
                return IdentityNo.toString().match(/^[a-zA-Z0-9-]{6,15}$/);
            },
            RemoveSpecialCharacters: function (IdentityNo) {
                return IdentityNo.toString().replace(/[^0-9A-Z]+/gi, "");

            }
        },

            Calculate: {
                FC: function (CurrencyId, FC, LC, Rate) {
                    FC = FC.toRounderFC();
                    LC = LC.toRounderLC();
                    Rate = Rate.toRounderFC();

                    if (CurrencyId === 1 || CurrencyId === '1') {
                        return FC;
                    } else {
                        let result = 0;
                        try {
                            result = LC / Rate;
                        } catch (e) {
                            result = 0;
                        }
                        return result.toRounderFC().toFC();
                    }
                }
            },
            AuthorizationStatus: {
                0: {
                    Message: 'Deactivated',
                    Type: 'secondary'
                },
                1: {
                    Message: 'Authorized',
                    Alternative: 'Activate',
                    Type: 'success'
                },
                2: {
                    Message: 'Unauthorized',
                    Type: 'danger'
                },
                3: {
                    Message: 'Unapproved',
                    Type: 'danger'
                },
                4: {
                    Message: 'Reversed',
                    Type: 'danger'
                },
                5: {
                    Message: 'New',
                    Type: 'danger'
                }

            },
            ServerCheck: {
                CheckLoggedInStatus: function () {
                    $.ajax({
                        url: '/Account/IsLoggedIn',
                        type: 'GET',
                        dataType: 'json',
                        success: function (response) {
                            if (!response) {
                                $('#logoutForm').submit();
                            }
                        },
                        error: function (request, error) {
                            $('#logoutForm').submit();
                            toastr.error('Logged Out');
                        }
                    });
                }
            },
            Layout: {
                CommonInitializations: async function () {
                    try {
                        window.CommonInitializations();
                    } catch (e) {
                        console.log('Delayed Layout Initization');
                    }
                }
            },
            Time: {
                CurrentLocationMoment: function () {
                    return moment($('#LayoutClock').attr('lastplayedtime'));
                },
                ServerMoment: function () {
                    return moment($('#LayoutClock').attr('orginaltime'));
                }
            },
            Dynamic: {
                //HTML: {
                //    _RawInput: function (Options) {
                //        let maxlength = (Options.maxlength !== undefined ? "maxlength='" + Options.maxlength +"'": "");
                //        let required = (Options.required !== undefined ? "required" : "");
                //        let type = (Options.type !== undefined ? "type"= : "");
                //        '<input ' + maxlength + ' ' + required ' type="text" id="rem_refno" name="rem_refno" value="" placeholder="Please Type Here! Ref #" autocomplete="off" class="forkt-control forkt-control-sm kt-input kt-input--air IsInt input-number pk-refer-no-mask">'
                //    },
                //    Input: function (Options) {
                //        if (Option.Required !== undefined) {
                //            return
                //            '<div class="input-group input-group-required input-group-sm kt-input-icon kt-input-icon--right">'
                //                + '<div class="input-group-prepend required" > <span class="input-group-text "></span></div>'
                //                + AppCodes.HTML._RawInput(Options)
                //                + '</div>';
                //        }
                //    }
                //},
                Append: {
                    Badge: {
                        Create: function (selector, value) {
                            $(selector).append('<span style="padding: 4px;" class="badge badge-warning">' + value + '</span>');
                        }, Update: function (selector, value) {
                            $(selector).find('.badge-warning').html(value);
                        }, Remove: function (selector) {
                            $(selector).find('.badge-warning').remove();

                        }
                    }
                },
                SmallButtons: function (Class = "", Id = "", Icon = "", Title = "Approve", Type = ' ') {
                    return '<button type="button" Id="' + Id + '" title="' + Title + '" class="btn btn-sm btn-' + Type + ' btn-elevate-hover btn-circle btn-icon ' + Class + '"><i class="' + Icon + '" ></i ></button>';
                },
                StandardButtons: {
                    Approve: function (Class = "", Id = "", Icon = "la la-check", Title = Humanize.capitalizeAll(Utilities.NLP.Verbs.GetInfinitive(AppCodes.AuthorizationStatus[1].Message)), Type = 'success') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Deactivate: function (Class = "", Id = "", Icon = "la la-power-off", Title = Humanize.capitalizeAll(Utilities.NLP.Verbs.GetInfinitive(AppCodes.AuthorizationStatus[0].Message)), Type = 'danger') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Remove: function (Class = "", Id = "", Icon = "fa fa-times", Title = 'Remove', Type = 'danger') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    }
                    ,
                    Print: function (Class = "", Id = "", Icon = "la la-print", Title = "Print", Type = 'success') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Edit: function (Class = "", Id = "", Icon = "la la-edit", Title = "Edit", Type = 'primary') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Reverse: function (Class = "", Id = "", Icon = "la la-reply-all", Title = "Reverse", Type = 'primary') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Insert: function (Class = "", Id = "", Icon = "la la-angle-double-right", Title = "Insert", Type = 'primary') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Reffer: function (Class = "", Id = "", Icon = "la la-forward", Title = "Reffer", Type = 'danger') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    },
                    Info: function (Class = "", Id = "", Icon = "fa fa-info-circle", Title = "Info", Type = 'primary') {
                        return AppCodes.Dynamic.SmallButtons(Class, Id, Icon, Title, Type);
                    }
                },
                Span: {
                    Ellipses: function (text, width, ExtraClasses = "") {
                        return "<div style='max-width:" + width + "' data-toggle='tooltip' data-placement='bottom' title='" + text + "' class='ellipses " + ExtraClasses + "'>" + text + "</div>";
                    },
                    TextWrap: {
                        BreakWords: function (text, width, ExtraClasses = "") {
                            return "<span style='max-width:" + width + "' data-toggle='tooltip' data-placement='bottom'  class='break-word " + ExtraClasses + "'>" + text + "</div>";
                        }
                    },
                    Accounts: function (Code, Description, CurrencyCode) {
                        return '<span style="color:green">' + Code + '</span> - ' + '<span>' + Description + '</span>' + (Utilities.IsNotUndefinedOrNull(CurrencyCode) ? ' - <span style="color:#246db7">' + CurrencyCode + '</span>' : '');
                    }
                },
                Icon: function ({ className, IconText }) {
                    if (Utilities.IsUndefinedOrNull(IconText)) {
                        IconText = '';
                    }
                    if (Utilities.IsUndefinedOrNull(className)) {
                        className = '';
                    }
                    return '<i class="' + className + '">' + IconText + '</i>';
                },
                Badge: function (status, Message) {
                    return '<span class="kt-badge kt-badge--' + status + '  kt-badge--inline kt-badge--pill">' + Message + '</span>';
                },
                Status: function (data) {
                    if (Utilities.IsUndefinedOrNull(data)) {
                        return '';
                    } else
                        return '<span class="kt-badge kt-badge--' + AppCodes.AuthorizationStatus[data].Type + '  kt-badge--inline kt-badge--pill">' + AppCodes.AuthorizationStatus[data].Message + '</span>';
                },
                Title: {
                    Accounts: function (Code, Description, CurrencyCode) {
                        return Code + ' - ' + Description + (Utilities.IsNotUndefinedOrNull(CurrencyCode) ? ' - ' + CurrencyCode : '');
                    }
                },
                Simplify: {
                    Brackets: function (Description) {
                        let NewDescription = Utilities.String.GetStringFromSquareBracets(Description).map((currentValue, index) => currentValue);
                        if (NewDescription.length === 0) {
                            return Description;
                        } else
                            return NewDescription.reduce((accumulator, currentValue) => accumulator + ' - ' + currentValue);
                    }
                },
                Colorify: {
                    DefaultColor: ['green', 'black', 'blue'],
                    Brackets: function (Description) {
                        let NewDescription = Utilities.String.GetStringFromSquareBracets(Description).map((currentValue, index) => '<span style="color:' + AppCodes.Dynamic.Colorify.DefaultColor[index] + '">' + currentValue + '<span>');
                        if (NewDescription.length === 0) {
                            return Description;
                        } else
                            return NewDescription.reduce((accumulator, currentValue) => accumulator + ' - ' + currentValue);
                    },
                    Array: function (Array) {
                        let NewDescription = Array.map((currentValue, index) => '<span style="color:' + AppCodes.Dynamic.Colorify.DefaultColor[index] + '">' + currentValue + '<span>');
                        if (NewDescription.length === 0) {
                            return Array;
                        } else
                            return NewDescription.reduce((accumulator, currentValue) => accumulator + ' - ' + currentValue);
                    }
                },
                Switch: function (Arguments) {
                    let Checked = Arguments.Checked, Type = Arguments.Type, Name = Arguments.Name,
                        Class = Arguments.Class, WithIcon = Arguments.WithIcon, Disabled = Arguments.Disabled;
                    if (Utilities.IsNotUndefinedOrNull(Type))
                        Type = 'kt-switch--' + Type;
                    var HTML = '<span class="kt-switch kt-switch--sm ' + Type + ' ' + (WithIcon ? "kt-switch--icon" : "") + ' "><label><input Class="' + Class + '" type="checkbox" ' + (Checked ? "checked" : "") + ' ' + (Disabled ? "disabled" : "") + ' name="' + Name + '"><span></span></label></span >';

                    return HTML;
                },
                DateTime: {
                    toDate: function ($moment) {
                        return moment($moment).format('DD/MM/YYYY');
                    },
                    toDateTime: function ($moment) {
                        return moment($moment).format('DD/MM/YYYY' + " hh:mm a");
                    },
                    FromNow: function ($moment) {
                        return moment(moment($moment)).from($('#LayoutClock').attr('lastplayedtime'));
                    },
                    toFromNowHTML: function ($moment) {
                        var DateTime = AppCodes.Dynamic.DateTime.toDateTime($moment);
                        var FromNow = AppCodes.Dynamic.DateTime.FromNow($moment);
                        return "<span realDateTime=" + $moment + " title='" + DateTime + "'><small style='color:rgb(161, 161, 161)' class='FromNow'>" + FromNow + "</small></span>";
                    },
                    toFromNowConditionalHTML: function ($moment, tillDays = 2) {
                        var DateTime = AppCodes.Dynamic.DateTime.toDateTime($moment);
                        var FromNow = AppCodes.Dynamic.DateTime.FromNow($moment);
                        if (moment($moment).isAfter(moment($('#LayoutClock').attr('lastplayedtime')).subtract(tillDays, 'days'))) {
                            return "<span realDateTime=" + $moment + " title='" + DateTime + "'><small style='color:rgb(161, 161, 161)' class='FromNow'>" + (FromNow === "Invalid date" ? "" : FromNow) + "</small></span>";

                        } else
                            return AppCodes.Dynamic.DateTime.toDateTimeHTML($moment);
                    },
                    toDateTimeHTML: function ($moment) {
                        var Date = AppCodes.Dynamic.DateTime.toDate($moment);
                        var DateTime = AppCodes.Dynamic.DateTime.toDateTime($moment);
                        var FromNow = AppCodes.Dynamic.DateTime.FromNow($moment);

                        return "<span realDateTime=" + $moment + " title='" + DateTime + "'>" + (Date === "Invalid date" ? "" : Date) + "</span>";
                    },
                    toDateTimeWithFromNowHTML: function ($moment) {
                        var Date = AppCodes.Dynamic.DateTime.toDate($moment);
                        var DateTime = AppCodes.Dynamic.DateTime.toDateTime($moment);
                        var FromNow = AppCodes.Dynamic.DateTime.FromNow($moment);

                        return "<span realDateTime=" + $moment + " title='" + DateTime + "'>" + Date + " <small style='color:rgb(161, 161, 161)' class='FromNow'>" + FromNow + "</small></span>";
                    }
                }
            },
            DateRangPickerDefaultOptions: {
                "showDropdowns": true,
                "locale": {
                    "format": 'DD/MM/YYYY'
                },
                direction: false,
                startDate: moment($('#LayoutClock').attr('lastplayedtime')).startOf('day'),
                endDate: moment($('#LayoutClock').attr('lastplayedtime')).endOf('day').milliseconds(0),
                minDate: new Date('07/01/2019'),
                maxDate: moment($('#LayoutClock').attr('lastplayedtime')).endOf('day').milliseconds(0),
                opens: 'right',
                alwaysShowCalendars: true,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            },
            CurrencyFormat: {
                Default: wNumb({
                    mark: '.',
                    thousand: ','
                }),
                to: function (number, wNumbInstance = AppCodes.CurrencyFormat.Default) {
                    return wNumbInstance.to(number);
                },
                from: function (number, wNumbInstance = AppCodes.CurrencyFormat.Default) {
                    return wNumbInstance.from(number);
                }
            },
            AlertArray: ['Error', 'Blocked'],
            ToastrArray: ['Success', 'Info'],
           
            Dialogue: function (AppCode, MesageMapJSON = {}) {
                let appCode = AppCodes[AppCode];
                if (!appCode) {
                    AppCode = 172;//Unknown Error
                    appCode = AppCodes[AppCode];
                }
                if (AppCodes.AlertArray.includes(appCode.Type)) {
                    let options = null;
                    if (appCode.Type === 'Blocked') {
                        options = {
                            showConfirmButton: false,
                            allowOutsideClick: false
                        };
                    }
                    Utilities.HTML.Alert.Show(AppCode, null, options, MesageMapJSON);
                } else {
                    Utilities.HTML.Toastr.Show(AppCode);
                }
                return appCode;
            },
            ///Standart Http StatusCodes do not use these
            200: {
                Message: 'Ok.',
                Description: 'Every thing is right',
                Type: 'Success'
            },
            201: {},
            202: {},
            203: {},
            204: {},
            205: {},
            206: {},
            207: {},
            208: {},
            226: {},
            300: {},
            301: {},
            302: {},
            303: {},
            304: {},
            305: {},
            306: {},
            307: {},
            308: {},
            400: {},
            401: {},
            402: {},
            403: {},
            404: {},
            405: {},
            406: {},
            407: {},
            408: {},
            409: {},
            410: {},
            411: {},
            412: {},
            413: {},
            414: {},
            415: {},
            416: {},
            417: {},
            426: {},
            500: {},
            501: {},
            502: {},
            503: {},
            504: {},
            505: {},
            ////////// Custom
            100: {
                Code: 100,
                Message: 'Blocked people server is down.',
                Description: 'Blocked people server is down, contact to administrator.',
                Type: 'Info'
            },
            101: {
                Code: 101,
                Message: 'No record found',
                Description: 'No record found for this entity',
                Type: 'Info'
            },
            102: {
                Code: 102,
                Message: 'Ok.',
                Description: 'Every thing is right',
                Type: 'Success'
            },
            103: {
                Code: 103,
                Message: 'Intenal server error.',
                Description: 'Intenal server error, contact to administrator.',
                Type: 'Error'
            },
            104: {
                Code: 104,
                Message: 'This customer Identity No. is blocked.',
                Description: 'This customer Identity No. is blocked, contact to administrator.',
                Type: 'Error'
            },
            105: {
                Code: 105,
                Message: 'Provide Transaction information.',
                Description: 'Provide Transaction information, to proceed.',
                Type: 'Info'
            },
            106: {
                Code: 106,
                Message: 'Record inserted successfully.',
                Description: 'Record inserted successfully.',
                Type: 'Success'
            },
            107: {
                Code: 107,
                Message: 'Some Thing Error',
                Description: 'Some Thing Error.',
                Type: 'Error'
            },
            108: {
                Code: 108,
                Message: 'Record Updated successfully.',
                Description: 'Record Updated successfully.',
                Type: 'Success'

            },
            109: {
                Code: 109,
                Message: 'Record Deteled successfully.',
                Description: '',
                Type: 'Success'
            },
            110: {
                Code: 110,
                Message: 'greater than two currncies transaction are not allowed.',
                Description: 'greater than two currncies transaction are not allowed, change currency.',
                Type: 'Info'
            },
            111: {
                Code: 111,
                Message: 'Same currency with the same Rate & FC already exists . . .!',
                Description: 'Same currency with the same Rate & FC already exists . . .!, change rates',
                Type: 'Info'
            },
            112: {
                Code: 112,
                Message: 'This person is parliamentarian, provide CDD info.',
                Description: 'Person with this CNIC# has found in parliamentarians list',
                Type: 'Error'
            },
            113: {
                Code: 113,
                Message: 'Aleady exists',
                Description: 'Aleady exists',
                Type: 'Info'
            },
            114: {
                Code: 114,
                Message: 'Saved',
                Description: 'Saved',
                Type: 'Success'
            },
            115: {
                Code: 115,
                Message: 'Person with this identity # already have performed sales transaction.',//, provide CDD information.',
                Description: 'Person with this identity # already have transctions, provide CDD information, contact to administrator',
                Type: 'Info'
            },
            116: {
                Code: 116,
                Message: window.BillGlobalSettings ? ('For more than ($' + window.BillGlobalSettings.USDForBill + '/PKR ' + window.BillGlobalSettings.PKRForBill + ') transction; CTR info is required. create CTR bill.') : '',
                //Description: 'For more than ($' + window.BillGlobalSettings.USDForBill + '/PKR ' + window.BillGlobalSettings.PKRForBill + ') transction; CTR info is required. create CTR bill, contact to administrator',
                Type: 'Warning'
            },
            117: {
                Code: 117,
                Message: 'Fill all fields.',
                Description: 'Fill all fields, organization/institution/name & address',
                Type: 'Warning'
            },
            118: {
                Code: 118,
                Message: 'Fill all fields.',
                Description: 'Fill all fields, identity information',
                Type: 'Warning'
            },
            119: {
                Code: 119,
                Message: 'Please Upload Outward Remittance File',
                Description: 'Please Upload Outward Remittance File.',
                Type: 'Error'
            },
            120: {
                Code: 120,
                Message: 'Please Upload Inward Remittance File.',
                Description: 'Please Upload Inward Remittance File.',
                Type: 'Error'
            },
            121: {
                Code: 121,
                Message: 'Please Upload Valid Inward Or Outward Remittance File.',
                Description: 'Please Upload Valid Inward Or Outward Remittance File.',
                Type: 'Error'
            },
            122: {
                Code: 122,
                Message: 'For resident/non-resident customer, max 3 transactions are allowed.',
                Description: 'For resident/non-resident customer, max 3 transactions are allowed, contact to administrator.',
                Type: 'Info'
            },
            123: {
                Code: 123,
                Message: 'You Have Uploaded Old File Press OK to Continue',
                Description: 'You Have Upload Old File Press OK to Continue',
                Type: 'Info'
            },
            124: {
                Code: 124,
                Message: 'Please Upload WUPUS File',
                Description: 'Please Upload WUPUS File',
                Type: 'Info'
            },
            125: {
                Code: 125,
                Message: 'Overbuying Not Allowed',
                Description: 'Overbuying Not Allowed',
                Type: 'Error'
            },
            126: {
                Code: 126,
                Message: 'Overselling Not Allowed',
                Description: 'Overselling Not Allowed',
                Type: 'Error'
            },
            127: {
                Code: 127,
                Message: 'Please Enter A Transaction',
                Description: 'Please Enter A Transaction',
                Type: 'Error'
            },
            128: {
                Code: 128,
                Message: 'Debit And Credit Difference Should Be Zero `0`',
                Description: 'Debit And Credit Difference Should Be Zero `0`',
                Type: 'Error'
            },
            129: {
                Code: 129,
                Message: 'Some Bills are required to authorized',
                Description: 'Some Bills are required to authorized, Authorized all unauthorized bills.',
                Type: 'Error'
            },
            130: {
                Code: 130,
                Message: 'Currency Rate does not exists.',
                Description: 'Currency Rate does not exists.',
                Type: 'Error'
            },
            131: {
                Code: 131,
                Message: window.BillGlobalSettings ? ('Sales Over ' + window.BillGlobalSettings.USDForTourist + ' equvalent USD for 4-Holiday tourist-3250/3350 is Not Allowed.') : '',
                //Description: 'Sales Over ' + window.BillGlobalSettings.USDForTourist + ' equvalent USD for 4-Holiday tourist-3250/3350 is Not Allowed, contact to administrator.',
                Type: 'Error'
            },
            132: {
                Code: 132,
                Message: 'Total Debit or Credit must be equal to sum of LC and Branch Commission.',
                Description: 'Total Debit or Credit must be equal to sum of LC and Branch Commission.',
                Type: 'Error'
            },
            133: {
                Code: 133,
                Message: 'No Row Selected.',
                Description: 'No Row Selected.',
                Type: 'Info'
            },
            134: {
                Code: 134,
                Message: 'LC can not be zero or null.',
                Description: 'LC can not be zero or null.',
                Type: 'Error'
            },
            135: {
                Code: 135,
                Message: window.BillGlobalSettings ? ('More than ($' + window.BillGlobalSettings.USDForTV + '/PKR ' + window.BillGlobalSettings.PKRForTV + ') amount transaction is not allowed.') : '',
                //Description: 'More (than $' + window.BillGlobalSettings.USDForBill + '/PKR ' + window.BillGlobalSettings.PKRForBill + ') amount transaction is not allowed, contact to administrator.',
                Type: 'Error'
            },
            136: {
                Code: 136,
                Message: window.BillGlobalSettings ? ('For more than ($' + window.BillGlobalSettings.USDForCTR + '/PKR ' + window.BillGlobalSettings.PKRForCTR + ') transction; TV info is required. create TV bill.') : '',
                //Description: 'For more than ($' + window.BillGlobalSettings.USDForCTR + '/PKR ' + window.BillGlobalSettings.PKRForCTR + ') transction; TV info is required. create TV bill, contact to administrator',
                Type: 'Warning'
            },
            137: {
                Code: 137,
                Message: 'Day Closing has already been Performed.',
                //Description: 'Day Already Closed',
                Type: 'Error'
            },
            138: {
                Code: 138,
                Message: 'Previous Closing is Required to Continue Further.',
                Description: '',
                Type: 'Error'
            },
            139: {
                Code: 139,
                Message: 'Some bills are required to authorised.',
                Description: '',
                Type: 'Error'
            },
            140: {
                Code: 140,
                Message: 'Some vouchers are required to authorised.',
                Description: '',
                Type: 'Error'
            },
            141: {
                Code: 141,
                Message: 'Total Debit or Credit must be equal to Sum of Amount in PKR and Charges.',
                Description: 'Total Debit or Credit must be equal to Sum of Amount in PKR and Charges.',
                Type: 'Error'
            },
            142: {
                Code: 142,
                Message: 'Day Closed',
                Description: '',
                Type: 'Success'
            },
            143: {
                Code: 143,
                Message: 'Bill Already Reversed',
                Description: '',
                Type: 'Error'
            },
            144: {
                Code: 144,
                Message: 'This Location can not log reverse.',
                Description: '',
                Type: 'Error'
            },
            145: {
                Code: 145,
                Message: 'Reversed Successfully',
                Description: '',
                Type: 'Success'
            },
            146: {
                Code: 146,
                Message: 'Does not exists',
                Description: '',
                Type: 'Error'
            },
            147: {
                Code: 147,
                Message: 'Please select a bill',
                Description: '',
                Type: 'Error'
            },
            148: {
                Code: 148,
                Message: 'For better use experience use <a target="_blank" href="https://www.google.com/chrome/">Google Chrome</a>.',
                Description: '',
                Type: 'Info'
            },
            149: {
                Code: 149,
                Message: 'Please set rates for the select currency.',
                Description: 'Please set rates for the select currency.',
                Type: 'Error'
            },
            150: {
                Code: 150,
                Message: 'Server Date can not be modified.',
                Description: '',
                Type: 'Error'
            },
            151: {
                Code: 151,
                Message: window.BillGlobalSettings ? ('For CTR PKR should be between (' + window.BillGlobalSettings.PKRForBill + ' - ' + window.BillGlobalSettings.PKRForCTR + ').') : '',
                //Description: 'For CTR PKR should be between (' + window.BillGlobalSettings.PKRForBill + ' - ' + window.BillGlobalSettings.PKRForCTR + '). Contact system administrator for more information',
                Type: 'Info'
            },
            152: {
                //Code: 149,
                Message: 'System password did not matched please contact System Administrator.',
                //Description: '',
                Type: 'Error'
            },
            153: {
                //Code: 149,
                Message: 'Updated Successfully',
                //Description: '',
                Type: 'Success'
            },
            154: {
                //Code: 154,
                Message: 'No Matching Account found Against this Currency',
                //Description: '',
                Type: 'Info'
            },
            155: {
                //Code: 154,
                Message: 'Please install latest version of Client Application.',
                //Description: '',
                Type: 'Info'
            },
            156: {
                //Code: 156,
                Message: 'Receiver Identity# Found in Proscribed Persons List',
                //Description: '',
                Type: 'Error'
            },
            157: {
                //Code: 157,
                Message: 'Sender Identity# Found in Proscribed Persons List',
                //Description: '',
                Type: 'Error'
            }, 158: {
                //Code: 157,
                Message: 'Status Changed',
                //Description: '',
                Type: 'Success'
            }, 159: {
                //Code: 157,
                Message: 'Head Office will authorized this Voucher',
                //Description: '',
                Type: 'Success'
            },
            160: {
                //Code: 160,
                Message: 'You Donot Have Enough Money',
                Description: '',
                Type: 'Info'
            },
            161: {
                //Code: 160,
                Message: 'Please Enter a Valid Remittance Voucher Code',
                Description: '',
                Type: 'Error'
            }, 162: {
                //Code: 160,
                Message: 'You can not set time more than current running time.',
                Description: '',
                Type: 'Error'
            }, 163: {
                //Code: 160,
                Message: 'Currency Already Entered.',
                Description: '',
                Type: 'Error'
            }, 164: {
                //Code: 160,
                Message: 'Select Currency and enter FC amount.',
                Description: '',
                Type: 'Error'
            }, 165: {
                //Code: 160,
                Message: 'Sale and Purchase Low should be less than High repectively.',
                Description: '',
                Type: 'Error'
            }, 166: {
                //Code: 160,
                Message: 'CDD Required',
                Description: '',
                Type: 'Error'
            },
            167: {
                //Code: 160,
                Message: 'Authorized',
                Description: '',
                Type: 'Success'
            },
            168: {
                //Code: 160,
                Message: 'Already STR',
                Description: '',
                Type: 'Success'
            },
            169: {
                Message: 'CDD/EDD Info is required!',
                Description: '',
                Type: 'Error'
            },
            170: {
                Message: 'Reference# Already Exists',
                Description: '',
                Type: 'Error'
            }
            ,
            171: {
                Message: 'Amount mismatched in ledger',
                Description: '',
                Type: 'Error'
            },
            172: {
                Message: 'Unknown error',
                Description: '',
                Type: 'Error'
            }
            ,
            173: {
                Message: 'TV Vouchers can not be reversed',
                Description: '',
                Type: 'Error'
            },
            174: {
                Code: 174,
                Message: window.BillGlobalSettings ? ('For more than ($' + window.BillGlobalSettings.USDForCTR + '/PKR ' + window.BillGlobalSettings.PKRForCTR + ') transction; TV and CTR info is required. create TV/CTR bill.') : '',
                //Description: 'For more than ($' + window.BillGlobalSettings.USDForCTR + '/PKR ' + window.BillGlobalSettings.PKRForCTR + ') transction; TV and CTR info is required. create TV/CTR bill, contact to administrator',
                Type: 'Warning'
            },
            175: {
                Code: 175,
                Message: 'Please Enter Remittance Info',
                Description: 'Please Enter Remittance Info',
                Type: 'Error'
            },
            176: {
                Code: 176,
                Message: 'Found.',
                Description: 'Found.',
                Type: 'Error'
            },
            177: {
                Code: 177,
                Message: 'Customer Name is not valid for Resident/Non-Resident Customers.',
                Description: 'Customer Name is not valid for Resident/Non-Resident Customers.',
                Type: 'Error'
            },
            178: {
                //Code: 177,
                Message: 'Not Implemented.',
                //Description: 'Customer Name is not valid for Resident/Non-Resident Customers.',
                Type: 'Warning'
            }
            , 179: {
                //Code: 179,
                Message: 'Please Select any Voucher Type',
                Type: 'Info'
            }
            , 180: {
                //Code: 180,
                Message: 'Month Closing has already been performed !',
                Type: 'Info'
            }
            , 181: {
                //Code: 180,
                Message: 'Day Closing is Required to continue further',
                Type: 'Info'
            }
            , 182: {
                //Code: 182,
                Message: 'Sender is in Blacklist',
                Type: 'Error'
            }
            , 183: {
                //Code: 183,
                Message: 'Receiver is in Blacklist',
                Type: 'Error'
            },
            184: {
                Message: 'This value can not be 0.',
                Type: 'Error'
            }, 185: {
                Message: 'There were Multiple errors while saving.',
                Type: 'Error'
            }
            , 186: {
                Message: 'You donot have permission to perform Month Closing please contact Head Office for further Information.',
                Type: 'Error'
            }
            , 187: {
                Message: 'Month Closing Already Permitted To All Branches Please Contact To Head Office.',
                Type: 'Error'
            }
            , 188: {
                Message: 'Month Closing Permission granted to all Branches.',
                Type: 'Success'
            }
            , 189: {
                Message: 'Revaluation Rates has not been updated yet.',
                Type: 'Error'
            }
            ,
            190: {
                Message: 'This Location Code Already Exists.',
                Type: 'Warning'
            },
            191: {
                Message: 'Incorrect Key',
                Type: 'Error'
            },
            192: {
                Message: 'Registred',
                Type: 'Success'
            },
            193: {
                Message: 'Please enter Complete CNIC or Passport',
                Type: 'Warning'
            },
            194: {
                Message: 'Please Select Account',
                Type: 'Info'
            }, 195: {
                Message: 'Incorrect Registration Key',
                Type: 'Info'
            }, 196: {
                Message: 'Not Found',
                Type: 'Success'
            },
            197: {
                Message: 'Please Provide All Values',
                Type: 'Info'
            },
            ///From now onword use more than 1000 AppCodes 1 to 999 Are now Reserved For HTTPStatus Codes
            1000: {
                Message: 'Please add transactions',
                Type: 'Error'

            },
            1001: {
                Message: 'Please Upload Non Empty Excel File with (Sr.,Name,FatherName,CNIC,Address) Columns!',
                Type: 'Error'

            },
            1002: {
                Message: 'Submitted Successfully!',
                Type: 'Success'
            },
            1003: {
                Message: 'Please Upload Valid Western Union Inward File !',
                Type: 'Warning'
            },
            1004: {
                Message: 'Please Upload Valid Western Union File !',
                Type: 'Warning'
            },
            1005: {
                Message: 'Please Upload Valid Western Union Outward File !',
                Type: 'Warning'
            }
            ,
            1006: {
                Message: 'Please Upload Valid RIA Inward File !',
                Type: 'Warning'
            }
            ,
            1007: {
                Message: 'Please Upload Valid MoneyGram File !',
                Type: 'Warning'
            },
            1008: {
                Message: 'Please Upload Valid MoneyGram Inward File !',
                Type: 'Warning'
            },
            1009: {
                Message: 'Please Upload Valid MoneyGram Outward File !',
                Type: 'Warning'
            }
            ,
            1010: {
                Message: 'Please Select VoucherType First !',
                Type: 'Warning'
            }
            ,
            1011: {
                Message: 'This beneficial owner identity No. is blocked.',
                Type: 'Error'
            },
            1012: {
                Message: 'This carrier CNIC No. is blocked.',
                Type: 'Error'
            },
            1013: {
                Message: 'This browser is currently not supported Please update your browser. <br><a href="{{URL}}" target="_blank">Download {{browser}}</a>',
                Type: 'Blocked'
        },
        1014: {
            Message: 'Sender Identity # is Required !',
            Type: 'Error'
        },
        1015: {
            Message: 'Receiver Identity # is Required !',
            Type: 'Error'
        }
        ,
        1016: {
            Message: 'Customer Identity # is Required !',
            Type: 'Error'
        }
        ,
        1017: {
            Message: 'Please Upload Valid Excel File!',
            Type: 'Error'
        }
        ,1018: {
             Message: 'Record deleted successfully.',
            Type: 'Success'
        }
        , 1019: {
            Message: 'Record updated successfully.',
            Type: 'Success'
        }, 1020: {
            Message: 'Your request for registeration is submitted successfully!<br />After Approval Your Company Profile will be created!',
            Type: 'Success'
        }, 1021: {
            Message: 'Request Approved Successfully!',
            Type: 'Success'
        }, 1022: {
            Message: 'Company With provided name already registered!<br />Please Try Another Name!',
            Type: 'Error'
        }, 1023: {
            Message: 'Requested Login Name already registered!<br />Please Try Another Login Name!',
            Type: 'Error'
        }, 1024: {
            Message: 'This Company Already Exists In Top Profiles!',
            Type: 'Error'
        }, 1025: {
            Message: 'This Nature of Conduct Already Exists Please Try Another Name!',
            Type: 'Error'
        },
        1026: {
            Message: 'This Description Already Exists Please Try Another!',
            Type: 'Error'
        },
        1027: {
            Message: 'This is approved request!Can not delete approved request!',
            Type: 'Error'
        },
        1028: {
            Message: 'Request deleted successfully!',
            Type: 'Success'
        }
        };
});

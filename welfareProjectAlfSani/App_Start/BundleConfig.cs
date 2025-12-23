using System.Web;
using System.Web.Optimization;

namespace AlifSani
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/GlobalMandatory").Include(
               //"~/Frontend/Metronic6/assets/vendors/general/perfect-scrollbar/css/perfect-scrollbar.css",
               "~/Frontend/Metronic6/assets/vendors/general/tether/dist/css/tether.min.css",
               "~/Frontend/Semantic-UI/semantic.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-datetime-picker/css/bootstrap-datetimepicker.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-timepicker/css/bootstrap-timepicker.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-daterangepicker/daterangepicker.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-select/dist/css/bootstrap-select.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/select2/dist/css/select2.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/ion-rangeslider/css/ion.rangeSlider.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/nouislider/distribute/nouislider.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/owl.carousel/dist/assets/owl.carousel.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/owl.carousel/dist/assets/owl.theme.default.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/dropzone/dist/dropzone.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/summernote/dist/summernote.css",
               "~/Frontend/Metronic6/assets/vendors/general/bootstrap-markdown/css/bootstrap-markdown.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/animate.css/animate.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/toastr/build/toastr.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/morris.js/morris.css",
               //"~/Frontend/Metronic6/assets/vendors/general/sweetalert2/dist/sweetalert2.min.css",
               "~/Frontend/Metronic6/assets/vendors/general/socicon/css/socicon.css",
               "~/Frontend/Metronic6/assets/vendors/custom/vendors/line-awesome/css/line-awesome.css",
               "~/Frontend/Metronic6/assets/vendors/custom/vendors/flaticon/flaticon.css",
               "~/Frontend/Metronic6/assets/vendors/custom/vendors/flaticon2/flaticon.css",
               //"~/Frontend/Metronic6/assets/vendors/general/fortawesome/fontawesome-free/css/all.min.css",
               "~/Frontend/Metronic6/assets/vendors/custom/vendors/fontawesome5/css/all.min.css",
               "~/Frontend/Metronic6/assets/css/demo1/style.bundle.min.css",
               //"~/Frontend/Metronic6/assets/css/demo1/skins/header/base/light.min.css",
               "~/Frontend/Metronic6/assets/css/demo1/skins/header/base/dark.min.css",
               "~/Frontend/Metronic6/assets/css/demo1/skins/header/menu/light.min.css",
               "~/Frontend/Metronic6/assets/css/demo1/skins/brand/dark.min.css",
               "~/Frontend/Metronic6/assets/css/demo1/skins/aside/dark.min.css",
               "~/Frontend/DataTables/DataTables-1.10.18/css/dataTables.bootstrap.min.css",
               "~/Frontend/DataTables/DataTables-1.10.18/css/dataTables.bootstrap4.min.css",
               "~/Frontend/DataTables/Buttons-1.5.4/css/buttons.bootstrap4.min.css",
               "~/Frontend/jquery-datatables-checkboxes-1.2.11/css/dataTables.checkboxes.css",
               "~/Frontend/DataTables/Buttons-1.5.4/css/buttons.dataTables.min.css",
               //"~/Frontend/MProgress.js/mprogress.min.css",
               "~/Content/Site-Metronic6.css",
               //"~/Frontend/PrintArea-master/css/jquery.printarea.css",
               // "~/Content/images/Flags/flags.css",
               "~/Content/images/Flags/css/flag-icon.min.css",
                //"~/Frontend/Marque/css/marquee.css",
                //"~/Frontend/SlickTheme/css/photo-slider.css",
                "~/Frontend/BizPage/css/style.css",
                 "~/Frontend/BizPage/lib/font-awesome/css/font-awesome.min.css",
                 "~/Frontend/BizPage/lib/ionicons/css/ionicons.min.css"
               ));

            bundles.Add(new ScriptBundle("~/bundles/GlobalMandatory").Include(
                "~/Frontend/Metronic6/assets/vendors/general/jquery/dist/jquery.js",
                "~/Frontend/Metronic6/assets/vendors/general/popper.js/dist/umd/popper.min.js",
                "~/Frontend/Semantic-UI/semantic.min.js",

                "~/Frontend/Metronic6/assets/vendors/general/bootstrap/dist/js/bootstrap.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/js-cookie/src/js.cookie.js",
                "~/Frontend/Metronic6/assets/vendors/general/moment/min/moment.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/tooltip.js/dist/umd/tooltip.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/perfect-scrollbar/dist/perfect-scrollbar.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/sticky-js/dist/sticky.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/wnumb/wNumb.js",
                "~/Scripts/bootbox.min.js",
                "~/Scripts/papaparse.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/jquery-form/dist/jquery.form.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/block-ui/jquery.blockUI.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/components/vendors/bootstrap-datepicker/init.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-datetime-picker/js/bootstrap-datetimepicker.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-timepicker/js/bootstrap-timepicker.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/components/vendors/bootstrap-timepicker/init.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-daterangepicker/daterangepicker.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-maxlength/bootstrap-maxlength.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/vendors/bootstrap-multiselectsplitter/bootstrap-multiselectsplitter.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-select/dist/js/bootstrap-select.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-switch/dist/js/bootstrap-switch.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/components/vendors/bootstrap-switch/init.js",
                "~/Frontend/Metronic6/assets/vendors/general/select2/dist/js/select2.full.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/ion-rangeslider/js/ion.rangeSlider.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/typeahead.js/dist/typeahead.bundle.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/handlebars/dist/handlebars.js",
                "~/Frontend/Metronic6/assets/vendors/general/inputmask/dist/min/jquery.inputmask.bundle.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/inputmask/dist/inputmask/min/inputmask/inputmask.date.extensions.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/inputmask/dist/inputmask/min/inputmask/inputmask.numeric.extensions.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/nouislider/distribute/nouislider.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/owl.carousel/dist/owl.carousel.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/autosize/dist/autosize.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/clipboard/dist/clipboard.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/dropzone/dist/min/dropzone.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/summernote/dist/summernote.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/markdown/lib/markdown.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-markdown/js/bootstrap-markdown.js",
                "~/Frontend/Metronic6/assets/vendors/custom/components/vendors/bootstrap-markdown/init.js",
                "~/Frontend/Metronic6/assets/vendors/general/bootstrap-notify/bootstrap-notify.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/components/vendors/bootstrap-notify/init.js",
                "~/Frontend/Metronic6/assets/vendors/general/jquery-validation/dist/jquery.validate.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/jquery-validation/dist/additional-methods.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/components/vendors/jquery-validation/init.js",
                "~/Frontend/Metronic6/assets/vendors/general/toastr/build/toastr.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/raphael/raphael.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/morris.js/morris.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/chart.js/dist/Chart.bundle.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/vendors/bootstrap-session-timeout/dist/bootstrap-session-timeout.min.js",
                "~/Frontend/Metronic6/assets/vendors/custom/vendors/jquery-idletimer/idle-timer.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/waypoints/lib/jquery.waypoints.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/counterup/jquery.counterup.min.js",
                "~/Frontend/Metronic6/assets/vendors/general/es6-promise-polyfill/promise.min.js",
                //"~/Frontend/Metronic6/assets/vendors/general/sweetalert2/dist/sweetalert2.min.js",
                "~/Scripts/sweetalert2@9.js",
                //"~/Frontend/Metronic6/assets/vendors/custom/components/vendors/sweetalert2/init.js",
                "~/Frinitontend/Metronic6/assets/vendors/general/jquery.repeater/src/lib.js",
                "~/Frontend/Metronic6/assets/vendors/general/jquery.repeater/src/jquery.input.js",
                "~/Frontend/Metronic6/assets/vendors/general/jquery.repeater/src/repeater.js",
                "~/Frontend/Metronic6/assets/vendors/general/dompurify/dist/purify.min.js",

                //"~/Frontend/Metronic6/assets/demo/default/base/scripts.bundle.min.js",
                // "~/Frontend/Metronic6/assets/js/demo1/scripts.bundle.min.js",

                "~/Frontend/Metronic6/assets/vendors/custom/fullcalendar/fullcalendar.bundle.min.js",
                "~/Frontend/DataTables/DataTables-1.10.18/js/jquery.dataTables.min.js",
                "~/Frontend/DataTables/DataTables-1.10.18/js/dataTables.bootstrap4.min.js",
                "~/Frontend/DataTables/Buttons-1.5.4/js/dataTables.buttons.min.js",
                "~/Frontend/DataTables/Buttons-1.5.4/js/buttons.bootstrap4.min.js",
                "~/Frontend/DataTables/JSZip-2.5.0/jszip.min.js",
                "~/Frontend/DataTables/Buttons-1.5.4/js/buttons.html5.min.js",
                "~/Frontend/DataTables/Buttons-1.5.4/js/buttons.print.min.js",
                "~/Frontend/DataTables/Buttons-1.5.4/js/buttons.colVis.min.js",
                "~/Frontend/jquery-datatables-checkboxes-1.2.11/js/dataTables.checkboxes.min.js",
                
                //"~/Frontend/hotkeys/hotkeys.min.js",
                "~/Scripts/Utilities.js",
                "~/Scripts/AppCodes.js",
               
                //"~/Frontend/Marque/js/marquee.js",
                //"~/Frontend/SlickTheme/js/slick.min.js",
                "~/Frontend/BizPage/lib/superfish/hoverIntent.js",
                "~/Frontend/BizPage/lib/superfish/superfish.min.js",
                "~/Frontend/tinymce/js/tinymce/tinymce.min.js",
                "~/Frontend/BizPage/contactform/contactform.js"
                ));
        }
    }
}

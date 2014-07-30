using System.Web;
using System.Web.Optimization;

namespace App
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/JScriptCommon.js"));
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                       "~/Res/easyui/jquery.easyui.min.js",
                      "~/Res/easyui/locale/easyui-lang-zh_CN.js",
                         "~/Res/My97DatePicker/WdatePicker.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));




            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Res/easyui/themes/default/easyui.css",
                      "~/Res/easyui/themes/icon.css",
                      "~/Content/default.css",
                      "~/Content/StyleSheet.css"));
        }
    }
}

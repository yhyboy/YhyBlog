/*==============================
版本：v0.1
创建：2016.3.2
作者：洞庭夕照
博客:http://mzwhj.cnblogs.com
==============================*/
using System.Web;
using System.Web.Optimization;

namespace WebPage
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //Control区域脚本
            bundles.Add(new ScriptBundle("~/bundles/Control").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/bootstrap-dialog.js",
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/bootstrap-select-zh_CN.js",
                        "~/Scripts/bootstrap-table.js",
                        "~/Scripts/bootstrap-table-zh-CN.js",
                        "~/Scripts/jquery.twbsPagination.js",
                        "~/Scripts/jquery.ztree.all.min.js"
                        ));
            //UEditor区域脚本
            bundles.Add(new ScriptBundle("~/bundles/UEditor").Include(
                        "~/ueditor/ueditor.config.js",
                        "~/ueditor/ueditor.all.min.js",
                       "~/ueditor/lang/zh-cn/zh-cn.js"
                        ));
            //Control式样
            bundles.Add(new StyleBundle("~/Content/Control").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Control.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/bootstrap-dialog.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/bootstrap-table.css",
                      "~/Content/zTree/metroStyle/metroStyle.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


        }
    }
}

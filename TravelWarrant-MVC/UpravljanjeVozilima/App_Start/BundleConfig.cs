using System.Collections.Generic;
using System.Web.Optimization;

namespace UpravljanjeVozilima
{
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }


    static class BundleExtentions
    {
        public static Bundle NonOrdering(this Bundle bundle)
        {
            bundle.Orderer=new NonOrderingBundleOrderer();
            return bundle;
        }
    }
    
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/js").NonOrdering().Include(
                "~/UI/vendor/jquery/dist/jquery.min.js",
                "~/UI/vendor/jquery/dist/jquery.validate.min.js",
                "~/UI/vendor/jquery/dist/jquery.validate.unobtrusive.min.js",
                "~/UI/vendor/jquery/dist/jquery.mask.min.js",
                "~/UI/vendor/bootstrap/dist/js/bootstrap.bundle.min.js",
                "~/UI/vendor/data-tables/jquery.dataTables.min.js",
                "~/UI/vendor/sweetalert/sweetalert2@10.js",
                "~/UI/vendor/bs-custom-file-input/dist/bs-custom-file-input.min.js",
                "~/UI/vendor/simplebar/dist/simplebar.min.js",
                "~/UI/vendor/smooth-scroll/dist/smooth-scroll.polyfills.min.js",
                "~/UI/js/utils.js",
                "~/UI/js/theme.min.js")
            );

            bundles.Add(new StyleBundle("~/UI/css").Include(
                "~/UI/vendor/data-tables/jquery.dataTables.min.css",
                "~/UI/css/theme.css",
                "~/UI/vendor/simplebar/dist/simplebar.min.css")
            );

        }
    }
}
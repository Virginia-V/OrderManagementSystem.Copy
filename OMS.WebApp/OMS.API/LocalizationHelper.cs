namespace OMS.API
{
    public static class LocalizationHelper
    {
        public static RequestLocalizationOptions GetLocalizationOptions()
        {
            var dir = new DirectoryInfo("Resources");
            var resuorceFiles = dir.EnumerateFiles();
            var supportedCultures = resuorceFiles.Select(x => x.Name.Replace("Resource.", string.Empty).Replace(".resx", string.Empty)).ToArray();
            var options = new RequestLocalizationOptions().AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);

            return options;
        }
    }

    public class Resource
    {

    }
}

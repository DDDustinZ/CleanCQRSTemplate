namespace IntegrationTests.Common;

public static class VerifySettingsFactory
{
    public static VerifySettings Default
    {
        get
        {
            var settings = new VerifySettings();
            settings.UseDirectory("Verify");
            return settings;
        }
    }
}
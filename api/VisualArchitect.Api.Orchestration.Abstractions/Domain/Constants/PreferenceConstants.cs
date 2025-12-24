namespace VisualArchitect.Api.Orchestration.Abstractions.Domain.Constants;

public static class PreferenceConstants
{
    public static class Theme
    {
        public const string Key = "theme";
        public const string DefaultValue = Values.Light;

        public static class Values
        {
            public const string System = "auto";
            public const string Light = "light";
            public const string Dark = "Dark";
        }
    }

    public static class Language
    {
        public const string Key = "language";
        public const string DefaultValue = Values.English;

        public static class Values
        {
            public const string English = "en";
            public const string German = "de";
        }
    }
}

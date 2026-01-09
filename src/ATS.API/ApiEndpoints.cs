namespace ATS.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Candidate
    {
        private const string Base = $"{ApiBase}/candidate";

        public const string Create = Base;
        public const string GetById = $"{Base}/{{id:guid}}";
        public const string List = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Applications = $"{Base}/{{id:guid}}/applications";
    }

    public static class Job
    {
        private const string Base = $"{ApiBase}/job";

        public const string Create = Base;
        public const string GetById = $"{Base}/{{id:guid}}";
        public const string List = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
        public const string Applications = $"{Base}/{{id:guid}}/applications";
    }

    public static class JobApplication
    {
        private const string Base = $"{ApiBase}/application";

        public const string Register = Base;
        public const string GetById = $"{Base}/{{id:guid}}";
    }
}

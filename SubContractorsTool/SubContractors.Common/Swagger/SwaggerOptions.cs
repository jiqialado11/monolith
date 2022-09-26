namespace SubContractors.Common.Swagger
{
    public class SwaggerOptions
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool IncludeSecurity { get; set; }
        public bool IncludeComments { get; set; }
        public string FileName { get; set; }
    }
}

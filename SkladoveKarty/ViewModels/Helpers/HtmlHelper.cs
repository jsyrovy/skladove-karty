namespace SkladoveKarty.ViewModels.Helpers
{
    using System.Collections.Generic;
    using Stubble.Core.Builders;

    public static class HtmlHelper
    {
        public static string Generate(string template, object model)
        {
            var stubble = new StubbleBuilder().Build();

            return stubble.Render(template, model);
        }
    }
}

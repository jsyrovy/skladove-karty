namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using Stubble.Core.Builders;

    public static class HtmlHelper
    {
        public static string Generate(string template, object model)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var stubble = new StubbleBuilder().Build();

            return stubble.Render(template, model);
        }
    }
}

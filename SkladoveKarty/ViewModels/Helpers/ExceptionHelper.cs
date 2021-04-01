namespace SkladoveKarty.ViewModels.Helpers
{
    using System;
    using System.Collections.Generic;

    public static class ExceptionHelper
    {
        public static string GetCompleteExceptionMessage(Exception exception)
        {
            var messages = new List<string>();

            var currentException = exception;

            while (currentException != null)
            {
                messages.Add(currentException.Message);
                currentException = currentException.InnerException;
            }

            return string.Join($"{Environment.NewLine}{Environment.NewLine}", messages);
        }
    }
}

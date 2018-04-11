namespace Blog.WebApp.Utility
{
    public static class JavaScript
    {
        public static string Confirm(string confirmationMessage, params object[] confirmationMessageParams)
        {
            return string.Format("return confirm('" + confirmationMessage + "')", confirmationMessageParams);
        }
    }
}
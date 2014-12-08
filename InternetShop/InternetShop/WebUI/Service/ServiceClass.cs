namespace WebUI.Service
{
    /// <summary>
    /// Обрезает входную строку до указанной длины.
    /// </summary>
    /// <param name="inputString">
    /// Строка для обрезания
    /// </param>
    /// <param name="lenght">
    /// Длина выходной строки
    /// </param>
    public static class ServiceClass
    {
        public static string GetAShortString(string inputString, int lenght)
        {
            string shortString = string.Empty;
            if (inputString == null || inputString.Length < lenght)
            {
                shortString = inputString;
            }
            else
            {
                int iNextSpace = inputString.LastIndexOf(" ", lenght);
                shortString = string.Format("{0}...", inputString.Substring(0, (iNextSpace > 0) ? iNextSpace : lenght).Trim());
            }
            return shortString;
        }
    }
}
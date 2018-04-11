using System;
using System.Configuration;

namespace Blog.WebApp.Utility
{
    public static class DateTimeExtensions
    {
        public static string ToFormattedDate(this DateTime utcDateTime)
        {
            var timezoneInfo = TimeZoneInfo
                .FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);

            var dateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneInfo);

            return dateTime.ToString("dd.MM.yyyy");
        }
    }
}
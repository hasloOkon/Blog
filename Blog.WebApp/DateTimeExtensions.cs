﻿using System;
using System.Configuration;

namespace Blog.WebApp
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDateTime)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return string.Format("{0}", TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneInfo).ToShortDateString());
        }
    }
}
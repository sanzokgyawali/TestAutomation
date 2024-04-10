﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Accounts.Test.TestQuery.ExtensionMethod
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime) => dateTime.AddDays(-(int)dateTime.DayOfWeek);

        public static DateTime EndOfWeek(this DateTime dateTime) => dateTime.StartOfWeek().AddDays(6);

        public static DateTime EndofMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));

        public static DateTime StartOfMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1);
    }
}
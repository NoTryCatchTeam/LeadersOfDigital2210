﻿using System;
using System.Globalization;
using MvvmCross.Converters;

namespace LeadersOfDigital.Converters
{
    public class NullDateToStringConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ((DateTime?)value)?.ToString("dd.MM.yyyy", new CultureInfo("ru-RU"));
    }
}
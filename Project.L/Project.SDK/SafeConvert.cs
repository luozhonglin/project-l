using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.SDK
{
    public static class SafeConvert
    {
        /// <summary>
        /// Changes the type.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="conversionType">Type of the conversion.</param>
        /// <returns></returns>
        public static object ChangeType(object obj, Type conversionType)
        {
            switch (conversionType.Name.ToLower())
            {
                case "int32":
                    return (object)SafeConvert.ToInt32(obj);
                case "string":
                    return (object)SafeConvert.ToString(obj);
                default:
                    return Convert.ChangeType(obj, conversionType);
            }
        }

        /// <summary>
        /// Determines whether [is database null] [the specified object].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if [is database null] [the specified object]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDBNull(object obj)
        {
            return Convert.IsDBNull(obj);
        }

        /// <summary>
        /// Converts to guid.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Guid ToGuid(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                try
                {
                    if (Guid.TryParse(obj.ToString(), out Guid result))
                    {
                        return result;
                    }
                }
                catch
                {
                    return Guid.Empty;
                }
            }
            return Guid.Empty;
        }

        /// <summary>
        /// Converts to guid.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Guid? ToGuid(object obj, Guid? defaultValue)
        {
            if (obj != null && obj != DBNull.Value)
            {
                try
                {
                    if (Guid.TryParse(obj.ToString(), out Guid result))
                    {
                        return result;
                    }
                }
                catch
                {
                    return defaultValue;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts to timespan.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(object obj)
        {
            return SafeConvert.ToTimeSpan(obj, TimeSpan.Zero);
        }

        /// <summary>
        /// Converts to timespan.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(object obj, TimeSpan defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToTimeSpan(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to timespan.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string s, TimeSpan defaultValue)
        {
            TimeSpan result;
            if (!TimeSpan.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to timespan.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string s)
        {
            return SafeConvert.ToTimeSpan(s, TimeSpan.Zero);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(object obj)
        {
            if (obj != null)
                return obj.ToString();
            return string.Empty;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(string s)
        {
            return SafeConvert.ToString(s, string.Empty);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultString">The default string.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(string s, string defaultString)
        {
            if (s == null)
                return defaultString;
            return s.ToString();
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultString">The default string.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(object s, string defaultString)
        {
            if (s == null)
                return defaultString;
            return s.ToString();
        }

        /// <summary>
        /// Converts to float.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static float ToFloat(string s)
        {
            return SafeConvert.ToFloat(s, 0);
        }

        /// <summary>
        /// Converts to float.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static float ToFloat(string s, float defaultValue = 0)
        {
            float result;
            if (!float.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to float.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static float ToFloat(object obj, float defaultValue = 0)
        {
            if (obj != null)
                return SafeConvert.ToFloat(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to double.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static double ToDouble(string s, double defaultValue)
        {
            double result;
            if (!double.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to double.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static double ToDouble(string s)
        {
            return SafeConvert.ToDouble(s, 0.0);
        }

        /// <summary>
        /// Converts to double.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static double ToDouble(object obj)
        {
            return SafeConvert.ToDouble(obj, 0.0);
        }

        /// <summary>
        /// Converts to double.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static double ToDouble(object obj, double defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToDouble(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to single.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static float ToSingle(string s, float defaultValue)
        {
            float result;
            if (!float.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to single.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static float ToSingle(string s)
        {
            return SafeConvert.ToSingle(s, 0.0f);
        }

        /// <summary>
        /// Converts to single.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static float ToSingle(object obj)
        {
            return SafeConvert.ToSingle(obj, 0.0f);
        }

        /// <summary>
        /// Converts to single.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static float ToSingle(object obj, float defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToSingle(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Decimal ToDecimal(string s, Decimal defaultValue)
        {
            Decimal result;
            if (!Decimal.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static Decimal ToDecimal(string s)
        {
            return SafeConvert.ToDecimal(s, new Decimal(0));
        }

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Decimal ToDecimal(object obj)
        {
            return SafeConvert.ToDecimal(obj, new Decimal(0));
        }

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Decimal ToDecimal(object obj, Decimal defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToDecimal(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to boolean.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public static bool ToBoolean(string s, bool defaultValue)
        {
            if (s == "1")
                return true;
            bool result;
            if (!bool.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to boolean.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static bool ToBoolean(string s)
        {
            return SafeConvert.ToBoolean(s, false);
        }

        /// <summary>
        /// Converts to boolean.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            return SafeConvert.ToBoolean(obj, false);
        }

        /// <summary>
        /// Converts to boolean.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public static bool ToBoolean(object obj, bool defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToBoolean(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to char.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static char ToChar(string s, char defaultValue)
        {
            char result;
            if (!char.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to char.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static char ToChar(string s)
        {
            return SafeConvert.ToChar(s, char.MinValue);
        }

        /// <summary>
        /// Converts to char.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static char ToChar(object obj)
        {
            return SafeConvert.ToChar(obj, char.MinValue);
        }

        /// <summary>
        /// Converts to char.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static char ToChar(object obj, char defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToChar(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to byte.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static byte ToByte(string s, byte defaultValue)
        {
            byte result;
            if (!byte.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to byte.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static byte ToByte(string s)
        {
            return SafeConvert.ToByte(s, (byte)0);
        }

        /// <summary>
        /// Converts to byte.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte ToByte(object obj)
        {
            return SafeConvert.ToByte(obj, (byte)0);
        }

        /// <summary>
        /// Converts to byte.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static byte ToByte(object obj, byte defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToByte(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to sbyte.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(string s, sbyte defaultValue)
        {
            sbyte result;
            if (!sbyte.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to sbyte.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static sbyte ToSByte(string s)
        {
            return SafeConvert.ToSByte(s, (sbyte)0);
        }

        /// <summary>
        /// Converts to sbyte.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static sbyte ToSByte(object obj)
        {
            return SafeConvert.ToSByte(obj, (sbyte)0);
        }

        /// <summary>
        /// Converts to sbyte.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static sbyte ToSByte(object obj, sbyte defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToSByte(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to int16.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static short ToInt16(string s, short defaultValue)
        {
            short result;
            if (!short.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to int16.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static short ToInt16(string s)
        {
            return SafeConvert.ToInt16(s, (short)0);
        }

        /// <summary>
        /// Converts to int16.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static short ToInt16(object obj)
        {
            return SafeConvert.ToInt16(obj, (short)0);
        }

        /// <summary>
        /// Converts to int16.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static short ToInt16(object obj, short defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToInt16(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to uint16.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(string s, ushort defaultValue)
        {
            ushort result;
            if (!ushort.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to uint16.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static ushort ToUInt16(string s)
        {
            return SafeConvert.ToUInt16(s, (ushort)0);
        }

        /// <summary>
        /// Converts to uint16.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static ushort ToUInt16(object obj)
        {
            return SafeConvert.ToUInt16(obj, (ushort)0);
        }

        /// <summary>
        /// Converts to uint16.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static ushort ToUInt16(object obj, ushort defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToUInt16(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to int32.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ToInt32(string s, int defaultValue)
        {
            int result;
            if (!int.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to int32.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static int ToInt32(string s)
        {
            return SafeConvert.ToInt32(s, 0);
        }

        /// <summary>
        /// Converts to int32.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static int ToInt32(object obj)
        {
            return SafeConvert.ToInt32(obj, 0);
        }

        /// <summary>
        /// Converts to int32.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ToInt32(object obj, int defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToInt32(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to uint32.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static uint ToUInt32(string s, uint defaultValue)
        {
            uint result;
            if (!uint.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to uint32.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static uint ToUInt32(string s)
        {
            return SafeConvert.ToUInt32(s, 0U);
        }

        /// <summary>
        /// Converts to uint32.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static uint ToUInt32(object obj)
        {
            return SafeConvert.ToUInt32(obj, 0U);
        }

        /// <summary>
        /// Converts to uint32.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static uint ToUInt32(object obj, uint defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToUInt32(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to int64.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static long ToInt64(string s, long defaultValue)
        {
            long result;
            if (!long.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to int64.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static long ToInt64(string s)
        {
            return SafeConvert.ToInt64(s, 0L);
        }

        /// <summary>
        /// Converts to int64.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static long ToInt64(object obj)
        {
            return SafeConvert.ToInt64(obj, 0L);
        }

        /// <summary>
        /// Converts to int64.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static long ToInt64(object obj, long defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToInt64(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to uint64.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(string s, ulong defaultValue)
        {
            ulong result;
            if (!ulong.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to uint64.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static ulong ToUInt64(string s)
        {
            return SafeConvert.ToUInt64(s, 0UL);
        }

        /// <summary>
        /// Converts to uint64.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static ulong ToUInt64(object obj)
        {
            return SafeConvert.ToUInt64(obj, 0UL);
        }

        /// <summary>
        /// Converts to uint64.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static ulong ToUInt64(object obj, ulong defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToUInt64(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s, DateTime defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="datePattern">日期格式</param>
        /// <returns></returns>
        public static DateTime ToDateTimeWithPattern(string s, string datePattern)
        {
            return SafeConvert.ToDateTimeWithPattern(s, datePattern, DateTime.MinValue);
        }

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="datePattern">日期格式</param>
        /// <param name="defaultValue">默认时间</param>
        /// <returns></returns>
        public static DateTime ToDateTimeWithPattern(string s, string datePattern, DateTime defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParseExact(s, datePattern, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="datePattern">日期格式</param>
        /// <param name="defaultValue">默认时间</param>
        /// <returns></returns>
        public static DateTime? ToDateTimeWithPattern(string s, string datePattern, DateTime? defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParseExact(s, datePattern, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(string s, DateTime? defaultValue)
        {
            DateTime result;
            if (!DateTime.TryParse(s, out result))
                return defaultValue;
            return result;
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary> 
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static DateTime? ToUnDateTime(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                DateTime result;
                if (!DateTime.TryParse(s, out result))
                    return null;
                return result;
            }
            else return null;
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s)
        {
            return SafeConvert.ToDateTime(s, DateTime.MinValue);
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            return SafeConvert.ToDateTime(obj, DateTime.MinValue);
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToDateTime(obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="text">The text.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static object ToEnum(Type enumType, string text, object defaultValue)
        {
            if (Enum.IsDefined(enumType, (object)text))
                return Enum.Parse(enumType, text, false);
            return defaultValue;
        }

        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="obj">The object.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static object ToEnum(Type enumType, object obj, object defaultValue)
        {
            if (obj != null)
                return SafeConvert.ToEnum(enumType, obj.ToString(), defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static object ToEnum(Type enumType, int index)
        {
            return Enum.ToObject(enumType, index);
        }

        /// <summary>
        /// Converts to enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static T ToEnum<T>(object obj) where T : struct
        {
            if (obj != null)
            {
                bool result = int.TryParse(obj.ToString(), out int intObj);
                if (Enum.IsDefined(typeof(T), obj))
                {
                    string[] names = Enum.GetNames(typeof(T));
                    string name = obj.ToString();
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (name == names[i])
                        {
                            return (T)Enum.Parse(typeof(T), name);
                        }
                    }
                    return (T)Enum.ToObject(typeof(T), obj);
                }
                else if (result)
                {
                    return (T)Enum.ToObject(typeof(T), intObj);
                }
            }
            return default(T);
        }

        /// <summary>
        /// Safes the string to array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">The string.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="converter">The converter.</param>
        /// <returns></returns>
        public static List<T> SafeStringToArray<T>(string str, char[] separator, Converter<string, T> converter)
        {
            List<T> objList = new List<T>();
            if (string.IsNullOrEmpty(str))
                return objList;
            string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray == null || strArray.Length == 0)
                return objList;
            foreach (string input in strArray)
            {
                try
                {
                    objList.Add(converter(input));
                }
                catch
                {
                }
            }
            return objList;
        }

        /// <summary>
        /// Arrays to string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputs">The inputs.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string ArrayToString<T>(List<T> inputs, string separator)
        {
            if (inputs == null || inputs.Count == 0)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (T input in inputs)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(separator);
                stringBuilder.Append(input.ToString());
            }
            return stringBuilder.ToString();
        }
    }

}

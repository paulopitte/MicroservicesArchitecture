using System;
using TimeZoneConverter;

namespace Core.Common.App
{
    /// <summary>
    /// User's culture information.
    /// </summary>
    public class UserCultureInfo
    {
        /// <summary>
        /// Brazilian timezone identifier.
        /// </summary>
        public const string Brazilian = "E. South America Standard Time";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCultureInfo"/> class.
        /// </summary>
        public UserCultureInfo()
        {
            // TODO: Need to through DB Context.
            TimeZone = TZConvert.GetTimeZoneInfo(Brazilian);
            DateTimeFormat = "dd/MM/yyyy HH:mm"; // Default format.
        }

        /// <summary>
        /// Gets or sets the date time format.
        /// </summary>
        /// <value>
        /// The date time format.
        /// </value>
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public TimeZoneInfo TimeZone { get; set; }

        /// <summary>
        /// Gets the user local time.
        /// </summary>
        /// <returns></returns>
        public DateTime GetUserLocalTime(DateTime? utcDateTime)
        {
            return TimeZoneInfo.ConvertTime(utcDateTime ?? DateTime.UtcNow, TimeZone);
        }

        /// <summary>
        /// Gets the user local time.
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetUserLocalTime() => GetUserLocalTime(null);

        /// <summary>
        /// Gets the UTC time.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>Get universal date time based on User's Timezone</returns>
        public DateTime GetUtcTime(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTime(datetime, TimeZone).ToUniversalTime();
        }
    }
}

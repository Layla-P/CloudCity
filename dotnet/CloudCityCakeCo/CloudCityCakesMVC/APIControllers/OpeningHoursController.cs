using System;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CloudCityCakesMVC.APIControllers
{
    [ApiController]
    public class OpeningHoursController : ControllerBase
    {
        //List of available timezones
        //https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-vista/cc749073(v=ws.10)?redirectedfrom=MSDN
        
        [HttpGet]
        [Route("api/OpeningHours")]
        public Task<JsonResult> Get(
            string timezoneName = "Europe/Berlin",
            bool? isBusinessHours = null,
            bool? isWeekday = null,
            string day = null)
        {
            var localDateTime = GetLocalTime(timezoneName);

            
            var openingHours = new OpeningHours
            {
                IsWeekday = isWeekday ?? CheckIsWeekday(localDateTime),
                IsBusinessHours = isBusinessHours ??CheckIsBusinessHours(localDateTime),
                DayOfWeek = day==null ? GetDay(localDateTime) : (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day, true)
            };
   
            return Task.FromResult(new JsonResult(openingHours));
        }

        
        [HttpGet]
        [Route("api/OpeningHours/GetTimezones")]
        public Task<JsonResult> GetTimezones()
        {
            //https://momentjs.com/timezone/
            var timezones = new string[]
            {
                "Europe/Berlin",
                "Europe/London",
                "Austrailia/Melbourne",
                "Asia/Tokyo",
                "America/Bogota",
                "America/New_York",
                "America/San_Francisco"
            };

            return Task.FromResult(new JsonResult(timezones));
        }
        private DateTime GetLocalTime(string timezoneName)
        {
            var dateTime = DateTime.UtcNow;
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneName);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timezone);
        }

        private DayOfWeek GetDay(DateTime localTime)
        {
            return localTime.DayOfWeek;
        }

        private bool CheckIsWeekday(DateTime localTime)
        {
            return localTime.DayOfWeek != DayOfWeek.Saturday && localTime.DayOfWeek != DayOfWeek.Sunday;
        }
        
        private bool CheckIsBusinessHours(DateTime localTime)
        {
            var hour = localTime.Hour;
            return hour > 8 && hour < 20;
        }
    }
}
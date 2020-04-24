using System;

namespace CloudCityCakesMVC.Models.DTO
{
    public struct OpeningHours
    {
        public bool IsWeekday { get; set; }
        public bool IsBusinessHours { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
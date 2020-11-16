using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JustInTime.Module.BusinessObjects
{
    public class TimeStampClock
    {
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("isAutoEnd")]
        public bool IsAutoEnd { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("isAutoStart")]
        public bool IsAutoStart { get; set; }
    }
}

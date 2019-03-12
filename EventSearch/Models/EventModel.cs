using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace EventSearch.Models
{
    [DataContract]
    public class EventModel
    {
        [DataMember(Name = "watching_count")]
        public object watching_count { get; set; }

        [DataMember(Name = "olson_path")]
        public string olson_path { get; set; }

        [DataMember(Name = "calendar_count")]
        public object calendar_count { get; set; }

        [DataMember(Name = "comment_count")]
        public object comment_count { get; set; }

        [DataMember(Name = "region_abbr")]
        public string region_abbr { get; set; }

        [DataMember(Name = "postal_code")]
        public string postal_code { get; set; }

        [DataMember(Name = "going_count")]
        public object going_count { get; set; }

        [DataMember(Name = "all_day")]
        public string all_day { get; set; }

        [DataMember(Name = "latitude")]
        public string latitude { get; set; }

        [DataMember(Name = "groups")]
        public object groups { get; set; }

        [DataMember(Name = "url")]
        public string url { get; set; }

        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "privacy")]
        public string privacy { get; set; }

        [DataMember(Name = "city_name")]
        public string city_name { get; set; }

        [DataMember(Name = "link_count")]
        public object link_count { get; set; }

        [DataMember(Name = "longitude")]
        public string longitude { get; set; }

        [DataMember(Name = "country_name")]
        public string country_name { get; set; }

        [DataMember(Name = "country_abbr")]
        public string country_abbr { get; set; }

        [DataMember(Name = "region_name")]
        public string region_name { get; set; }

        [DataMember(Name = "start_time")]
        public string start_time { get; set; }

        [DataMember(Name = "tz_id")]
        public object tz_id { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "modified")]
        public string modified { get; set; }

        [DataMember(Name = "venue_display")]
        public string venue_display { get; set; }

        [DataMember(Name = "tz_country")]
        public object tz_country { get; set; }

        [DataMember(Name = "performers")]
        public object performers { get; set; }

        [DataMember(Name = "title")]
        public string title { get; set; }

        [DataMember(Name = "venue_address")]
        public string venue_address { get; set; }

        [DataMember(Name = "geocode_type")]
        public string geocode_type { get; set; }

        [DataMember(Name = "tz_olson_path")]
        public object tz_olson_path { get; set; }

        [DataMember(Name = "recur_string")]
        public string recur_string { get; set; }

        [DataMember(Name = "calendars")]
        public object calendars { get; set; }

        [DataMember(Name = "owner")]
        public string owner { get; set; }

        [DataMember(Name = "going")]
        public object going { get; set; }

        [DataMember(Name = "country_abbr2")]
        public string country_abbr2 { get; set; }

        [DataMember(Name = "image")]
        public object image { get; set; }

        [DataMember(Name = "created")]
        public string created { get; set; }

        [DataMember(Name = "venue_id")]
        public string venue_id { get; set; }

        [DataMember(Name = "tz_city")]
        public object tz_city { get; set; }

        [DataMember(Name = "stop_time")]
        public string stop_time { get; set; }

        [DataMember(Name = "venue_name")]
        public string venue_name { get; set; }

        [DataMember(Name = "venue_url")]
        public string venue_url { get; set; }

    }
    //public class Events
    //{
    //    public IList<EventModel> eventModel { get; set; }
    //}

    //public class Eventful
    //{
    //    public object last_item { get; set; }
    //    public string total_items { get; set; }
    //    public object first_item { get; set; }
    //    public string page_number { get; set; }
    //    public string page_size { get; set; }
    //    public object page_items { get; set; }
    //    public string search_time { get; set; }
    //    public string page_count { get; set; }
    //    public Events events { get; set; }
    //}

}

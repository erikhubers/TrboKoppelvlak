﻿namespace TrboPortal.Model.Api
{
    
    public partial class Radio
    {
        [Newtonsoft.Json.JsonProperty("radioId", Required = Newtonsoft.Json.Required.Always)]
        public int RadioId { get; set; }

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("GpsMode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GpsModeEnum? GpsMode { get; set; }

        /// <summary>GPS request interval in milliseconds</summary>
        [Newtonsoft.Json.JsonProperty("requestInterval", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? RequestInterval { get; set; }


    }
}
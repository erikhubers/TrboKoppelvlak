﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TrboPortal.Mappers;
using TrboPortal.Model.Api;
using TrboPortal.TrboNet;
using GpsMeasurement = TrboPortal.Model.Api.GpsMeasurement;

namespace TrboPortal.Controllers
{
    [RoutePrefix("TrboPortal/v1")]
    public class TrboPortalController : ApiController
    {
        /// <summary>List of all radios</summary>
        /// <param name="id">Tags to filter by</param>
        /// <returns>successful operation</returns>
        [HttpGet, Route("radio")]
        public Task<ICollection<Model.Api.Radio>> GetRadios([FromUri] IEnumerable<int> id)
        {
            return Task.FromResult<ICollection<Model.Api.Radio>>(TurboController.Instance.GetSettings());
        }

        /// <summary>Update gps mode and interval radios</summary>
        /// <param name="radioSettings">Radios to update</param>
        [HttpPatch, Route("radio")]
        public Task UpdateRadioSettings([FromBody] [Required] IEnumerable<Model.Api.Radio> radioSettings)
        {
            return Task.Run(() => TrboPortalHelper.UpdateRadioSettings(radioSettings));
        }

        /// <summary>Returns last known GPS position of radio(s)</summary>
        /// <param name="ids">Radios</param>
        /// <returns>successful operation</returns>
        [HttpGet, Route("gps")]
        public Task<ICollection<GpsMeasurement>> GetMostRecentGps([FromUri] IEnumerable<int> ids)
        {
            return Task.FromResult(TrboPortalHelper.GetGpsMeasurements(ids, null, null));
        }

        /// <summary>Returns last known GPS position of radio(s)</summary>
        /// <param name="id">Radios</param>
        /// <param name="from">From TimeStamp for GPS measurements to get</param>
        /// <param name="through">Through TimeStamp for GPS measurements to get</param>
        /// <returns>successful operation</returns>
        [HttpGet, Route("gps/history")]
        public Task<ICollection<GpsMeasurement>> GetGpsHistory([FromUri] IEnumerable<int> id, [FromUri] string from, [FromUri] string through)
        {
            var f = DateTimeMapper.ToDateTime(from);
            var t = DateTimeMapper.ToDateTime(through);

            return Task.FromResult(TrboPortalHelper.GetGpsMeasurements(id, f, t));
        }

        /// <summary>Request GPS opdate for radio(s)</summary>
        /// <param name="id">Radios</param>
        /// <returns>successful operation</returns>
        [HttpGet, Route("gps/update")]
        public Task RequestGpsUpdate([FromUri] IEnumerable<int> id)
        {
            // TODO never null ?
            return Task.Run(() => TurboController.Instance.PollForGps(id.ToArray()));
        }

        /// <summary>TrboNet message queue</summary>
        /// <returns>Messages in TrboNet queue</returns>
        [HttpGet, Route("system/queue")]
        public Task<ICollection<MessageQueueItem>> GetMessageQueue()
        {
            return Task.FromResult<ICollection<MessageQueueItem>>(TurboController.Instance.GetRequestQueue()
                .Select(rqi => MessageQueueMapper.Map(rqi))
                .OrderBy(i => i.Timestamp)
                .ToList()
            );
        }

        /// <summary>System settings</summary>
        /// <returns>Blaat</returns>
        [HttpGet, Route("system/settings")]
        public Task<SystemSettings> GetSystemSettings()
        {
            return Task.FromResult(TrboPortalHelper.GetSystemSettings());
        }

        /// <summary>Update System settings</summary>
        /// <param name="settings">System settings to store</param>
        [HttpPatch, Route("system/settings")]
        public Task SetSystemSettings([FromBody] [Required] SystemSettings settings)
        {
            return Task.Run(() => TrboPortalHelper.UpdateSystemSettings(settings));
        }

        /// <summary>System logging</summary>
        /// <param name="loglevel">Log level filter of logging to return</param>
        /// <param name="from">DateTime From filter of logging to return</param>
        /// <param name="through">DateTime Through filter of logging to return</param>
        /// <returns>Blaat</returns>
        [HttpGet, Route("system/logs")]
        public Task<ICollection<string>> GetLogging([Required] string loglevel, [FromUri] string from, [FromUri] string through)
        {
            throw new NotImplementedException();
        }

    }
}
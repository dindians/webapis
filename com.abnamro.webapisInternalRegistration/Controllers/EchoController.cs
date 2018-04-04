﻿using com.abnamro.core;
using com.abnamro.webapi.core;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace com.abnamro.webapisInternalRegistration.Controllers
{
    public class EchoController : ApiController
    {
        /// <summary>
        /// http://localhost:8087/echo/hello-there
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        [Route(nameof(WebapiRoute.echo) + "/{echo}")]
        [HttpGet]
        public EchoResponse Echo(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(echo, $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.echoasync) + "/{echo}")]
        [HttpGet]
        public async Task<EchoResponse> EchoAsync(string echo)
        {
            this.ThrowIfModelStateNotValid();
            return await EchoResponseCreator.CreateEchoResponseAsync(echo, $"async-response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.echo))]
        [HttpPost]
        public EchoResponse Echo(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(jsonObject?.ToObject<EchoRequest>(), $"response-from {this.GetControllerInfo()}");
        }

        [Route(nameof(WebapiRoute.echoasync))]
        [HttpPost]
        public async Task<EchoResponse> EchoAsync(JObject jsonObject)
        {
            this.ThrowIfModelStateNotValid();
            return await EchoResponseCreator.CreateEchoResponseAsync(jsonObject?.ToObject<EchoRequest>(), $"response-from {this.GetControllerInfo()}");
        }

        /// <summary>
        /// http://localhost:8087/EchoPostedString
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        [Route(nameof(EchoPostedString))]
        [HttpPost]
        public EchoResponse EchoPostedString([FromBody]string echo)
        {
            this.ThrowIfModelStateNotValid();
            return EchoResponseCreator.CreateEchoResponse(echo, $"response-from {this.GetControllerInfo()}");
        }

        /// <summary>
        /// http://localhost:8087/throwecho
        /// </summary>
        /// <returns></returns>
        [Route(nameof(ThrowEcho))]
        [HttpGet]
        public string ThrowEcho()
        {
            this.ThrowIfModelStateNotValid();
            throw new Exception($"Echo from {nameof(EchoController)}.{nameof(ThrowEcho)}() @ {DateTime.Now}");
        }
    }
}

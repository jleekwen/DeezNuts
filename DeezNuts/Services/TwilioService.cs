using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DeezNuts.Services
{
    public class TwilioService : ITwilioService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DeezNutsConfig _config;

        public TwilioService(
            IOptions<DeezNutsConfig> config,
            IProductService productService,
            ISettingRepository settingRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _config = config.Value;
            _productService = productService;
            _settingRepository = settingRepository;
            _httpContextAccessor = httpContextAccessor;

            var sid = _config.TwilioSid;
            var auth = _config.TwilioAuth;

            TwilioClient.Init(sid, auth);
        }

        public void SendMessage(SendMessageDto dto, MessageBuilderContext mbContext)
        {
            var dict = BuildDictionary(mbContext);
            var message = dict.Aggregate(mbContext.SendText, (current, value) =>
                current.Replace(value.Key, value.Value));

            MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(dto.FromNumber),
                statusCallback: new Uri($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Headers["X-Original-Host"]}{_httpContextAccessor.HttpContext.Request.Path}/status"),
                to: new Twilio.Types.PhoneNumber(dto.ToNumber)
            );
        }

        private Dictionary<string, string> BuildDictionary(MessageBuilderContext context)
        {
            return new Dictionary<string, string>
            {
                { KeywordToken.BotName, _settingRepository.GetSettingByType(SettingType.BotName).Value ?? "" },
                { KeywordToken.CompanyName, _settingRepository.GetSettingByType(SettingType.CompanyName).Value ?? "" },
                { KeywordToken.CustomerName, !string.IsNullOrEmpty(context.Customer.Name) ? context.Customer.Name : "" },
                { KeywordToken.CustomerNameAllCaps, !string.IsNullOrEmpty(context.Customer.Name) ? context.Customer.Name.ToUpper() : "" },
                { KeywordToken.InputText, context.InputText ?? "" },
                { KeywordToken.ListeningActionMatches, context.ListeningActionMatches ?? "" },
                { KeywordToken.Products, _productService.BuildProductList() ?? "" }
            };
        }

        private class KeywordToken
        {
            public static string BotName { get { return "{BOTNAME}"; } }
            public static string CompanyName { get { return "{COMPANYNAME}"; } }
            public static string CustomerName { get { return "{CustName}"; } }
            public static string CustomerNameAllCaps { get { return "{CUSTNAME}"; } }
            public static string InputText { get { return "{INPUT}"; } }
            public static string ListeningActionMatches { get { return "{ACTIONMATCHES}"; } }
            public static string Products { get { return "{PRODUCTS}"; } }
        }
    }
}

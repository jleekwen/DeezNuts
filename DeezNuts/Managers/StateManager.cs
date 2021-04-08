using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Managers.States;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace DeezNuts.Managers
{
    public class StateManager : IState
    {
        private readonly IOptions<DeezNutsConfig> _config;
        private readonly IListeningActionService _listeningActionService;
        private readonly IMessageService _messageService;
        private readonly ITwilioService _twilioService;

        private Dictionary<SessionState, IState> _stateDictionary;

        public StateManager(
            IOptions<DeezNutsConfig> config,
            IListeningActionService listeningActionService,
            IMessageService messageService,
            ITwilioService twilioService)
        {
            _config = config;
            _listeningActionService = listeningActionService;
            _messageService = messageService;
            _twilioService = twilioService;

            BuildDictionary();
        }

        public Customer DoAction(StateActionDto dto)
        {
            return _stateDictionary[dto.Customer.Session.AwaitState].DoAction(dto);
        }

        private void BuildDictionary()
        {
            _stateDictionary = new Dictionary<SessionState, IState>()
            {
                { SessionState.AwaitName, new AwaitNameState(_config, _twilioService, _messageService ) },
                { SessionState.GreetingNew, new GreetingNewState(_twilioService, _messageService) },
                { SessionState.GreetingReturning, new GreetingReturningState(_twilioService, _messageService) },
                { SessionState.Listening, new ListeningState(_twilioService, _listeningActionService, _messageService ) }
            };
        }
    }
}

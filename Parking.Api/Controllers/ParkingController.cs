using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Commands;
using Parking.Api.Commands.Handlers;
using Parking.Api.Queries;
using Parking.Api.Queries.Handlers;
using Parking.Api.Requests;
using Parking.Api.Responses;
using Parking.Api.Services;

namespace Parking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly DbContext _dbContext;
        private readonly AuthenticationService _authenticationService;
        private readonly CommandStoreService _commandStoreService;
        private readonly ParkingQueryHandler _queryHandler;

        public ParkingController(DbContext dbContext, AuthenticationService authenticationService, CommandStoreService commandStoreService, ParkingQueryHandler queryHandler)
        {
            _dbContext = dbContext;
            _authenticationService = authenticationService;
            _commandStoreService = commandStoreService;
            _queryHandler = queryHandler;
        }

        [HttpGet("availablePlaces/count")]
        public int GetTotalAvailablePlaces()
        {
            var query = new GetTotalAvailablePlacesQuery();

            return _queryHandler.Handle(query);
        }

        [HttpGet("availablePlaces/random")]
        public ParkingPlaceInfo GetRandomAvailablePlace()
        {
            var query = new GetRandomAvailablePlace();

            return _queryHandler.Handle(query);
        }

        [HttpGet]
        public IEnumerable<ParkingInfo> GetAllParkingInfos()
        {
            var query = new GetAllParkingInfoQuery();

            return _queryHandler.Handle(query);
        }

        [HttpGet("{parkingName}")]
        public ParkingInfo GetParkingInfo(string parkingName)
        {
            var query = new GetParkingInfoQuery(parkingName);

            return _queryHandler.Handle(query);
        }

        [HttpPost]
        public void CreateParking([FromBody] CreateParkingRequest request)
        {
            var command = new CreateParkingCommand(request.ParkingName, request.Capacity);

            var commandHandler = new CreateParkingCommandHandler(_dbContext, _commandStoreService);
            commandHandler.Handle(command);
        }

        [HttpPost("{parkingName}/open")]
        public void OpenParking(string parkingName)
        {
            var command = new OpenParkingCommand(parkingName);

            var commandHandler = new OpenParkingCommandHandler( _dbContext,_commandStoreService);
            commandHandler.Handle(command);
        }

        [HttpPost("{parkingName}/close")]
        public void CloseParking(string parkingName)
        {
            var command = new CloseParkingCommand(parkingName);
            
            var commandHandler = new CloseParkingCommandHandler(_dbContext, _commandStoreService);
            commandHandler.Handle(command);
        }

        [HttpPost("{parkingName}/{placeNumber}/take")]
        public void TakeParkingPlace(string parkingName, int placeNumber)
        {
            var command = new TakeParkingPlaceCommand(parkingName, placeNumber);

            var commandHandler = new TakeParkingPlaceCommandHandler(_dbContext, _authenticationService, _commandStoreService);
            commandHandler.Handle(command);
        }

        [HttpPost("{parkingName}/{placeNumber}/leave")]
        public void LeaveParkingPlace(string parkingName, int placeNumber)
        {
            var command = new LeaveParkingPlaceCommand(parkingName, placeNumber);
            
            var commandHandler = new LeaveParkingPlaceCommandHandler(_dbContext, _commandStoreService);
            commandHandler.Handle(command);
        }
    }
}

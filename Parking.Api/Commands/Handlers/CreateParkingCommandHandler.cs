using System.Linq;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Models;
using Parking.Api.Services;

namespace Parking.Api.Commands.Handlers
{
    public class CreateParkingCommandHandler : ICommandHandler<CreateParkingCommand>
    {
        private readonly DbContext _dbContext;
        private readonly CommandStoreService _commandStoreService;

        public CreateParkingCommandHandler(DbContext dbContext, CommandStoreService commandStoreService)
        {
            _dbContext = dbContext;
            _commandStoreService = commandStoreService;
        }

        public void Handle(CreateParkingCommand command)
        {
            var places = Enumerable.Range(1, command.Capacity)
                .Select(number => new ParkingPlace
                {
                    ParkingName = command.ParkingName,
                    Number = number,
                    IsFree = true
                }).ToList();

            var parking = new Models.Parking
            {
                Name = command.ParkingName,
                IsOpened = true,
                Places = places
            };

            _dbContext.Add(parking);
            _dbContext.SaveChanges();

            // Taking command handling all the way will remove the dependency to the command store service
            _commandStoreService.Push(command);
        }
    }
}
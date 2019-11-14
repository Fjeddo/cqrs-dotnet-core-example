using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Models;
using Parking.Api.Services;

namespace Parking.Api.Commands.Handlers
{
    public class LeaveParkingPlaceCommandHandler : ICommandHandler<LeaveParkingPlaceCommand>
    {
        private readonly DbContext _dbContext;
        private readonly CommandStoreService _commandStoreService;

        public LeaveParkingPlaceCommandHandler(DbContext dbContext, CommandStoreService commandStoreService)
        {
            _dbContext = dbContext;
            _commandStoreService = commandStoreService;
        }

        public void Handle(LeaveParkingPlaceCommand command)
        {
            var parking = _dbContext.Set<Models.Parking>().FirstOrDefault(p => p.Name == command.ParkingName);

            if (parking == null)
            {
                throw new Exception($"Cannot find parking '{command.ParkingName}'.");
            }

            if (!parking.IsOpened)
            {
                throw new Exception($"The parking '{command.ParkingName}' is closed.");
            }

            var parkingPlace = _dbContext.Set<ParkingPlace>().FirstOrDefault(p => p.ParkingName == command.ParkingName && p.Number == command.PlaceNumber);

            if (parkingPlace == null)
            {
                throw new Exception($"Cannot find place #{command.PlaceNumber} in the parking '{command.ParkingName}'.");
            }

            if (parkingPlace.IsFree)
            {
                throw new Exception($"Parking place #{command.PlaceNumber} is still free.");
            }

            parkingPlace.IsFree = true;
            parkingPlace.UserId = null;

            _dbContext.SaveChanges();
            _commandStoreService.Push(command);
        }
    }
}
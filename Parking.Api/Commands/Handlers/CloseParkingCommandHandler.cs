﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Services;

namespace Parking.Api.Commands.Handlers
{
    public class CloseParkingCommandHandler: ICommandHandler<CloseParkingCommand>
    {
        private readonly DbContext _dbContext;
        private readonly CommandStoreService _commandStoreService;

        public CloseParkingCommandHandler(DbContext dbContext, CommandStoreService commandStoreService)
        {
            _dbContext = dbContext;
            _commandStoreService = commandStoreService;
        }

        public void Handle(CloseParkingCommand command)
        {
            var parking = _dbContext.Set<Models.Parking>().FirstOrDefault(p => p.Name == command.ParkingName);

            if (parking == null)
            {
                throw new Exception($"Cannot find parking '{command.ParkingName}'.");
            }

            if (!parking.IsOpened)
            {
                throw new Exception($"Parking '{command.ParkingName}' is already closed.");
            }

            parking.IsOpened = false;

            _dbContext.SaveChanges();
            _commandStoreService.Push(command);
        }
    }
}
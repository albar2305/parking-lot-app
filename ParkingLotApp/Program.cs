using System;
using ParkingLotApp.Enums;
using ParkingLotApp.Repository;
using ParkingLotApp.Service;

namespace ParkingLotApp
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      IParkingSlotRepository repository = new InmemoryParkingSlotRepository(6);
      var parkingLotService = new ParkingLotService(repository);

      while (true)
      {
        var command = Console.ReadLine();
        if (command != null)
        {
          var parts = command.Split(' ');

          if (parts[0] == "create_parking_lot")
          {
            var totalSlots = int.Parse(parts[1]);
            var createdSlots = parkingLotService.CreateParkingLot(totalSlots);
            Console.WriteLine($"Created a parking lot with {createdSlots} slots");
          }
          else if (parts[0] == "park")
          {
            var regNumber = parts[1];
            var color = parts[2];
            var vehicleType = Enum.Parse<VehicleType>(parts[3], true);

            var allocatedSlotNumber = parkingLotService.ParkVehicle(regNumber, vehicleType, color);
            if (allocatedSlotNumber != -1)
              Console.WriteLine($"Allocated slot number: {allocatedSlotNumber}");
            else
              Console.WriteLine("Sorry, parking lot is full.");
          }
          else if (parts[0] == "leave")
          {
            var slotToVacate = int.Parse(parts[1]);
            var success = parkingLotService.LeaveParkingLot(slotToVacate);
            if (success)
              Console.WriteLine($"Slot number {slotToVacate} is free");
            else
              Console.WriteLine($"Slot number {slotToVacate} is already vacant");
          }
          else if (parts[0] == "status")
          {
            PrintParkingLotStatus(parkingLotService);
          }
          else if (parts[0] == "type_of_vehicles")
          {
            var vehicleType = Enum.Parse<VehicleType>(parts[1], true);
            var count = parkingLotService.GetVehicleCountByType(vehicleType);
            Console.WriteLine(count);
          }
          else if (parts[0] == "registration_numbers_for_vehicles_with_ood_plate")
          {
            var oddPlateNumbers = parkingLotService.GetRegistrationNumbersWithOddPlate();
            Console.WriteLine(string.Join(", ", oddPlateNumbers));
          }
          else if (parts[0] == "registration_numbers_for_vehicles_with_event_plate")
          {
            var evenPlateNumbers = parkingLotService.GetRegistrationNumbersWithEvenPlate();
            Console.WriteLine(string.Join(", ", evenPlateNumbers));
          }
          else if (parts[0] == "registration_numbers_for_vehicles_with_colour")
          {
            var color = parts[1];
            var registrationNumbers = parkingLotService.GetRegistrationNumbersByColor(color);
            Console.WriteLine(string.Join(", ", registrationNumbers));
          }
          else if (parts[0] == "slot_numbers_for_vehicles_with_colour")
          {
            var color = parts[1];
            var slotNumbers = parkingLotService.GetSlotNumbersByColor(color);
            Console.WriteLine(string.Join(", ", slotNumbers));
          }
          else if (parts[0] == "slot_number_for_registration_number")
          {
            var regNumber = parts[1];
            var slotNumber = parkingLotService.GetSlotNumberByRegistrationNumber(regNumber);
            if (slotNumber != -1)
              Console.WriteLine(slotNumber);
            else
              Console.WriteLine("Not found");
          }
          else if (parts[0] == "exit")
          {
            return;
          }
          else
          {
            Console.WriteLine("Invalid command.");
          }
        }
      }
    }

    private static void PrintParkingLotStatus(ParkingLotService parkingLotService)
    {
      var slots = parkingLotService.GetAllSlots();

      Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColour");
      for (var index = 0; index < slots.Count; index++)
      {
        var slot = slots[index];
        if (slot.IsOccupied)
        {
          var vehicle = slot.ParkedVehicle;
          Console.WriteLine($"{index + 1}\t{vehicle.RegistrationNumber}\t{vehicle.Type}\t{vehicle.Color}");
        }
      }
    }
  }
}

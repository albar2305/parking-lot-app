using System;
using System.Collections.Generic;
using System.Linq;
using ParkingLotApp.Enums;
using ParkingLotApp.Model;
using ParkingLotApp.Repository;

namespace ParkingLotApp.Service
{
  public class ParkingLotService
  {
    private IParkingSlotRepository parkingSlotRepository;

    public ParkingLotService(IParkingSlotRepository repository)
    {
      parkingSlotRepository = repository;
    }

    public int CreateParkingLot(int totalSlots)
    {
      if (totalSlots <= 0) throw new ArgumentException("Total slots must be greater than 0.");

      parkingSlotRepository = new InmemoryParkingSlotRepository(totalSlots);
      return totalSlots;
    }

    public int ParkVehicle(string registrationNumber, VehicleType type, string color)
    {
      if (string.IsNullOrEmpty(registrationNumber) || string.IsNullOrEmpty(color))
        throw new ArgumentException("Registration number and color cannot be empty.");

      var vehicle = new Vehicle(registrationNumber, color, type);
      var slots = parkingSlotRepository.GetAllSlots();

      foreach (var (slot, index) in slots.Select((slot, index) => (slot, index)))
        if (!slot.IsOccupied)
        {
          slot.Park(vehicle);
          return index + 1;
        }

      return -1;
    }

    public bool LeaveParkingLot(int slotNumber)
    {
      if (slotNumber <= 0 || slotNumber > parkingSlotRepository.GetAllSlots().Count) return false;

      var slot = parkingSlotRepository.GetAllSlots()[slotNumber - 1];
      if (slot.IsOccupied)
      {
        slot.Vacate();
        return true;
      }

      return false;
    }

    public List<ParkingSlot> GetAllSlots()
    {
      return parkingSlotRepository.GetAllSlots();
    }

    public int GetVehicleCountByType(VehicleType type)
    {
      var slots = parkingSlotRepository.GetAllSlots();
      return slots.Count(slot => slot.IsOccupied && slot.ParkedVehicle.Type == type);
    }

    public List<string> GetRegistrationNumbersWithOddPlate()
    {
      var slots = parkingSlotRepository.GetAllSlots();
      var oddPlateNumbers = new List<string>();

      foreach (var slot in slots)
        if (slot.IsOccupied && IsOddPlate(slot.ParkedVehicle.RegistrationNumber))
          oddPlateNumbers.Add(slot.ParkedVehicle.RegistrationNumber);

      return oddPlateNumbers;
    }

    public List<string> GetRegistrationNumbersWithEvenPlate()
    {
      var slots = parkingSlotRepository.GetAllSlots();
      var evenPlateNumbers = new List<string>();

      foreach (var slot in slots)
        if (slot.IsOccupied && IsEvenPlate(slot.ParkedVehicle.RegistrationNumber))
          evenPlateNumbers.Add(slot.ParkedVehicle.RegistrationNumber);

      return evenPlateNumbers;
    }

    public List<string> GetRegistrationNumbersByColor(string color)
    {
      var slots = parkingSlotRepository.GetAllSlots();
      return slots
        .Where(slot => slot.IsOccupied && slot.ParkedVehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
        .Select(slot => slot.ParkedVehicle.RegistrationNumber)
        .ToList();
    }

    public List<int> GetSlotNumbersByColor(string color)
    {
      var slots = parkingSlotRepository.GetAllSlots();
      var slotNumbers = new List<int>();

      for (var index = 0; index < slots.Count; index++)
      {
        var slot = slots[index];
        if (slot.IsOccupied && slot.ParkedVehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
          slotNumbers.Add(index + 1);
      }

      return slotNumbers;
    }

    public int GetSlotNumberByRegistrationNumber(string registrationNumber)
    {
      var slots = parkingSlotRepository.GetAllSlots();
      var occupiedSlot = slots.FirstOrDefault(slot =>
        slot.IsOccupied &&
        slot.ParkedVehicle.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));

      return occupiedSlot != null ? slots.IndexOf(occupiedSlot) + 1 : -1;
    }

    private bool IsOddPlate(string registrationNumber)
    {
      var numberPart = registrationNumber.Split('-')[1];
      var lastDigit = numberPart[numberPart.Length - 1];

      var digitValue = int.Parse(lastDigit.ToString());
      return digitValue % 2 != 0;
    }

    private bool IsEvenPlate(string registrationNumber)
    {
      var numberPart = registrationNumber.Split('-')[1];

      var lastDigit = numberPart[numberPart.Length - 1];

      var digitValue = int.Parse(lastDigit.ToString());
      return digitValue % 2 == 0;
    }
  }
}

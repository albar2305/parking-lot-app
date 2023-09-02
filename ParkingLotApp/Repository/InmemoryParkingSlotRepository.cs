using System.Collections.Generic;
using ParkingLotApp.Model;

namespace ParkingLotApp.Repository
{
  public class InmemoryParkingSlotRepository : IParkingSlotRepository
  {
    private List<ParkingSlot> parkingSlots;
    public InmemoryParkingSlotRepository(int totalSlots)
    {
      parkingSlots = new List<ParkingSlot>();
      for (int i = 0; i < totalSlots; i++)
      {
        parkingSlots.Add(new ParkingSlot());
      }
    }
    public bool IsSlotOccupied(int slotNumber)
    {
      return parkingSlots[slotNumber - 1].IsOccupied;
    }

    public void ParkVehicle(int slotNumber, Vehicle vehicle)
    {
      parkingSlots[slotNumber - 1].Park(vehicle);
    }

    public void VacateSlot(int slotNumber)
    {
      parkingSlots[slotNumber - 1].Vacate();
    }

    public List<ParkingSlot> GetAllSlots()
    {
      return parkingSlots;
    }
  }
}

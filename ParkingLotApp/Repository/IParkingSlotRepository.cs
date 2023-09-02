using System.Collections.Generic;
using ParkingLotApp.Model;

namespace ParkingLotApp.Repository
{
  public interface IParkingSlotRepository
  {
    bool IsSlotOccupied(int slotNumber);
    void ParkVehicle(int slotNumber, Vehicle vehicle);
    void VacateSlot(int slotNumber);
    List<ParkingSlot> GetAllSlots();
  }
}

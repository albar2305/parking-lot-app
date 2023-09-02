namespace ParkingLotApp.Model
{
  public class ParkingSlot
  {
    public Vehicle ParkedVehicle { get; private set; }
    public bool IsOccupied => ParkedVehicle != null;
    public void Park(Vehicle vehicle)
    {
      ParkedVehicle = vehicle;
    }
    public void Vacate()
    {
      ParkedVehicle = null;
    }
  }
}

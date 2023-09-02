using ParkingLotApp.Enums;

namespace ParkingLotApp.Model
{
  public class Vehicle
  {
    public Vehicle(string registrationNumber, string color, VehicleType type)
    {
      RegistrationNumber = registrationNumber;
      Color = color;
      Type = type;
    }

    public string RegistrationNumber { get; }
    public string Color { get; }
    public VehicleType Type { get; }
  }
}

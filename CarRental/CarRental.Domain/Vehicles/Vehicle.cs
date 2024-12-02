 namespace CarRental.CarRental.Domain.Vehicles
{
    public class Vehicle
    {
        public Guid Id { get; set; }   
        public string Name { get; set; }   
        public string Plate { get; set; }   
        public double ActiveWorkTime { get; set; }  // Aktif çalışma süresi (saat)
        public double MaintenanceTime { get; set; }  // Bakım süresi (saat)
        public double IdleTime => TotalWorkTime - (ActiveWorkTime + MaintenanceTime); // Boşta bekleme süresi (saat) 
        public double TotalWorkTime => 7 * 24;  // Toplam çalışma süresi: 7 gün x 24 saat
         
        // Aktif çalışma süresi oranı (Yüzde)
        public double ActiveWorkTimePercentage => (ActiveWorkTime / TotalWorkTime) * 100;

        // Boşta bekleme süresi oranı (Yüzde)
        public double IdleTimePercentage => (IdleTime / TotalWorkTime) * 100;
    }
}

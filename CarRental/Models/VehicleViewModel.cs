namespace CarRental.Models
{
    public class VehicleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Plate { get; set; }
        public double ActiveWorkTime { get; set; }   
        public double MaintenanceTime { get; set; }  
        public double IdleTime => TotalWorkTime - (ActiveWorkTime + MaintenanceTime);  
        public double TotalWorkTime => 7 * 24;   
        public double ActiveWorkTimePercentage => (ActiveWorkTime / TotalWorkTime) * 100; 
        public double IdleTimePercentage => (IdleTime / TotalWorkTime) * 100;
    }
}

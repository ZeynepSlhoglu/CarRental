namespace CarRental.Models
{
    public class GraphDataViewModel
    {
        public List<VehicleViewModel> VehicleViewModels { get; set; }
        public List<double> ActiveWorkTimes { get; set; }
        public List<double> IdleTimes { get; set; }
        public List<string> Labels { get; set; }

    }
}

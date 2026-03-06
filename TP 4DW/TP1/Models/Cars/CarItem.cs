using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Cars
{
    public class CarItem
    {
        public Guid Id { get; set; }
        public required string CarBrand { get; set; }
        public required string CarModel { get; set; }
        public required int? Year { get; set; }
        public required string Nickname { get; set; }
        public required string SerialNumber { get; set; }
        public required int? Mileage { get; set; }
        public required string Color { get; set; }
        public required string Registration { get; set; }
        public required decimal? EstimatedValue { get; set; }


        public required bool Status { get; set; }

        public required bool Availability { get; set; }

        public required bool State { get; set; }

        //public required int BranchId { get; set; }
        //public required virtual Branch Branch { get; set; }

    }
}

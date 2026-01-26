using System;

namespace FarmManagement.Web.Models
{
    public class SoldByDateDto
    {
        public DateTime Date { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalWeight { get; set; }
        public int MaleQuantity { get; set; }
        public int FemaleQuantity { get; set; }
        public int MixedQuantity { get; set; }
    }

    public class SoldByGenderDto
    {
        public int Male { get; set; }
        public int Female { get; set; }
        public int Mixed { get; set; }
    }
}

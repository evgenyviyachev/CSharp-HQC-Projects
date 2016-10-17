namespace CS_OOP_Advanced_Exam_Prep_July_2016.Models.Shops
{
    using CS_OOP_Advanced_Exam_Prep_July_2016.Contracts;

    public class Bazaar : Shop
    {
        private const int CapacityOfBazaar = 30;

        public Bazaar(IShop successor)
            : base(CapacityOfBazaar, successor)
        {
        }
    }
}

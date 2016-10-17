namespace CS_OOP_Advanced_Exam_Prep_July_2016.Models.Shops
{
    using CS_OOP_Advanced_Exam_Prep_July_2016.Contracts;

    public class Store : Shop
    {
        private const int CapacityOfStore = 15;

        public Store(IShop successor)
            : base(CapacityOfStore, successor)
        {
        }
    }
}

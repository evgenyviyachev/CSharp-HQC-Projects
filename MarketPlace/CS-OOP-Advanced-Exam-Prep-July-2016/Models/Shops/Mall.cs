namespace CS_OOP_Advanced_Exam_Prep_July_2016.Models.Shops
{
    using CS_OOP_Advanced_Exam_Prep_July_2016.Contracts;

    public class Mall : Shop
    {
        private const int CapacityOfMall = int.MaxValue;

        public Mall(IShop successor)
            : base(CapacityOfMall, successor)
        {
        }
    }
}

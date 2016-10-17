namespace CS_OOP_Advanced_Exam_Prep_July_2016.Contracts
{
    using System.Collections.Generic;

    public interface IShop
    {
        int Capacity { get; set; }
        IShop Successor { get; }
        ICollection<IProduct> Products { get; }
        string AddProduct(IProduct product);
        bool IsEmpty();
    }
}

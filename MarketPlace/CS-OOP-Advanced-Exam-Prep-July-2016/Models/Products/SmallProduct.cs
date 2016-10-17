namespace CS_OOP_Advanced_Exam_Prep_July_2016.Models.Products
{
    public class SmallProduct : Product
    {
        public SmallProduct(string name, int size)
            : base(name, size)
        {
        }
        
        public override int Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value / 2;
            }
        }
    }
}

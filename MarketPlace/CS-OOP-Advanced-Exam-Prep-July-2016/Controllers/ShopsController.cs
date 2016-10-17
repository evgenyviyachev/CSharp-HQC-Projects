namespace CS_OOP_Advanced_Exam_Prep_July_2016.Controllers
{
    using Contracts;
    using CS_OOP_Advanced_Exam_Prep_July_2016.Lifecycle.Controller;
    using Lifecycle;
    using Lifecycle.Request;

    [Controller]
    public class ShopsController
    {
        private IDatabase db;

        [RequestMapping("/shop/{type}/{productId}", RequestMethod.ADD)]
        public string AddProductToShop()
        {
            //to implement
            return null;
        }
    }
}

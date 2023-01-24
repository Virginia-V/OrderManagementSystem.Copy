namespace OMS.Common
{
    public static class ErrorMessages
    {
        public const string GenericError = "Something Went Wrong, pleasy try later!";
        public const string ProductAlreadyExists = "Product with the entered SKU number already exists!";
        public const string CustomerAlreadyExists = "Customer with the entered name already exists!";
        public const string NoCategories = "There aren't any product categories!";
        public const string NoCustomerTypes = "There aren't any customer types!";
        public const string NoDiscounts = "There aren't any discounts!";
        public const string NoOrderTypes = "There aren't any order types!";
        public const string NoPaymentStatuses = "There aren't any payment statuses!";
        public const string NoPaymentTerms = "There aren't any payment terms!";
        public const string OrderAlreadyExists = "Order with the entered Purchase Order number already exists!";
        public const string InvoiceAlreadyExists = "Invoice with the entered number already exists!";
        public const string OrderDoesNotExist = "Order with the entered Purchase Order number doesn't exist!";
        public const string SameProductError = "Please don't enter the same product more than once!";
        public const string NoProductForUpdate = "Product you are trying to update doesn't exist!";
        public const string NoProductForDelete = "Product you are trying to delete doesn't exist!";
        public const string NoCustomerForUpdate = "Customer you are trying to update doesn't exist!";
        public const string NoCustomerForDelete = "Customer you are trying to delete doesn't exist!";
        public const string NoInvoiceForDelete = "Invoice you are trying to delete doesn't exist!";
        public const string NoOrderForDelete = "Order you are trying to delete doesn't exist!";
    }
}

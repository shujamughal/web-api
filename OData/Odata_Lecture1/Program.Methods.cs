using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Odata_Lecture1.Models;


partial class Program
    {
        static IEdmModel GetEdmModelForCatagories()
        {
            ODataConventionModelBuilder builder = new();
            builder.EntitySet<Category>("Categories");
            return builder.GetEdmModel();
        }

    static IEdmModel GetEdmModelForProducts()
    {
        ODataConventionModelBuilder builder = new();
        builder.EntitySet<Product>("Products");
        return builder.GetEdmModel();
    }

    static IEdmModel GetEdmModelForCustomer()
    {
        ODataConventionModelBuilder builder = new();
        builder.EntitySet<Customer>("Customers");
        return builder.GetEdmModel();
    }

}


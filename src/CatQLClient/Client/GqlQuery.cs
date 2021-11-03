using System.ComponentModel.DataAnnotations;

namespace CatQL.GraphQL.Client
{
    public class GqlQuery
    {
        [Required]
        public string Query { get; set; }
        public string OperationName { get; set; }
        
        
        public string Variables { get; set; }
    }
}
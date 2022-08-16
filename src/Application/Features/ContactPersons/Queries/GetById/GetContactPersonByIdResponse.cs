namespace DynaCore.Application.Features.ContactPersons.Queries.GetById
{
    public class GetContactPersonByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string Description { get; set; }
    }
}
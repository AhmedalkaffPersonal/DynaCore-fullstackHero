namespace DynaCore.Application.Features.ContactPersons.Queries.GetAll
{
    public class GetAllContactPersonsResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
namespace CQRS_ByMe.Domain.Mementos
{
    public class CustomerMemento : BaseMemento
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
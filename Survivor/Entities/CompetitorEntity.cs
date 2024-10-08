namespace Survivor.Entities
{
    public class CompetitorEntity:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int CategoryId { get; set; }

        //Relation Property 

        public CategoryEntity Category { get; set; }

    }
}

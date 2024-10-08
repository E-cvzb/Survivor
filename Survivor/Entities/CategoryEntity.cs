namespace Survivor.Entities
{
    public class CategoryEntity:BaseEntity
    {
        public string Name { get; set; }

        //Relation Property

        public List<CompetitorEntity> Competitors { get; set; }

    }
}

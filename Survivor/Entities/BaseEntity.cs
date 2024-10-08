namespace Survivor.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool  IsDelete { get; set; }
    }
}// bütün entity de bulunacak property tanımlıyoruz ve miras veriyoruz

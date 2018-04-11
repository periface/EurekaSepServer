namespace Eureka.Spe.Tags.Inputs
{
    public class AddTagToElementInput
    {
        public AddTagToElementInput(int tagId,string entityName,int entityId)
        {
            TagId = tagId;
            EntityName = entityName;
            EntityId = entityId;
        }
        public int TagId { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
}
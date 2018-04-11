namespace Eureka.Spe.Tags.Inputs
{
    public class AddTagsToElementByCollectionInput
    {
        protected AddTagsToElementByCollectionInput() { }
        public AddTagsToElementByCollectionInput(int collectionId, string entityName, int entityId)
        {
            CollectionId = collectionId;
            EntityName = entityName;
            EntityId = entityId;
        }
        public int CollectionId { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
}
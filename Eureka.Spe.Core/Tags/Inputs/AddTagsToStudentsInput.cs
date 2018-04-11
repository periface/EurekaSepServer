namespace Eureka.Spe.Tags.Inputs
{
    public class AddTagsToStudentsInput
    {
        protected AddTagsToStudentsInput()
        {

        }
        public AddTagsToStudentsInput(string entityName, int entityId, int behaviorValue, int studentId)
        {
            EntityName = entityName;
            EntityId = entityId;
            BehaviorValue = behaviorValue;
            StudentId = studentId;
        }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public int BehaviorValue { get; set; }
        public int StudentId { get; set; }
    }
}
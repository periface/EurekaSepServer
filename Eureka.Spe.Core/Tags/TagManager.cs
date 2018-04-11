using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eureka.Spe.Tags.Inputs;

namespace Eureka.Spe.Tags
{
    public class TagManager : ITagManager
    {
        public Task AddTagToCollectionAsync(string tagName, int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task AddTagsToCollectionAsync(List<string> tagNames, int collectionId)
        {
            throw new NotImplementedException();
        }

        public Task AddTagsToStudentAsync(AddTagsToStudentsInput input)
        {
            throw new NotImplementedException();
        }

        public Task AddTagCollectionToElementAsync(AddTagsToElementByCollectionInput input)
        {
            throw new NotImplementedException();
        }

        public Task AddTagToElementAsync(AddTagToElementInput input)
        {
            throw new NotImplementedException();
        }

        public Task AddTagsToElementAsync(List<AddTagToElementInput> input)
        {
            throw new NotImplementedException();
        }
    }
}

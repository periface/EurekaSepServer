using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Eureka.Spe.Tags.Inputs;

namespace Eureka.Spe.Tags
{
    public interface ITagManager : IDomainService
    {
        /// <summary>
        /// Agrega un tag a una colección
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        Task AddTagToCollectionAsync(string tagName, int collectionId);

        /// <summary>
        /// Agregar múltiples tags a una colección
        /// </summary>
        /// <param name="tagNames"></param>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        Task AddTagsToCollectionAsync(List<string> tagNames, int collectionId);
        /// <summary>
        /// Agrega un conjunto de tags al estudiante basándose en la calificación que le dio 
        /// al elemento fuente
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddTagsToStudentAsync(AddTagsToStudentsInput input);
        /// <summary>
        /// Agrega un conjunto de tags (por colección) a un elemento
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddTagCollectionToElementAsync(AddTagsToElementByCollectionInput input);
        /// <summary>
        /// Agrega un tag a un elemento
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddTagToElementAsync(AddTagToElementInput input);
        /// <summary>
        /// Agrega multiples tags a un elemento
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddTagsToElementAsync(List<AddTagToElementInput> input);
    }
}

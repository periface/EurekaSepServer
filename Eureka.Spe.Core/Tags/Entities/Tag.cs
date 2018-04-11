using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Eureka.Spe.Tags.Entities
{
    /// <summary>
    /// Solo para organizar las etiquetas de forma mas sencilla
    /// </summary>
    public class TagCollection : FullAuditedEntity
    {
        public TagCollection()
        {
            Tags = new HashSet<Tag>();
        }
        public string CollectionName { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
    /// <summary>
    /// El elemento etiqueta básico
    /// </summary>
    public class Tag : FullAuditedEntity
    {
        public string Name { get; set; }
        
        public Tag()
        {
            TagCollections = new HashSet<TagCollection>();
        }

        public ICollection<TagCollection> TagCollections { get; set; }
    }
    /// <summary>
    /// Sirve de relación entre las etiquetas y el elemento del sistema
    /// No se hizo una relación con Colecciones de entityf para simplificar el proceso
    /// ademas de hacer esto de una forma mas minimalista y genérica
    /// </summary>
    public class ElementTags : FullAuditedEntity
    {
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        public int StudentId { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
    /// <summary>
    /// Sirve de relación entre el estudiante y las etiquetas
    /// Tiene el valor de comportamiento, negativo o positivo
    /// El elemento del que se obtuvo la etiqueta
    /// La relación con la etiqueta
    /// </summary>
    public class StudentTags : FullAuditedEntity
    {
        public int TagId { get; set; }
        public bool PositiveBehavior { get; set; }
        public int BehaviorValue { get; set; }

        public int SourceElementId { get; set; }
        public string SourceElementName { get; set; }
    }
    
}

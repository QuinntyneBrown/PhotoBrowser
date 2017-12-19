using System;
using System.Collections.Generic;
using PhotoBrowser.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static PhotoBrowser.Constants;

namespace PhotoBrowser.Model
{
    [SoftDelete("IsDeleted")]
    public class Gallery: ILoggable
    {
        public int Id { get; set; }        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        public ICollection<GalleryDigitalAsset> GalleryDigitalAssets { get; set; } = new HashSet<GalleryDigitalAsset>();
        [Index("GalleryNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]     
        [StringLength(MaxStringLength)]		   
		public string Name { get; set; }        
		public DateTime CreatedOn { get; set; }        
		public DateTime LastModifiedOn { get; set; }        
		public string CreatedBy { get; set; }        
		public string LastModifiedBy { get; set; }        
		public bool IsDeleted { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}

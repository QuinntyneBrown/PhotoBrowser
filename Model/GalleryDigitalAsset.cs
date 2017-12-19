using System;
using PhotoBrowser.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoBrowser.Model
{
    [SoftDelete("IsDeleted")]
    public class GalleryDigitalAsset: ILoggable
    {
        public int Id { get; set; }
        [ForeignKey("Gallery")]
        public int? GalleryId { get; set; }
        [ForeignKey("DigitalAsset")]
        public int? DigitalAssetId { get; set; }
        public DateTime CreatedOn { get; set; }        
		public DateTime LastModifiedOn { get; set; }        
		public string CreatedBy { get; set; } 
		public string LastModifiedBy { get; set; } 
		public bool IsDeleted { get; set; }
        public DigitalAsset DigitalAsset { get; set; }
        public Gallery Gallery { get; set; }
    }
}

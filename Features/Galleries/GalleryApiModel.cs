using PhotoBrowser.Model;

namespace PhotoBrowser.Features.Galleries
{
    public class GalleryApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromGallery<TModel>(Gallery gallery) where
            TModel : GalleryApiModel, new()
        {
            var model = new TModel();
            model.Id = gallery.Id;
            model.TenantId = gallery.TenantId;
            model.Name = gallery.Name;
            return model;
        }

        public static GalleryApiModel FromGallery(Gallery gallery)
            => FromGallery<GalleryApiModel>(gallery);

    }
}

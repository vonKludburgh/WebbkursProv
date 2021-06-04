using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbkursProv.Models
{
    public interface IMaxGateway
    {
        // Pages
        Task<List<CreatedPage>> GetCreatedPages();
        Task<CreatedPage> PostCreatedPage(CreatedPage createdPage);
        Task EditCreatedPage(long editId, CreatedPage createdPage);
        Task<CreatedPage> DeleteCreatedPage(long deleteId);

        // Article

        Task<List<Article>> GetArticles();
        Task<Article> PostArticle(Article article);
        Task EditArticle(long editId, Article article);
        Task<Article> DeleteArticle(long deleteId);

        // Images

        Task<List<Image>> GetImages();
        Task<Image> PostImage(Image image);
        Task EditImage(long editId, Image image);
        Task<Image> DeleteImage(long deleteId);

        // Documents

        Task<List<Document>> GetDocuments();
        Task<Document> PostDocument(Document document);
        Task EditDocument(long editId, Document document);
        Task<Document> DeleteDocument(long deleteId);

        // Links

        Task<List<Link>> GetLinks();
        Task<Link> PostLink(Link document);
        Task EditLink(long editId, Link document);
        Task<Link> DeleteLink(long deleteId);

        // Other Articles

        Task<List<OtherArticle>> GetOtherArticles();
        Task<OtherArticle> PostOtherArticle(OtherArticle otherArticle);
        Task EditOtherArticle(long editId, OtherArticle otherArticle);
        Task<OtherArticle> DeleteOtherArticle(long deleteId);
    }
}

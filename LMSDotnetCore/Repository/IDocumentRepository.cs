using LMSDotnetCore.Models;

namespace LMSDotnetCore.Repository
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetAllDocuments();
        Task<Document?> GetDocumentByID(int id);
        Task<bool> AddNewDocument(Document document);
        Task<bool> UpdateDocument(Document document);
        Task<bool> DeleteDocument(int id);
        Task<bool> ApproveArticle(int documentID);
        Task<bool> DeleteArticle(int documentID);
    }
}

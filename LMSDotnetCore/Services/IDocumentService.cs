using LMSDotnetCore.Models;

namespace LMSDotnetCore.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAllDocuments();
        Task<Document?> GetDocumentByID(int documentID);
        Task<bool> AddNewDocument(Document document);
        Task<bool> UpdateDocument(Document document);
        Task<bool> DeleteDocument(int id);
        Task<bool> ApproveArticle(int documentID);
        Task<bool> DeleteArticle(int documentID);
    }
}

using LMSDotnetCore.Models;

namespace LMSDotnetCore.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<Document>> GetAllDocuments();
        Task<Document?> GetDocumentByID(int documentID);
    }
}

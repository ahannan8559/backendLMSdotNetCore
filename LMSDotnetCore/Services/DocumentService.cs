using LMSDotnetCore.Models;
using LMSDotnetCore.Repository;

namespace LMSDotnetCore.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            return (IEnumerable<Document>)await _documentRepository.GetAllDocuments();
        }

        public async Task<Document?> GetDocumentByID(int documentID)
        {
            return await _documentRepository.GetDocumentByID(documentID);
        }

    }
}

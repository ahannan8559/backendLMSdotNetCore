using LMSDotnetCore.Authentication;
using LMSDotnetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LMSDotnetCore.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> AddNewDocument(Document document)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApproveArticle(int documentID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteArticle(int documentID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDocument(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document?> GetDocumentByID(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync<Document>(doc => doc.DocumentID == id);
        }

        public Task<bool> UpdateDocument(Document document)
        {
            throw new NotImplementedException();
        }
    }
}

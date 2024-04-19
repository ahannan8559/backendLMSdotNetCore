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

        public async Task<bool> AddNewDocument(Document document)
        {
            try
            {
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                return document.DocumentID != 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error 500: "+ex);

    }
        }

        public async Task<bool> ApproveArticle(int documentID)
        {
            var entity = await _context.Documents.FindAsync(documentID);

            if (entity != null)
            {
                if (entity.isPublished)
                {
                    throw new InvalidOperationException($"Entity is already published.");
                }
                entity.isPublished = true;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                //400
                throw new InvalidOperationException($"Entity with ID {documentID} not found.");
            }
        }

        public Task<bool> DeleteArticle(int documentID)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteDocument(int id)
        {
            try
            {
                var entity = await _context.Documents.FindAsync(id);
                if (entity == null)
                {
                    return false;
                }

                _context.Documents.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                // Log the exception or handle it as needed
                //throw new RepositoryException("Error occurred while deleting entity", ex);
            }

        }

        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<Document?> GetDocumentByID(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync<Document>(doc => doc.DocumentID == id);
        }

        public async Task<bool> UpdateDocument(Document document)
        {
            _context.Entry(document).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false; 
            }
        }
    }
}

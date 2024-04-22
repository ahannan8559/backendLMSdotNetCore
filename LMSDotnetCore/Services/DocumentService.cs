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
            try
            {
                return (IEnumerable<Document>)await _documentRepository.GetAllDocuments();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }         
        }

        public async Task<Document?> GetDocumentByID(int documentID)
        {
            try
            {
                return await _documentRepository.GetDocumentByID(documentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<bool> AddNewDocument(Document document)
        {
            try
            {
                return await _documentRepository.AddNewDocument(document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> ApproveArticle(int documentID)
        {
            try
            {
                return await _documentRepository.ApproveArticle(documentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> DeleteArticle(int documentID)
        {
            try
            {
                return await _documentRepository.DeleteArticle(documentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> DeleteDocument(int id)
        {
            try
            {
                return await _documentRepository.DeleteDocument(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<bool> UpdateDocument(Document document)
        {
            try
            {
                return await _documentRepository.UpdateDocument(document);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}

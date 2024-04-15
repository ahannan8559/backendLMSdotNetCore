using LMSDotnetCore.Authentication;
using LMSDotnetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace LMSDotnetCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentRepository)
        {
            _documentService = documentRepository;
        }

        [HttpGet("getdocuments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await _documentService.GetAllDocuments();
            if (documents == null)
            {
                return BadRequest("No documents exist.....");
            }

            return Ok(documents);
        }

        [HttpPost("getdocument/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Getdocument(int id)
        {
            var document = await _documentService.GetDocumentByID(id);
            if (document == null)
            {
                return BadRequest("Document Not Found");
            }
            return Ok( document);
        }

    }
}

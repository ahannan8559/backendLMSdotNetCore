using LMSDotnetCore.Models;
using LMSDotnetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest("No document exists");
            }

            return Ok(documents);
        }

        [HttpGet("getdocument/{id}")]
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

        [HttpPost("adddocument")]
        [AllowAnonymous]
        public async Task<IActionResult> AddDocument([FromBody] Document document)
        {
            try
            {
                var result = await _documentService.AddNewDocument(document);
                if (result)
                {
                    return Ok("Document Added Successfully.");
                }
                else
                {
                    return BadRequest("Document was not added.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updatedocument")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateDocument([FromBody] Document document)
        {
            try
            {
                var result = await _documentService.UpdateDocument(document);
                if (result)
                {
                    return Ok("Document Updated Successfully.");
                }
                else
                {
                    return BadRequest("Document was not Updated.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("approvedocument/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ApproveArticle(int documentID)
        {
            try
            {
                var result = await _documentService.ApproveArticle(documentID);
                if (result)
                {
                    return Ok("Document Approved Successfully.");
                }
                else
                {
                    return BadRequest("Document was not Approved.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletedocument/{documentID}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteDocument(int documentID)
        {
            try
            {
                var result = await _documentService.DeleteDocument(documentID);
                if (result)
                {
                    return Ok("Document Deleted Successfully.");
                }
                else
                {
                    return BadRequest("Document was not Deleted.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

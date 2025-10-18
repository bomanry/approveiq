using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BLH.ApproveIQ.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class DocumentsController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public DocumentsController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpGet("invoice-pdf/{fileName}")]
    public IActionResult GetInvoicePdf(string fileName)
    {
        try
        {
            // Validate file name to prevent directory traversal attacks
            if (string.IsNullOrWhiteSpace(fileName) || 
                fileName.Contains("..") || 
                fileName.Contains("/") || 
                fileName.Contains("\\") ||
                !fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file name");
            }

            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"PDF file '{fileName}' not found");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            
            // Return PDF with proper headers for ng2-pdf-viewer
            Response.Headers.Add("Content-Disposition", "inline");
            return File(fileBytes, "application/pdf");
        }
        catch (Exception ex)
        {
            // Log the exception in a real application
            return StatusCode(500, "An error occurred while retrieving the PDF");
        }
    }

    [HttpGet("invoice-pdf-base64/{fileName}")]
    public IActionResult GetInvoicePdfAsBase64(string fileName)
    {
        try
        {
            // Validate file name to prevent directory traversal attacks
            if (string.IsNullOrWhiteSpace(fileName) || 
                fileName.Contains("..") || 
                fileName.Contains("/") || 
                fileName.Contains("\\") ||
                !fileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file name");
            }

            var filePath = Path.Combine(_environment.WebRootPath, "pdfs", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"PDF file '{fileName}' not found");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(fileBytes);
            
            return Ok(new { pdfData = base64String, fileName = fileName });
        }
        catch (Exception ex)
        {
            // Log the exception in a real application
            return StatusCode(500, "An error occurred while retrieving the PDF");
        }
    }

    [HttpGet("invoice-pdfs")]
    public IActionResult GetAvailableInvoicePdfs()
    {
        try
        {
            var pdfDirectory = Path.Combine(_environment.WebRootPath, "pdfs");
            
            if (!Directory.Exists(pdfDirectory))
            {
                return Ok(new string[0]); // Return empty array if directory doesn't exist
            }

            var pdfFiles = Directory.GetFiles(pdfDirectory, "*.pdf")
                .Select(Path.GetFileName)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();

            return Ok(pdfFiles);
        }
        catch (Exception ex)
        {
            // Log the exception in a real application
            return StatusCode(500, "An error occurred while retrieving available PDFs");
        }
    }
}
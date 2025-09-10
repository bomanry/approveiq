using Microsoft.AspNetCore.Http;

namespace BLH.ApproveIQ.Presentation.Contracts;

public class FormDataFileWithPayloadContract
{
    public IFormFile File { get; set; }
    public string Payload { get; set; }
}
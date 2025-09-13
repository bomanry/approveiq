using Microsoft.Extensions.Configuration;
using BLH.ApproveIQ.Domain.Identity.Models;
using TypeScriptModelsGenerator;
using TypeScriptModelsGenerator.Definitions;
using TypeScriptModelsGenerator.Options;

namespace Sparkhound.CodeGenerator;

public class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine("Running the Sparkhound Code Generator...");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json");

        var config = configuration.Build();

        var outputPath = config["OutputPath"];

        Console.WriteLine(Directory.GetCurrentDirectory() + @"\" + outputPath);

        var models = BLH.ApproveIQ.Domain.AssemblyReference.Assembly.ExportedTypes
            .Where(x => x.Namespace == "BLH.ApproveIQ.Domain.Entities"
                || x.Namespace == "BLH.ApproveIQ.Domain.Models")
            .ToList();

        var moreModels = BLH.ApproveIQ.Application.AssemblyReference.Assembly.ExportedTypes
            .Where(x => x.Namespace == "BLH.ApproveIQ.Application.Requests"
                        || x.Namespace == "BLH.ApproveIQ.Application.Responses")
            .ToList();

        var dtos = BLH.ApproveIQ.Application.AssemblyReference.Assembly.ExportedTypes
            .Where(x => x.Namespace == "BLH.ApproveIQ.Application.DTOs")
            .ToList();

        models.Add(typeof(ApplicationIdentityConstants.Roles));

        var typeScriptDefinition = TypeScriptDefinitionFactory.Create()
            .Folder("data", generatedFolder =>
            {
                generatedFolder.Include(models);
                generatedFolder.Include(moreModels);
                generatedFolder.Include(dtos);
            });

        TypeScriptModelsGeneration
                .Setup(typeScriptDefinition, outputPath, (options) =>
                {
                    options.GenerationMode = GenerationMode.Crawl;

                    options.AddNamespaceReplaceRule("Studyville", string.Empty);
                    options.AddNamespaceReplaceRule(nameof(BLH.ApproveIQ), string.Empty);
                    options.AddNamespaceReplaceRule(nameof(BLH.ApproveIQ.Application), string.Empty);
                    options.AddNamespaceReplaceRule(nameof(System), string.Empty);
                    options.AddNamespaceReplaceRule(nameof(BLH.ApproveIQ.Domain), string.Empty);

                    options.AddNamespaceReplaceRule(nameof(BLH.ApproveIQ.Application.DTOs), "dtos");

                    options.CleanDestinationDirectories.AddRange(new[] { "data" });
                })
                .Execute();

        return 0;
    }
}

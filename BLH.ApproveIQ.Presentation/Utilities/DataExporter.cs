using System.Text;

namespace BLH.ApproveIQ.Presentation.Utilities;

public class DataExporter<T> where T : class
{
    // Export to CSV
    public string ExportToCsv(List<T> data) 
    {
        var properties = typeof(T).GetProperties();
        
        var csvBuilder = new StringBuilder();

        // Add headers
        csvBuilder.AppendLine(string.Join(",", properties.Select(p => p.Name)));

        // Add rows
        foreach (var item in data)
        {
            var row = string.Join(",", properties.Select(p => p.GetValue(item, null)?.ToString() ?? string.Empty));
            csvBuilder.AppendLine(row);
        }

        return csvBuilder.ToString();
    }

    // Export to JSON
    public string ExportToJson(List<T> data)
    {
        return System.Text.Json.JsonSerializer.Serialize(data);
    }

    // Export to XML
    public string ExportToXml(List<T> data)
    {
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));

        using (var stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, data);
            return stringWriter.ToString();
        }
    }
}

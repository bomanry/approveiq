using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Snippets.Font;
using BLH.ApproveIQ.Application.Abstractions;
using BLH.ApproveIQ.Domain.Entities;

namespace BLH.ApproveIQ.Infrastructure.Services.Pdf;

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

public class MigraDocPdfGenerator : IPdfGenerator
{
    private Document StartDocument()
    {
        var document = new Document();

        GlobalFontSettings.FontResolver = new FailsafeFontResolver();

        var style = document.Styles[StyleNames.Normal]!;
        style.Font.Name = "Arial";

        return document;
    }

    private byte[] RenderDocument(Document document)
    {
        // Create a renderer for the MigraDoc document.
        var pdfRenderer = new PdfDocumentRenderer
        {
            // Associate the MigraDoc document with a renderer.
            Document = document,
            PdfDocument =
            {
                // Change some settings before rendering the MigraDoc document.
                PageLayout = PdfPageLayout.SinglePage,
                ViewerPreferences =
                {
                    FitWindow = true
                }
            }
        };

        // Render the PDF
        pdfRenderer.RenderDocument();

        using var stream = new MemoryStream();
        pdfRenderer.Save(stream, false);
        return stream.ToArray();
    }

    // public byte[] GenerateStudentDetailPdf(Student student, DateTime start, DateTime end, int timezoneOffsetInMinutes, List<StudentSessionAssessmentResult> assessments)
    // {
    //     var document = StartDocument();
    //
    //     var filteredSubSessions = student
    //         .AssignedSessions
    //         .SelectMany(m => m.SubSessions)
    //         .Where(m =>
    //             m.SessionDate >= start
    //             && m.SessionDate <= end)
    //         .OrderBy(m => m.SessionDate)
    //         .ToList();
    //
    //     //File header
    //     //TODO: Add image
    //     var section = document.AddSection();
    //     section.AddParagraph($"Student Name:  {student.FirstName} {student.LastName}");
    //     section.AddParagraph($"Student ID: {student.Id}");
    //     section.AddParagraph($"Grade: {student.GradeLevel}");
    //     section.AddParagraph($"Start: {start}");
    //     section.AddParagraph($"End: {end}");
    //
    //     // Have to count hours before writing things in the loop, since they want it at the top
    //     var doneHours = 0;
    //     var absentHours = 0;
    //     var incompleteHours = 0;
    //
    //     foreach (var subSession in filteredSubSessions)
    //     {
    //         var results = student.StudentResults.FirstOrDefault(m => m.SubSession == subSession);
    //
    //         if (results is not null)
    //         {
    //             if (results.IsAbsent) absentHours += subSession.Session.Duration;
    //             else doneHours += subSession.Session.Duration;
    //         }
    //         else incompleteHours += subSession.Session.Duration;
    //     }
    //
    //     var hoursTable = section.AddTable();
    //     var columnWidth = (Unit.FromInch(8.5) - section.PageSetup.LeftMargin - section.PageSetup.RightMargin) * 0.3;
    //
    //     hoursTable.AddColumn(columnWidth);
    //     hoursTable.AddColumn(columnWidth);
    //     hoursTable.AddColumn(columnWidth);
    //
    //     var hoursRow = hoursTable.AddRow();
    //     var currentParagraph = hoursRow.Cells[0].AddParagraph();
    //     currentParagraph.AddFormattedText("Hours Complete: ", TextFormat.Bold);
    //     currentParagraph.AddFormattedText(String.Format("{0:0.00}", doneHours/60.0), TextFormat.Underline);
    //     currentParagraph = hoursRow.Cells[1].AddParagraph();
    //     currentParagraph.AddFormattedText("Hours Absent: ", TextFormat.Bold);
    //     currentParagraph.AddFormattedText(String.Format("{0:0.00}", absentHours / 60.0), TextFormat.Underline);
    //     currentParagraph = hoursRow.Cells[2].AddParagraph();
    //     currentParagraph.AddFormattedText("Hours Incomplete: ", TextFormat.Bold);
    //     currentParagraph.AddFormattedText(String.Format("{0:0.00}", incompleteHours / 60.0), TextFormat.Underline);
    //
    //     section.AddParagraph(" ");
    //     
    //     //Now print out all the session information in this loop
    //     foreach (var subSession in filteredSubSessions)
    //     {
    //         var sessionAssessments = assessments
    //             .Where(m => m.SubSession.Id == subSession.Id)
    //             .ToList();
    //
    //         //section = document.AddSection();
    //         var table = section.AddTable();
    //         table.AddColumn(columnWidth);
    //         table.AddColumn(columnWidth);
    //
    //         var row = table.AddRow();
    //         currentParagraph = row.Cells[0].AddParagraph();
    //         currentParagraph.AddFormattedText("Session Date: ", TextFormat.Bold);
    //
    //         var dt = subSession.SessionDate!.Value;
    //         dt = dt.AddMinutes(-timezoneOffsetInMinutes);
    //         
    //         currentParagraph.AddFormattedText($"{dt:MM/dd/yyyy h:mm tt}", TextFormat.Underline);
    //
    //         var results = student.StudentResults
    //             .FirstOrDefault(m => m.SubSession == subSession);
    //
    //         if (results is not null)
    //         {
    //             if (results.SubSession!.IsTutorAbsent)
    //             {
    //                 currentParagraph = row.Cells[1].AddParagraph();
    //                 currentParagraph.AddFormattedText("Tutor Absent", TextFormat.Bold);
    //                 section.AddParagraph();
    //                 continue;
    //             }
    //             
    //             if (results.IsAbsent)
    //             {
    //                 currentParagraph = row.Cells[1].AddParagraph();
    //                 currentParagraph.AddFormattedText("Absent", TextFormat.Bold);
    //                 section.AddParagraph();
    //                 continue;
    //             }
    //             currentParagraph = row.Cells[1].AddParagraph();
    //             currentParagraph.AddFormattedText($"Subject: ", TextFormat.Bold);
    //             currentParagraph.AddFormattedText($"{results.Subtopic}", TextFormat.Underline);
    //             
    //             row = table.AddRow();
    //             currentParagraph = row.Cells[0].AddParagraph();
    //             currentParagraph.AddFormattedText($"Rating Before Session: ", TextFormat.Bold);
    //             currentParagraph.AddFormattedText($"{results.RatingBeforeSession}", TextFormat.Underline);
    //             currentParagraph = row.Cells[1].AddParagraph();
    //             currentParagraph.AddFormattedText($"Rating After Session: ", TextFormat.Bold);
    //             currentParagraph.AddFormattedText($"{results.RatingAfterSession}", TextFormat.Underline);
    //
    //             if (sessionAssessments.Count > 0)
    //             {
    //                 currentParagraph = section.AddParagraph();
    //                 currentParagraph.AddFormattedText("Assessments: ", TextFormat.Bold);
    //                 currentParagraph.AddFormattedText(String.Join(", ", sessionAssessments.Select(m => $"{m.Assessment} - {m.Grade}")), TextFormat.Underline);
    //             }
    //
    //             if (results.HqMaterials != null)
    //             {
    //                 currentParagraph = section.AddParagraph();
    //                 currentParagraph.AddFormattedText("HQ Materials: ", TextFormat.Bold);
    //                 currentParagraph.AddFormattedText(results.HqMaterials, TextFormat.Underline);
    //             }
    //
    //             if (results.Tags != null)
    //             {
    //                 currentParagraph = section.AddParagraph();
    //                 currentParagraph.AddFormattedText("Tags: ", TextFormat.Bold);
    //                 currentParagraph.AddFormattedText(results.Tags, TextFormat.Underline);
    //             }
    //
    //             if (results.TutorNotes != null)
    //             {
    //                 currentParagraph = section.AddParagraph();
    //                 currentParagraph.AddFormattedText("Tutor Notes: ", TextFormat.Bold);
    //                 currentParagraph.AddText($"{results.TutorNotes}");
    //             }
    //
    //         } else
    //         {
    //             currentParagraph = row.Cells[1].AddParagraph();
    //             currentParagraph.AddFormattedText("No Result Submitted", TextFormat.Bold);
    //         }
    //
    //         section.AddParagraph();
    //     }
    //
    //     return RenderDocument(document);
    // }
}

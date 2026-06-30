// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples.AspNetCore.Cpu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "OpenCV5Sharp CPU REST API Sample. Use POST /api/process with an image file.");

            app.MapPost("/api/process", async (HttpContext context) =>
            {
                if (!context.Request.HasFormContentType)
                {
                    return Results.BadRequest("Invalid form content type.");
                }

                var form = await context.Request.ReadFormAsync();
                var file = form.Files.GetFile("file");
                if (file == null || file.Length == 0)
                {
                    return Results.BadRequest("No file uploaded. Please upload a file via key 'file'.");
                }

                // 1. Read the file into a memory buffer
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();

                try
                {
                    // 2. Decode image using Cv2.Imdecode (avoiding local disk writes)
                    using var src = Cv2.Imdecode(fileBytes, (int)ImreadModes.Color);
                    if (src.Empty())
                    {
                        return Results.BadRequest("Invalid or corrupt image format.");
                    }

                    // 3. Process image (convert to grayscale)
                    using var gray = new Mat();
                    Cv2.CvtColor(src, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);

                    // 4. Re-encode image to memory buffer
                    byte[] outputBytes = Cv2.Imencode(".jpg", gray);

                    // 5. Return processed file
                    return Results.File(outputBytes, "image/jpeg");
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Image processing failed: {ex.Message}");
                }
            });

            app.Run();
        }
    }
}

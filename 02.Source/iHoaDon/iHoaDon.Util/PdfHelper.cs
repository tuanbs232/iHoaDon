using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using iTextSharp.text.pdf;

namespace iHoaDon.Util
{
    /// <summary>
    /// Pdf utilities
    /// </summary>
    public static class PdfHelper
    {
        #region Data Extraction
        /// <summary>
        /// Gets the acro dict (dictionary containins values of fields).
        /// </summary>
        /// <param name="pdfFile">The PDF file.</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetAcroDict(string pdfFile)
        {
            var reader = new PdfReader(pdfFile);
            var acroFields = reader.AcroFields;
            return acroFields.Fields.Keys.ToDictionary(k => k, acroFields.GetField);
        }

        /// <summary>
        /// Gets the acro dict (dictionary containins values of fields).
        /// </summary>
        /// <param name="pdfFile">The PDF file.</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetAcroDict(byte[] pdfFile)
        {
            var reader = new PdfReader(pdfFile);
            var acroFields = reader.AcroFields;
            return acroFields.Fields.Keys.ToDictionary(k => k, acroFields.GetField);
        }

        /// <summary>
        /// Get a list of acrofield keys in a file.
        /// </summary>
        /// <param name="pdfFile">The PDF file.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetAcrofieldKeys(string pdfFile)
        {
            if (String.IsNullOrEmpty(pdfFile))
            {
                throw new ArgumentNullException("pdfFile");
            }
            if (!File.Exists(pdfFile))
            {
                throw new FileNotFoundException("Pdf sample not found: " + pdfFile, pdfFile);
            }

            return GetAcrofieldKeys(File.OpenRead(pdfFile));
        }

        /// <summary>
        /// Gets the acrofield keys.
        /// </summary>
        /// <param name="pdfFileStream">The PDF file stream.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetAcrofieldKeys(Stream pdfFileStream)
        {
            if (pdfFileStream == null)
            {
                throw new ArgumentNullException("pdfFileStream");
            }
            var reader = new PdfReader(pdfFileStream);
            return reader.AcroFields.Fields.Keys;
        }

        #endregion

        #region Convert html to pdf and join all pdf files in one file
        /// <summary>
        /// Convert an html file to pdf
        /// </summary>
        /// <param name="toolPath">Absolute path to wkHtmlToPdf's executable</param>
        /// <param name="toolOptions">The tool options, defaults to --enable-forms.</param>
        /// <param name="htmlInput">The HTML input.</param>
        /// <param name="pdfOutput">The PDF output.</param>
        public static void Html2Pdf(string toolPath, string toolOptions, string htmlInput, string pdfOutput)
        {
            if (String.IsNullOrEmpty(toolPath))
            {
                throw new ArgumentNullException("toolPath");
            }
            if (String.IsNullOrEmpty(htmlInput))
            {
                throw new ArgumentNullException("htmlInput");
            }
            if (String.IsNullOrEmpty(pdfOutput))
            {
                throw new ArgumentNullException("pdfOutput");
            }
            if (!File.Exists(htmlInput))
            {
                throw new FileNotFoundException("Input file not found:" + htmlInput, htmlInput);
            }
            if (String.IsNullOrEmpty(toolOptions))
            {
                toolOptions = String.Join(" ", new[] {
                    "--margin-top 17mm",
                    "--margin-bottom 15mm",
                    "--margin-right 10mm",
                    "--margin-left 10mm",
                    "--enable-forms",
                    "--page-size A4",
                    "--no-background",
                });
            }

            int exitCode;
            using (var wkHtmlToPdf = Process.Start(new ProcessStartInfo(toolPath)
            {
                Arguments = String.Join(" ", toolOptions, htmlInput, " - "),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                ErrorDialog = false,
                UseShellExecute = false,
                WorkingDirectory = Path.GetDirectoryName(toolPath)
            }))
            using (var file = File.OpenWrite(pdfOutput))
            {
                wkHtmlToPdf.StandardOutput.BaseStream.CopyTo(file);
                wkHtmlToPdf.WaitForExit(15000);
                exitCode = wkHtmlToPdf.ExitCode;
                wkHtmlToPdf.Close();
            }
            Trace.WriteLine("wkhtml2pdf exit code: " + exitCode);
            if (!(exitCode == 0 || exitCode == 2))
            {
                var error = File.ReadAllText(pdfOutput);
                File.Delete(pdfOutput);
                throw new Exception("Error while converting html to pdf: " + error);
            }
        }

        /// <summary>
        /// Merge multiple pdf files into one.
        /// </summary>
        public static void MergePdfs(string outPdf, string masterPart, params string[] parts)
        {
            if (String.IsNullOrEmpty(outPdf))
            {
                throw new ArgumentNullException("outPdf");
            }
            if (String.IsNullOrEmpty(masterPart))
            {
                throw new ArgumentNullException("masterPart");
            }
            if (outPdf.Equals(masterPart, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("the output file must be different from the master file");
            }
            if (parts == null || parts.Length == 0)
            {
                //No need to do a merge here, just copy the only one
                File.Copy(masterPart, outPdf);
                return;
            }

            PdfCopyFields output = null;
            try
            {
                output = new PdfCopyFields(File.OpenWrite(outPdf));
                foreach (var part in new[] {masterPart}.Concat(parts))
                {
                    output.AddDocument(new PdfReader(part));
                }
            }
            finally
            {
                if (output != null)
                {
                    try
                    {
                        output.Close();
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error closing merged file: " + exception.Message, exception);
                    }
                }
            }
        }
        #endregion
    }
}
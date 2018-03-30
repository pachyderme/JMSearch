using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using JMSearch.Models;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;

namespace JMSearch.Documents.Treatment.Services
{
    public class ParsingService
    {
        /// <summary>
        /// Files location
        /// </summary>
        private string _DirectoryPath;

        private string[] _FilesPath;
        private DocumentDatabase _DocumentDatabase;

        public ParsingService(string directoryPath)
        {
            _DirectoryPath = directoryPath;
            _FilesPath = Directory.GetFiles(_DirectoryPath, "*.doc");
            _DocumentDatabase = new DocumentDatabase();

            _DocumentDatabase.Create(new Models.Document
            {
                Name = "2008.4",
                Paragraph = "blabllrétzefdsgçz_hgeji,ks",
                ViewNumber = 0
            });

            ParseDocuments();
        }

        private void ParseDocuments()
        {
            foreach (var filePath in _FilesPath)
            {

                StringBuilder sb = new StringBuilder();
                int i = 0;
                Microsoft.Office.Interop.Word.ApplicationClass wordApp = new ApplicationClass();

                object file = filePath;

                object nullobj = System.Reflection.Missing.Value;

                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open

                                                        (ref file, ref nullobj, ref nullobj,

                                                        ref nullobj, ref nullobj, ref nullobj,

                                                        ref nullobj, ref nullobj, ref nullobj,

                                                        ref nullobj, ref nullobj, ref nullobj);



                Microsoft.Office.Interop.Word.Paragraphs DocPar = doc.Paragraphs;

                // Count number of paragraphs in the file

                long parCount = DocPar.Count;

                // Step through the paragraphs

                while (i < parCount)

                {

                    i++;



                    sb.Append(DocPar[i].Range.Text);



                }

                doc.Close(ref nullobj, ref nullobj, ref nullobj);

                wordApp.Quit(ref nullobj, ref nullobj, ref nullobj);



            }
        }
    }

}


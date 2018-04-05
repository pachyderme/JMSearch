using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using JMSearch.Models;
using Microsoft.Office.Interop.Word;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JMSearch.Documents.Treatment.Services
{
    public class ParsingService
    {
        #region Field
        /// <summary>
        /// Directory for picking up the files
        /// </summary>
        private string _DirectoryPath;

        /// <summary>
        /// All files path in directory
        /// </summary>
        private string[] _FilesPath;

        /// <summary>
        /// Database link
        /// </summary>
        private DocumentDatabase _DocumentDatabase;
        #endregion

        #region Constructor
        public ParsingService(string directoryPath)
        {
            _DirectoryPath = directoryPath;
            _FilesPath = Directory.GetFiles(_DirectoryPath, "*.doc", SearchOption.AllDirectories)
                         .Where(str => !Path.GetFileName(str).StartsWith("~$")).ToArray();
            _DocumentDatabase = DocumentDatabase.GetInstance();
            ParseDocuments();
        }
        #endregion

        #region Methods
        private void ParseDocuments()
        {
            Parallel.ForEach(_FilesPath, (filePath) =>
            {
                List<Models.Document> _ListParagraphes = new List<Models.Document>();
                
                Console.WriteLine("Début Document : " + Path.GetFileName(filePath));
                StringBuilder sb = new StringBuilder();
                int i = 0;
                ApplicationClass wordApp = new ApplicationClass();
                object file = filePath;
                object nullobj = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open
                                                        (ref file, ref nullobj, ref nullobj,
                                                        ref nullobj, ref nullobj, ref nullobj,
                                                        ref nullobj, ref nullobj, ref nullobj,
                                                        ref nullobj, ref nullobj, ref nullobj);
                Paragraphs DocPar = doc.Paragraphs;
                // Count number of paragraphs in the file
                long parCount = DocPar.Count;
                // Step through the paragraphs
                string paragraph = string.Empty;
                int pageNumber = 0;
                Console.WriteLine("Paragraphes trouvés : " + parCount+ " -- Document : "+ Path.GetFileName(filePath));
                while (i < parCount)
                {
                    if (i % 100 == 0)
                    {
                        Console.WriteLine("Paragraphes parcourus : " + i + " -- Document : " + Path.GetFileName(filePath));
                    }
                    i++;
                    if (DocPar[i].Range.Text != "\r"
                        && DocPar[i].Range.Text != "\f"
                        && DocPar[i].Range.Text != "\t")
                    {
                        _ListParagraphes.Add(new Models.Document
                        {
                            Name = Path.GetFileName(filePath),
                            Paragraph = DocPar[i].Range.Text,
                            ViewNumber = 0,
                            Page = GetPageNumberOfRange(DocPar[i].Range)
                        });
                    }
                }

                foreach (var paragraphe in _ListParagraphes)
                {
                    _DocumentDatabase.Create(paragraphe);
                }

                doc.Close(ref nullobj, ref nullobj, ref nullobj);
                wordApp.Quit(ref nullobj, ref nullobj, ref nullobj);
                Console.WriteLine("FIN Document : " + Path.GetFileName(filePath));
                Console.WriteLine("---------------------------------------------------------------------------------");
            });
        }

        private static int GetPageNumberOfRange(Range range)
        {
            return (int)range.get_Information(WdInformation.wdActiveEndPageNumber);
        }
        #endregion
    }

}


using JMSearch.Documents.Treatment.Services;
using System;

namespace JMSearch.Documents.Treatment
{
    class Program
    {
        static void Main(string[] args)
        {
            // New document parsing serice
            new ParsingService(@"C:\ARCHI\");
        }
    }
}

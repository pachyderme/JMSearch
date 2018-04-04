using Microsoft.AspNetCore.Mvc;

using System.IO;

namespace JMSearch.Documents.API.Controllers
{
	/// <summary>
	/// Contrôleur pour les documents
	/// </summary>
	[Route("api/Documents")]
	public class DocumentController : Controller
    {
		private readonly string _pathDocuments = @"C:\Archives\";

		/// <summary>
		/// Obtient un pdf en fonction du nom.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[Produces(typeof(Stream))]
		public IActionResult Get(string name)
		{
			if (!Directory.Exists(_pathDocuments))
				throw new DirectoryNotFoundException("Le dossier : \"" + _pathDocuments + "\" n'existe pas.");

			var fileInfo = new FileInfo(Path.Combine(_pathDocuments, name + ".pdf"));

			if (!fileInfo.Exists)
				return BadRequest();

			return Ok(fileInfo.OpenRead());
		}
    }
}

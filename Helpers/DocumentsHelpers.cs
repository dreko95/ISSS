using System;
using System.Collections.Generic;
using System.IO;
using ISSS.Models;

namespace ISSS.Helpers
{
    public static class DocumentsHelpers
    {
        // Метод зчитує всі файли з папки Documents в проекті і перетворює їх у словник моделей DocumentModel
        public static Dictionary<int, DocumentModel> LoadDocuments()
        {
            var documents = new Dictionary<int, DocumentModel>();
            var documentId = 1;
            foreach (var file in Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}Documents\\"))
            {
                string documentName = Path.GetFileNameWithoutExtension(file);
                string content = File.ReadAllText(file);
                documents.Add(documentId, new DocumentModel(documentId, documentName, content));
                documentId++;
            }

            return documents;
        }
    }
}

using ISSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ISSS.Classes
{
    public class IndexingService
    {
        private DocumentSpider _documentSpider;
        // Інвертований індекс. Індекс складається з ключа (токенів які пройшли лематизацію) та значення (списка ідентифікаторів документів, які містять токен)
        private Dictionary<string, List<int>> _index = new Dictionary<string, List<int>>();

        public IndexingService()
        {
            _documentSpider = new DocumentSpider();
        }

        // Будуємо інвертований індекс.
        public void BuildIndex(List<DocumentModel> documents)
        {
            if (documents == null || documents.Count == 0)
                return;

            foreach (var document in documents)
            {
                var tokens = _documentSpider.Preprocess(document.Content);
                foreach (var token in tokens)
                {
                    if (!_index.ContainsKey(token))
                    {
                        _index.Add(token, new List<int> { document.Id });
                        continue;
                    }

                    var documentsList = _index[token];
                    if(!documentsList.Contains(document.Id))
                        documentsList.Add(document.Id);
                }
            }
        }

        /// <summary>
        /// Метод шукає ідентифікатори документів в яких зустрічаються -
        /// усі слова із параметру query (після проведення токенізації, видалення стопслів та лематизації)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<int> Search(string query)
        {
            var tokens = _documentSpider.Preprocess(query);
            var docIndexes = new List<int>();

            if(tokens.Count == 0)
                return new List<int>();

            if (!_index.ContainsKey(tokens[0]))
                return new List<int>();

            if (_index[tokens[0]].Count == 0)
                return new List<int>();

            docIndexes.AddRange(_index[tokens[0]]);
            if (tokens.Count == 1)
                return docIndexes;

            for (var i = 1; i < tokens.Count; i++)
            {
                var token = tokens[i];
                if (!_index.ContainsKey(token))
                    return new List<int>();

                docIndexes = docIndexes.Intersect(_index[token]).ToList();
                if(docIndexes.Count == 0)
                    return new List<int>();
            }

            return docIndexes.ToList();
        }
    }
}

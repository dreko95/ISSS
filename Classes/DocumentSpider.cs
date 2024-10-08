using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using System.IO;
using LemmaSharp.Classes;

namespace ISSS.Classes
{
    public class DocumentSpider
    {
        private Lemmatizer lemmatizer;
        private static HashSet<string> stopWords = new HashSet<string>{
            "а", "але", "без", "більш", "був", "була", "були", "було", "бути", "вам", "вас", "від", "він", "вона", "вони", "воно", "все", "всього", "ви", "де", "для", "до", "його", "її", "їм", "із", "інші", "й", "як", "коли", "крім", "ми", "мене", "мені", "мій", "моє", "моя", "на", "нам", "нас", "наш", "не", "нього", "неї", "ні", "о", "під", "після", "про", "та", "так", "також", "те", "тебе", "тепер", "те", "ти", "тих", "то", "того", "той", "тому", "у", "уже", "що", "щоб", "це", "ці", "ця", "чи", "ще", "я"
        };

        public DocumentSpider()
        {
            var stream = File.OpenRead($"{AppDomain.CurrentDomain.BaseDirectory}Libs\\full7z-mlteast-uk.lem");
            lemmatizer = new Lemmatizer(stream);
        }

        // Токенізуємо текст. Прибираємо все крім букв і цифр та приводимо до нижнього регістру
        public List<string> Tokenize(string text)
        {
            return Regex.Split(text.ToLower(), @"\W+").Where(token => token.Length > 1).ToList();
        }

        // Видаляємо стоп слова
        public List<string> RemoveStopWords(List<string> tokens)
        {
            return tokens.Where(token => !stopWords.Contains(token)).ToList();
        }

        // Лематизація
        public List<string> Lemmatize(List<string> tokens)
        {
            // Для лиматизації слів використав бібліотеку LemmaGenerator 
            return tokens.Select(token => lemmatizer.Lemmatize(token)).ToList();
        }

        public List<string> Preprocess(string text)
        {
            var tokens = Tokenize(text);
            tokens = RemoveStopWords(tokens);
            return Lemmatize(tokens);
        }
    }
}

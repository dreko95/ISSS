using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ISSS.Classes;
using ISSS.Models;
using static ISSS.Helpers.DocumentsHelpers;

namespace ISSS
{
    public partial class MainWindow : Window
    {
        private Dictionary<int, DocumentModel> documents;
        private IndexingService _idexingService;

        public MainWindow()
        {
            InitializeComponent(); 
            MainGrid.DataContext = this;
            _idexingService = new IndexingService();

            documents = LoadDocuments();
            _idexingService.BuildIndex(documents.Values.ToList());
        }

        public ObservableCollection<DocumentModel> Results { get; set; } = new ObservableCollection<DocumentModel>();

        // Подія натискання на кнопку "Пошук". Видаляємо пеопередні результати, проводимо пошук документів та виводимо нові результати
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Results.Clear();
            if (string.IsNullOrWhiteSpace(SearchedText.Text))
                return;

            List<int> resultsIds = _idexingService.Search(SearchedText.Text);
            foreach (var documentId in resultsIds)
            {
                Results.Add(documents[documentId]);
            }
        }
    }
}

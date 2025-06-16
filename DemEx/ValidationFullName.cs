using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using WordTable = DocumentFormat.OpenXml.Wordprocessing.Table;
using WordRow = DocumentFormat.OpenXml.Wordprocessing.TableRow;
using WordCell = DocumentFormat.OpenXml.Wordprocessing.TableCell;

namespace DemEx
{
    public partial class ValidationFullName : Form
    {

        private readonly HttpClient client = new HttpClient();
        private readonly string apiUrl = "http://prb.sylas.ru/TransferSimulator/fullName";
        private string fullName;
        private bool isValid;
        public ValidationFullName()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public class ApiResponse
        {
            [JsonProperty("value")] 
            public string Value { get; set; }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {

                var response = await client.GetAsync(apiUrl);
                var responseBody = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                fullName = apiResponse.Value?.Trim();

                // Проверка на пустое значение
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    lblFullName.Text = "Не получены данные";
                    lblValidationResult.Text = "Ошибка: пустое значение ФИО";
                    return;
                }


                lblFullName.Text = fullName;
            }
            catch (Exception ex)
            {
                lblFullName.Text = "Ошибка при получении данных";
                lblValidationResult.Text = $"Ошибка: {ex.Message}";
            }
        }
        private bool IsValidFullName(string name)
        {
            // Проверка на специальные символы и цифры
            if (Regex.IsMatch(name, @"[0-9!@#$%^&*()_+=\[\]{};':""\\|,.<>\/?]"))
                return false;

            // Проверка на минимальную длину (3 символа) и наличие пробелов
            return name.Length >= 3 && name.Contains(" ");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Сначала получите данные, нажав кнопку 'Получить данные'");
                return;
            }

            try
            {
                isValid = IsValidFullName(fullName);

                lblFullName.Text = fullName;
                lblValidationResult.Text = isValid ? "ФИО не содержит запрещенные символы" : "ФИО содержит запрещенные символы";
                RecordTestCase();
                MessageBox.Show("Результат теста успешно записан в документ");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи в документ: {ex.Message}");
            }
        }
        private void RecordTestCase()
        {
            string filePath = @"D:\Загрузки\КОД 09.02.07-5-2025 Приложения к образцу задания Том 1\ТестКейс.docx";

            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, true))
            {
                // Получаем таблицу (предполагаем, что она существует)
                WordTable table = doc.MainDocumentPart.Document.Body.Elements<WordTable>().First();

                WordRow newRow = new WordRow(
                    new WordCell(new Paragraph(new Run(new Text("Проверка ФИО")))),
                    new WordCell(new Paragraph(new Run(new Text(fullName)))),
                    new WordCell(new Paragraph(new Run(new Text(isValid ? "ФИО не содержит запрещенные символы" : "ФИО содержит запрещенные символы"))))
                );

                table.AppendChild(newRow);
            }
        }

        private void ValidationFullName_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

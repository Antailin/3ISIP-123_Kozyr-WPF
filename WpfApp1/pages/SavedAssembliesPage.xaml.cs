using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для SavedAssembliesPage.xaml
    /// </summary>
    public partial class SavedAssembliesPage : Page
    {
        private static readonly Dictionary<string, string> TypeTranslations = new Dictionary<string, string>
        {
            { "cpu",          "Процессор" },
            { "gpu",          "Видеокарта" },
            { "motherboard",  "Материнская плата" },
            { "ram",          "Оперативная память" },
            { "psu",          "Блок питания" },
            { "cooler",       "Кулер" },
            { "case",         "Корпус" },
            { "storage",      "Накопитель" }
        };

        public SavedAssembliesPage()
        {
            InitializeComponent();
            LoadAssemblies();
        }


        private void LoadAssemblies()
        {
            LbAssemblies.ItemsSource = Core.Context.assembly_
                .OrderByDescending(a => a.id)
                .ToList();
        }

        private void LbAssemblies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbAssemblies.SelectedItem is assembly_ selected)
                ShowAssembly(selected);
        }

        private void ShowAssembly(assembly_ a)
        {
            TxtAssemblyTitle.Text = a.name;
            TxtAssemblyAuthor.Text = $"Автор: {a.author}";

            var partIds = Core.Context.partassembly_
                .Where(pa => pa.assemblyid == a.id)
                .Select(pa => pa.partid)
                .ToList();

            var parts = Core.Context.basepart_
                .Where(bp => partIds.Contains(bp.id))
                .ToList();

            var rows = parts.Select(bp =>
            {
                var rawType = bp.parttype_?.name ?? "—";
                string translatedType;
                if (!TypeTranslations.TryGetValue(rawType.ToLower(), out translatedType))
                    translatedType = rawType;

                return new SavedPartRow
                {
                    TypeName = translatedType,
                    Name = bp.name,
                    Manufacturer = bp.manufacturer_?.name ?? "—",
                    PriceFormatted = bp.PriceFormatted,
                    RawPrice = bp.price,
                    ImageUrl = bp.image ?? ""  
                };
            }).ToList();

            LvAssemblyParts.ItemsSource = rows;

            var total = rows.Sum(r => r.RawPrice);
            TxtSavedTotal.Text = $"{total:N0} ₽";
        }

        private void DeleteAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (LbAssemblies.SelectedItem is assembly_ selected)
            {
                var result = MessageBox.Show(
                    $"Удалить сборку \"{selected.name}\"?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    var linked = Core.Context.partassembly_
                        .Where(pa => pa.assemblyid == selected.id)
                        .ToList();
                    Core.Context.partassembly_.RemoveRange(linked);
                    Core.Context.assembly_.Remove(selected);
                    Core.Context.SaveChanges();
                    TxtAssemblyTitle.Text = "Выберите сборку слева";
                    TxtAssemblyAuthor.Text = "";
                    LvAssemblyParts.ItemsSource = null;
                    TxtSavedTotal.Text = "";

                    LoadAssemblies();
                }
            }
            else
            {
                MessageBox.Show("Выберите сборку для удаления.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

    public class SavedPartRow
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string PriceFormatted { get; set; }
        public decimal RawPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}















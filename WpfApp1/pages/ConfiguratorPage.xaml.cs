using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для ConfiguratorPage.xaml
    /// </summary>
    public partial class ConfiguratorPage : Page
    {
        private Dictionary<int, basepart_> _selectedParts = new Dictionary<int, basepart_>();
        private List<PartDisplayItem> _allParts = new List<PartDisplayItem>();

        private bool _hasErrors = false;

        public ConfiguratorPage()
        {
            InitializeComponent();
            LoadPartTypes();
        }


        private void LoadPartTypes()
        {
            var types = Core.Context.parttype_.OrderBy(t => t.name).ToList();
            CmbPartType.ItemsSource = types;
            CmbPartType.DisplayMemberPath = "name";
            if (types.Count > 0)
                CmbPartType.SelectedIndex = 0;
        }

        private void CmbPartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbPartType.SelectedItem is parttype_ type)
                LoadPartsForType(type);
        }

        private void LoadPartsForType(parttype_ type)
        {
            var parts = Core.Context.basepart_
                .Where(p => p.parttypeid == type.id)
                .ToList();

            _allParts = parts.Select(bp => new PartDisplayItem
            {
                Basepart = bp,
                name = bp.name,
                Manufacturer = bp.manufacturer_,
                PriceFormatted = bp.PriceFormatted,
                Specs = BuildSpecs(bp, type.name),
                ImageUrl = bp.image  
            }).ToList();

            var manufacturers = _allParts
                .Select(p => p.Manufacturer)
                .Where(m => m != null)
                .GroupBy(m => m.id)
                .Select(g => g.First())
                .OrderBy(m => m.name)
                .ToList();

            var allItem = new manufacturer_ { id = 0, name = "Все производители" };
            var manuList = new List<manufacturer_> { allItem };
            manuList.AddRange(manufacturers);

            CmbManufacturer.ItemsSource = manuList;
            CmbManufacturer.DisplayMemberPath = "name";
            CmbManufacturer.SelectedIndex = 0;

            ApplyFilter();
        }

        private string BuildSpecs(basepart_ bp, string typeName)
        {
            try
            {
                switch (typeName?.ToLower())
                {
                    case "cpu":
                        {
                            var c = Core.Context.cpu_.Find(bp.id);
                            if (c == null) break;
                            var socket = Core.Context.socket_.Find(c.socketid);
                            return $"Сокет: {socket?.name}, {c.numberofcores} ядер, {c.basecorefrequency} ГГц, TDP {c.thermalpower} Вт";
                        }
                    case "gpu":
                        {
                            var g = Core.Context.gpu_.Find(bp.id);
                            if (g == null) break;
                            return $"Память: {g.videomemory} МБ, рек. питание: {g.recommendpower} Вт";
                        }
                    case "motherboard":
                        {
                            var m = Core.Context.motherboard_.Find(bp.id);
                            if (m == null) break;
                            var socket = Core.Context.socket_.Find(m.socketid);
                            var memType = Core.Context.memorytype_.Find(m.memorytypeid);
                            var ff = Core.Context.formfactor_.Find(m.formfactorid);
                            return $"Сокет: {socket?.name}, {memType?.name}, {ff?.name}";
                        }
                    case "ram":
                        {
                            var r = Core.Context.ram_.Find(bp.id);
                            if (r == null) break;
                            var memType = Core.Context.memorytype_.Find(r.memorytypeid);
                            return $"{memType?.name}, {r.capacity} ГБ, {r.ghz} МГц";
                        }
                    case "cooler":
                        {
                            var cooler = Core.Context.processorcooler_.Find(bp.id);
                            if (cooler == null) break;
                            return $"Трубок: {cooler.heatpipes}, макс. {cooler.maxspeed} об/мин";
                        }
                    case "powersupply":
                        {
                            var p = Core.Context.powersupply_.Find(bp.id);
                            if (p == null) break;
                            var cert = Core.Context.certificate_.Find(p.certificationid);
                            return $"{p.power} Вт, {cert?.name}";
                        }
                    case "case":
                        {
                            var cs = Core.Context.case_.Find(bp.id);
                            if (cs == null) break;
                            var size = Core.Context.casesize_.Find(cs.sizeid);
                            return $"{size?.name}, слоты: {cs.expansionslots}, вент.: {cs.fans}";
                        }
                    case "storage":
                        {
                            var sd = Core.Context.storagedevice_.Find(bp.id);
                            if (sd == null) break;
                            return $"{sd.capacity} ГБ";
                        }
                }
            }
            catch { }
            return "";
        }


        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilter();
        private void CmbManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilter();

        private void ApplyFilter()
        {
            var search = TxtSearch?.Text?.ToLower() ?? "";
            var manuId = (CmbManufacturer?.SelectedItem as manufacturer_)?.id ?? 0;

            LvParts.ItemsSource = _allParts
                .Where(p =>
                    (string.IsNullOrEmpty(search) || p.name.ToLower().Contains(search))
                    && (manuId == 0 || p.Manufacturer?.id == manuId))
                .ToList();
        }


        private void LvParts_DoubleClick(object sender, MouseButtonEventArgs e) => AddSelectedPart();
        private void AddSelected_Click(object sender, RoutedEventArgs e) => AddSelectedPart();

        private void AddSelectedPart()
        {
            if (LvParts.SelectedItem is PartDisplayItem item)
            {
                _selectedParts[item.Basepart.parttypeid] = item.Basepart;
                RefreshSelected();
                CheckCompatibility();
            }
        }

        private void RemovePart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is SelectedPartItem item)
            {
                _selectedParts.Remove(item.Basepart.parttypeid);
                RefreshSelected();
                CheckCompatibility();
            }
        }

        private void RefreshSelected()
        {
            LvSelected.ItemsSource = _selectedParts.Values
                .Select(bp => new SelectedPartItem { Basepart = bp })
                .ToList();

            var total = _selectedParts.Values.Sum(bp => bp.price);
            TxtTotal.Text = $"{total:N0} ₽";
        }

        private void CheckCompatibility()
        {
            var errors = new List<string>();

            cpu_ cpuObj = null;
            gpu_ gpuObj = null;
            motherboard_ mbObj = null;
            ram_ ramObj = null;
            processorcooler_ coolerObj = null;
            powersupply_ psuObj = null;
            case_ caseObj = null;

            foreach (var bp in _selectedParts.Values)
            {
                switch (bp.parttype_?.name?.ToLower())
                {
                    case "cpu": cpuObj = Core.Context.cpu_.Find(bp.id); break;
                    case "gpu": gpuObj = Core.Context.gpu_.Find(bp.id); break;
                    case "motherboard": mbObj = Core.Context.motherboard_.Find(bp.id); break;
                    case "ram": ramObj = Core.Context.ram_.Find(bp.id); break;
                    case "processorcooler": coolerObj = Core.Context.processorcooler_.Find(bp.id); break;
                    case "powersupply": psuObj = Core.Context.powersupply_.Find(bp.id); break;
                    case "case": caseObj = Core.Context.case_.Find(bp.id); break;
                    case "storagedevice": break;
                }
            }

            if (cpuObj != null && mbObj != null && cpuObj.socketid != mbObj.socketid)
            {
                var s1 = Core.Context.socket_.Find(cpuObj.socketid);
                var s2 = Core.Context.socket_.Find(mbObj.socketid);
                errors.Add($"❌ Сокет процессора ({s1?.name}) не совпадает с сокетом материнской платы ({s2?.name}).");
            }

            if (coolerObj != null && mbObj != null)
            {
                var supported = Core.Context.socketprocessorcooler_
                    .Where(s => s.processorcoolerid == coolerObj.id)
                    .Select(s => s.socketid)
                    .ToList();

                if (supported.Count > 0 && !supported.Contains(mbObj.socketid))
                {
                    var s = Core.Context.socket_.Find(mbObj.socketid);
                    errors.Add($"❌ Кулер не поддерживает сокет материнской платы ({s?.name}).");
                }
            }

            if (mbObj != null && caseObj != null)
            {
                var supported = Core.Context.boardformfactorcase_
                    .Where(f => f.caseid == caseObj.id)
                    .Select(f => f.formfactorid)
                    .ToList();

                if (supported.Count > 0 && !supported.Contains(mbObj.formfactorid))
                {
                    var ff = Core.Context.formfactor_.Find(mbObj.formfactorid);
                    errors.Add($"❌ Форм-фактор материнской платы ({ff?.name}) не поддерживается корпусом.");
                }
            }

            if (ramObj != null && mbObj != null && ramObj.memorytypeid != mbObj.memorytypeid)
            {
                var t1 = Core.Context.memorytype_.Find(ramObj.memorytypeid);
                var t2 = Core.Context.memorytype_.Find(mbObj.memorytypeid);
                errors.Add($"❌ Тип памяти ОЗУ ({t1?.name}) не совместим с материнской платой ({t2?.name}).");
            }

            if (psuObj != null && gpuObj != null)
            {
                if (gpuObj.recommendpower.HasValue && psuObj.power < gpuObj.recommendpower.Value)
                    errors.Add($"❌ Блок питания ({psuObj.power} Вт) не хватает для видеокарты (рекомендуется {gpuObj.recommendpower} Вт).");
                else if (gpuObj.recommendpower.HasValue && psuObj.power >= gpuObj.recommendpower.Value)
                    errors.Add($"✅ Блок питания ({psuObj.power} Вт) достаточен для видеокарты (рекомендуется {gpuObj.recommendpower} Вт).");
            }
            _hasErrors = errors.Any(err => err.StartsWith("❌"));

            IcErrors.ItemsSource = errors;
            PnlErrors.Visibility = errors.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }


        private void SaveAssembly_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtAssemblyName.Text))
            {
                MessageBox.Show("Введите название сборки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtAuthor.Text))
            {
                MessageBox.Show("Введите имя автора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_selectedParts.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одно комплектующее.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_hasErrors)
            {
                MessageBox.Show(
                    "Невозможно сохранить сборку с ошибками совместимости.\nИсправьте конфликты и попробуйте снова.",
                    "Ошибка совместимости",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var newAssembly = new assembly_
            {
                name = TxtAssemblyName.Text.Trim(),
                author = TxtAuthor.Text.Trim()
            };
            Core.Context.assembly_.Add(newAssembly);
            Core.Context.SaveChanges();

            foreach (var bp in _selectedParts.Values)
            {
                Core.Context.partassembly_.Add(new partassembly_
                {
                    assemblyid = newAssembly.id,
                    partid = bp.id
                });
            }
            Core.Context.SaveChanges();

            MessageBox.Show("Сборка успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            TxtAssemblyName.Clear();
            TxtAuthor.Clear();
        }
    }

    public class PartDisplayItem
    {
        public basepart_ Basepart { get; set; }
        public string name { get; set; }
        public manufacturer_ Manufacturer { get; set; }
        public string PriceFormatted { get; set; }
        public string Specs { get; set; }
        public string ImageUrl { get; set; }
    }

    public class SelectedPartItem
    {
        public basepart_ Basepart { get; set; }

        public string TypeName => Basepart?.parttype_?.name ?? "—";
        public string Name => Basepart?.name ?? "—";
        public string PriceFormatted => Basepart?.PriceFormatted ?? "—";
        public string ImageUrl => Basepart?.image ?? "";
    }
}
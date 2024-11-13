using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using Repositories;
using Repositories.FOOD;
using Services;

namespace WpfApp.FoodCalc
{
    public partial class FishFoodCalculator : Page
    {
        private readonly IFishFoodCalculatorService _fishFoodCalculatorService;
        private readonly IPondService _pondService;
        private readonly IFishService _fishService;
        private readonly IFoodTypeService _foodTypeService;
        public ObservableCollection<PondViewModel> Ponds { get; set; }
        public ObservableCollection<FoodType> FoodTypes { get; set; }

        public FishFoodCalculator()
        {
            InitializeComponent();
            _pondService = new PondService();
            _fishService = new FishService();
            _fishFoodCalculatorService = new FishFoodCalculatorService(new FishFoodCalculatorRepository());
            _foodTypeService = new FoodTypeService(new FoodTypeRepository());
            DataContext = this;
            Ponds = new ObservableCollection<PondViewModel>();
            FoodTypes = new ObservableCollection<FoodType>();
            LoadData();
            PondsGrid.ItemsSource = Ponds;
        }

        private void LoadData()
        {
            LoadPonds();
            LoadFoodTypes();
        }

        private void LoadPonds()
        {
            var pondsData = _pondService.GetAll(UserSession.GetInstance().MemberId)
                .Where(p => p.IsActive)
                .Select(p => new
                {
                    p.PondId,
                    p.Name,
                    p.Length,
                    p.Width,
                    p.Depth,
                    p.ImagePath,
                    TotalFish = _fishService.GetFishByPondId(p.PondId)?.Count() ?? 0,
                    AvgFishAge = _fishService.GetFishByPondId(p.PondId)?.Any() == true ? _fishService.GetAvgFishAge(p.PondId) : 0,
                    AvgFishSize = _fishService.GetFishByPondId(p.PondId)?.Any() == true ? _fishService.GetAvgFishSize(p.PondId) : 0
                });

            Ponds.Clear();
            foreach (var pond in pondsData)
            {
                Ponds.Add(new PondViewModel
                {
                    PondId = pond.PondId,
                    Name = pond.Name,
                    Dimensions = $"{pond.Length}×{pond.Width}×{pond.Depth}",
                    TotalFish = pond.TotalFish,
                    AvgFishAge = pond.AvgFishAge,
                    AvgFishSize = pond.AvgFishSize,
                    ImagePath = pond.ImagePath
                });
            }
        }

        private void LoadFoodTypes()
        {
            IEnumerable foodTypes = _foodTypeService.GetAllActive();
            FoodTypes.Clear();
            foreach (FoodType foodType in foodTypes)
            {
                FoodTypes.Add(foodType);
            }
        }

        private void CalculateFood_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;
            if (button.Parent is not StackPanel stackPanel) return;

            var pond = button.Tag as PondViewModel;
            var combo = stackPanel.Children.OfType<ComboBox>().FirstOrDefault();


            var totalFish = _fishService.GetFishByPondId(pond.PondId)?.Count() ?? 0;
            if (totalFish == 0)
            {
                MessageBox.Show($"Please add koi to {pond.Name} before calculating food.",
                    "No Fish Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (combo == null || combo.SelectedValue == null)
            {
                MessageBox.Show("Please select a food type first!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (pond == null) return;


            var foodTypeId = (int)combo.SelectedValue;
            var foodTypeName = (combo.SelectedItem as FoodType)?.Name;
            var avgFishSize = _fishService.GetAvgFishSize(pond.PondId);
            var avgFishAge = _fishService.GetAvgFishAge(pond.PondId);

            decimal recommendedAmount = CalculateRecommendedFood(totalFish, avgFishSize, avgFishAge, foodTypeName);
            SaveFoodCalculation(pond.PondId, foodTypeId, recommendedAmount);        
            
            MessageBox.Show(
                $"Recommended feeding for {pond.Name}:\n" +
                $"{recommendedAmount:F2}g of {foodTypeName} per day",
                "Feeding Recommendation", 
                MessageBoxButton.OK, 
                MessageBoxImage.Information);
        }

        private decimal GetAmountFoodType(string? foodType)
        {
            double amount;
            switch (foodType)
            {
                case "Pellets":
                    amount = 0.05;
                    break;
                case "Flakes":
                    amount = 0.03;
                    break;
                case "Live Worms":
                    amount = 0.07;
                    break;
                case "Frozen Shrimp":
                    amount = 0.4;
                    break;
                default:
                    amount = 0.05;
                    break;
            }

            return (decimal)amount;
        }

        private decimal CalculateRecommendedFood(int totalFish, decimal avgSize, double avgAge, string? foodType)
        {
            var amount = GetAmountFoodType(foodType);
            var recommendedAmount = (totalFish * avgSize * (decimal)avgAge) * amount;
            return recommendedAmount;
        }

        private void SaveFoodCalculation(int pondId, int foodTypeId, decimal amount)
        {
            var foodCalc = new FoodCalculator
            {
                FoodTypeId = foodTypeId,
                RecommendedAmount = amount,
                CalculationDate = DateTime.Now,
                Notes = $"Calculated for pond {pondId}"
            };

            _fishFoodCalculatorService.AddNewFoodCalculator(foodCalc);
        }
    }

    public class PondViewModel
    {
        public int PondId { get; set; }
        public string? Name { get; set; }
        public string? Dimensions { get; set; }
        public int TotalFish { get; set; }
        public double AvgFishAge { get; set; }
        public decimal AvgFishSize { get; set; }
        public string? ImagePath { get; set; }
    }
}
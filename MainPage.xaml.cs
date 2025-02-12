using System.Diagnostics;

namespace BMICalculatorDemo
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AboutSection.Opacity = 0;
            await AboutSection.FadeTo(1, 1500);
        }
        private async void OnCalculateBMI(object sender, EventArgs e)
        {
            // Ensure the user has selected a gender
            if (GenderPicker.SelectedIndex < 0)
            {
                ResultLabel.Text = "Please select a gender.";
                ResultLabel.TextColor = Colors.Red;
                await ResultLabel.FadeTo(1, 200);
                return;
            }

            string gender = GenderPicker.SelectedItem.ToString();

            // Validate weight
            if (!double.TryParse(WeightEntry.Text, out double weight) || weight <= 0)
            {
                ResultLabel.Text = "Please enter a valid weight.";
                ResultLabel.TextColor = Colors.Red;
                await ResultLabel.FadeTo(1, 200);
                return;
            }

            // Validate height (in cm)
            if (!double.TryParse(HeightEntry.Text, out double heightCm) || heightCm <= 0)
            {
                ResultLabel.Text = "Please enter a valid height.";
                ResultLabel.TextColor = Colors.Red;
                await ResultLabel.FadeTo(1, 200);
                return;
            }

            // Convert height from centimeters to meters
            double heightMeters = heightCm / 100.0;
            double bmi = weight / (heightMeters * heightMeters);

            // Get category and color based on gender-specific thresholds
            string category = GetBMICategory(bmi, gender);
            Color resultColor = GetCategoryColor(bmi, gender);

            ResultLabel.Text = $"Your BMI: {bmi:F2}\n({category})";
            ResultLabel.TextColor = resultColor;

            // Animate the result label: fade in and scale for a pop effect
            ResultLabel.Opacity = 0;
            await ResultLabel.FadeTo(1, 300);
            await ResultLabel.ScaleTo(1.2, 200, Easing.CubicInOut);
            await ResultLabel.ScaleTo(1, 200, Easing.CubicInOut);
        }

        private string GetBMICategory(double bmi, string gender)
        {
            if (gender == "Male")
            {
                if (bmi < 20) return "Underweight";
                if (bmi < 25) return "Normal weight";
                if (bmi < 30) return "Overweight";
                return "Obese";
            }
            else // Female
            {
                if (bmi < 18) return "Underweight";
                if (bmi < 24) return "Normal weight";
                if (bmi < 29) return "Overweight";
                return "Obese";
            }
        }

        private Color GetCategoryColor(double bmi, string gender)
        {
            if (gender == "Male")
            {
                if (bmi < 20) return Colors.Blue;
                if (bmi < 25) return Colors.Green;
                if (bmi < 30) return Colors.Orange;
                return Colors.Red;
            }
            else // Female
            {
                if (bmi < 18) return Colors.Blue;
                if (bmi < 24) return Colors.Green;
                if (bmi < 29) return Colors.Orange;
                return Colors.Red;
            }
        }

        private void OnGitHubTapped(object sender, TappedEventArgs e)
        {
            string githubUrl = "https://github.com/rohanalom";
            try
            {
                Launcher.OpenAsync(new Uri(githubUrl));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening GitHub: {ex.Message}");
            }
        }
    }

}

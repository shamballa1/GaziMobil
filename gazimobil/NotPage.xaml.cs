using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;


namespace gazimobil
{
    public partial class NotPage : ContentPage
    {
        private List<Course> courses = new List<Course>();

        public NotPage()
        {
            InitializeComponent();
        }

        private void OnCalculationTypeChanged(object sender, EventArgs e)
        {
            bool isSemester = CalculationTypePicker.SelectedIndex == 0;
            bool isBoth = CalculationTypePicker.SelectedIndex == 1;

            SemesterInputs.IsVisible = isSemester || isBoth;
            BothInputs.IsVisible = isBoth;
        }

        private async void OnAddCourseClicked(object sender, EventArgs e)
        {
            string courseName = CourseNameEntry.Text;
            if (string.IsNullOrWhiteSpace(courseName))
            {
                await DisplayAlert("Hata", "Lütfen ders adını girin.", "Tamam");
                return;
            }

            if (CreditPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Hata", "Lütfen Kredi / AKTS seçin.", "Tamam");
                return;
            }

            if (GradePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Hata", "Lütfen harf notu seçin.", "Tamam");
                return;
            }

            double credit = double.Parse(CreditPicker.SelectedItem.ToString());
            string gradeLetter = GradePicker.SelectedItem.ToString();
            double grade = GetNumericGrade(gradeLetter);

            Course course = new Course { Name = courseName, Credit = credit, Grade = grade, GradeLetter = gradeLetter };
            courses.Add(course);
            CoursesListView.ItemsSource = null;
            CoursesListView.ItemsSource = courses;

            UpdateAverages();

            // Input alanlarını sıfırla
            CourseNameEntry.Text = string.Empty;
            CreditPicker.SelectedIndex = -1;
            GradePicker.SelectedIndex = -1;
        }

        private double GetNumericGrade(string gradeLetter)
        {
            return gradeLetter switch
            {
                "AA" => 4.0,
                "BA" => 3.5,
                "BB" => 3.0,
                "CB" => 2.5,
                "CC" => 2.0,
                "DC" => 1.5,
                "DD" => 1.0,
                "FD" => 0.5,
                "FF" => 0.0,
                _ => throw new ArgumentException("Geçersiz harf notu!"),
            };
        }

        private void UpdateAverages()
        {
            double totalPoints = 0;
            double totalCredits = 0;

            foreach (var course in courses)
            {
                totalPoints += course.Credit * course.Grade;
                totalCredits += course.Credit;
            }

            double semesterAverage = totalPoints / totalCredits;
            SemesterAverageLabel.Text = semesterAverage.ToString("F2");


        }

        private async void OnCalculateBothGpaClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(BothCreditsEntry.Text, out double existingCredits) || existingCredits <= 0)
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir mevcut kredi girin.", "Tamam");
                return;
            }

            if (!double.TryParse(BothGpaEntry.Text, out double existingGpa) || existingGpa < 0 || existingGpa > 4)
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir mevcut ortalama girin.", "Tamam");
                return;
            }

            double totalPoints = existingGpa * existingCredits;
            double totalCredits = existingCredits;

            foreach (var course in courses)
            {
                totalPoints += course.Credit * course.Grade;
                totalCredits += course.Credit;
            }

            double overallGpa = totalPoints / totalCredits;
            BothOverallGpaLabel.Text = overallGpa.ToString("F2");

            UpdateAverages();
        }

        private void OnDeleteCourse(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var course = menuItem?.BindingContext as Course;
            if (course != null)
            {
                courses.Remove(course);
                CoursesListView.ItemsSource = null;
                CoursesListView.ItemsSource = courses;
                UpdateAverages();
            }
        }
    }

    public class Course
    {
        public string Name { get; set; }
        public double Credit { get; set; }
        public double Grade { get; set; }
        public string GradeLetter { get; set; }

        public override string ToString()
        {
            return $"{Name}  Kredi/AKTS: {Credit} Harf Notu: {GradeLetter}";
        }
    }
}


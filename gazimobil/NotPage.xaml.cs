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

            Course course = new Course { Name = courseName, Credit = credit, Grade = grade, GradeLetter= gradeLetter };
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
            switch (gradeLetter)
            {
                case "AA": return 4.0;
                case "BA": return 3.5;
                case "BB": return 3.0;
                case "CB": return 2.5;
                case "CC": return 2.0;
                case "DC": return 1.5;
                case "DD": return 1.0;
                case "FD": return 0.5;
                case "FF": return 0.0;
                default: throw new ArgumentException("Geçersiz harf notu!");
            }
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

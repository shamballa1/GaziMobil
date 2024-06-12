using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace gazimobil
{
    public partial class DersprogramiPage : ContentPage
    {
        private List<Ders> _dersList;
        private readonly List<string> _timeSlots = new List<string>
        {
            "08.30 - 09.20",
            "09.30 - 10.20",
            "10.30 - 11.20",
            "11.30 - 12.20",
            "12.30 - 13.20",
            "13.30 - 14.20",
            "14.30 - 15.20",
            "15.30 - 16.20",
            "16.30 - 17.20",
            "17.30 - 18.20",
            "18.30 - 19.20",
            "19.30 - 20.20",
            "20.30 - 21.20",
            "21.30 - 22.20"
        };

        private readonly List<string> _daysOfWeek = new List<string>
        {
            "Pazartesi",
            "Salý",
            "Çarþamba",
            "Perþembe",
            "Cuma"
        };

        public DersprogramiPage()
        {
            InitializeComponent();
            _dersList = new List<Ders>();
            TimePicker.ItemsSource = _timeSlots; // Picker'a saat aralýklarýný ekleme
            InitializePickers(); // Günleri Picker'a eklemek için InitializePickers metodunu çaðýrýn
        }

        private void InitializePickers()
        {
            // Günlerin Picker'a eklenmesi
            foreach (var day in _daysOfWeek)
            {
                DayPicker.Items.Add(day);
            }
        }

        private void FabButonAtama(object sender, EventArgs e)
        {
            AddLessonFrame.IsVisible = true;
            FabButton.IsVisible = false;
        }

        private void KapamaButonAtama(object sender, EventArgs e)
        {
            AddLessonFrame.IsVisible = false;
            FabButton.IsVisible = true;
        }

        private void EkleButonAtama(object sender, EventArgs e)
        {
            string gun = DayPicker.SelectedItem?.ToString();
            string saat = TimePicker.SelectedItem?.ToString();
            string subject = SubjectEntry.Text;

            if (string.IsNullOrWhiteSpace(gun) || !_daysOfWeek.Contains(gun))
            {
                DisplayAlert("Hata", "Geçersiz gün seçildi.", "Tamam");
                return;
            }

            if (string.IsNullOrWhiteSpace(saat))
            {
                DisplayAlert("Hata", "Lütfen bir saat aralýðý seçin.", "Tamam");
                return;
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                DisplayAlert("Hata", "Ders adý boþ olamaz.", "Tamam");
                return;
            }

            var ders = new Ders
            {
                Day = gun,
                Time = saat,
                Subject = subject
            };

            _dersList.Add(ders);
            UpdateScheduleTable();

            // Formu kapat ve FAB'ý tekrar göster
            AddLessonFrame.IsVisible = false;
            FabButton.IsVisible = true;
        }

        private void UpdateScheduleTable()
        {
            ScheduleTableRoot.Clear();

            // Dersleri kronolojik sýraya göre sýrala
            var siraliList = _dersList
                .OrderBy(d => _daysOfWeek.IndexOf(d.Day))
                .ThenBy(d => _timeSlots.IndexOf(d.Time))
                .ToList();

            foreach (var group in siraliList.GroupBy(d => d.Day))
            {
                var section = new TableSection(group.Key);

                foreach (var ders in group)
                {
                    section.Add(new TextCell
                    {
                        Text = ders.Time,
                        Detail = ders.Subject,
                        TextColor = Microsoft.Maui.Graphics.Colors.Black,
                        DetailColor = Microsoft.Maui.Graphics.Colors.Black
                    });

                    // Çizgi eklemek için bir ViewCell ekle
                    section.Add(new ViewCell
                    {
                        View = new BoxView
                        {
                            HeightRequest = 1,
                            BackgroundColor = Microsoft.Maui.Graphics.Colors.LightGray
                        }
                    });
                }

                ScheduleTableRoot.Add(section);
            }
        }
    }

    public class Ders
    {
        public string Day { get; set; }
        public string Time { get; set; }
        public string Subject { get; set; }
    }
}

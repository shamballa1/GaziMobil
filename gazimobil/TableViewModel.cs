using System.Collections.ObjectModel;

namespace MauiApp1
{
    public class TableViewModel
    {
        public ObservableCollection<DaySchedule> Days { get; set; }

        public TableViewModel()
        {
            Days = new ObservableCollection<DaySchedule>
            {
                new DaySchedule { Time = "08:00 - 09:00", Monday = "Math", Tuesday = "Physics", Wednesday = "Chemistry", Thursday = "Biology", Friday = "History", Saturday = "Free", Sunday = "Free" },
                new DaySchedule { Time = "09:00 - 10:00", Monday = "English", Tuesday = "Geography", Wednesday = "PE", Thursday = "Music", Friday = "Art", Saturday = "Free", Sunday = "Free" },
                new DaySchedule { Time = "10:00 - 11:00", Monday = "Computer Science", Tuesday = "Math", Wednesday = "Physics", Thursday = "Chemistry", Friday = "Biology", Saturday = "Free", Sunday = "Free" },
                new DaySchedule { Time = "11:00 - 12:00", Monday = "History", Tuesday = "English", Wednesday = "Geography", Thursday = "PE", Friday = "Music", Saturday = "Free", Sunday = "Free" }
            };
        }
    }

    public class DaySchedule
    {
        public string Time { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}

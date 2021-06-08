using System;
using System.Linq;

using Newtonsoft.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using static Android.Views.View;

namespace UIDatePicker.Droid
{
    [Activity(NoHistory = true, Theme = "@android:style/Theme.Dialog")]
    public class Picker : Activity, IOnClickListener
    {
        public Picker()
        {
            StartDate = DateTime.Now;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //No title
            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetBackgroundDrawableResource(Resource.Color.transparent);
            SetContentView(Resource.Layout.date_picker);

            _container = FindViewById<LinearLayout>(Resource.Id.container);
            _prev = FindViewById<ImageButton>(Resource.Id.prev);
            _next = FindViewById<ImageButton>(Resource.Id.next);
            _year = FindViewById<TextView>(Resource.Id.year);
            _date = FindViewById<TextView>(Resource.Id.date);
            _title = FindViewById<TextView>(Resource.Id.title);
            _cancel = FindViewById<Button>(Resource.Id.cancel);
            _ok = FindViewById<Button>(Resource.Id.ok);

            _cancel.SetOnClickListener(this);
            _ok.SetOnClickListener(this);
            _prev.SetOnClickListener(this);
            _next.SetOnClickListener(this);


            var extra = Intent.GetStringExtra("cfg");
            if (!string.IsNullOrEmpty(extra))
            {
                PickerConfig cfg = PickerConfig.Deserialize(extra);
                MinDate = cfg.MinDate;
                MaxDate = cfg.MaxDate;
                DaysOfWeek = cfg.DaysOfWeek;
                Options = cfg.Options;
                UsesDaysForRange = cfg.UsesDaysForRange;
            }

            InitializeView();
        }

        #region Elements

        private LinearLayout _container { get; set; }

        private ImageButton _next { get; set; }

        private ImageButton _prev { get; set; }

        private Button _cancel { get; set; }
        
        private Button _ok { get; set; }

        private TextView _year { get; set; }

        private TextView _date { get; set; }

        private TextView _title { get; set; }

        private LayoutInflater _inflater;

        private LayoutInflater Inflater
        {
            get
            {
                if (_inflater == null)
                    _inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;

                return _inflater;
            }
        }

        #endregion

        #region Properties

        public EventHandler OnPick;

        public DayOfWeek[] DaysOfWeek;

        public int CurrentMonth
        {
            get
            {
                return _currentMonth;
            }
            set
            {
                if (value >= 1 && value <= 12)
                    _currentMonth = value;
            }
        }

        public int CurrentYear
        {
            get;

            set;
        }

        public DateTime MaxDate
        {
            get
            {
                return _maxDate;
            }
            set
            {
                if (value == DateTime.MinValue)
                    return;

                if (value < MinDate && MinDate != DateTime.MinValue)
                    return;

                _maxDate = value;
            }
        }
        
        public DateTime MinDate
        {
            get
            {
                return _minDate;
            }
            set
            {
                if (value == DateTime.MinValue)
                    return;

                if (value > MaxDate && MaxDate != DateTime.MinValue)
                    return;

                _minDate = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate == DateTime.MinValue ? DateTime.Now : _startDate;
            }
            set
            {
                if (value == DateTime.MinValue)
                    return;

                if (value > MaxDate && MaxDate != DateTime.MinValue)
                    return;

                if (value < MinDate && MinDate != DateTime.MinValue)
                    return;

                _startDate = value;
                CurrentMonth = _startDate.Month;
                CurrentYear = _startDate.Year;
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return _currentDate == DateTime.MinValue ? DateTime.Now : _currentDate;
            }
            set
            {
                if (value == DateTime.MinValue)
                    return;

                if (value > MaxDate && MaxDate != DateTime.MinValue)
                    return;

                if (value < MinDate && MinDate != DateTime.MinValue)
                    return;

                _currentDate = value;
                CurrentMonth = _currentDate.Month;
                CurrentYear = _currentDate.Year;
            }
        }

        public bool UsesDaysForRange { get; set; } = false;

        public PickerDayOptions Options { get; set; }

        private int _currentMonth;

        private DateTime _startDate;

        private DateTime _maxDate;

        private DateTime _minDate;

        private DateTime _currentDate;

        private PickerDay selectedDay;

        #endregion

        #region Helpers

        public static int DaysInMonth(DateTime month)
        {
            return DateTime.DaysInMonth(month.Year, month.Month);
        }

        #endregion

        #region Methods

        public void IncrementMonth()
        {
            if (this.CurrentMonth == 12)
            {
                this.CurrentMonth = 1;
                this.CurrentYear++;
            }
            else
            {
                this.CurrentMonth++;
            }

            InitializeView();
        }

        public void DecrementMonth()
        {
            if (this.CurrentMonth == 1)
            {
                this.CurrentMonth = 12;
                this.CurrentYear--;
            }
            else
            {
                this.CurrentMonth--;
            }

            InitializeView();
        }

        public void CancelDialog()
        {
            SetResult(Result.Canceled);
            Finish();
        }

        public void SetDate()
        {
            Intent intent = new Intent();
            intent.PutExtra("date", CurrentDate.ToString());
            SetResult(Result.Ok, intent);
            Finish();
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.next)
                IncrementMonth();

            if (v.Id == Resource.Id.prev)
                DecrementMonth();

            if (v.Id == Resource.Id.cancel)
                CancelDialog();

            if (v.Id == Resource.Id.ok)
                SetDate();

        }

        public void InitializeView()
        {
            //dialog size
            Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);


            _container.RemoveAllViews();

            _title.Text = CurrentMonth.MonthFromNumber();
            _year.Text = CurrentYear.ToString();
            _date.Text = CurrentDate.ToString("ddd, MMM dd");
            
            var dayCounter = 1;
            var daysToPrint = DateTime.DaysInMonth(CurrentYear, CurrentMonth);

            //always have six rows
            for (int i = 1; i <= 6; i++)
            {
                var row = Inflater.Inflate(Resource.Layout.calendar_row, null) as LinearLayout;
                var daysToPrepend = 0;
                

                if (i == 1)
                {
                    daysToPrepend = Extensions.DaysToPrepend(CurrentYear, CurrentMonth);
                }

                for (int j = 1; j <= 7; j++)
                {
                    //var date = new DateTime(CurrentYear, CurrentMonth, j * 1);
                    var day = row.FindViewById<PickerDay>(Extensions.IdFromIdx(j)) as PickerDay;
                    
                    if (daysToPrepend > 0)
                    {
                        day.Text = string.Empty;
                        daysToPrepend--;
                    }
                    else
                    {
                        if (daysToPrint > 0)
                        {
                            day.Initialize(dayCounter.ToString(), IsDayEnabled(CurrentYear, CurrentMonth, dayCounter, Options, UsesDaysForRange));
                            dayCounter++;
                            daysToPrint = daysToPrint -1;
                            day.Click += DaySelected;
                        }
                        else
                        {
                            day.Text = string.Empty;
                        }
                    }
                }

                _container.AddView(row);
            }
        }

        public void DaySelected(object sender, EventArgs e)
        {
            var view = (PickerDay)sender;

            if (view == null || !view.IsActive)
                return;

            CurrentDate = new DateTime(CurrentYear, CurrentMonth, Convert.ToInt32(view.Text));

            if (selectedDay != null)
            {
                selectedDay.IsSelected = false;
            }

            view.IsSelected = true;
            selectedDay = view;

            _date.Text = CurrentDate.ToString("ddd, MMM dd");
            _year.Text = CurrentDate.Year.ToString();

            Console.WriteLine(view.Text);
        }

        public bool IsDayEnabled(int Year, int Month, int Day, PickerDayOptions Options, bool UsesDayOptions)
        {
            try
            {
                var day = new DateTime(Year, Month, Day);

                return UsesDaysForRange
                    ? day > MaxDate
                        ? false
                        : day < MinDate
                            ? false
                            : Options.IsWithinRanges(day)
                    : day > MaxDate
                        ? false
                        : day < MinDate
                            ? false
                            : DaysOfWeek.Contains(day.DayOfWeek);
            }
            catch
            {
                return false;
            } 
        }

        #endregion
    }

    public class PickerConfig
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
        public DayOfWeek[] DaysOfWeek { get; set; }
        public EventHandler OnPick { get; set; }

        public bool UsesDaysForRange { get; set; } = false;

        public PickerDayOptions Options { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static PickerConfig Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<PickerConfig>(json);
        }
    }

    public class PickerDayOptions
    {
        public DateTime Cutoff { get; set; }

        public DayOfWeek[] DaysOfWeekBefore { get; set; }

        public DayOfWeek[] DaysOfWeekAfter { get; set; }
    }

    public static class Extensions
    {
        #region PickerHelpers
        
        public static DayOfWeek[] All(this DayOfWeek value)
        {
            return new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
        }

        public static bool IsWithinRanges(this PickerDayOptions options, DateTime date)
        {
            return date >= options.Cutoff 
                ? options.DaysOfWeekAfter.Contains(date.DayOfWeek) 
                : options.DaysOfWeekBefore.Contains(date.DayOfWeek);
        }

        public static PickerConfig WithMinDate(this PickerConfig picker, DateTime MinDate)
        {
            picker.MinDate = MinDate;
            return picker;
        }

        public static PickerConfig WithDaysForRange(this PickerConfig picker, DayOfWeek[] DaysBefore, DayOfWeek[] DaysAfter, DateTime Cutoff)
        {
            picker.UsesDaysForRange = true;

            picker.Options = new PickerDayOptions
            {
                DaysOfWeekAfter = DaysAfter,
                DaysOfWeekBefore = DaysBefore,
                Cutoff = Cutoff
            };
            
            return picker;
        }

        public static PickerConfig WithMaxDate(this PickerConfig picker, DateTime MaxDate)
        {
            picker.MaxDate = MaxDate;
            return picker;
        }

        public static void Pick(this PickerConfig picker, Activity Activity)
        {
            var intent = new Intent(Activity, typeof(Picker));
            intent.PutExtra("cfg", picker.Serialize());
            //Activity.StartActivity(intent);
            Activity.StartActivityForResult(intent, 0);
        }

        public static PickerConfig WithAllowedDays(this PickerConfig picker, DayOfWeek[] Days)
        {
            picker.DaysOfWeek = Days;
            return picker;
        }

        public static int IdFromIdx(int idx)
        {
            switch (idx)
            {
                default:
                case 1:
                    return Resource.Id.sunday;
                case 2:
                    return Resource.Id.monday;
                case 3:
                    return Resource.Id.tuesday;
                case 4:
                    return Resource.Id.wednesday;
                case 5:
                    return Resource.Id.thursday;
                case 6:
                    return Resource.Id.friday;
                case 7:
                    return Resource.Id.saturday;
            }
        }

        #endregion

        #region DateHelpers

        public static int DaysInMonth(int Year, int Month)
        {
            return DateTime.DaysInMonth(Year, Month);
        }

        public static int DaysToPrepend(int Year, int Month)
        {
            return Convert.ToInt32(new DateTime(Year, Month, 1).DayOfWeek);
        }

        public static string MonthFromNumber(this int val)
        {
            switch (val)
            {
                default:
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";

            }
        }

        #endregion
    }
}
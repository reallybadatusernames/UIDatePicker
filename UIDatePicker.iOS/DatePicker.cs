using System;
using System.Collections.Generic;
using System.Linq;

using UIKit;

namespace UIDatePicker.iOS
{
    public partial class DatePicker : UIViewController
    {
        public DatePicker() : base("DatePicker", null)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            monthLabel.Text = DateTime.Now.ToString("MMMM");
            yearLabel.Text = DateTime.Now.ToString("yyyy");
            dateLabel.Text = DateTime.Now.ToString("ddd, MMM dd");
            next.Layer.BorderWidth = 0f;
            back.Layer.BorderWidth = 0f;
            next.Layer.ShadowColor = UIColor.Clear.CGColor;
            back.Layer.ShadowColor = UIColor.Clear.CGColor;
            next.Layer.ShadowOpacity = 0f;
            back.Layer.ShadowOpacity = 0f;
            next.Layer.CornerRadius = 0f;
            back.Layer.CornerRadius = 0f;
            next.Layer.ShadowRadius = 0f;
            back.Layer.ShadowRadius = 0f;
            next.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 0);
            back.Layer.ShadowOffset = new CoreGraphics.CGSize(0, 0);
            this.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
            this.View.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0f);


        }

        private bool _inited = false;

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            InitiateView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        #region Methods 
        private void InitiateView()
        {             var daysToPrint = DateTime.DaysInMonth(CurrentYear, CurrentMonth);

            //setup label row for day abbr.
            var labelRow = new UIView(new CoreGraphics.CGRect(0, 0, dayContainer.Frame.Width, PickerDay.Height));             var labelFont = UIFont.FromName("OpenSans-Semibold", 14f);             for (var i = 0; i <= 6; i++)
            {
                var width = labelRow.Frame.Width / 7;
                var label = new UILabel(new CoreGraphics.CGRect(width * (i), 0, width, PickerDay.Height));
                label.TextColor = UIColor.Gray;
                if (labelFont != null)
                    label.Font = labelFont;
                label.Text = i.AbbrByIdx();
                labelRow.Add(label);
                label.TextAlignment = UITextAlignment.Center;
            }             dayContainer.Add(labelRow);             var dayCounter = 1;             for (int i = 1; i <= 6; i++)
            {                 var row = new UIView(new CoreGraphics.CGRect(0, PickerDay.Height * (i), dayContainer.Frame.Width, PickerDay.Height));                 var daysToPrepend = 0;                  if (i == 1)
                {                     daysToPrepend = DateTimeExtensions.DaysToPrepend(CurrentYear, CurrentMonth);                 }                  List<UIView> days = new List<UIView>();                 for (int j = 1; j <= 7; j++)
                {
                    var width = row.Frame.Width / 7;
                     var day = new PickerDay(dayCounter, IsDayEnabled(CurrentYear, CurrentMonth, dayCounter, Options));                      if (daysToPrepend > 0)
                    {                         day.Text = string.Empty;                         daysToPrepend--;                     }                     else
                    {                         if (daysToPrint > 0)
                        {
                            day.Frame = new CoreGraphics.CGRect(width * (j - 1), 0, row.Frame.Width / 7, PickerDay.Height);                             day.Day = dayCounter;
                            day.Text = dayCounter.ToString();                             dayCounter++;                             daysToPrint--;                             day.TouchUpInside += Day_TouchUpInside;                         }                     }
                    row.Add(day);                     days.Add(day);                 }

                //days.DistributeInParent(row, 5, PickerDay.Height);
                dayContainer.Add(row);

             }
         }

        private void ResizeDialog()
        {
            nfloat height = 0f;

            height += header.Frame.Height;
            height += topControls.Frame.Height;
            height += dayContainer.Frame.Height;
            height += bottomControls.Frame.Height;

            //bottomControls.Frame = new CoreGraphics.CGRect(0, dayContainer.Frame.Y, bottomControls.Frame.Width, bottomControls.Frame.Height);
            pickerDialog.Frame = new CoreGraphics.CGRect(pickerDialog.Frame.X, pickerDialog.Frame.Y, pickerDialog.Frame.Width, height + 30f);
        }

        partial void Ok_TouchUpInside(UIButton sender)
        {
            CompletionHandler?.Invoke();
            DismissViewController(true, null);
        }


        partial void Back_TouchUpInside(UIButton sender)
        {
            DecrementMonth();
        }

        partial void Next_TouchUpInside(UIButton sender)
        {
            IncrementMonth();
        }

        partial void Cancel_TouchUpInside(UIButton sender)
        {
            DismissViewController(true, null);
        }
         private void Day_TouchUpInside(object sender, EventArgs e)         {             var day = (PickerDay)sender;
             if (day.IsEnabled)
            {
                if (SelectedDay != null)
                    SelectedDay.IsActive = false;

                if (day.IsActive)
                {
                    day.IsActive = false;
                    SelectedDay = null;
                }
                else
                {
                    day.IsActive = true;
                    SelectedDay = day;
                }

                //day.IsActive = day.IsActive ? false : true;
                SelectedDate = new DateTime(CurrentYear, CurrentMonth, day.Day);
                dateLabel.Text = SelectedDate.ToString("ddd, MMM dd");
                yearLabel.Text = SelectedDate.Year.ToString();             }         }          public bool IsDayEnabled(int Year, int Month, int Day, PickerDayOptions Options)         {
            try
            {
                var day = new DateTime(Year, Month, Day);

                if (UsesDayOptions)
                {
                    if (day > MaxDate)
                        return false;
                    if (day < MinDate)
                        return false;
                    if (Options.IsWithinRanges(day))
                        return true;

                    return false;
                }
                else
                {
                    if (day > MaxDate)
                        return false;
                    if (day < MinDate)
                        return false;
                    if (DaysOfWeek.Contains(day.DayOfWeek))
                        return true;

                    return false;
                }
            }
            catch
            {
                return false;
            }         } 
        private void ClearDays()
        {
            foreach (var subview in dayContainer.Subviews)
            {
                subview.RemoveFromSuperview();
                subview.Dispose();
            }
        }          public void IncrementMonth()         {
            ClearDays();             if (this.CurrentMonth == 12)             {                 this.CurrentMonth = 1;                 this.CurrentYear++;             }             else             {                 this.CurrentMonth++;             }              InitiateView();         }          public void DecrementMonth()         {
            ClearDays();             if (this.CurrentMonth == 1)             {                 this.CurrentMonth = 12;                 this.CurrentYear--;             }             else             {                 this.CurrentMonth--;             }              InitiateView();         }

        private void SetLabel()
        {
            if (monthLabel != null)
            {
                monthLabel.Text = CurrentMonth.MonthFromNumber();
            }
        }

        #endregion 
        #region Properties 
        private int _currentMonth;

        private int _currentYear;

        private PickerDay SelectedDay;

        public DayOfWeek[] DaysOfWeek { get; set; }          public DateTime MaxDate { get; set; }          public DateTime MinDate { get; set; }          public DateTime SelectedDate { get; set; }          public bool UsesDayOptions { get; set; }          public PickerDayOptions Options { get; set; }          public int CurrentMonth
        {
            get
            {
                return _currentMonth;
            }
            set
            {
                _currentMonth = value;
                SetLabel();
            }
        }
         public int CurrentYear
        {
            get
            {
                return _currentYear;
            }
            set
            {
                _currentYear = value;
                SetLabel();
            }
        }
         public Action CompletionHandler { get; set; }

        #endregion 
        #region Helpers 
        private nfloat WindowWidth
        {             get
            {                 return UIScreen.MainScreen.Bounds.Width;             }         }          private nfloat WindowHeight
        {             get
            {                 return UIScreen.MainScreen.Bounds.Height;             }         }          private nfloat StatusBarHeight
        {             get
            {                 return UIApplication.SharedApplication.StatusBarFrame.Height;             }         }

        #endregion     }      public class PickerDayOptions
    {
        public DateTime Cutoff { get; set; }

        public DayOfWeek[] DaysBefore { get; set; }

        public DayOfWeek[] DaysAfter { get; set; }
    }      public static class PickerHelpers
    {         public static DatePicker CreatePicker()
        {
            return new DatePicker
            {
                CurrentYear = DateTime.Now.Year,
                CurrentMonth = DateTime.Now.Month
            };         }          public static string AbbrByIdx(this int idx) => idx switch
        {
            0 => "Sun",
            1 => "Mon",
            2 => "Tue",
            3 => "Wed",
            4 => "Thu",
            5 => "Fri",
            6 => "Sat",
            _ => string.Empty,
        };          public static DayOfWeek[] All(this DayOfWeek day)
        {
            return new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
        }


        public static bool IsWithinRanges(this PickerDayOptions options, DateTime date)
        {
            return date >= options.Cutoff
                ? options.DaysAfter.Contains(date.DayOfWeek)
                : options.DaysBefore.Contains(date.DayOfWeek);
        }          public static DatePicker WithDayOptions(this DatePicker picker, DateTime cutOff, DayOfWeek[] daysBefore, DayOfWeek[] daysAfter)
        {
            picker.UsesDayOptions = true;
            picker.Options = new PickerDayOptions
            {
                Cutoff = cutOff,
                DaysAfter = daysAfter,
                DaysBefore = daysBefore
            };

            return picker;
        }          public static DatePicker WithDays(this DatePicker picker, DayOfWeek[] Days)
        {             picker.DaysOfWeek = Days;              return picker;         }          public static DatePicker WithMinDate(this DatePicker picker, DateTime minDate)
        {             picker.MinDate = minDate;              return picker;         }          public static DatePicker WithMaxDate(this DatePicker picker, DateTime maxDate)
        {             picker.MaxDate = maxDate;              return picker;         }          public static DatePicker WithCompletionHandler(this DatePicker picker, Action completionHandler)
        {             picker.CompletionHandler = completionHandler;              return picker;         }          public static DatePicker Pick(this DatePicker picker, UIViewController controller, Action completionHandler = null)         {             controller.PresentViewController(picker, true, completionHandler);             return picker;         }     }
}


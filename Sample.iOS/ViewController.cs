using System;

using UIKit;

using UIDatePicker.iOS;

namespace Sample.iOS
{
    public partial class ViewController : UIViewController
    {
        DatePicker picker;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void UIButton197_TouchUpInside(UIButton sender)
        {
            picker = new DatePicker();

            picker = PickerHelpers.CreatePicker()
            .WithDays(new[] { DayOfWeek.Wednesday, DayOfWeek.Saturday })
            .WithMaxDate(DateTime.Now)
            .WithMinDate(DateTime.Now.AddDays(-180))
            .WithCompletionHandler(CompletionHandler)
            .WithDayOptions(new DateTime(2021, 06, 01), new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Thursday }, DayOfWeek.Monday.All())
            .Pick(this);
        }

        private void CompletionHandler() {
            var value = picker.SelectedDate;
        }
    }
}

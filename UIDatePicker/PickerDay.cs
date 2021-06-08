using Android.Util;
using Android.Widget;
using Android.Content;
using Android.Graphics;


namespace UIDatePicker.Droid
{
    public class PickerDay : TextView
    {
        public PickerDay(Context context) : base(context) { }

        public PickerDay(Context context, IAttributeSet attrs) : base(context, attrs) {  }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            SetMeasuredDimension(MeasuredWidth, MeasuredWidth);
        }

        private bool _isActive;

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                ToggleState();
            }
        }

        public void Initialize(string text, bool isActive)
        {
            this.Text = text;
            this.IsActive = isActive;
            this.SetTextColor(IsActive ? Color.Black : Color.LightGray);
        }

        public void ToggleState()
        {
            this.SetBackgroundResource(this.IsSelected ? Resource.Drawable.date_picker_circle : 0);
            this.SetTextColor(this.IsSelected ? Color.White : Color.Black);
        }
    }
}
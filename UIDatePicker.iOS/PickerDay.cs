using System;

using UIKit;
using CoreGraphics;
using CoreAnimation;

namespace UIDatePicker.iOS
{
    public class PickerDay : UIButton
    {
        private UILabel _label;

        private string _text;

        private bool _isEnabled;

        private bool _isActive;

        public string Text {
            get {
                return _text;
            }
            set {
                _text = value;
                if (_label != null)
                    _label.Text = _text;
            }
        }

        public bool IsEnabled {
            get {
                return _isEnabled;
            }
            set {
                _isEnabled = value;
            }
        }

        public bool IsActive {
            get {
                return _isActive;
            }
            set {
                _isActive = value;
                ToggleActive();
            }
        }

        public int Day { get; set; }

        public static nfloat Height {
            get {
                return (UIScreen.MainScreen.Bounds.Width - 40) / 7;
            }
        }

        public PickerDay(int Day, bool isEnabled) : base()
        {
            IsEnabled = isEnabled;

            if (_label == null)
                _label = new UILabel();

            var font = UIFont.FromName("OpenSans-Semibold", 14f);
            if (font != null)
                _label.Font = font;
            _label.TextColor = IsEnabled ? UIColor.Black : UIColor.Gray;
            _label.TextAlignment = UITextAlignment.Center;
            _label.Text = Text;

            AddSubview(_label);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            _label.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
        }

        private void ToggleActive() {

            _label.TextColor = IsActive ? UIColor.White : UIColor.Black;
            if (IsActive) {
                //remove label
                clearLabel();

                //add circle
                var circleLayer = new CAShapeLayer();
                circleLayer.Path = UIBezierPath.FromOval(new CoreGraphics.CGRect(0, 0, 40, 40)).CGPath;
                circleLayer.Position = new CoreGraphics.CGPoint((this.Frame.Width / 2) - 20, (this.Frame.Height / 2) - 20);
                circleLayer.FillColor = UIColor.FromRGB(7,7,78).CGColor;
                this.Layer.AddSublayer(circleLayer);

                //add label
                addLabel();
            }
            else {
                foreach (var sublayer in Layer.Sublayers) {
                    if (sublayer.GetType() == typeof(CAShapeLayer)) {
                        sublayer.RemoveFromSuperLayer();
                    }
                }
            }
        }

        private void clearLabel() {
            if (_label != null)
                _label.RemoveFromSuperview();
        }

        private void addLabel() {
            Add(_label);
        }
    }
}

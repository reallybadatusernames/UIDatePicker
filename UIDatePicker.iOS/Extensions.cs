using System;
using System.Collections.Generic;

using UIKit;
using CoreGraphics;

namespace UIDatePicker.iOS
{
    public static class FrameExtensions
    {
        public static CGPoint CornerTopLeft(this CGRect rect)
        {
            return rect.Location;
        }

        public static CGPoint CornerTopRight(this CGRect rect)
        {
            rect.X += rect.Width;
            return rect.Location;
        }

        public static CGPoint CornerBottomRight(this CGRect rect)
        {
            return new CGPoint(rect.GetMaxX(), rect.GetMaxY());
        }

        public static CGPoint CornerBottomLeft(this CGRect rect)
        {
            return new CGPoint(rect.GetMinX(), rect.GetMaxY());
        }

        /// <summary>
        /// Will center the view in the middle of the view's parent view.
        /// </summary>
        /// <param name="view">View.</param>
        public static void CenterInParent(this UIView view, bool vertically = false)
        {
            if (vertically) {
                view.Frame = new CGRect((view.Superview.Frame.Width / 2) - (view.Frame.Width / 2), (view.Superview.Frame.Height / 2) - (view.Frame.Height / 2), view.Frame.Width, view.Frame.Height);
            }
            else {
                view.Frame = new CGRect((view.Superview.Frame.Width / 2) - (view.Frame.Width / 2), view.Frame.Y, view.Frame.Width, view.Frame.Height);
            }

        }

        /// <summary>
        /// Will make the given view 100% width of parent and set the x origin to 0
        /// </summary>
        /// <param name="view">View.</param>
        public static UIView ConsumeParent(this UIView view, int leftRightPadding = 20)
        {
            view.Frame = new CGRect(leftRightPadding, view.Frame.Y, view.Superview.Frame.Width - (leftRightPadding * 2), view.Frame.Height);
            return view;
        }

        public static UIView ConsumeParentWidth(this UIView view)
        {
            view.Frame = new CGRect(0, view.Frame.Y, view.Superview.Frame.Width, view.Frame.Height);
            return view;
        }

        /// <summary>
        /// Will center the view in the middle of the window. Does not take into consideration the outline of the document.
        /// </summary>
        /// <param name="view">View.</param>
        public static void CenterInWindow(this UIView view)
        {
            view.Frame = new CGRect((WindowWidth / 2) - (view.Frame.Width / 2), view.Frame.Y, view.Frame.Width, view.Frame.Height);
        }

        /// <summary>
        /// Will manipulate the view to take the full width of the window;
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="padding">Amount of space to leave at the begining and trailing end of the element</param>
        public static void DistributeInWindow(this UIView view, double padding = 15)
        {
            view.Frame = new CGRect(padding, view.Frame.Y, WindowWidth - (padding * 2), view.Frame.Height);
        }

        /// <summary>
        /// Will manipulate the view to be the width of the window
        /// </summary>
        /// <param name="view">View.</param>
        public static void SetToWindowWidth(this UIView view)
        {
            view.Frame = new CGRect(0, view.Frame.Y, WindowWidth, view.Frame.Height);
        }

        /// <summary>
        /// Will manipulate a view to be the height of the window.
        /// </summary>
        /// <param name="view">View.</param>
        public static void SetToWindowHeight(this UIView view)
        {
            view.Frame = new CGRect(0, view.Frame.Y, view.Frame.Width, WindowHeight);
        }

        /// <summary>
        /// Squares the view based on view width;
        /// </summary>
        /// <param name="view">View.</param>
        public static void SquareView(this UIView view)
        {
            view.Frame = new CGRect(view.Frame.X, view.Frame.Y, view.Frame.Width, view.Frame.Width);
        }

        /// <summary>
        /// Places the uiview below the ancestor.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="ancestor">Ancestor.</param>
        public static void PlaceBelow(this UIView view, UIView ancestor) {
            view.Frame = new CGRect(view.Frame.X, ancestor.Frame.Y + ancestor.Frame.Height, view.Frame.Width, view.Frame.Height);
        }

        public static void DistributeInParent(this List<UIView> items, UIView parent, nfloat itemPadding, nfloat itemWidth, bool AddToParent = false)
        {
            var startPadding = (parent.Frame.Width - (items.Count * itemWidth)) / 2;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                item.Frame = new CGRect(
                    x: (i * itemWidth + itemPadding) + startPadding,
                    y: item.Frame.Y,
                    width: itemWidth - itemPadding,
                    height: itemWidth - itemPadding
                );

                if (AddToParent)
                    parent.Add(item);
            }
        }

        #region Helpers

        private static nfloat WindowWidth
        {
            get
            {
                return UIScreen.MainScreen.Bounds.Width;
            }
        }

        private static nfloat WindowHeight
        {
            get
            {
                return UIScreen.MainScreen.Bounds.Height;
            }
        }

        #endregion
    }

    public static class DateTimeExtensions {
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

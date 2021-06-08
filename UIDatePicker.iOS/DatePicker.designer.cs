// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace UIDatePicker.iOS
{
    [Register ("DatePicker")]
    partial class DatePicker
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton back { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView bottomControls { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton cancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel dateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView dayContainer { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView header { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel monthLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton next { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ok { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView pickerDialog { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView pickerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView topControls { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel yearLabel { get; set; }

        [Action ("Back_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Back_TouchUpInside (UIKit.UIButton sender);

        [Action ("Cancel_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Cancel_TouchUpInside (UIKit.UIButton sender);

        [Action ("Next_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Next_TouchUpInside (UIKit.UIButton sender);

        [Action ("Ok_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Ok_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (back != null) {
                back.Dispose ();
                back = null;
            }

            if (bottomControls != null) {
                bottomControls.Dispose ();
                bottomControls = null;
            }

            if (cancel != null) {
                cancel.Dispose ();
                cancel = null;
            }

            if (dateLabel != null) {
                dateLabel.Dispose ();
                dateLabel = null;
            }

            if (dayContainer != null) {
                dayContainer.Dispose ();
                dayContainer = null;
            }

            if (header != null) {
                header.Dispose ();
                header = null;
            }

            if (monthLabel != null) {
                monthLabel.Dispose ();
                monthLabel = null;
            }

            if (next != null) {
                next.Dispose ();
                next = null;
            }

            if (ok != null) {
                ok.Dispose ();
                ok = null;
            }

            if (pickerDialog != null) {
                pickerDialog.Dispose ();
                pickerDialog = null;
            }

            if (pickerView != null) {
                pickerView.Dispose ();
                pickerView = null;
            }

            if (topControls != null) {
                topControls.Dispose ();
                topControls = null;
            }

            if (yearLabel != null) {
                yearLabel.Dispose ();
                yearLabel = null;
            }
        }
    }
}
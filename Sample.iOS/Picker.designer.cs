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
    [Register ("Picker")]
    partial class Picker
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel date { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView dayContainer { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView header { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel month { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView parentView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView pickerDialog { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView topControls { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel year { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (date != null) {
                date.Dispose ();
                date = null;
            }

            if (dayContainer != null) {
                dayContainer.Dispose ();
                dayContainer = null;
            }

            if (header != null) {
                header.Dispose ();
                header = null;
            }

            if (month != null) {
                month.Dispose ();
                month = null;
            }

            if (parentView != null) {
                parentView.Dispose ();
                parentView = null;
            }

            if (pickerDialog != null) {
                pickerDialog.Dispose ();
                pickerDialog = null;
            }

            if (topControls != null) {
                topControls.Dispose ();
                topControls = null;
            }

            if (year != null) {
                year.Dispose ();
                year = null;
            }
        }
    }
}
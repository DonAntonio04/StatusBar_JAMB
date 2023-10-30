using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CurrentActivity;
using StatusBar_JAMB.Droid.statusbar;
using StatusBar_JAMB.VistaModelo;
using Xamarin.Forms;

[assembly:Dependency(typeof(StatusBar))]

namespace StatusBar_JAMB.Droid.statusbar
{
    internal class StatusBar : VMstatusbar
    {
        WindowManagerFlags _originalFlags;

        Window GetCrurrentwindow()
        {
            var windows = CrossCurrentActivity.Current.Activity.Window;
            windows.ClearFlags(WindowManagerFlags.TranslucentStatus);
            windows.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
           return windows;
        }

        public void CambiarColor()
        {
            if(Build.VERSION.SdkInt>= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCrurrentwindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFlags;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Rgb(18, 18, 18));
                });

                
            }
        }

        public void MostrarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }

        public void OcultarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        public void Transparente()
        {
            MostrarStatusBar();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCrurrentwindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });


            }
        }

        public void Traslucido()
        {
            MostrarStatusBar();
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.TranslucentStatus;
            activity.Window.Attributes = attrs;
        }
    }
}
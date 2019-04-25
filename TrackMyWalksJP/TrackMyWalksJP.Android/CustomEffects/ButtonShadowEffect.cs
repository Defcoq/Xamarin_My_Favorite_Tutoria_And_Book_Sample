using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System;

[assembly: ResolutionGroupName("GeniesoftStudios")]
[assembly: ExportEffect(typeof(TrackMyWalksJP.Droid.CustomEffects.ButtonShadowEffect), "ButtonShadowEffect")]
namespace TrackMyWalksJP.Droid.CustomEffects
{
    public class ButtonShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as Android.Widget.Button;
                Android.Graphics.Color color = Android.Graphics.Color.Red;
                control.SetShadowLayer(12, 4, 4, color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        protected override void OnDetached()
        {
           // throw new NotImplementedException();
        }
    }
}
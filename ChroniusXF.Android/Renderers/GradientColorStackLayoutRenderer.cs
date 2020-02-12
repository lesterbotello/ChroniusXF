using System;
using Android.Content;
using ChroniusXF.Droid.Renderers;
using ChroniusXF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportRenderer(typeof(GradientColorStackLayout), typeof(GradientColorStackLayoutRenderer))]
namespace ChroniusXF.Droid.Renderers
{
    public class GradientColorStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        private Xamarin.Forms.Color StartColor { get; set; }
        private Xamarin.Forms.Color EndColor { get; set; }

        public GradientColorStackLayoutRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            var gradient = new LinearGradient(
                0,
                0,
                0,
                Height,
                StartColor.ToAndroid(),
                EndColor.ToAndroid(),
                Shader.TileMode.Mirror);

            var paint = new Paint() { Dither = true };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientColorStackLayout;
                StartColor = stack.StartColor;
                EndColor = stack.EndColor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GradientColorStackLayout: Unable to render element: {ex.Message}");
            }
        }
    }
}

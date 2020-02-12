using System;
using ChroniusXF.iOS.Renderers;
using ChroniusXF.Renderers;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientColorStackLayout), typeof(GradientColorStackLayoutRenderer))]
namespace ChroniusXF.iOS.Renderers
{
    public class GradientColorStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        public GradientColorStackLayoutRenderer()
        {
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var stack = Element as GradientColorStackLayout;
            var startColor = stack.StartColor.ToCGColor();
            var endColor = stack.EndColor.ToCGColor();
            var gradientLayer = new CAGradientLayer();
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };
            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}

using System;
using System.Drawing;
using ChroniusXF.iOS.Renderers;
using ChroniusXF.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconifiedDatePicker), typeof(IconifiedDatePickerRenderer))]
namespace ChroniusXF.iOS.Renderers
{
    public class IconifiedDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (e.NewElement is IconifiedDatePicker element)
                {
                    Control.BackgroundColor = UIColor.White;
                    Control.BorderStyle = UITextBorderStyle.RoundedRect;
                    Control.Layer.CornerRadius = 17f;
                    Control.LeftView = new UIView(new RectangleF(0, 10, 12, 12));
                    Control.LeftViewMode = UITextFieldViewMode.Always;
                    Control.Layer.MasksToBounds = true;

                    if (element.Icon != "")
                    {
                        var img = UIImage.FromBundle(element.Icon);
                        var imgView = new UIImageView(img);

                        var recognizer = new UITapGestureRecognizer(_ =>
                        {
                            element.SendIconClicked();
                        });

                        imgView.UserInteractionEnabled = true;
                        imgView.AddGestureRecognizer(recognizer);

                        Control.LeftViewMode = UITextFieldViewMode.Always;
                        Control.LeftView = imgView;
                    }
                }
            }
        }
    }
}

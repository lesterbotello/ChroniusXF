using System;
using System.Drawing;
using ChroniusXF.iOS.Renderers;
using ChroniusXF.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconifiedEntry), typeof(IconifiedEntryRenderer))]
namespace ChroniusXF.iOS.Renderers
{
    public class IconifiedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (e.NewElement is IconifiedEntry element)
                {
                    Control.BackgroundColor = UIColor.White;
                    Control.Layer.CornerRadius = 17f;
                    Control.LeftView = new UIView(new RectangleF(0, 10, 12, 12));
                    Control.LeftViewMode = UITextFieldViewMode.Always;

                    if (element.Icon != "")
                    {
                        var img = UIImage.FromBundle(element.Icon);
                        var imgView = new UIImageView(img);

                        var recognizer = new UITapGestureRecognizer((obj) =>
                        {
                            element.SendIconClicked();
                        });

                        imgView.UserInteractionEnabled = true;
                        imgView.AddGestureRecognizer(recognizer);

                        if (element.IconPosition == IconPosition.Left)
                        {
                            Control.LeftViewMode = UITextFieldViewMode.Always;
                            Control.LeftView = imgView;
                        }
                        else
                        {
                            Control.RightViewMode = UITextFieldViewMode.Always;
                            Control.RightView = imgView;
                        }
                    }
                }
            }
        }
        public IconifiedEntryRenderer()
        {
        }
    }
}

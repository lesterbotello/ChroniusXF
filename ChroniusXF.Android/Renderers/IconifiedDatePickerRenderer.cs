using System;
using Android.Content;
using Android.Views;
using ChroniusXF.Droid.Renderers;
using ChroniusXF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IconifiedDatePicker), typeof(IconifiedDatePickerRenderer))]
namespace ChroniusXF.Droid.Renderers
{
    public class IconifiedDatePickerRenderer : DatePickerRenderer
    {
        public IconifiedDatePickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null) return;
            if (Control != null)
            {
                var element = e.NewElement as IconifiedDatePicker;
                if (element.Icon != null)
                {
                    try
                    {
                        Control.SetBackgroundResource(Resource.Drawable.rounded_entry_border);
                        var img = Context.GetDrawable(Resources.GetIdentifier(element.Icon, "drawable", Context.PackageName));
                        if (img != null)
                        {
                            Control.SetCompoundDrawablesWithIntrinsicBounds(img, null, null, null);
                            Control.CompoundDrawablePadding = 25;
                            Control.Touch += (sender, ev) => {
                                var drawableIndex = element.IconPosition == IconPosition.Left ? 0 : 2;

                                if (ev.Event.Action == MotionEventActions.Up
                                   && ev.Event.GetX() > Control.Width - Control.GetCompoundDrawables()[drawableIndex].Bounds.Width())
                                {
                                    element.SendIconClicked();
                                    ev.Handled = true;
                                }
                                else
                                    ev.Handled = false;
                            };
                        }
                    }
                    catch (Android.Content.Res.Resources.NotFoundException)
                    {
                        // TODO: Set empty border resource...
                        Control.SetBackgroundResource(Resource.Drawable.empty_rounded_entry_border);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }
    }
}

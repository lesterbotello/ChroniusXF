using System;
using Android.Content;
using Android.Views;
using ChroniusXF.Droid.Renderers;
using ChroniusXF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IconifiedEntry), typeof(IconifiedEntryRenderer))]
namespace ChroniusXF.Droid.Renderers
{
    public class IconifiedEntryRenderer : EntryRenderer
    {
        public IconifiedEntryRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null) return;
            if (Control != null)
            {
                var element = e.NewElement as IconifiedEntry;
                if(element.Icon != null)
                {
                    if (!string.IsNullOrEmpty(element.Icon))
                    {
                        try
                        {
                            if (element.IconPosition == IconPosition.Left)
                                Control.SetBackgroundResource(Resource.Drawable.rounded_entry_border);
                            else
                                Control.SetBackgroundResource(Resource.Drawable.rounded_entry_border_inv);

                            var img = Context.GetDrawable(Resources.GetIdentifier(element.Icon, "drawable", Context.PackageName));
                            if (img != null)
                            {
                                if (element.IconPosition == IconPosition.Left)
                                    Control.SetCompoundDrawablesWithIntrinsicBounds(img, null, null, null);
                                else
                                    Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, img, null);

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
                    else
                        Control.SetBackgroundResource(Resource.Drawable.empty_rounded_entry_border);
                }
            }
        }

        // TODO: Check if this will ever be useful
        //float DpToPx(Context context, int v)
        //{
        //    var x = Math.Round(v / context.Resources.DisplayMetrics.Xdpi / 160);
        //    return Convert.ToSingle(x);
        //}
    }
}

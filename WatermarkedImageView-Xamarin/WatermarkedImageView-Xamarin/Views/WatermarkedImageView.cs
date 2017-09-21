using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using Android.Graphics;

namespace WatermarkedImageView_Xamarin.Views
{
    [Register("com.jubleo.WatermarkedImageView")] // (Optional) Register with custom name for using in xml
    public class WatermarkedImageView : ImageView
    {
        private float TextPadding = 18f;
        private string WatermarkText = "WatermarkedImageView";
        private float WatermarkTextSize = 48f;
        private WatermarkPosition WatermarkPosition;

        public WatermarkedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            TypedArray typedArray = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.CustomImageView, 0, 0);

            try
            {
                //Retrieve assigned attributes
                WatermarkText = typedArray.GetString(Resource.Styleable.CustomImageView_watermarkText);
                WatermarkPosition = (WatermarkPosition)typedArray.GetInteger(Resource.Styleable.CustomImageView_watermarkPosition, 0);
                WatermarkTextSize = typedArray.GetDimension(Resource.Styleable.CustomImageView_watermarkTextSize, 48f);

                typedArray.Recycle();
            }
            catch (Exception e)
            {
                Log.Error("WatermarkedImageView", e.Message);
            }
        }

        public WatermarkedImageView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            Paint TextPaint = new Paint(PaintFlags.AntiAlias)
            {
                Color = Color.White,
                TextAlign = Paint.Align.Center,
                TextSize = WatermarkTextSize,
                Alpha = 128
            };


            Rect textBounds = new Rect();
            TextPaint.GetTextBounds(WatermarkText, 0, WatermarkText.Length, textBounds);

            float textWidth = textBounds.Width() * 1f;
            float textHeight = textBounds.Height() * 1f;
            float canvasWidth = canvas.Width * 1f;
            float canvasHeight = canvas.Height * 1f;


            RectF backgroundRectangle = new RectF();
            PointF textPosition = new PointF();
            switch (WatermarkPosition)
            {
                case WatermarkPosition.Top:
                    textPosition = new PointF(canvasWidth / 2f, textHeight + TextPadding);
                    break;
                case WatermarkPosition.Bottom:
                    textPosition = new PointF(canvasWidth / 2f, canvasHeight - TextPadding);
                    break;
                case WatermarkPosition.Left:
                    textPosition = new PointF(canvasWidth - textHeight - TextPadding, canvasHeight / 2f + textWidth / 2f);
                    canvas.Rotate(90, textPosition.X, textPosition.Y);
                    break;
                case WatermarkPosition.Right:
                    textPosition = new PointF(TextPadding + textHeight, canvasHeight / 2f - textWidth / 2f);
                    canvas.Rotate(-90, textPosition.X, textPosition.Y);
                    break;
            }

            //draws text
            canvas.DrawText(WatermarkText, 0, WatermarkText.Length, textPosition.X, textPosition.Y, TextPaint);
        }
    }

    public enum WatermarkPosition
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3
    }
}
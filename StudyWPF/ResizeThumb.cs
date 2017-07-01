using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Input;
using System.Collections.Generic;

namespace StudyWPF
{
    class ResizeThumb : Thumb
    {
        private RotateTransform rotateTransform;
        private double angle;
       // private Adorner adorner;
        private Point transformOrigin;
        private FrameworkElement designerItem;
        private Canvas canvas;

        public ResizeThumb(FrameworkElement designerItem)
        {
            this.designerItem = designerItem;
            DragStarted += new DragStartedEventHandler(this.ResizeThumb_DragStarted);
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
            //DragCompleted += new DragCompletedEventHandler(this.ResizeThumb_DragCompleted);
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            //this.designerItem = this.DataContext as ContentControl;

            if (this.designerItem != null)
            {
                this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;

                if (this.canvas != null)
                {
                    this.transformOrigin = this.designerItem.RenderTransformOrigin;

                    this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                    if (this.rotateTransform != null)
                    {
                        this.angle = this.rotateTransform.Angle * Math.PI / 180.0;
                    }
                    else
                    {
                        this.angle = 0.0d;
                    }

                    //AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.canvas);
                    //if (adornerLayer != null)
                    //{
                    //    this.adorner = new SizeAdorner(this.designerItem);
                    //    adornerLayer.Add(this.adorner);
                    //}
                }
            }
        }

        /// <summary>
        /// Checks the values so that the ratio beween them has a defined value.
        /// </summary>
        /// <param name="dragDeltaHorizontal">horizontal delta</param>
        /// <param name="dragDeltaVertical">vertical delta</param>
        /// <param name="aspectRatio">horizontal to vertical ration</param>
        private void CheckAspectRatio(ref double? dragDeltaHorizontal, ref double? dragDeltaVertical, double aspectRatio)
        {
            double? dragValue = null;
            if (dragDeltaVertical.HasValue && dragDeltaHorizontal.HasValue)
            {
                dragValue = Math.Max(dragDeltaVertical.Value, dragDeltaHorizontal.Value);
            }
            else if (dragDeltaVertical.HasValue)
            {
                dragValue = dragDeltaVertical;
            }
            else if (dragDeltaHorizontal.HasValue)
            {
                dragValue = dragDeltaHorizontal;
            }

            if (dragValue.HasValue)
            {
                dragDeltaVertical = dragValue.Value * aspectRatio;
                dragDeltaHorizontal = dragValue;
            }
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.designerItem == null)
            {
                return;
            }

            var item = this.designerItem;
            var minLeft = double.MaxValue;
            var minTop = double.MaxValue;
            var minDeltaHorizontal = double.MaxValue;
            var minDeltaVertical = double.MaxValue;

            minLeft = Math.Min(Canvas.GetLeft(item), minLeft);
            minTop = Math.Min(Canvas.GetTop(item), minTop);

            minDeltaVertical = Math.Min(minDeltaVertical, item.ActualHeight - item.MinHeight);
            minDeltaHorizontal = Math.Min(minDeltaHorizontal, item.ActualWidth - item.MinWidth);

            // stop moving when
            // at least one of the selected items is locked
            //if (item.IsLocked)
            //{
            //    return;
            //}

            double? dragDeltaVertical = null;
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    dragDeltaVertical = Math.Min(-e.VerticalChange, minDeltaVertical);
                    break;
                case VerticalAlignment.Top:
                    dragDeltaVertical = Math.Min(Math.Max(-minTop, e.VerticalChange), minDeltaVertical);
                    break;
            }

            double? dragDeltaHorizontal = null;
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    dragDeltaHorizontal = Math.Min(Math.Max(-minLeft, e.HorizontalChange), minDeltaHorizontal);
                    break;
                case HorizontalAlignment.Right:
                    dragDeltaHorizontal = Math.Min(-e.HorizontalChange, minDeltaHorizontal);
                    break;
            }

            // in case the aspect ratio is kept then adjust both width and height
            //if (item.KeepAspectRatio)
            {
                CheckAspectRatio(ref dragDeltaHorizontal, ref dragDeltaVertical, item.ActualHeight / item.ActualWidth);
            }

            if (dragDeltaVertical.HasValue)
            {
                switch (VerticalAlignment)
                {
                    case System.Windows.VerticalAlignment.Bottom:
                        Canvas.SetTop(item, Canvas.GetTop(item) + (this.transformOrigin.Y * dragDeltaVertical.Value * (1 - Math.Cos(-this.angle))));
                        Canvas.SetLeft(item, Canvas.GetLeft(item) - dragDeltaVertical.Value * this.transformOrigin.Y * Math.Sin(-this.angle));
                        break;
                    case System.Windows.VerticalAlignment.Top:
                        Canvas.SetTop(item, Canvas.GetTop(item) + dragDeltaVertical.Value * Math.Cos(-this.angle) + (this.transformOrigin.Y * dragDeltaVertical.Value * (1 - Math.Cos(-this.angle))));
                        Canvas.SetLeft(item, Canvas.GetLeft(item) + dragDeltaVertical.Value * Math.Sin(-this.angle) - (this.transformOrigin.Y * dragDeltaVertical.Value * Math.Sin(-this.angle)));
                        break;
                    default:
                        break;
                }

                item.Height = item.ActualHeight - dragDeltaVertical.Value;
            }

            if (dragDeltaHorizontal.HasValue)
            {
                switch (HorizontalAlignment)
                {
                    case System.Windows.HorizontalAlignment.Left:
                        Canvas.SetTop(item, Canvas.GetTop(item) + dragDeltaHorizontal.Value * Math.Sin(this.angle) - this.transformOrigin.X * dragDeltaHorizontal.Value * Math.Sin(this.angle));
                        Canvas.SetLeft(item, Canvas.GetLeft(item) + dragDeltaHorizontal.Value * Math.Cos(this.angle) + (this.transformOrigin.X * dragDeltaHorizontal.Value * (1 - Math.Cos(this.angle))));
                        break;
                    case System.Windows.HorizontalAlignment.Right:
                        Canvas.SetTop(item, Canvas.GetTop(item) - this.transformOrigin.X * dragDeltaHorizontal.Value * Math.Sin(this.angle));
                        Canvas.SetLeft(item, Canvas.GetLeft(item) + (dragDeltaHorizontal.Value * this.transformOrigin.X * (1 - Math.Cos(this.angle))));
                        break;
                    default:
                        break;
                }

                item.Width = item.ActualWidth - dragDeltaHorizontal.Value;
            }
            e.Handled = true;
        }

        //private void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        //{
        //    if (this.adorner != null)
        //    {
        //        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.canvas);
        //        if (adornerLayer != null)
        //        {
        //            adornerLayer.Remove(this.adorner);
        //        }

        //        this.adorner = null;
        //    }
        //}
    }
}

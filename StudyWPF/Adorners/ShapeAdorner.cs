using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace StudyWPF.Adorners
{
    class ShapeAdorner : Adorner
    {
        ResizeThumb _topLeft, _topRight, _bottomLeft, _bottomRight;

        // To store and manage the adorner's visual children.
        VisualCollection _visualChildren;

        // Constructor
        public ShapeAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            _visualChildren = new VisualCollection(this);

            // Call a helper method to initialize the Thumbs
            // with a customized cursors.
            BuildAdornerCorner(ref _topLeft, Cursors.SizeNWSE);
            BuildAdornerCorner(ref _topRight, Cursors.SizeNESW);
            BuildAdornerCorner(ref _bottomLeft, Cursors.SizeNESW);
            BuildAdornerCorner(ref _bottomRight, Cursors.SizeNWSE);

            //_topLeft.VerticalAlignment = VerticalAlignment.Top;
            //_topLeft.HorizontalAlignment = HorizontalAlignment.Left;

            //_topRight.VerticalAlignment = VerticalAlignment.Top;
            //_topRight.HorizontalAlignment = HorizontalAlignment.Right;

            //_bottomLeft.VerticalAlignment = VerticalAlignment.Bottom;
            //_bottomLeft.HorizontalAlignment = HorizontalAlignment.Left;

            //_bottomRight.VerticalAlignment = VerticalAlignment.Bottom;
            //_bottomRight.HorizontalAlignment = HorizontalAlignment.Right;
        }

        // http://stackoverflow.com/questions/11660672/how-can-i-resize-a-rotated-control-without-change-the-aspect-ratio-based-on-the


        // Arrange the Adorners.
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the element that's being adorned.  
            // These will be used to place the ResizingAdorner at the corners of the adorned element.  
            double desiredWidth = AdornedElement.DesiredSize.Width;
            double desiredHeight = AdornedElement.DesiredSize.Height;
            // adornerWidth & adornerHeight are used for placement as well.
            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;

            _topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            _topRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            _bottomLeft.Arrange(new Rect(-adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));
            _bottomRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            // Return the final size.
            return finalSize;
        }

        // Helper method to instantiate the corner Thumbs, set the Cursor property, 
        // set some appearance properties, and add the elements to the visual tree.
        void BuildAdornerCorner(ref ResizeThumb cornerThumb, Cursor customizedCursor)
        {
            if (cornerThumb != null) return;

            cornerThumb = new ResizeThumb(this.AdornedElement as FrameworkElement);

            // Set some arbitrary visual characteristics.
            cornerThumb.Cursor = customizedCursor;
            cornerThumb.Height = cornerThumb.Width = 10;
            cornerThumb.Opacity = 0.40;
            cornerThumb.Background = new SolidColorBrush(Colors.MediumBlue);

            _visualChildren.Add(cornerThumb);
        }

        // This method ensures that the Widths and Heights are initialized.  Sizing to content produces
        // Width and Height values of Double.NaN.  Because this Adorner explicitly resizes, the Width and Height
        // need to be set first.  It also sets the maximum size of the adorned element.
        void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }
        // Override the VisualChildrenCount and GetVisualChild properties to interface with 
        // the adorner's visual collection.
        protected override int VisualChildrenCount { get { return _visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return _visualChildren[index]; }
    }
}

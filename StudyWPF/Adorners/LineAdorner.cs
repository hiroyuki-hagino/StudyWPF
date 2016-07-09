using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace StudyWPF.Adorners
{
    /// <summary>
    /// 
    /// </summary>
    class LineAdorner : Adorner
    {
        private Point _start;
        private Point _end;
        private Thumb _startThumb;
        private Thumb _endThumb;
        private Line _selectedLine;
        private VisualCollection visualChildren;

        // Constructor
        public LineAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);

            //startThumb = new Thumb { Cursor = Cursors.Hand, Width = 10, Height = 10, Background = Brushes.Green };
            //endThumb = new Thumb { Cursor = Cursors.Hand, Width = 10, Height = 10, Background = Brushes.BlueViolet };
            _startThumb = new Thumb { Cursor = Cursors.Hand, Width = 10, Height = 10 };
            _endThumb = new Thumb { Cursor = Cursors.Hand, Width = 10, Height = 10 };

            _startThumb.DragDelta += StartDragDelta;
            _endThumb.DragDelta += EndDragDelta;

            visualChildren.Add(_startThumb);
            visualChildren.Add(_endThumb);

            _selectedLine = AdornedElement as Line;
        }

        // Event for the Thumb Start Point
        private void StartDragDelta(object sender, DragDeltaEventArgs e)
        {
            Point position = Mouse.GetPosition(this);

            _selectedLine.X1 = position.X;
            _selectedLine.Y1 = position.Y;
        }

        // Event for the Thumb End Point
        private void EndDragDelta(object sender, DragDeltaEventArgs e)
        {
            Point position = Mouse.GetPosition(this);

            _selectedLine.X2 = position.X;
            _selectedLine.Y2 = position.Y;
        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (AdornedElement is Line)
            {
                _selectedLine = AdornedElement as Line;
                _start = new Point(_selectedLine.X1, _selectedLine.Y1);
                _end = new Point(_selectedLine.X2, _selectedLine.Y2);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _selectedLine = AdornedElement as Line;

            double left = Math.Min(_selectedLine.X1, _selectedLine.X2);
            double top = Math.Min(_selectedLine.Y1, _selectedLine.Y2);

            var startRect = new Rect(_selectedLine.X1 - (_startThumb.Width / 2), _selectedLine.Y1 - (_startThumb.Width / 2), _startThumb.Width, _startThumb.Height);
            _startThumb.Arrange(startRect);

            var endRect = new Rect(_selectedLine.X2 - (_endThumb.Width / 2), _selectedLine.Y2 - (_endThumb.Height / 2), _endThumb.Width, _endThumb.Height);
            _endThumb.Arrange(endRect);

            return finalSize;
        }
    }
}

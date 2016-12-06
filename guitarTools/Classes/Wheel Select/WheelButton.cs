using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace guitarTools.Classes.Wheel_Select
{
    class WheelButton
    {
        private double w = 80;
        private double h = 55;

        public WheelButton(Viewbox viewbox)
        {
            Grid container = new Grid()
            {
                Width = w,
                Height = h
            };

            Path shape = new Path()
            {
                Data = DrawButton(),
                Fill = Brushes.Red,
                Stroke = Brushes.White,
                StrokeThickness = 0
            };

            Path cutout = new Path()
            {
                Data = DrawCutout(),
                Fill = Brushes.White,
                Stroke = Brushes.White,
                StrokeThickness = 0
            };

            Label label = new Label()
            {
                Content = "C#",
                Foreground = Brushes.White,
                FontSize = h * 0.5,

                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            
            container.Children.Add(shape);           
            container.Children.Add(cutout);
            container.Children.Add(label);

            viewbox.Child = container;
        }

        private PathGeometry DrawButton()
        {
            ArcSegment arcTop = new ArcSegment()
            {
                Point = new Point(0, 0),
                Size = new Size(60, 60)
            };

            PointCollection coords = new PointCollection();
            coords.Add(new Point(0,0));
            coords.Add(new Point(w, 0));
            coords.Add(new Point(w-w*0.3, h));

            PolyLineSegment rectangle = new PolyLineSegment() { Points = coords };

            PathSegmentCollection psColArcTop = new PathSegmentCollection();
            psColArcTop.Add(arcTop);

            PathSegmentCollection psColRec = new PathSegmentCollection();
            psColRec.Add(rectangle);

            PathFigure pathFigureArc = new PathFigure()
            {
                Segments = psColArcTop,
                StartPoint = new Point(w,0)
            };

            PathFigure pathFigureRec = new PathFigure()
            {               
                Segments = psColRec,
                StartPoint = new Point(w*0.3, h)
            };

            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            pathFigureCollection.Add(pathFigureArc);          
            pathFigureCollection.Add(pathFigureRec);

            PathGeometry pathGeometry = new PathGeometry() { Figures = pathFigureCollection };

            return pathGeometry;
        }

        private PathGeometry DrawCutout()
        {
            ArcSegment bottomArc = new ArcSegment()
            {
                Point = new Point(w * 0.3, h),
                Size = new Size(60, 60),
            };

            PathSegmentCollection psColArcBot = new PathSegmentCollection();
            psColArcBot.Add(bottomArc);

            PathFigure pathFigureArc = new PathFigure()
            {
                Segments = psColArcBot,
                StartPoint = new Point(w - w * 0.3, h)
            };

            PathFigureCollection pathFigureCollection = new PathFigureCollection();
            pathFigureCollection.Add(pathFigureArc);

            PathGeometry pathGeometry = new PathGeometry()
            {
                Figures = pathFigureCollection
            };

            return pathGeometry;
        }


    }
}

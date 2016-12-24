using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace guitarTools.Classes.Wheel_Select
{
    // DOC Write documentation for WheelButton class

    class WheelButton
    {
        private double width = 80;
        private double height = 55;
        public string LabelContent { get; set; }

        public WheelButton()
        {
            Grid container = new Grid()
            {
                Width = width,
                Height = height
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

            // UNDONE Place label vertically center
            Label label = new Label()
            {
                Content = LabelContent,
                Foreground = Brushes.White,
                FontSize = height * 0.5,

                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            
            container.Children.Add(shape);           
            container.Children.Add(cutout);
            container.Children.Add(label);       
        }

        // UNDONE Move arc down the Y axis to fit grid container
        private PathGeometry DrawButton()
        {
            // UNDONE Calculate correct arc size
            ArcSegment arcTop = new ArcSegment()
            {
                Point = new Point(0, 0),
                Size = new Size(60, 60)
            };

            PointCollection coords = new PointCollection();
            coords.Add(new Point(0,0));
            coords.Add(new Point(width, 0));
            coords.Add(new Point(width-width*0.3, height));

            PolyLineSegment rectangle = new PolyLineSegment() { Points = coords };

            PathSegmentCollection psColArcTop = new PathSegmentCollection();
            psColArcTop.Add(arcTop);

            PathSegmentCollection psColRec = new PathSegmentCollection();
            psColRec.Add(rectangle);

            PathFigure pathFigureArc = new PathFigure()
            {
                Segments = psColArcTop,
                StartPoint = new Point(width,0)
            };

            PathFigure pathFigureRec = new PathFigure()
            {               
                Segments = psColRec,
                StartPoint = new Point(width*0.3, height)
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
                Point = new Point(width * 0.3, height),
                Size = new Size(60, 60),
            };

            PathSegmentCollection psColArcBot = new PathSegmentCollection();
            psColArcBot.Add(bottomArc);

            PathFigure pathFigureArc = new PathFigure()
            {
                Segments = psColArcBot,
                StartPoint = new Point(width - width * 0.3, height)
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

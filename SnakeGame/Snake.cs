using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame
{
    class Snake
    {
        public double x, y;
        public Ellipse ellSnake = new Ellipse();

        public Snake(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
        public void SetSnakePosition()
        {
            ellSnake.Width = 12;
            ellSnake.Height = 12;
            ellSnake.Fill = Brushes.Red;
            Canvas.SetLeft(ellSnake, x);
            Canvas.SetTop(ellSnake, y);
        }
    }
}

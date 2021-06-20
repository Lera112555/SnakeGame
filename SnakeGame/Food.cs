using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace SnakeGame
{
    class Food
    {
        public double x,y;
        public Ellipse ellFood = new Ellipse();
        public Food(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
        public void SetFoodPosition()
        {
            ellFood.Width = 8;
            ellFood.Height = 10;
            ellFood.Fill = Brushes.Yellow;
            Canvas.SetLeft(ellFood, x);
            Canvas.SetTop(ellFood,y);
        }
    }
}

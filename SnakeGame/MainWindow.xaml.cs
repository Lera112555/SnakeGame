using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer time;
        readonly List<Snake> snakebody;
        private readonly List<Food> food;
        private readonly Random rd = new Random();
        private double x = 100;
        double y = 100;
        private int direction = 0;
        private readonly int left = 4;
        private readonly int right = 6;
        private readonly int up = 8;
        private readonly int down = 2;
        private int score = 0;
        private int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            time = new DispatcherTimer();
            snakebody = new List<Snake>();
            food = new List<Food>();
            snakebody.Add(new Snake(x, y));
            food.Add(new Food(rd.Next(0, 37) * 10, rd.Next(0, 35) * 10));
            time.Interval = new TimeSpan(0, 0, 0, 0, 50);   //изменение скорости змейки
            time.Tick += Time_Tick;
        }

        private void AddFoodinCanvas()
        {
            food[0].SetFoodPosition();
            mycanvas.Children.Insert(0,food[0].ellFood);
        }

        private void AddSnakeinCanvas()
        {
            foreach (Snake snake in snakebody)
            {
                snake.SetSnakePosition();
                mycanvas.Children.Add(snake.ellSnake);
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            if (direction != 0)
            {
                for (int i = snakebody.Count - 1; i > 0; i--)
                {
                    snakebody[i] = snakebody[i - 1];
                }
            }

            MoveFreeSnake();

            if (snakebody[0].x == food[0].x && snakebody[0].y == food[0].y)
            {
                snakebody.Add(new Snake(food[0].x, food[0].y));
                food[0] = new Food(rd.Next(0, 37) * 10, rd.Next(0, 35) * 10);
                mycanvas.Children.RemoveAt(0);
                AddFoodinCanvas();
                score++;
                txtScore.Text = score.ToString();
            }


            snakebody[0] = new Snake(x, y); // увеличение змеи 

            if (snakebody[0].x > 370 || snakebody[0].y > 350 || snakebody[0].x < 0 || snakebody[0].y < 0)
            {
                this.Close();
            }

            for (var i = 1; i < snakebody.Count; i++)
            {
                if (snakebody[0].x == snakebody[i].x && snakebody[0].y == snakebody[i].y)
                {
                    this.Close();
                }
            }

            for (var i = 0; i < mycanvas.Children.Count; i++)
            {
                if (mycanvas.Children[i] is Ellipse)
                    count++;
            }
            mycanvas.Children.RemoveRange(1, count);
            count = 0;
            AddSnakeinCanvas();
        }

        /// <summary>
        /// Произвольное движение змейки
        /// </summary>
        private void MoveFreeSnake()
        {
            if (direction == up)
                y -= 10;
            if (direction == down)
                y += 10;
            if (direction == left)
                x -= 10;
            if (direction == right)
                x += 10;
        }

        /// <summary>
        /// Метод управления змейкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e) // управление 
        {
            if (e.Key == Key.Up && direction != down)
            {
                direction = up;
            }

            if (e.Key == Key.Down && direction != up)
            {
                direction = down;
            }

            if (e.Key == Key.Left && direction != right)
            {
                direction = left;
                
            }

            if (e.Key == Key.Right && direction != left)
            {
                direction = right;

            }
        }

        /// <summary>
        /// Первоночальное размещение змейки и яблок на холсте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddSnakeinCanvas();
            AddFoodinCanvas();
            time.Start();
        }
    }
}

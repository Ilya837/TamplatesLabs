using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TamplatesLabs5_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        struct EllipseMoveEvent
        {
            public Ellipse Ellipse;
            public double start_x;
            public double start_y;
            public double finish_x;
            public double finish_y;
            public int startIndex;
        }

        private LinkedList<EllipseMoveEvent> History = [];

        int StartIndex = 0;
        double StartX = 0;
        double StartY = 0;
        bool HaventStart = true;



        private void Ellipse3_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse3.Fill =new SolidColorBrush(Colors.Green);
        }

        private void Ellipse3_MouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse3.Fill = new SolidColorBrush(Colors.White);
        }

        private void Ellipse3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse3.Fill = new SolidColorBrush(Colors.Red);
            StartIndex = Canvas.Children.IndexOf(Ellipse3);
            Canvas.Children.Remove(Ellipse3);
            Canvas.Children.Add(Ellipse3);
            
        }

        private void Ellipse3_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse3.Fill = new SolidColorBrush(Colors.Green);

            EllipseMoveEvent moveEvent;

            moveEvent.Ellipse = Ellipse3;
            moveEvent.start_x = Ellipse3.Margin.Left;
            moveEvent.start_y = Ellipse3.Margin.Top;
            moveEvent.finish_x = Ellipse3.Margin.Left;
            moveEvent.finish_y = Ellipse3.Margin.Top;
            moveEvent.startIndex = StartIndex;

            History.AddLast(moveEvent);
        }

        private void Ellipse3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Ellipse3, Ellipse3, DragDropEffects.Move);

            }
        }


        private void Ellipse3_DragOver(object sender, DragEventArgs e)
        {
            Point dragPosition = e.GetPosition(Canvas);

            if (HaventStart)
            {
                StartX = Ellipse3.Margin.Left;
                StartY = Ellipse3.Margin.Top;
                HaventStart = false;
            }


            Ellipse3.Margin = new Thickness(dragPosition.X - Ellipse3.Width / 2, dragPosition.Y - Ellipse3.Height / 2, 0, 0);
            Ellipse3.Fill = new SolidColorBrush(Colors.Red);
        }

        private void Ellipse3_Drop(object sender, DragEventArgs e)
        {
            Ellipse3.Fill = new SolidColorBrush(Colors.Green);

            EllipseMoveEvent moveEvent;

            moveEvent.Ellipse = Ellipse3;
            moveEvent.start_x = StartX;
            moveEvent.start_y = StartY;
            moveEvent.finish_x = Ellipse3.Margin.Left;
            moveEvent.finish_y = Ellipse3.Margin.Top;
            moveEvent.startIndex = StartIndex;

            History.AddLast(moveEvent);

            HaventStart = true;

        }

        private void Ellipse2_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse2.Fill = new SolidColorBrush(Colors.Green);
        }

        private void Ellipse2_MouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse2.Fill = new SolidColorBrush(Colors.White);
        }

        private void Ellipse2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse2.Fill = new SolidColorBrush(Colors.Red);
            StartIndex = Canvas.Children.IndexOf(Ellipse2);
            Canvas.Children.Remove(Ellipse2);
            Canvas.Children.Add(Ellipse2);
        }

        private void Ellipse2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse2.Fill = new SolidColorBrush(Colors.Green);

            EllipseMoveEvent moveEvent;

            moveEvent.Ellipse = Ellipse2;
            moveEvent.start_x = Ellipse2.Margin.Left;
            moveEvent.start_y = Ellipse2.Margin.Top;
            moveEvent.finish_x = Ellipse2.Margin.Left;
            moveEvent.finish_y = Ellipse2.Margin.Top;
            moveEvent.startIndex = StartIndex;

            History.AddLast(moveEvent);
        }

        private void Ellipse2_DragOver(object sender, DragEventArgs e)
        {
            Point dragPosition = e.GetPosition(Canvas);

            if (HaventStart)
            {
                StartX = Ellipse2.Margin.Left;
                StartY = Ellipse2.Margin.Top;
                HaventStart = false;
            }

            Ellipse2.Margin = new Thickness(dragPosition.X - Ellipse2.Width / 2, dragPosition.Y - Ellipse2.Height / 2, 0, 0);
            Ellipse2.Fill = new SolidColorBrush(Colors.Red);
        }

        private void Ellipse2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Ellipse2, Ellipse2, DragDropEffects.Move);

            }
        }

        private void Ellipse2_Drop(object sender, DragEventArgs e)
        {
            Ellipse2.Fill = new SolidColorBrush(Colors.Green);

            EllipseMoveEvent moveEvent;

            moveEvent.Ellipse = Ellipse2;
            moveEvent.start_x = StartX;
            moveEvent.start_y = StartY;
            moveEvent.finish_x = Ellipse2.Margin.Left;
            moveEvent.finish_y = Ellipse2.Margin.Top;
            moveEvent.startIndex = StartIndex;

            History.AddLast(moveEvent);

            HaventStart = true;
        }

        private void Ellipse1_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse1.Fill = new SolidColorBrush(Colors.Green);
        }

        private void Ellipse1_MouseLeave(object sender, MouseEventArgs e)
        {
            Ellipse1.Fill = new SolidColorBrush(Colors.White);
        }

       

        private void Ellipse1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse1.Fill = new SolidColorBrush(Colors.Green);

            EllipseMoveEvent moveEvent;

            moveEvent.Ellipse = Ellipse1;
            moveEvent.start_x = Ellipse1.Margin.Left;
            moveEvent.start_y = Ellipse1.Margin.Top;
            moveEvent.finish_x = Ellipse1.Margin.Left;
            moveEvent.finish_y = Ellipse1.Margin.Top;
            moveEvent.startIndex = StartIndex;

            History.AddLast(moveEvent);
        }

        private void Ellipse1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse1.Fill = new SolidColorBrush(Colors.Red);
            StartIndex = Canvas.Children.IndexOf(Ellipse1);
            Canvas.Children.Remove(Ellipse1);
            Canvas.Children.Add(Ellipse1);
        }

        

        private void Ellipse1_DragOver(object sender, DragEventArgs e)
        {
            Point dragPosition = e.GetPosition(Canvas);

            if (HaventStart)
            {
                StartX = Ellipse1.Margin.Left;
                StartY = Ellipse1.Margin.Top;
                HaventStart = false;
            }



            Ellipse1.Margin = new Thickness(dragPosition.X - Ellipse1.Width / 2, dragPosition.Y - Ellipse1.Height / 2, 0, 0);
            Ellipse1.Fill = new SolidColorBrush(Colors.Red);
        }

        private void Ellipse1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(Ellipse1, Ellipse1, DragDropEffects.Move);

            }
        }


        private void Ellipse1_Drop(object sender, DragEventArgs e)
        {
            Ellipse1.Fill = new SolidColorBrush(Colors.Green);

            EllipseMoveEvent moveEvent;

            moveEvent.Ellipse = Ellipse1;
            moveEvent.start_x = StartX;
            moveEvent.start_y = StartY;
            moveEvent.finish_x = Ellipse1.Margin.Left;
            moveEvent.finish_y = Ellipse1.Margin.Top;
            moveEvent.startIndex = StartIndex;

            History.AddLast(moveEvent);

            HaventStart = true;
        }


        private void CtrlZEvent() {
            if (History.Count != 0)
            {
                EllipseMoveEvent moveEvent = History.Last.Value;

                History.Remove(moveEvent);

                Canvas.Children.Remove(moveEvent.Ellipse);

                Canvas.Children.Insert(moveEvent.startIndex, moveEvent.Ellipse);


                moveEvent.Ellipse.Margin = new Thickness(moveEvent.start_x, moveEvent.start_y, 0, 0);
                int a = 0;
            }
        }

        

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CtrlZEvent();
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RippleProgressBar
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RippleProgressBar : UserControl,INotifyPropertyChanged
    {
        private Storyboard rippleStoryBoard = new Storyboard();
        private bool hasAnimationRanBefore =false;

        private double animationTimeInMilliSeconds=2000; //2 seconds default animation time

        public Brush CircleColor
        {
            get
            {
                return (Brush)GetValue(CircleColorProperty);
            }
            set
            {
                SetValue(CircleColorProperty, value);
            }
        }

        public Uri CentralImageSource
        {
            get
            {
                return (Uri)GetValue(CentralImageSourceProperty);
            }
            set
            {
                SetValue(CentralImageSourceProperty, value);
            }

        }

        private static readonly DependencyProperty CentralImageSourceProperty = DependencyProperty.Register(nameof(CentralImageSource), typeof(Uri),
            typeof(RippleProgressBar),
            new PropertyMetadata(null, ImageSourceChangedCallback));

        private static void ImageSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private static readonly DependencyProperty CircleColorProperty = DependencyProperty.Register(nameof(CircleColor), typeof(Brush), typeof(RippleProgressBar),
            
            new PropertyMetadata(Brushes.Gray,ColorChangedCallback));

        private static void ColorChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public Brush CircleColorBrush
        //{
        //    get 
        //    { 
        //        if(circleColorBrush == null)
        //        {
        //            circleColorBrush = (Brush)new BrushConverter().ConvertFrom("#393A4D");
        //        }
        //        return circleColorBrush;
        //    }
        //    set
        //    {
        //        circleColorBrush = value;
        //        InvokePropertyChanged(nameof(CircleColorBrush));
        //    }
        //}

        public double AnimationTimeInMilliSeconds
        {
            get => animationTimeInMilliSeconds;
            set { 
                animationTimeInMilliSeconds = value;
                OnAnimationTimeChanged();
            }
        }

        private void OnAnimationTimeChanged()
        {
            try
            {
                rippleStoryBoard.Stop();
                
            }
            catch
            {

            }
            finally
            {
                RunAnimation();
            }
        }

        public RippleProgressBar()
        {
            InitializeComponent();
            DataContext = this;
            IsVisibleChanged += RippleProgressBar_IsVisibleChanged;
        }

        private void RippleProgressBar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                //stop animation
                rippleStoryBoard.Pause();
            }
            else
            {
                try
                {
                    if (rippleStoryBoard.GetIsPaused())
                    {
                        rippleStoryBoard.Resume();
                    }
                    else
                    {
                        if (hasAnimationRanBefore)
                        {
                            rippleStoryBoard.Resume();
                        }

                    }
                }
                catch
                {

                }
                finally
                {

                }
            }
            
        }

        private async void RunAnimation()
        {
           
            var gridParent = OpaqueEllipse.Parent as Grid;
            var toValue = gridParent.ActualHeight < gridParent.ActualWidth ? gridParent.ActualHeight : gridParent.ActualWidth;
            double ratioWH = gridParent.ActualWidth / gridParent.ActualHeight;
            
            DoubleAnimation animRippleWidth = new DoubleAnimation(0, gridParent.ActualWidth, TimeSpan.FromMilliseconds(AnimationTimeInMilliSeconds));
            DoubleAnimation animRippleSize = new DoubleAnimation(0, gridParent.ActualHeight, TimeSpan.FromMilliseconds(AnimationTimeInMilliSeconds));
            DoubleAnimation animateOpacity = new DoubleAnimation(0, 0.2, TimeSpan.FromMilliseconds(AnimationTimeInMilliSeconds));
            animateOpacity.RepeatBehavior = RepeatBehavior.Forever;
            animRippleSize.RepeatBehavior = RepeatBehavior.Forever;
            animRippleWidth.RepeatBehavior = RepeatBehavior.Forever;
            animRippleSize.AutoReverse = true;
            animRippleWidth.AutoReverse = true;
            animateOpacity.AutoReverse = true;
            rippleStoryBoard.Children.Add(animRippleSize);
            rippleStoryBoard.Children.Add(animateOpacity);
            rippleStoryBoard.Children.Add(animRippleWidth);

            Storyboard.SetTargetName(animRippleWidth, OpaqueEllipse.Name);
            Storyboard.SetTargetProperty(animRippleWidth, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTargetName(animRippleSize, OpaqueEllipse.Name);
            Storyboard.SetTargetProperty(animRippleSize, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTargetName(animateOpacity, OpaqueEllipse.Name);
            Storyboard.SetTargetProperty(animateOpacity, new PropertyPath(Ellipse.OpacityProperty));

            //rippleStoryBoard.AutoReverse = true;
            rippleStoryBoard.Begin(OpaqueEllipse);
            await Task.Run(() => {
                Thread.Sleep((int)(AnimationTimeInMilliSeconds * 0.5));
            });
            RunAnimationOnEllipseTwo();
        }

        private void RunAnimationOnEllipseTwo()
        {
            var gridParent = OpaqueEllipse.Parent as Grid;
            var toValue = gridParent.ActualHeight < gridParent.ActualWidth ? gridParent.ActualHeight : gridParent.ActualWidth;
            double ratioWH = gridParent.ActualWidth / gridParent.ActualHeight;

            DoubleAnimation animRippleWidth = new DoubleAnimation(0, gridParent.ActualWidth, TimeSpan.FromMilliseconds(AnimationTimeInMilliSeconds*0.9));
            DoubleAnimation animRippleSize = new DoubleAnimation(0, gridParent.ActualHeight, TimeSpan.FromMilliseconds(AnimationTimeInMilliSeconds*0.9));
            DoubleAnimation animateOpacity = new DoubleAnimation(0, 0.2, TimeSpan.FromMilliseconds(AnimationTimeInMilliSeconds*0.8));
            animateOpacity.RepeatBehavior = RepeatBehavior.Forever;
            animRippleSize.RepeatBehavior = RepeatBehavior.Forever;
            animRippleWidth.RepeatBehavior = RepeatBehavior.Forever;
            animRippleSize.AutoReverse = true;
            animRippleWidth.AutoReverse = true;
            animateOpacity.AutoReverse = true;
            rippleStoryBoard.Children.Add(animRippleSize);
            rippleStoryBoard.Children.Add(animateOpacity);
            rippleStoryBoard.Children.Add(animRippleWidth);

            Storyboard.SetTargetName(animRippleWidth, OpaqueEllipseTwo.Name);
            Storyboard.SetTargetProperty(animRippleWidth, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTargetName(animRippleSize, OpaqueEllipseTwo.Name);
            Storyboard.SetTargetProperty(animRippleSize, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTargetName(animateOpacity, OpaqueEllipseTwo.Name);
            Storyboard.SetTargetProperty(animateOpacity, new PropertyPath(Ellipse.OpacityProperty));

            //rippleStoryBoard.AutoReverse = true;
            rippleStoryBoard.Begin(OpaqueEllipse);
        }

        private void MainRIppleGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
            RunAnimation();
        }
    }
}
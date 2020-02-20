using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SoccerPrediction.AnimatedContentControl
{
    [TemplatePart(Name = "PART_PaintArea", Type = typeof(Shape))]
    [TemplatePart(Name = "PART_MainContent", Type = typeof(ContentPresenter))]
    public class AnimatedContentControl : ContentControl
    {
        #region Private Member

        private Shape _paintArea;
        private ContentPresenter _mainContent;

        #endregion

        #region Constructor

        public AnimatedContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedContentControl), new FrameworkPropertyMetadata(typeof(AnimatedContentControl)));
        }

        #endregion

        #region DependencyProperties



        public AnimationTypeEnum AnimationType
        {
            get { return (AnimationTypeEnum)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register("AnimationType", typeof(AnimationTypeEnum), typeof(AnimatedContentControl), new PropertyMetadata(AnimationTypeEnum.SlideLeft));



        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(AnimatedContentControl), new PropertyMetadata(TimeSpan.FromSeconds(0.5)));



        public bool EaseAnimation
        {
            get { return (bool)GetValue(EaseAnimationProperty); }
            set { SetValue(EaseAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EaseAnimation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EaseAnimationProperty =
            DependencyProperty.Register("EaseAnimation", typeof(bool), typeof(AnimatedContentControl), new PropertyMetadata(true));

        #endregion

        #region Overrides

        public override void OnApplyTemplate()
        {
            _paintArea = (Shape)Template.FindName("PART_PaintArea", this);
            _mainContent = (ContentPresenter)Template.FindName("PART_MainContent", this);
            base.OnApplyTemplate();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (_paintArea != null && _mainContent != null && AnimationType != AnimationTypeEnum.Switch)
            {
                _paintArea.Fill = CreateBrushFromVisual(_mainContent);
                BeginAnimation();
            }

            base.OnContentChanged(oldContent, newContent);
        }

        #endregion

        private void BeginAnimation()
        {
            var newContentTransform = new TranslateTransform();
            var oldContentTransform = new TranslateTransform();

            _paintArea.RenderTransform = oldContentTransform;
            _mainContent.RenderTransform = newContentTransform;
            _paintArea.Visibility = Visibility.Visible;

            switch ((AnimationTypeEnum)GetValue(AnimationTypeProperty))
            {
                case AnimationTypeEnum.SlideLeft:
                    newContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(this.ActualWidth, 0));
                    oldContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(0, -this.ActualWidth, (s, e) => _paintArea.Visibility = Visibility.Hidden));
                    break;
                case AnimationTypeEnum.SlideRight:
                    newContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(-this.ActualWidth, 0));
                    oldContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(0, this.ActualWidth, (s, e) => _paintArea.Visibility = Visibility.Hidden));
                    break;
                case AnimationTypeEnum.SlideUp:
                    newContentTransform.BeginAnimation(TranslateTransform.YProperty, CreateAnimation(this.ActualHeight, 0));
                    oldContentTransform.BeginAnimation(TranslateTransform.YProperty, CreateAnimation(0, -this.ActualHeight, (s, e) => _paintArea.Visibility = Visibility.Hidden));
                    break;
                case AnimationTypeEnum.SlideDown:
                    newContentTransform.BeginAnimation(TranslateTransform.YProperty, CreateAnimation(-this.ActualHeight, 0));
                    oldContentTransform.BeginAnimation(TranslateTransform.YProperty, CreateAnimation(0, this.ActualHeight, (s, e) => _paintArea.Visibility = Visibility.Hidden));
                    break;
                case AnimationTypeEnum.Switch:
                    break;
                default:
                    break;
            }
        }

        private AnimationTimeline CreateAnimation(double fromValue, double toValue, EventHandler whenDone = null)
        {
            IEasingFunction ease = new BackEase() { Amplitude = 0.5, EasingMode = EasingMode.EaseInOut };
            var duration = new Duration(AnimationDuration);
            var anim = new DoubleAnimation(fromValue, toValue, duration);
            if (EaseAnimation)
                anim.EasingFunction = ease;
            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }
        private Brush CreateBrushFromVisual(Visual mainContent)
        {
            if (mainContent == null)
                throw new ArgumentNullException(nameof(mainContent));
            var target = new RenderTargetBitmap(Convert.ToInt32(ActualWidth), Convert.ToInt32(ActualHeight), 96, 96, PixelFormats.Pbgra32);
            target.Render(mainContent);
            var brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }
    }
    public enum AnimationTypeEnum
    {
        SlideLeft = 0,
        SlideRight = 1,
        SlideUp = 2,
        SlideDown = 3,
        Switch = 4
    }
}

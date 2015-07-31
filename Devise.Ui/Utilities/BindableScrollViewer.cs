using System.Windows;
using System.Windows.Controls;

namespace Devise.Ui.Utilities
{
    public class BindableScrollViewer : ScrollViewer
    {
        #region Fields
        
        private static readonly DependencyProperty BindableVerticalOffsetProperty = DependencyProperty.Register(
            "BindableVerticalOffset", typeof(double), typeof(BindableScrollViewer), new PropertyMetadata(default(double), MoveScrollBar));
        
        public static readonly DependencyProperty BindableHorizontalOffsetProperty = DependencyProperty.Register(
            "BindableHorizontalOffset", typeof(double), typeof(BindableScrollViewer), new PropertyMetadata(default(double), MoveScrollBar));
        
        private bool _lock;

        #endregion  //  Fields

        #region Properties
        
        public double BindableVerticalOffset
        {
            get { return (double)GetValue(BindableVerticalOffsetProperty); }
            set { SetValue(BindableVerticalOffsetProperty, value); }
        }
        
        public double BindableHorizontalOffset
        {
            get { return (double)GetValue(BindableHorizontalOffsetProperty); }
            set { SetValue(BindableHorizontalOffsetProperty, value); }
        }

        #endregion  //  Properties

        #region Constructor
        
        public BindableScrollViewer()
        {
            ScrollChanged += OnScrollChanged;
            Loaded += OnLoaded;
        }

        #endregion  //  Constructor

        #region Members
        
        private void OnScrollChanged(object sender, ScrollChangedEventArgs scrollChangedEventArgs)
        {
            if (_lock || scrollChangedEventArgs.OriginalSource.GetType() != typeof(BindableScrollViewer))
                return;

            _lock = true;

            BindableVerticalOffset = scrollChangedEventArgs.VerticalOffset;
            BindableHorizontalOffset = scrollChangedEventArgs.HorizontalOffset;

            _lock = false;
        }
        
        private static void MoveScrollBar(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var sender = dependencyObject as BindableScrollViewer;

            if (sender == null || sender._lock) return;

            sender._lock = true;

            sender.ScrollToVerticalOffset(sender.BindableVerticalOffset);
            sender.ScrollToHorizontalOffset(sender.BindableHorizontalOffset);

            sender._lock = false;
        }
        
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            MoveScrollBar(this, new DependencyPropertyChangedEventArgs());
        }
        
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            MoveScrollBar(this, new DependencyPropertyChangedEventArgs());
        }

        #endregion  //  Members
    }
}

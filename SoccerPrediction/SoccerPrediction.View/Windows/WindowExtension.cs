using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SoccerPrediction.View
{
    public partial class WindowExtension
    {
        public static double? GetMaxWidth(DependencyObject obj)
        {
            return (double?)obj.GetValue(MaxWidthProperty);
        }

        public static void SetMaxWidth(DependencyObject obj, double? value)
        {
            obj.SetValue(MaxWidthProperty, value);
        }

        public static readonly DependencyProperty MaxWidthProperty = DependencyProperty.RegisterAttached("MaxWidth", typeof(double?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.MaxWidth on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).MaxWidth = Convert.ToDouble(GetMaxWidth(ctl)); };
            }
        });

        public static double? GetMaxHeight(DependencyObject obj)
        {
            return (double?)obj.GetValue(MaxHeightProperty);
        }

        public static void SetMaxHeight(DependencyObject obj, double? value)
        {
            obj.SetValue(MaxHeightProperty, value);
        }

        public static readonly DependencyProperty MaxHeightProperty = DependencyProperty.RegisterAttached("MaxHeight", typeof(double?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.MaxHeight on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).MaxHeight = Convert.ToDouble(GetMaxHeight(ctl)); };
            }
        });

        public static double? GetMinWidth(DependencyObject obj)
        {
            return (double?)obj.GetValue(MinWidthProperty);
        }

        public static void SetMinWidth(DependencyObject obj, double? value)
        {
            obj.SetValue(MinWidthProperty, value);
        }

        public static readonly DependencyProperty MinWidthProperty = DependencyProperty.RegisterAttached("MinWidth", typeof(double?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.MinWidth on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).MinWidth = Convert.ToDouble(GetMinWidth(ctl)); };
            }
        });

        public static double? GetMinHeight(DependencyObject obj)
        {
            return (double?)obj.GetValue(MinHeightProperty);
        }

        public static void SetMinHeight(DependencyObject obj, double? value)
        {
            obj.SetValue(MinHeightProperty, value);
        }

        public static readonly DependencyProperty MinHeightProperty = DependencyProperty.RegisterAttached("MinHeight", typeof(double?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.MinHeight on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).MinHeight = Convert.ToDouble(GetMinHeight(ctl)); };
            }
        });

        public static SizeToContent? GetSizeToContent(DependencyObject obj)
        {
            return (SizeToContent?)obj.GetValue(SizeToContentProperty);
        }

        public static void SetSizeToContent(DependencyObject obj, SizeToContent? value)
        {
            obj.SetValue(SizeToContentProperty, value);
        }

        public static readonly DependencyProperty SizeToContentProperty = DependencyProperty.RegisterAttached("SizeToContent", typeof(SizeToContent?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.SizeToContent on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).SizeToContent = (SizeToContent)GetSizeToContent(ctl); };
            }
        });

        public static WindowStartupLocation? GetWindowStartupLocation(DependencyObject obj)
        {
            return (WindowStartupLocation?)obj.GetValue(WindowStartupLocationProperty);
        }

        public static void SetWindowStartupLocation(DependencyObject obj, WindowStartupLocation? value)
        {
            obj.SetValue(WindowStartupLocationProperty, value);
        }

        public static readonly DependencyProperty WindowStartupLocationProperty = DependencyProperty.RegisterAttached("WindowStartupLocation", typeof(WindowStartupLocation?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.WindowStartupLocation on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).WindowStartupLocation = (WindowStartupLocation)GetWindowStartupLocation(ctl); };
            }
        });

        public static ResizeMode? GetResizeMode(DependencyObject obj)
        {
            return (ResizeMode?)obj.GetValue(ResizeModeProperty);
        }

        public static void SetResizeMode(DependencyObject obj, ResizeMode? value)
        {
            obj.SetValue(ResizeModeProperty, value);
        }

        public static readonly DependencyProperty ResizeModeProperty = DependencyProperty.RegisterAttached("ResizeMode", typeof(ResizeMode?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.ResizeMode on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) Window.GetWindow(ctl).ResizeMode = (ResizeMode)GetResizeMode(ctl); };
            }
        });

        public static WindowStyle? GetWindowStyle(DependencyObject obj)
        {
            return (WindowStyle?)obj.GetValue(WindowStyleProperty);
        }

        public static void SetWindowStyle(DependencyObject obj, WindowStyle? value)
        {
            obj.SetValue(WindowStyleProperty, value);
        }

        public static readonly DependencyProperty WindowStyleProperty = DependencyProperty.RegisterAttached("WindowStyle", typeof(WindowStyle?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.WindowStyle on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) ((AppWindow)Window.GetWindow(ctl)).WindowStyle = (WindowStyle)GetWindowStyle(ctl); };
            }
        });

        public static bool? GetCanCloseWithEsc(DependencyObject obj)
        {
            return (bool?)obj.GetValue(CanCloseWithEscProperty);
        }

        public static void SetCanCloseWithEsc(DependencyObject obj, bool? value)
        {
            obj.SetValue(CanCloseWithEscProperty, value);
        }

        public static readonly DependencyProperty CanCloseWithEscProperty = DependencyProperty.RegisterAttached("CanCloseWithEsc", typeof(bool?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.CanCloseWithESC on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) ((AppWindow)Window.GetWindow(ctl)).CanCloseWithEsc = Convert.ToBoolean(GetCanCloseWithEsc(ctl)); };
            }
        });

        public static ImageSource GetIcon(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(ImageSource), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.Icon on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) ((AppWindow)Window.GetWindow(ctl)).Icon = GetIcon(ctl); };
            }
        });

        public static bool? GetShowInTaskbar(DependencyObject obj)
        {
            return (bool?)obj.GetValue(ShowInTaskbarProperty);
        }

        public static void SetShowInTaskbar(DependencyObject obj, bool? value)
        {
            obj.SetValue(ShowInTaskbarProperty, value);
        }

        public static readonly DependencyProperty ShowInTaskbarProperty = DependencyProperty.RegisterAttached("ShowInTaskbar", typeof(bool?), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.ShowInTaskbar on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) ((AppWindow)Window.GetWindow(ctl)).ShowInTaskbar = Convert.ToBoolean(GetShowInTaskbar(ctl)); };
            }
        });

        public static string GetWindowTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(WindowTitleProperty);
        }

        public static void SetWindowTitle(DependencyObject obj, string value)
        {
            obj.SetValue(WindowTitleProperty, value);
        }

        public static readonly DependencyProperty WindowTitleProperty = DependencyProperty.RegisterAttached("WindowTitle", typeof(string), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.WindowTitle on a control");
                }

                ctl.Loaded += (sender, e2) => { if (Window.GetWindow(ctl) != null) ((AppWindow)Window.GetWindow(ctl)).Title = GetWindowTitle(ctl); };
            }
        });

        public static WindowState GetWindowState(DependencyObject obj)
        {
            return (WindowState)obj.GetValue(WindowStateProperty);
        }

        public static void SetWindowState(DependencyObject obj, WindowState value)
        {
            obj.SetValue(WindowStateProperty, value);
        }

        public static readonly DependencyProperty WindowStateProperty = DependencyProperty.RegisterAttached("WindowState", typeof(WindowState), typeof(WindowExtension), new UIPropertyMetadata()
        {
            PropertyChangedCallback = (obj, e) =>
            {
                Control ctl = obj as Control;
                if (ctl == null)
                {
                    throw new InvalidOperationException("Can only use AppWindowHelper.WindowState on a control");
                }

                ctl.Loaded += (sender, e2) =>
                {
                    if (Window.GetWindow(ctl) != null)
                    {
                        var win = (AppWindow)Window.GetWindow(ctl);

                        if (Convert.ToInt32(GetWindowState(ctl)) == 2)
                        {
                            win.SetWindowState(true);
                        }
                        else
                        {
                            win.SetWindowState(false);
                        }
                    }
                };
            }
        });
    }
}

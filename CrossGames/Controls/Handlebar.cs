using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;
using System.Windows.Input;

namespace CrossGames.Controls
{
    /// <summary>
    /// 模拟手柄
    /// </summary>
    public class Handlebar:ContentControl
    {
        public static readonly StyledProperty<ICommand> BaseCommandProperty =
            AvaloniaProperty.Register<Handlebar, ICommand>(nameof(BaseCommand));
        public static readonly StyledProperty<ICommand> LeftCommandProperty=
            AvaloniaProperty.Register<Handlebar, ICommand>(nameof(LeftCommand));
        public static readonly StyledProperty<ICommand> RightCommandProperty =
            AvaloniaProperty.Register<Handlebar, ICommand>(nameof(RightCommand));
        public static readonly StyledProperty<ICommand> UpCommandProperty =
            AvaloniaProperty.Register<Handlebar, ICommand>(nameof(UpCommand));
        public static readonly StyledProperty<ICommand> DownCommandProperty =
            AvaloniaProperty.Register<Handlebar, ICommand>(nameof(DownCommand));
        public static readonly StyledProperty<ICommand> CenterCommandProperty =
            AvaloniaProperty.Register<Handlebar, ICommand>(nameof(CenterCommand));
        public static readonly StyledProperty<bool> CenterVisibleProperty =
            AvaloniaProperty.Register<Handlebar, bool>(nameof(CenterVisible),false);
        public static readonly StyledProperty<bool> UpVisibleProperty =
            AvaloniaProperty.Register<Handlebar, bool>(nameof(UpVisible), true);
        public bool UpVisible
        {
            get => GetValue(UpVisibleProperty);
            set => SetValue(UpVisibleProperty, value);
        }
        public bool CenterVisible
        {
            get => GetValue(CenterVisibleProperty);
            set => SetValue(CenterVisibleProperty, value);
        }

        public ICommand CenterCommand
        {
            get => GetValue(CenterCommandProperty);
            set => SetValue(CenterCommandProperty, value);
        }
        public ICommand LeftCommand
        {
            get => GetValue(LeftCommandProperty);
            set => SetValue(LeftCommandProperty, value);
        }
        public ICommand RightCommand
        {
            get => GetValue(RightCommandProperty);
            set => SetValue(RightCommandProperty, value);
        }
        public ICommand UpCommand
        {
            get => GetValue(UpCommandProperty);
            set => SetValue(UpCommandProperty, value);
        }
        public ICommand DownCommand
        {
            get => GetValue(DownCommandProperty);
            set => SetValue(DownCommandProperty, value);
        }
        public ICommand BaseCommand
        {
            get => GetValue(BaseCommandProperty); 
            set => SetValue(BaseCommandProperty, value);
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            LeftCommand ??= BaseCommand;
            RightCommand ??= BaseCommand;
            UpCommand ??= BaseCommand;
            DownCommand ??= BaseCommand;
            CenterVisible = CenterCommand != null;
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            var min = Math.Min(availableSize.Height, availableSize.Width);
            var newSize = new Size(min, min);
            var size = base.MeasureOverride(newSize);
            return size;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            var min = Math.Min(finalSize.Height, finalSize.Width);
            var newSize = new Size(min, min);
            var size = base.ArrangeOverride(newSize);
            return size;
        }
    }

}
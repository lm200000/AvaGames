using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CrossGames.ViewModels;
using Ursa.Controls;

namespace CrossGames.Views;

public partial class Page2048View : UserControl
{
    public Page2048View()
    {
        InitializeComponent();
        this.KeyUp += Uc_KeyUp;
        Gestures.ScrollGestureEvent.AddClassHandler<Page2048View>(HandleScrollGes);
    }
    private void Uc_KeyUp(object? sender, KeyEventArgs e)
    {
        if (DataContext is Page2048ViewModel vm)
        {
            vm.Move(e.Key);
        }
    }
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (DataContext is not Page2048ViewModel vm) return;
        var topLevel = TopLevel.GetTopLevel(this);
        vm.NotificationManager = WindowNotificationManager.TryGetNotificationManager(topLevel, out var manager)
            ? manager
            : new WindowNotificationManager(topLevel);
    }
    private void HandleScrollGes(Page2048View w, ScrollGestureEventArgs e)
    {
        //if (w.DataContext is Page2048ViewModel vm)
        //{
        //    var vector = e.Delta;
        //    if ((vector.X == 0 && vector.Y == 0) || id == 0)
        //    {
        //        id = 1;
        //        return;
        //    }
        //    id = 0;
        //    if (Math.Abs(vector.X) > Math.Abs(vector.Y))
        //    {
        //        if (vector.X > 0)
        //            vm.Move(Key.Left);
        //        else
        //            vm.Move(Key.Right);
        //    }
        //    else
        //    {
        //        if (vector.Y > 0)
        //            vm.Move(Key.Up);
        //        else
        //            vm.Move(Key.Down);
        //    }
        //}
    }  
}
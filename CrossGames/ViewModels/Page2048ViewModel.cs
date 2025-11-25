using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Games.Models;

namespace CrossGames.ViewModels
{
    public partial class Page2048ViewModel:ViewModelBase
    {
        public Page2048ViewModel()
        {
            matrix = new CubeMatrix();
            loadmaxScore();
            updateMatrixProperty();
        }
        public Ursa.Controls.WindowNotificationManager? NotificationManager { get; set; }
        private CubeMatrix matrix;
        [ObservableProperty]
        private int _maxScore;
        [ObservableProperty]
        private int _score;
        [ObservableProperty]
        public int _matrix11;
        [ObservableProperty]
        public int _matrix12;
        [ObservableProperty]
        public int _matrix13;
        [ObservableProperty]
        public int _matrix14;
        [ObservableProperty]
        public int _matrix21;
        [ObservableProperty]
        public int _matrix22;
        [ObservableProperty]
        public int _matrix23;
        [ObservableProperty]
        public int _matrix24;
        [ObservableProperty]
        public int _matrix31;
        [ObservableProperty]
        public int _matrix32;
        [ObservableProperty]
        public int _matrix33;
        [ObservableProperty]
        public int _matrix34;
        [ObservableProperty]
        public int _matrix41;
        [ObservableProperty]
        public int _matrix42;
        [ObservableProperty]
        public int _matrix43;
        [ObservableProperty]
        public int _matrix44;
        [RelayCommand]
        private void Restart()
        {
            matrix = new CubeMatrix();
            updateMatrixProperty();
        }
        [RelayCommand]
        private void move(object content)
        {
            var key = content.ToString() switch
            {
                "↑" => Key.Up,
                "↓" => Key.Down,
                "←" => Key.Left,
                "→" => Key.Right,
                _ => Key.None,
            };
            Move(key);
        }
        public void Move(Key key)
        {
            if (!matrix.IsGameOver)
            {
                switch (key)
                {
                    case Key.Up:
                        matrix.Move(MoveDirection.Up);
                        break;
                    case Key.Down:
                        matrix.Move(MoveDirection.Down);
                        break;
                    case Key.Left:
                        matrix.Move(MoveDirection.Left);
                        break;
                    case Key.Right:
                        matrix.Move(MoveDirection.Right);
                        break;
                    default:
                        break;
                }
                updateMatrixProperty();
            }
            else
            {
                NotificationManager?.Show(new Ursa.Controls.Notification("操作提示", "无可移动方向！"), type: Avalonia.Controls.Notifications.NotificationType.Information, classes: ["Light"]);
            }
        }
        private void updateMatrixProperty()
        {
            Score = matrix.Score;
            if (Score > MaxScore)
            {
                MaxScore = Score;
                Common.Appsetting.maxScore = MaxScore;
                Common.Appsetting.Save();
            }
            Matrix11 = matrix.Grid[0, 0];
            Matrix12 = matrix.Grid[0, 1];
            Matrix13 = matrix.Grid[0, 2];
            Matrix14 = matrix.Grid[0, 3];
            Matrix21 = matrix.Grid[1, 0];
            Matrix22 = matrix.Grid[1, 1];
            Matrix23 = matrix.Grid[1, 2];
            Matrix24 = matrix.Grid[1, 3];
            Matrix31 = matrix.Grid[2, 0];
            Matrix32 = matrix.Grid[2, 1];
            Matrix33 = matrix.Grid[2, 2];
            Matrix34 = matrix.Grid[2, 3];
            Matrix41 = matrix.Grid[3, 0];
            Matrix42 = matrix.Grid[3, 1];
            Matrix43 = matrix.Grid[3, 2];
            Matrix44 = matrix.Grid[3, 3];
        }
        private void loadmaxScore()
        {
            Common.Appsetting.Load();
            MaxScore = Common.Appsetting.maxScore;
        }
    }
}

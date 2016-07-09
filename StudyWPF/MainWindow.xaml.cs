using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudyWPF.Adorners;

namespace StudyWPF
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 修飾する対象のコントロールのレイヤーを取得
            var layer = AdornerLayer.GetAdornerLayer(this.line);

            // Adorner のインスタンスを生成
            var adorner = new LineAdorner(this.line);

            // レイヤーに Adorner を追加
            layer.Add(adorner);
        }
    }
}

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
using System.IO;
using System.Reflection;
using System.Xaml;
using System.Diagnostics;
using StudyWPF.Adorners;
using StudyWPF.ViewModels;

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
            this.button.Click += Button_Click;
            // mastar revison test3 for confrict
            // branch test1
            // branch test2
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // mastar revison test1
            // 読み込み
            {
                // mastar revison test2
                // XAML Readerに無視されるプレフィックスを指定するmc:Ignorable属性
                // http://d.hatena.ne.jp/Yamaki/20070409/1176098753
                // XAML
                var xaml = @"<FluentInfo xmlns='clr-namespace:StudyWPF.ViewModels' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' ";
                xaml += @"xmlns:d='http://schemas.microsoft.com/expression/blend/2008' xmlns:mc='http://schemas.openxmlformats.org/markup-compatibility/2006' ";
                xaml += @"mc:Ignorable='d' xmlns:prop='clr-namespace:StudyWPF.Properties' d:IsHiddenCustomize='True' Header='{x:Static prop:Resources.Hagino}' /> ";
                
                // 
                // 読み込み
                // 
                //XamlSchemaContext xamlContext = System.Windows.Markup.XamlReader.GetWpfSchemaContext();
                var r = new XamlXmlReader(new StringReader(xaml), //xamlContext,
                    new XamlXmlReaderSettings
                    {
                        LocalAssembly = Assembly.GetExecutingAssembly(),
                    });
                // 読み込み
                var p = XamlServices.Load(r) as FluentInfo;

                // 確認
                Debug.WriteLine(p.Header);
            }

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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;


namespace Main
{

    public partial class MainWindow : Window
    {
        public static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();

            instance = this;
            
            Connection.Connect();
            
            Thread threadLondonUr = new Thread(new ThreadStart(Ur.London));
            Thread threadKøbenhavnUr = new Thread(new ThreadStart(Ur.København));
            
            threadLondonUr.Start();
            threadKøbenhavnUr.Start();
            
        }

        internal string LondonUr
        {
            get { return LabelLondon.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { LabelLondon.Content = value; })); }
        }

        internal string KøbenhavnUr
        {
            get { return LabelKøbenhavn.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { LabelKøbenhavn.Content = value; })); }
        }

        private void Button_Click()
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            textbox4.Text = Convert.ToString(PrisBeregner.BeregnPris(Convert.ToInt32(textbox1.Text), textbox2.Text, Convert.ToInt32(textbox3.Text)));
        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }
    }
}

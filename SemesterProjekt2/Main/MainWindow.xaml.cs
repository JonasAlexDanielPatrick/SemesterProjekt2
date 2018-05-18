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


    }
}

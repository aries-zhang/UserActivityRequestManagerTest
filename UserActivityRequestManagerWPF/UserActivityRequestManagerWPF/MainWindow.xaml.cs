using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Interop;
using Windows.ApplicationModel.UserActivities;

namespace UserActivityRequestManagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Windows.ApplicationModel.UserActivities.UserActivityRequestManager requestMgr = Windows.ApplicationModel.UserActivities.UserActivityRequestManager.GetForCurrentView();
            //requestManager.UserActivityRequested += UserActivityRequested;

            var userActivityRequestManagerInterop = (IUserActivityRequestManagerInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(UserActivityRequestManager));

            this.Dispatcher.Invoke(new Action(() =>
            {
                IntPtr Handle = new WindowInteropHelper(this).Handle;
                UserActivityRequestManager manager = null;
                userActivityRequestManagerInterop.GetForWindow(Handle, typeof(UserActivityRequestManager).GUID, out manager);
            }));
        }
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("DD69F876-9699-4715-9095-E37EA30DFA1B")]
    internal interface IUserActivityRequestManagerInterop
    {
        void GetForWindow(IntPtr window, Guid riid, out UserActivityRequestManager manager);
    }
}

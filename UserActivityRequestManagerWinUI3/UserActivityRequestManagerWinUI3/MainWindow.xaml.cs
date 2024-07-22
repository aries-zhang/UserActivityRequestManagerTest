using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.UserActivities;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UserActivityRequestManagerWinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";

            DispatcherQueue.TryEnqueue(() =>
            {
                IntPtr handle = WinRT.Interop.WindowNative.GetWindowHandle(this);
                UserActivityRequestManager manager = GetUserActivityRequestManagerForWindow(handle);
            });
        }

        private static UserActivityRequestManager GetUserActivityRequestManagerForWindow(IntPtr handle)
        {
            var userActivityRequestManagerInterop = (IUserActivityRequestManagerInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(UserActivityRequestManager));
            UserActivityRequestManager manager = null;
            userActivityRequestManagerInterop.GetForWindow(handle, typeof(UserActivityRequestManager).GUID, out manager);

            return manager;
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("DD69F876-9699-4715-9095-E37EA30DFA1B")]
        internal interface IUserActivityRequestManagerInterop
        {
            void GetForWindow(IntPtr window, Guid riid, out UserActivityRequestManager manager);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.UserActivities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UserActivityRequestManagerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //UserActivityRequestManager requestManager = UserActivityRequestManager.GetForCurrentView();
            //requestManager.UserActivityRequested += UserActivityRequested;
            dynamic coreWindow = Windows.UI.Core.CoreWindow.GetForCurrentThread();
            var interop = (ICoreWindowInterop)coreWindow;

            var userActivityRequestManagerInterop = (IUserActivityRequestManagerInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(UserActivityRequestManager));

            UserActivityRequestManager manager = null;
            userActivityRequestManagerInterop.GetForWindow(interop.WindowHandle, typeof(UserActivityRequestManager).GUID, out manager);
        }

        private void UserActivityRequested(UserActivityRequestManager sender, UserActivityRequestedEventArgs args)
        {
            UserActivity userActivity = new UserActivity("Test Activity from UWP");
            userActivity.ActivationUri = new Uri("ms-windows-store://");
            args.Request.SetUserActivity(userActivity);
        }
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("DD69F876-9699-4715-9095-E37EA30DFA1B")]
    internal interface IUserActivityRequestManagerInterop
    {
        void GetForWindow(IntPtr window, [In] ref Guid riid, out UserActivityRequestManager manager);
    }

    [ComImport, Guid("45D64A29-A63E-4CB6-B498-5781D298CB4F")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICoreWindowInterop
    {
        IntPtr WindowHandle { get; }
        bool MessageHandled { get; }
    }
}

using System;
using SimpleMvvmToolkit.Express;

namespace SimpleMvvmSamples.UWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        private string _bannerText = "Hello Simple MVVM Toolkit";
        public string BannerText
        {
            get { return _bannerText; }
            set
            {
                _bannerText = value;
                NotifyPropertyChanged(m => m.BannerText);
            }
        }

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            ErrorNotice?.Invoke(this, new NotificationEventArgs<Exception>(message, error));
        }
    }
}
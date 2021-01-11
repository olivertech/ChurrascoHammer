using Acr.UserDialogs;

namespace ChurrascoApp.Classes
{
    public static class Util
    {
        public static void GreenToast(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);

            toastConfig.SetPosition(ToastPosition.Bottom);
            toastConfig.SetDuration(3000);
            toastConfig.SetBackgroundColor(Xamarin.Forms.Color.Green);
            toastConfig.SetIcon("ic_check_circle_white_18dp.png");

            UserDialogs.Instance.Toast(toastConfig);
        }

        public static void RedToast(string mensagem)
        {
            ToastConfig toastConfig = new ToastConfig(mensagem);

            toastConfig.SetPosition(ToastPosition.Bottom);
            toastConfig.SetDuration(3000);
            toastConfig.SetBackgroundColor(Xamarin.Forms.Color.Red);
            toastConfig.SetIcon("ic_info_white_18dp.png");

            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}

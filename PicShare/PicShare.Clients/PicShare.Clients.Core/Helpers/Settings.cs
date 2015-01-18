// Helpers/Settings.cs
using Cirrious.CrossCore;
using EShyMedia.MvvmCross.Plugins.Settings;

namespace PicShare.Clients.Core.Helpers
{
    public static class Settings
    {
        /// <summary>
        /// Simply setup your settings once when it is initialized.
        /// </summary>
        private static ISettings m_Settings;
        private static ISettings AppSettings
        {
            get
            {
                return m_Settings ?? (m_Settings = Mvx.GetSingleton<ISettings>());
            }
        }

#region Setting Constants

		private const string NickNameKey = "nickName";
		private static string NickNameDefault = string.Empty;

        private const string PasswordKey = "password";
#endregion

	public static string NickName
        {
            get
            {
				return AppSettings.GetValueOrDefault(NickNameKey, NickNameDefault);
            }
            set
            {
				AppSettings.AddOrUpdateValue(NickNameKey, value);
            }
        }

        public static string Password
        {
            get { return AppSettings.GetSecuredValue(PasswordKey); }
            set
            {
                AppSettings.AddOrUpdateSecuredValue(PasswordKey, value);
            }
        }
    }

}
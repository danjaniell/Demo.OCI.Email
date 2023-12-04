using Oci.Common;
using Oci.Common.Auth;
using Oci.EmailService;
using Server.Data.Configurations;

namespace Server.Services.Helpers
{
    public static class ConfigurationHelper
    {
        public static EmailClient InitEmailClient(OCIConfig ociConfig)
        {
            var accumulator = new ConfigFile.ConfigAccumulator();
            accumulator
                .configurationByProfile
                .Add(
                    ConfigNames.DEFAULT,
                    new Dictionary<string, string>()
                    {
                        { ConfigNames.USER, ociConfig.User! },
                        { ConfigNames.FINGERPRINT, ociConfig.Fingerprint! },
                        { ConfigNames.KEY_FILE, ociConfig.KeyFile! },
                        { ConfigNames.TENANCY, ociConfig.Tenancy! },
                        { ConfigNames.CUSTOM_COMPARTMENT_ID, ociConfig.CustomCompartmentId! },
                        { ConfigNames.REGION, ociConfig.Region! },
                    }
                );

            ConfigFile configFile = new(accumulator, ConfigNames.DEFAULT);
            var auth = new ConfigFileAuthenticationDetailsProvider(configFile);
            var config = new ClientConfiguration();

            return new EmailClient(auth, config);
        }

        internal static class ConfigNames
        {
            internal const string DEFAULT = "DEFAULT";
            internal const string USER = "user";
            internal const string FINGERPRINT = "fingerprint";
            internal const string KEY_FILE = "key_file";
            internal const string TENANCY = "tenancy";
            internal const string CUSTOM_COMPARTMENT_ID = "customCompartmentId";
            internal const string REGION = "region";
        }
    }
}

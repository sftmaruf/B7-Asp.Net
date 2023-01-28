using Amazon;
using Amazon.Runtime.CredentialManagement;

namespace assignment7
{
    public abstract class AwsService
    {
        protected readonly string _accessKey;
        protected readonly string _secretKey;
        protected readonly RegionEndpoint _region;

        public AwsService()
        {
            var credential = new NetSDKCredentialsFile();
            credential.TryGetProfile("assignment7", out var profile);

            _accessKey = profile.Options.AccessKey;
            _secretKey = profile.Options.SecretKey;
            _region = profile.Region;
        }
    }
}

using System.ComponentModel;

namespace Contracts.Services.Infrastructure.SocialNetworks
{
    public enum SocialNetwork
    {
        [Description("twitter")]
        Twitter = 1,

        [Description("facebook")]
        Facebook,

        [Description("google")]
        Google,

        [Description("vk")]
        Vk,

        [Description("coub")]
        Coub,

        [Description("vine")]
        Vine
    }
}
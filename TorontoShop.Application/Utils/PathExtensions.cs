using System.IO;

namespace TorontoShop.Application.Utils
{
    public static class PathExtensions
    {
        #region user avatar

        public static string UserAvatarOrigin="/img/userAvatar/orgin/";
        public static string UserAvatarOriginServer=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/userAvatar/origin/");

        public static string UserAvatarThumb = "/img/userAvatar/thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/userAvatar/thumb/");

        #endregion
    }
}

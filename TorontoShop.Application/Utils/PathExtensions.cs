using System.IO;

namespace TorontoShop.Application.Utils
{
    public static class PathExtensions
    {
        #region user avatar

        public static string UserAvatarOrigin="/img/userAvatar/origin/";
        public static string UserAvatarOriginServer=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/userAvatar/origin/");

        public static string UserAvatarThumb = "/img/userAvatar/thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/userAvatar/thumb/");

        #endregion

        public static string CategoryOrigin = "/img/category/origin/";
        public static string CategoryOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/category/origin/");

        public static string CategoryThumb = "/img/category/thumb/";
        public static string CategoryThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/category/thumb/");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public enum MediaType
    {
        Image,
        Video,
        Audio,
        Document,
        Other
    }

    public static class MediaTypeExtensions
    {
        public static string ToReprString(this MediaType mediaType)
        {
            return mediaType switch
            {
                MediaType.Image => "image",
                MediaType.Video => "video",
                MediaType.Audio => "audio",
                MediaType.Document => "document",
                MediaType.Other => "other",
                _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, null)
            };
        }

        public static MediaType ToMediaTypeEnum(this string mediaType)
        {
            return mediaType switch
            {
                "image" => MediaType.Image,
                "video" => MediaType.Video,
                "audio" => MediaType.Audio,
                "document" => MediaType.Document,
                "other" => MediaType.Other,
                _ => throw new ArgumentOutOfRangeException(nameof(mediaType), mediaType, null)
            };
        }
    }
}

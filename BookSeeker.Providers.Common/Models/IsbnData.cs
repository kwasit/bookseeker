using System;

namespace BookSeeker.Providers.Common.Models
{
    public class IsbnData : IEquatable<IsbnData>
    {
        public string Id13Digits { get; set; }
        public string Id10Digits { get; set; }

        public bool Equals(IsbnData other)
        {
            bool IdEquals(string id, string otherId) => !string.IsNullOrEmpty(id) 
                                                       && !string.IsNullOrEmpty(otherId) 
                                                       && id == otherId;

            if (other == null)
            {
                return false;
            }

            return IdEquals(Id13Digits, other.Id13Digits) || IdEquals(Id10Digits, other.Id10Digits);
        }
    }
}
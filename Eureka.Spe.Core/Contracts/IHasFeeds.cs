using System.Collections.Generic;
using System.Collections.ObjectModel;
using Eureka.Spe.NewsFeed.Entities;

namespace Eureka.Spe.Contracts
{
    public interface IHasFeeds
    {
        ICollection<Feed> Feeds { get; set; }
    }
}
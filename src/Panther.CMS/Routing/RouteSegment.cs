namespace Panther.CMS.Routing
{
    public class RouteSegment
    {
        public bool IsGreedy { get; internal set; }

        public bool IsToken { get; internal set; }

        public string Name { get; internal set; }
    }
}
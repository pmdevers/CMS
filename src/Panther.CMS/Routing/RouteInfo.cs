using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Panther.CMS.Routing
{
    public class RouteInfo
    {
        private LinkedList<RouteSegment> _segments = new LinkedList<RouteSegment>();
        private bool _hasGreedy = false;
        private IDictionary<string, object> _defaults = new Dictionary<string, object>();
        private bool _finished;
        private LinkedListNode<RouteSegment> _currentSegment;

        public RouteInfo(string route)
        {
            MinRequiredSegments = 0;

            CheckRoute(route);
            GetSegments(route);
            SetMinimalRequired();
            CheckForDefaultsAfterGreedy();
        }

        public IDictionary<string, object> ParseRoute(string virtualPath)
        {
            var results = new Dictionary<string, object>();
            var segments = virtualPath
                .Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            Array.Reverse(segments);
            Stack<string> parts = new Stack<string>(segments);

            // number of request route parts must match route URL definition
            if (parts.Count < this.MinRequiredSegments)
            {
                return null;
            }

            ParseFromBeginnning(results, ref parts);
            ParseFromTheEnd(results, ref parts);
            FillGreedySegment(results, ref parts);
            AddRemainingDefaultValues(results, ref parts);

            return results;
        }

        private void AddRemainingDefaultValues(Dictionary<string, object> result, ref Stack<string> parts)
        {
            foreach (KeyValuePair<string, object> def in this.Defaults)
            {
                if (!result.ContainsKey(def.Key))
                {
                    result.Add(def.Key, def.Value);
                }
            }
        }

        private void FillGreedySegment(Dictionary<string, object> result, ref Stack<string> parts)
        {
            if (!_finished)
            {
                object remaining = string.Join("/", parts) ?? this.Defaults[_currentSegment.Value.Name];
                if (!result.ContainsKey(_currentSegment.Value.Name))
                    result.Add(_currentSegment.Value.Name, remaining);
            }
        }

        private void ParseFromTheEnd(Dictionary<string, object> result, ref Stack<string> parts)
        {
            // continue from the end if needed
            parts = new Stack<string>(parts); // this will reverse stack elements
            _currentSegment = _segments.Last;
            while (!_finished && !_currentSegment.Value.IsGreedy)
            {
                object p = parts.Count > 0 ? parts.Pop() : null;
                if (_currentSegment.Value.IsToken)
                {
                    p = p ?? this.Defaults[_currentSegment.Value.Name];
                    result.Add(_currentSegment.Value.Name, p);
                }
                else
                {
                    if (!_currentSegment.Value.Name.Equals(p))
                    {
                        return;
                    }
                }
                _currentSegment = _currentSegment.Previous;
                _finished = _currentSegment == null;
            }
        }

        private void ParseFromBeginnning(IDictionary<string, object> result, ref Stack<string> parts)
        {
            // start parsing from the beginning
            bool finished = false;
            _currentSegment = _segments.First;
            while (!finished && !_currentSegment.Value.IsGreedy)
            {
                object p = parts.Count > 0 ? parts.Pop() : null;
                if (_currentSegment.Value.IsToken)
                {
                    p = p ?? this.Defaults[_currentSegment.Value.Name];
                    result.Add(_currentSegment.Value.Name, p);
                }
                else
                {
                    if (!_currentSegment.Value.Name.Equals(p))
                    {
                        return;
                    }
                }
                _currentSegment = _currentSegment.Next;
                finished = _currentSegment == null;
            }
        }

        public LinkedList<RouteSegment> Segments { get { return _segments; } }

        public bool HasGreedy { get { return _hasGreedy; } }

        public int MinRequiredSegments { get; private set; }

        public IDictionary<string, object> Defaults { get { return _defaults; } }

        private void CheckRoute(string route)
        {
            if (string.IsNullOrEmpty(route))
            {
                throw new ArgumentException("Route URL must be defined.", "route");
            }

            // correct URL definition can have AT MOST ONE greedy segment
            if (route.Split('*').Length > 2)
            {
                throw new ArgumentException("Route URL can have at most one greedy segment, but not more.", "route");
            }
        }

        private void GetSegments(string route)
        {
            var rx = new Regex(@"^(?<isToken>{)?(?(isToken)(?<isGreedy>\*?))(?<name>[a-zA-Z0-9-_]+)(?(isToken)})$", RegexOptions.Compiled | RegexOptions.Singleline);
            foreach (string segment in route.Split('/'))
            {
                // segment must not be empty
                if (string.IsNullOrEmpty(segment))
                {
                    throw new ArgumentException("Route URL is invalid. Sequence \"//\" is not allowed.", "url");
                }

                if (rx.IsMatch(segment))
                {
                    var m = rx.Match(segment);
                    var s = new RouteSegment
                    {
                        IsToken = m.Groups["isToken"].Value.Length.Equals(1),
                        IsGreedy = m.Groups["isGreedy"].Value.Length.Equals(1),
                        Name = m.Groups["name"].Value
                    };
                    _segments.AddLast(s);
                    _hasGreedy |= s.IsGreedy;

                    continue;
                }
                throw new ArgumentException("Route URL is invalid.", "url");
            }
        }

        private void SetMinimalRequired()
        {
            LinkedListNode<RouteSegment> seg = _segments.Last;
            int sIndex = _segments.Count;
            while (seg != null && MinRequiredSegments.Equals(0))
            {
                if (!seg.Value.IsToken || !this.Defaults.ContainsKey(seg.Value.Name))
                {
                    this.MinRequiredSegments = Math.Max(this.MinRequiredSegments, sIndex);
                }
                sIndex--;
                seg = seg.Previous;
            }
        }

        private void CheckForDefaultsAfterGreedy()
        {
            // check that segments after greedy segment don't define a default
            if (HasGreedy)
            {
                LinkedListNode<RouteSegment> s = _segments.Last;
                while (s != null && !s.Value.IsGreedy)
                {
                    if (s.Value.IsToken && _defaults.ContainsKey(s.Value.Name))
                    {
                        throw new ArgumentException(string.Format("Defaults for route segment \"{0}\" is not allowed, because it's specified after greedy catch-all segment.", s.Value.Name), "defaults");
                    }
                    s = s.Previous;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderTest48.Domain.Dtos
{
    public class Spider
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Position CurrentPosition { get; set; }
        public string CurrentOrientation { get; set; }

        public Spider()
        {
            
        }

        public Spider(string id, string name, Position currentPosition, string currentOrientation)
        {
            Id = id;
            Name = name;
            CurrentPosition = currentPosition;
            CurrentOrientation = currentOrientation;
        }
    }
}

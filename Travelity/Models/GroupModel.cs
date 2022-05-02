using System.Collections.Generic;

namespace Travelity.Models

{
    public class GroupModel
    {
        public string Name { get; set; }
        public string Destination { get; set; }
        public List<PeopleModel> People { get; set; }
        public List<AlbumModel> Albums { get; set; }
        public string LocationThumbnail { get; set; }
        public string MapThumbnail { get; set; }

    }
}

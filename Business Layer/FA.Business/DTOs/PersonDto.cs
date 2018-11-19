using System.Collections.Generic;

namespace FA.Business.DTOs
{
    public class PersonDto
    {
        public string PersonId { get; set; }
        public List<string> PersistedFaceIds { get; set; }
        public string Name { get; set; }
        public string UserData { get; set; }
    }
}

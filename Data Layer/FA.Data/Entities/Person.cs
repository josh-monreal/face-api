using System.Collections.Generic;

namespace FA.Data.Entities
{
    public class Person
    {
        public string PersonId { get; set; }
        public List<string> PersistedFaceIds { get; set; }
        public string Name { get; set; }
        public string UserData { get; set; }
    }
}

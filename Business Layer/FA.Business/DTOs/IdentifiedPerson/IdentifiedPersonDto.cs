using System.Collections.Generic;

namespace FA.Business.DTOs.IdentifiedPerson
{
    public class IdentifiedPersonDto
    {
        public string FaceId { get; set; }
        public List<CandidateDto> Candidates { get; set; }
    }
}

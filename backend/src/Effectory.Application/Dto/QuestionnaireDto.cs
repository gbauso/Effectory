using System.Collections.Generic;

namespace Effectory.Application.Dto
{
    public class QuestionnaireDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace Effectory.Application.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public SubjectDto Subject { get; set; }
        public AnswerTypeEnumDto AnswerType { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}

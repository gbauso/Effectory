using Effectory.Shared.Domain;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Effectory.Core.Model
{
    public class Subject : IValueObject
    {
        private Subject() { }

        public int SubjectId { get; private set; }
        public int OrderNumber { get; private set; }
        public IDictionary<string, string> Texts { get; private set; }
        public ItemType ItemType { get; private set; }
        public ICollection<Question> Questions { get; private set; }
    }
}

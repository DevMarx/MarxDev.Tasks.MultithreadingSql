using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarxDev.Tasks.MultithreadingSql
{
    public class StringGeneration
    {
        public Guid Id { get; set; }
        public int ThreadId { get; set; }
        public string Text { get; set; }

        [NotMapped]
        public int? GenerationNumber { get; set; }

    }
}

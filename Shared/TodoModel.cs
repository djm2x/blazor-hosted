using System;

namespace MyBlazor.Shared
{
    public class TodoModel
    {
        public int Id { get; set; }

        public string Todo { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; }

    }
}

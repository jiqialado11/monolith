using SubContractors.Common.EfCore;

namespace SubContractors.Domain.Project.Task
{
    public class Note : Entity<int>
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public NoteStatus Status { get; set; }
        public Task Task { get; set; }
    }

    public enum NoteStatus
    {
        Active = 1,
        InActive = 2
    }
}
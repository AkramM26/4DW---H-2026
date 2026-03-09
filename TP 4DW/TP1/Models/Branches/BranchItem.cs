using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Branches
{
    public class BranchItem
    {

        public Guid Id { get; set; }     
        public required bool Status { get; set; }
        public required string Name { get; set; }
    }
}

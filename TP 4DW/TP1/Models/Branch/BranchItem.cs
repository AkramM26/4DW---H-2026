using System.ComponentModel.DataAnnotations;

namespace TP1.Models.Branch
{
    public class BranchItem
    {

        public Guid Id { get; set; }     
        public required bool Status { get; set; }
        public required string Name { get; set; }
    }
}

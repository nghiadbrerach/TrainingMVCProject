//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingMVCProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TraineeAssign
    {
        public int TraineeAssignID { get; set; }
        public string CourseID { get; set; }
        public string TraineeID { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Trainee Trainee { get; set; }
    }
}

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
    
    public partial class CourseCategory
    {
        public string CourseCategoryID { get; set; }
        public string CourseCategoryName { get; set; }
        public string CourseCategoryDescription { get; set; }
        public string CourseID { get; set; }
    
        public virtual Course Course { get; set; }
    }
}

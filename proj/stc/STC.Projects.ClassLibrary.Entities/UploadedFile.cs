//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STC.Projects.ClassLibrary.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class UploadedFile
    {
        public int FileId { get; set; }
        public string FileOriginalName { get; set; }
        public Nullable<System.DateTime> FileUploadTime { get; set; }
        public Nullable<int> FileInsertedRowsCount { get; set; }
        public Nullable<int> FileUploadedBy { get; set; }
        public Nullable<int> FileSourceType { get; set; }
        public Nullable<int> FileDuplicatedRowsCount { get; set; }
        public Nullable<int> FileCorruptedRowsCount { get; set; }
    }
}

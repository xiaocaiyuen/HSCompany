//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shu.Model
{
    
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Workflow_AuditActionDefinition
    {
        public string AuditActionDefinitionID { get; set; }
        public string AuditActionDefinition_Code { get; set; }
        public string AuditActionDefinition_Name { get; set; }
        public Nullable<bool> AuditActionDefinition_IsStartWF { get; set; }
        public Nullable<System.DateTime> AuditActionDefinition_UpdateTime { get; set; }
        public string AuditActionDefinition_UpdateUserID { get; set; }
        public Nullable<bool> AuditActionDefinition_IsDeleted { get; set; }
    }
}
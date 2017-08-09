using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class cars
    {
        private string cField;
        private carsCar[] carField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string C
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("car", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public carsCar[] car
        {
            get
            {
                return this.carField;
            }
            set
            {
                this.carField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class carsCar
    {
        private string sField;
        private double xField;
        private double yField;
        private string cField;
        private string idField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "integer")]
        public string S
        {
            get
            {
                return this.sField;
            }
            set
            {
                this.sField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double X
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public double Y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
        public string c
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "integer")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
}
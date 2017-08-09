using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer.Class
{
    public class PlateKey
    {
        public string PlateNo{get;set;}

        public long? PlateOrgNo{get;set;}

        public int? PlateColorCode{get;set;}

        public int? PlateKindCode{get;set;}

        public int? PlateTypeCode{get;set;}

        public int? PlateSourceCode{get;set;}

        public string PlateColorArabicDesc{get;set;}

        public string PlateColorEnglishDesc{get;set;}

        public string PlateSourceArabicDesc{get;set;}

        public string PlateSourceEnglishDesc{get;set;}

        public string PlateKindArabicDesc{get;set;}

        public string PlateKindEnglishDesc{get;set;}

        public string PlateTypeArabicDesc{get;set;}

        public string PlateTypeEnglishDesc{get;set;}
    }
}
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    [ServiceContract]
    public interface IViolationsLayer
    {
        [OperationContract]
        [WebGet]
        AssetViolationCountDTO GetViolationCountsByAssetCode(string assetCode);
        [OperationContract]
        [WebGet]
        UploadedFileReportDTO UploadViolationFile(List<ManualViolationDTO> violations);
    }
}

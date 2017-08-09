using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace STC.Projects.WCF.ServiceLayer
{
    public class ViolationsLayer : IViolationsLayer
    {
        public AssetViolationCountDTO GetViolationCountsByAssetCode(string assetCode)
        {
            return new ViolationNotificationDAL().GetViolationCountsByAssetCode(assetCode);
        }

        public UploadedFileReportDTO UploadViolationFile(List<ManualViolationDTO> violations)
        {
            try
            {
                var dal = new ManualViolationDAL();

                if (!dal.SaveManualViolationListToCDS(violations))
                    return null;

                dal.SaveManualViolationListToOperational(violations);

                return new UploadedFileReportDTO
                {
                    InsertedRowsCount = dal.insertedRowsCount,
                    DuplicatedRowsCount = dal.duplicatedRowsCount,
                    CorruptedRowsCount = dal.corruptedRowsCount
                };
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog(ex);
                return null;
            }
        }
    }
}

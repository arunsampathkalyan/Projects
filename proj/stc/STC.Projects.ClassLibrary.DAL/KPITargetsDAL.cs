using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STC.Projects.ClassLibrary.DAL
{
    public class KPITargetsDAL
    {
        private STCOperationalDataContext operationalDataContext = new STCOperationalDataContext();
        public List<KPITargetDTO> GetAllTargets()
        {
            return operationalDataContext.KPITargets
                .Select(y => new KPITargetDTO
                {
                    CreatedBy = y.CreatedBy,
                    DateCreated = y.DateCreated,
                    DateModified = y.DateModified,
                    ModifiedBy = y.ModifiedBy,
                    TaregtDescriptionEn = y.TaregtDescriptionEn,
                    TargetDescriptionAr = y.TargetDescriptionAr,
                    TargetID = y.TargetID,
                    TargetName = y.TargetName,
                    TargetValue = y.TargetValue
                }).ToList();
        }


        public bool UpdateTarget(string keyName,double newValue,int userId)
        {
            var oldTarget = operationalDataContext.KPITargets.FirstOrDefault(x => x.TargetName == keyName);
            if (oldTarget == null)
                return false;
            
            oldTarget.DateModified = DateTime.Now;
            oldTarget.ModifiedBy = userId;
            oldTarget.TargetValue = newValue;

            return operationalDataContext.SaveChanges() > 0;
        }

        public bool SaveTarget(KPITargetDTO target)
        {
            var oldTarget = operationalDataContext.KPITargets.FirstOrDefault(x=> x.TargetID == target.TargetID);
            if (oldTarget == null)
                oldTarget = new KPITarget();
            oldTarget.CreatedBy = target.CreatedBy;
                    oldTarget.DateCreated = target.DateCreated;
                    oldTarget.DateModified = target.DateModified;
                    oldTarget.ModifiedBy = target.ModifiedBy;
                    oldTarget.TaregtDescriptionEn = target.TaregtDescriptionEn;
                    oldTarget.TargetDescriptionAr = target.TargetDescriptionAr;
                    oldTarget.TargetID = target.TargetID;
                    oldTarget.TargetName = target.TargetName;
                    oldTarget.TargetValue = target.TargetValue;
                    
            operationalDataContext.KPITargets.Add(oldTarget);
            return operationalDataContext.SaveChanges() > 0;
        }

        public KPITargetDTO GetKPITargetByKey(string keyname)
        {
            return operationalDataContext.KPITargets.Where(x =>x.TargetName == keyname)
                .Select(y => new KPITargetDTO
                {
                    CreatedBy = y.CreatedBy,
                    DateCreated = y.DateCreated,
                    DateModified = y.DateModified,
                    ModifiedBy = y.ModifiedBy,
                    TaregtDescriptionEn = y.TaregtDescriptionEn,
                    TargetDescriptionAr = y.TargetDescriptionAr,
                    TargetID = y.TargetID,
                    TargetName = y.TargetName,
                    TargetValue = y.TargetValue
                }).FirstOrDefault();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: If you change the class name "wcfService" here, you must also update the reference to "wcfService" in Web.config.
public class wcfService : IwcfService
{
    public List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> GetPackDTLForTrans(string COMP_CODE, string BRANCH_CODE)
    {
        try
        {
            List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> dt = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetPackDTLForTrans(COMP_CODE, BRANCH_CODE);
            return dt;
        }
        catch
        {
            throw;
        }
    }
    
    public List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> GetPackDTLForFGAppr(string COMP_CODE, string BRANCH_CODE)
    {
        try
        {
            List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> dt = SaitexBL.Interface.Method.YRN_PROD_PACKING.GetPackDTLForFGAppr(COMP_CODE, BRANCH_CODE);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    public bool UpdatePackDtlTranToFG(SaitexDM.Common.DataModel.YRN_PROD_PACKING oYRN_PROD_PACKING)
    {
        try
        {
            return SaitexBL.Interface.Method.YRN_PROD_PACKING.UpdatePackDtlTranToFG(oYRN_PROD_PACKING);

        }
        catch
        {
            throw;
        }
    }

    public bool UpdatePackDtlapprByFG(SaitexDM.Common.DataModel.YRN_PROD_PACKING oYRN_PROD_PACKING)
    {
        try
        {
            return SaitexBL.Interface.Method.YRN_PROD_PACKING.UpdatePackDtlapprByFG(oYRN_PROD_PACKING);

        }
        catch
        {
            throw;
        }
    }
}

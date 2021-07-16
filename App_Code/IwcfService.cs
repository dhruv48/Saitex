using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: If you change the interface name "IwcfService" here, you must also update the reference to "IwcfService" in Web.config.
[ServiceContract]
public interface IwcfService
{
    [OperationContract]
    List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> GetPackDTLForTrans(string COMP_CODE, string BRANCH_CODE);

    [OperationContract]
    List<SaitexDM.Common.DataModel.YRN_PROD_PACKING> GetPackDTLForFGAppr(string COMP_CODE, string BRANCH_CODE);

    [OperationContract]
    bool UpdatePackDtlTranToFG(SaitexDM.Common.DataModel.YRN_PROD_PACKING oYRN_PROD_PACKING);

    [OperationContract]
    bool UpdatePackDtlapprByFG(SaitexDM.Common.DataModel.YRN_PROD_PACKING oYRN_PROD_PACKING);
}

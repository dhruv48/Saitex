using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Common;
using System.Web;
using System.Data;

namespace WCFMain
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class custapp
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];
        DataTable dtPODetail = new DataTable();
        bool result = false;
        // Add [WebGet] attribute to use HTTP GET
        [OperationContract]
        public bool confirmCustomerRequest(DataTable dtPODetail)
        {
            try
            {

                int iResult = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
                if (iResult > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch
            {
                throw;
            }
            // Add your operation implementation here

        }

        [OperationContract]
        public bool ClosedCustomerRequest()
        {
            try
            {
                if (HttpContext.Current.Session["dtPODetailClosed"] != null)
                {

                    dtPODetail = HttpContext.Current.Session["dtPODetailClosed"] as DataTable;

                    int iResult = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
                    if (iResult > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }

        [OperationContract]
        public DataTable GetCRBySearchFilterUnApprovedOnly(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty,string partyYarn)
        {
            try
            {
                DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterUnApprovedOnly(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty, partyYarn);

                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }


        [OperationContract]
        public DataTable GetCRBySearchFilterApprovedOnly(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party)
        {
            try
            {
                DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterApprovedOnly(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party);

                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }
        [OperationContract]
        public DataTable GetCRBySearchFilterApprUnClose(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty,string PARTY_ARTICLE_DESC)
        {
            try
            {
                DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterApprUnClose(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty, PARTY_ARTICLE_DESC);

                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }
        [OperationContract]
        public DataTable GetCRBySearchFilterApprClose(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty)
        {
            try
            {
                DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterApprClose(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty);

                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }
        [OperationContract]
        public DataTable GetCRBySearchFilterAll(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty)
        {
            try
            {
                DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterAll(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty);

                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }

        // Add more operations here and mark them with [OperationContract]

        public DataTable GetCRBySearchFilterUnApprovedOnly(string COMP_CODE, int YEAR, string PRODUCT_TYPE, string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty)
        {
            try
            {
                DataTable result = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.GetCRBySearchFilterUnApprovedOnly(COMP_CODE, YEAR, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty,"");

                return result;

            }
            catch
            {
                throw;

            }
        }
    }

    public class WIP_LOT_MOVEMENT
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];
        DataTable dtPODetail = new DataTable();
        bool result = false;
        // Add [WebGet] attribute to use HTTP GET

        [OperationContract]
        public bool ConfirmLotMovement()
        {
            try
            {
                if (HttpContext.Current.Session["dtPODetail"] != null)
                {
                    dtPODetail = HttpContext.Current.Session["dtPODetail"] as DataTable;

                    int iResult = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
                    if (iResult > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
            // Add your operation implementation here

        }

        [OperationContract]
        public bool RejectLotMovement()
        {
            try
            {
                if (HttpContext.Current.Session["dtPODetailClosed"] != null)
                {

                    dtPODetail = HttpContext.Current.Session["dtPODetailClosed"] as DataTable;

                    int iResult = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
                    if (iResult > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }

        [OperationContract]
        public bool GetLotMovement()
        {
            try
            {
                if (HttpContext.Current.Session["dtPODetailClosed"] != null)
                {

                    dtPODetail = HttpContext.Current.Session["dtPODetailClosed"] as DataTable;

                    int iResult = SaitexDL.Interface.Method.OD_CAPTURE_FIBER_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
                    if (iResult > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;

            }
            catch
            {
                throw;

            }
            // Add your operation implementation here

        }

        // Add more operations here and mark them with [OperationContract]
    }

    public class DynamicGridForQueryForm
    {
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

        [OperationContract]
        public DataTable GetDataByQuery(string Query)
        {
            try
            {
                DataTable data = null;
                if (HttpContext.Current.Session["LoginDetail"] != null)
                {
                    oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)HttpContext.Current.Session["LoginDetail"];

                    data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(Query, "", "", "", "", "");
                }

                return data;
            }
            catch
            {
                throw;
            }
            // Add your operation implementation here

        }

    }

    public class YRN_PROD_PACK
    {

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Services;

/// <summary>
/// Summary description for MOM
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MOM : System.Web.Services.WebService
{

    public MOM()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public string[] AutoYarntxtYarnDescL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchYarnReceivingData("YARN_DESC", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["YARN_DESC"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

   

    [WebMethod]
    public string[] AutoYarnyarncode(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("FIBER_CODE", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["FIBER_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarnyarncat(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("YARN_CAT", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["YARN_CAT"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarnyarntype(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("YARN_TYPE", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["YARN_TYPE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtPly(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("PLY", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["PLY"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtColour(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("COLOUR", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["COLOUR"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtUOM(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("UOM", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["UOM"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtYarnDesc(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("YARN_DESC", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["YARN_DESC"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtMaxStock(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("MAX_STOCK", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["MAX_STOCK"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtFancyEffect(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("FANCY_EFFECT", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["FANCY_EFFECT"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtBlending(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingData("BLENDING_PROCESS", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["BLENDING_PROCESS"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }



    [WebMethod]
    public string[] AutoYarntxtYarnCodeL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("FIBER_CODE", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["FIBER_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtPoNumbL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("PO_NUMB", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["PO_NUMB"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    public string[] AutoYarntxtLotNoL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("LOT_NO", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["LOT_NO"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtgradeL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("GRADE", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["GRADE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtNOOFUNITL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("NO_OF_UNIT", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["NO_OF_UNIT"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtSHADECODEL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("SHADE_CODE", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["SHADE_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtSHADEFAMILYL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("SHADE_FAMILY", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["SHADE_FAMILY"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtRGB(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("RGB", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["RGB"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtLOCATION(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("LOCATION", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["LOCATION"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtSTORE(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("STORE", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["STORE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
    public string[] AutoYarntxtWEIGHTOFUNITL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.YARN_QUERY_MASTER.SearchFabricReceivingDataLOT("WEIGHT_OF_UNIT", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["WEIGHT_OF_UNIT"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetItemDescription(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.ItemMaster.SelectItemDescriptionFromItemMaster(prefix);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["ITEM_DESC"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
        }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetYarnDescription(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.YRN_MST.SelectYarnDescriptionFromYarnMaster(prefix);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["YARN_DESC"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetFiberDescription(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.TX_FIBER_MST.SelectFiberDescriptionFromFiberMaster(prefix);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["FIBER_DESC"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetPartyDescription(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.TX_VENDOR_MST.SelectPartyDescriptionFromVendorMaster(prefix);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["PRTY_NAME"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtPartyNameL(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.TX_VENDOR_MST.SearchFabricReceivingDataLOT("PRTY_NAME", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["PRTY_NAME"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetMOMState(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectMOMDetails(prefix, "STATE");
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["MST_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetMOMMerge(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectMOMDetails(prefix, "MERGE_NO");
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["MST_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetMOMGrade(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectMOMDetails(prefix, "GRADE");
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["MST_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetMOMPalletMaster(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectMOMDetails(prefix, "PALLET_MASTER");
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["MST_CODE"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }

    [WebMethod]
    public string[] AutoYarntxtPartyNameLot(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = SaitexBL.Interface.Method.TX_VENDOR_MST.SearchFabricDataLOT("PRTY_NAME", prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["PRTY_NAME"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] AutoLotFromYarnLotMakingMaster(string prefix)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = SaitexDL.Interface.Method.YRN_LOT_MAKING.SearchLotNoFromYarnLotMaster("LOT_NO",prefix,"%");
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["LOT_NO"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
}

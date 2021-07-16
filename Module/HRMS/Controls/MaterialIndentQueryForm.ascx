<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialIndentQueryForm.ascx.cs"
    Inherits="Inventory_Controls_MaterialIndentQueryForm" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>

<script type="text/javascript">
    var isAppliedFilter = false;
       var printGridOnCallback = false;
		    var currentPageSize = 10;
    function applyFilter() {

        document.getElementById('apply').style.display = '';
        document.getElementById('remove').style.display = '';
        document.getElementById('hide').style.display = '';
        document.getElementById('show').style.display = 'none';

        grid1.filter()
        isAppliedFilter = true;
        return false;
    }
    function hideFilter() {

        if (isAppliedFilter == true) {
            document.getElementById('remove').style.display = '';
        } else {
            document.getElementById('remove').style.display = 'none';
        }
        document.getElementById('show').style.display = '';
        document.getElementById('hide').style.display = 'none';
        document.getElementById('apply').style.display = 'none';

        grid1.hideFilter()

        return false;
    }
    function showFilter() {

            if (isAppliedFilter == true) {
                document.getElementById('remove').style.display = '';
            } else {
                document.getElementById('remove').style.display = 'none';
            }
            document.getElementById('apply').style.display = '';
            document.getElementById('hide').style.display = '';
            document.getElementById('show').style.display = 'none';
            grid1.showFilter();

            return false;
    }

    function removeFilter() {

        document.getElementById('show').style.display = '';
        document.getElementById('apply').style.display = 'none';
        document.getElementById('hide').style.display = 'none';
        document.getElementById('remove').style.display = 'none';

        grid1.removeFilter();
        grid1.hideFilter();
        isAppliedFilter = false;
        return false;
    }
   
		    function printGrid(printAll) {
		        if (printAll) {
		            printGridOnCallback = true;
		            currentPageSize = grid1.getPageSize();
		            ob_grid1PageSizeSelector.value(-1);
		        } else {
		            grid1.print();
		        }

		        return false;
		    }

		    function grid1_Callback() {
		        if (printGridOnCallback) {
		            grid1.print();
		            printGridOnCallback = false;
		            ob_grid1PageSizeSelector.value(currentPageSize);
		        }
		    }
</script>

<style type="text/css">
    .tdText
    {
        font: 9pt Arial;
        color: #333333;
    }
    
    .titleheading
{
	font-family: arial;
	font-size: 12px;
	font-weight: bold;
	color: #ffffff;
}

    
    .option2
    {
        font: 9pt Arial;
        color: #0033cc;
        padding-left: 2px;
        padding-right: 2px;
    }
    a
    {
        font: 9pt Arial;
        color: #315686;
        text-decoration: underline;
    }
    .td
    {
        border-style: ridge;
        border-bottom-width: .5px;
        border-color: #C1D3FB;
    }
    a:hover
    {
        color: crimson;
    }
   
.ob_gMCont_DT
{
    overflow: hidden;
}

.ob_gMCont
{
    position: relative;
}

.ob_gFCont_F_D
{		
	border-style: none;
        border-color: inherit;
        border-width: 0px;
background-image: url('../../../StyleSheet/black_glass/footer.png');
	    margin-top: 0px;
	    margin-left: 5px;
	    margin-right: 5px;
	    height: 27px;
	    padding-left: 20px;
	    padding-top: 0px;
	    padding-bottom: 0px;
	    vertical-align: middle;
	    position: relative;
	    cursor: default;
	    font-family: Verdana;
	    font-size: 10px;
	    color: #080808;
	    white-space: nowrap;
	    z-index: 0;
	    overflow: hidden;
        padding-right: 0px;
    }

.ob_gPSTT
{
	position: relative;
	top:9px;
	display:-moz-inline-stack;
    display:inline-block;
	height: 19px;
	z-index: 2;
	padding-top: 1px;
}

.ob_gFCont_F_D .ob_gFEC
{
    width: 100%;
}

.ob_gFEC
{
    position: absolute;
    top: 6px;
    right: 0px;
    height: 23px;
    white-space: nowrap;
    z-index: 1;
}

.ob_gFCont_F_D .ob_gFP
{
    width: 305px;
}

.ob_gFP
{
    float: right;
    #float: none;
    margin-right: 6px;
}

.ob_gFCont_F_D .ob_gFALC
{
    float: none;
}

.ob_gFALC
{
    height: 19px;
    line-height: 20px;
    float: right;
    #float: none;
}

.ob_gALFS /*.ob_gridAdd_FilterSpacer*/
{
	width: 30px;
	text-align: center;
	display:-moz-inline-stack;
    display:inline-block;
    zoom:1;
    *display:inline;
    float: right;
}

.ob_gFAL
{
    display:-moz-inline-stack;
    display:inline-block;
    zoom:1;
    *display:inline;
    float: right;
}

.ob_gFContTL
{
    width: 25px;
	height: 46px;
	background-image: url('../../../StyleSheet/black_glass/grouping_area_left.png');
	position: absolute;
	left: -25px;
	top: 0px;
}

.ob_gFContTR
{
    width: 25px;
	height: 46px;
	background-image: url('../../../StyleSheet/black_glass/grouping_area_right.png');
	position: absolute;
	right: -25px;
	top: 0px;
}

.ob_gHCont
{
    position: relative;
    overflow: hidden;
    margin-left: 5px;
    margin-right: 5px;
}

.ob_gMCont_DT .ob_gHICont
{
    height: 25px;
}

* html .ob_gHICont, .ob_gMCont_DT .ob_gHICont
{
    width: 100%;
}

.ob_gHICont
{
    position: relative;
    overflow: hidden;
}

.ob_gH
{
    border-collapse: collapse;
    table-layout: fixed;
}


.ob_gHCont .ob_gH .ob_gC, .ob_gHCont .ob_gH .ob_gCW
{
    background-image: url('../../../StyleSheet/black_glass/header.png');
    background-repeat: repeat-x;
    background-color: #A8AEBD;
    height: 25px;
    font-family: Verdana;
	font-size: 11px;
	font-weight: normal;
	color: #01354d;
	text-align: left;
	cursor: pointer;
}

.ob_gC
{
    white-space: nowrap;
    cursor: default;
    padding: 0px;
    margin: 0px;
    vertical-align: middle;
}

.ob_gHCont .ob_gH .ob_gC div.ob_gCc1, .ob_gHCont .ob_gH .ob_gCW div.ob_gCc1
{
    padding-bottom: 6px;
}

.ob_gMCont_DT .ob_gH div.ob_gCc1
{
    margin-top: -6px;
}

.ob_gH .ob_gC div.ob_gCc1, .ob_gH .ob_gCW div.ob_gCc1
{
    position: relative;
}

.ob_gC_Fc div.ob_gCc2, .ob_gC_Fc div.ob_gCc2C, .ob_gC_Fc div.ob_gCc2R
{
    /*padding-left: 25px;*/
    padding-left: 20px;
}

.ob_gH div.ob_gCc2, .ob_gH div.ob_gCc2C, .ob_gH div.ob_gCc2R
{
    margin-right: 16px;
}

.ob_gC div.ob_gCc2, .ob_gCW div.ob_gCc2, .ob_gC div.ob_gCc2C, .ob_gCW div.ob_gCc2C, .ob_gC div.ob_gCc2R, .ob_gCW div.ob_gCc2R
{
    padding: 0px;
    /*padding-top: 2px;*/
    padding-left: 20px;
    margin-right: 3px;
    position: static;
    width: auto;
    overflow: hidden;
}

.ob_gC div.ob_gCc2
{
    white-space: nowrap;
}

.ob_gH .ob_gC div.ob_gHSI_F_D, .ob_gH .ob_gCW div.ob_gHSI_F_D
{
    margin-top: -10px;
}

.ob_gH .ob_gC div.ob_gHSI, .ob_gH .ob_gCW div.ob_gHSI
{
    position: absolute;
    height: 15px;
    right: 2px;
    top: 50%;    
    margin: 0px;
    margin-top: -11px;
    margin-top /*\**/: -10px\9;
    #margin-top: -11px;
    -margin-top: -14px;    
}

.ob_gH .ob_gC_Lc div.ob_gCc2, .ob_gH .ob_gC_Lc div.ob_gCc2C, .ob_gH .ob_gC_Lc div.ob_gCc2R
{
    margin-right: 26px;
}


.ob_gC_Lc div.ob_gCc2, .ob_gC_Lc div.ob_gCc2C, .ob_gC_Lc div.ob_gCc2R
{
    margin-right: 12px;
}

.ob_gH .ob_gC_Lc div.ob_gHSI
{
    right: 12px;
}

.ob_gHCont .ob_gCS, .ob_gHContWG .ob_gCS
{
    bottom: 0px;
}

.ob_gHCont .ob_gCS, .ob_gHContWG .ob_gCS, .ob_gHCont .ob_gCS_F, .ob_gHContWG .ob_gCS_F
{
    position: absolute;
    top: 0px;    
    font-size: 1px;
    cursor: e-resize;
    padding-left: 1px;
    padding-right: 1px;
    margin-left: -1px;
    background-color: Transparent !important;
}

.ob_gHCont .ob_gCS div, .ob_gHContWG .ob_gCS div, .ob_gHCont .ob_gCS_F div, .ob_gHContWG .ob_gCS_F div
{
    width: 1px;
    position: absolute;
    top: 0px;
    bottom: 0px;
    height: 100%;
}





.ob_gHCont .ob_gCS div, .ob_gHCont .ob_gCS_F div
{
    background-color: #EEEEEE;
}

.ob_gFlCont
{
    position: relative;
    overflow: hidden;
    margin-left: 5px;
    margin-right: 5px;
}

* html .ob_gFlICont, .ob_gMCont_DT .ob_gFlICont
{
    width: 100%;
}

.ob_gFlICont
{
    position: relative;
    overflow: hidden;
}

.ob_gFl
{
    border-collapse: collapse;
    table-layout: fixed;
}

.ob_gFl .ob_gC, .ob_gFl .ob_gCW
{
    background-image: url('../../../StyleSheet/black_glass/filter.png');
    height: 50px;
    font-family: Verdana;
	font-size: 10px;
	font-weight: normal;
	color: #FFFFFF;
	text-align: left;
}

.ob_gFl .ob_gC div.ob_gCc1, .ob_gFl .ob_gCW div.ob_gCc1
{    
    overflow: visible;
    position: relative;
    padding-top: 2px;
    padding-bottom: 2px;
}


	.ob_iDdlITCN
	{
		position: relative;
        display:-moz-inline-stack;
        display:inline-block;
        zoom:1;
        *display:inline;
        height: 21px;
        font-size: 10px;
    	padding: 0px;
    	text-align: center;    	
	}
	
	.ob_iDdlITCN .ob_iDdlTL
	{
		background-position: 0px 0px;
	}
	
	.ob_iDdlTL
	{
		position: absolute;
    	font-size: 1px;
    	height: 21px;
    	width: 7px;
    	left: 0px;
    	cursor: pointer;
    	background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/horizontal.png');
	}
	
	.ob_iDdlITCN .ob_iDdlTR
	{
		background-position: -7px 0px;
	}
	
	.ob_iDdlTR
	{
		position: absolute;    	
    	font-size: 1px;
    	height: 21px;
		width: 26px;
		right: 0px;
		cursor: pointer;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/horizontal.png');
	}
	
	.ob_iDdlITCN .ob_iDdlTC
	{
		background-position: 0px 0px;
	}
		
	.ob_iDdlTC
	{
		height: 21px;
		line-height: 21px;
		cursor: pointer;
		margin-left: 7px;
		margin-right: 26px;
		position: relative;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/vertical.png');
	}
	
	.ob_iDdlITCN .ob_iDdlIE
	{
	    color: #2b4c61;
	}
	
	
	
	
	
	.ob_iDdlIE
	{
		width: 100%;
		position: absolute;
		left: 0px;
		right: 0px;
		top:0px;
		display: block;
    	background-color: transparent;
    	border: 0px;
    	margin: 0px;
    	padding: 0px;
    	margin-top: 4px !important;
    	font-family: Verdana !important;
		font-size: 10px !important;
		height: 13px !important;
		cursor: pointer;
	}
	
	.ob_iDdlIC
	{
		width: 100%;
		height: auto;
		font-size: 10px;
		position: absolute;
		top:20px;
		left:0px;
		visibility: hidden;
		z-index: 9999;
		border: 0px;
		padding-bottom: 5px;
	}
	
	.ob_iDdlICH
	{
		position: relative;
    	height: 12px;
    	font-size: 1px;
	}
	
	.ob_iDdlICHCL
	{
		position: absolute;
    	width: 12px;
    	height: 12px;
    	top: 0px;
    	left: -5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-repeat: no-repeat;
		background-position: 0px 0px;
	}
	
	.ob_iDdlICHCM
	{
		height: 12px;
    	margin-left: 5px;
	    margin-right: 5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-repeat: repeat-x;
		background-position: 0px -120px;
	}
	
	.ob_iDdlICHCR
	{
		position: absolute;
    	width: 12px;
    	height: 12px;
    	top: 0px;
    	right: -5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-repeat: no-repeat;
		background-position: 0px -42px;
	}
	
	.ob_iDdlICB
	{
		position: relative;
    	height: 100%;
    	font-size: 1px;
	}
	
	
	.ob_iDdlICBL
	{
		position: absolute;
    	width: 12px;
    	height: 100%;
    	top: 0px;
    	left: -5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-vertical.png');
		background-position: 0px 0px;
		overflow: hidden;
		z-index: 5;
	}
	
	.ob_iDdlICBLI
	{
		position: absolute;
    	width: 6px;
    	height: 30px;
    	top: 0px;
    	left: 6px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-position: -6px -12px;
	}
	
	.ob_iDdlICBC
	{
		margin: 0px 3px;
        position: relative;
    	    width: auto;
    	    height: 100%;
    	    min-height: 42px;
    	    overflow: auto;
    	padding-bottom: 4px;
	    background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
	        background-position: 0px -132px;
    	    background-repeat: repeat-x;
	        background-color: #d0dee5;
	        font-size: 10px;
	        z-index: 10;
            padding: 0px;
            list-style-type: none !important;
	}
	
		
	
	.ob_iDdlICBC li
    {
        position: relative;
        margin: 0px;
        padding: 0px;
        margin-left: 15px;
        margin-right: 15px;
        height: 19px;
        font-family: Verdana;
	    font-size: 10px;
	    color: #2b4c61;
	    cursor: pointer;
	    background-image: none;
    }
        
    .ob_iDdlICBC li b /*, .ob_iDdlICBC li div*/
    {
        background-position: 0px 0px;
        background-repeat: no-repeat;
        position: absolute;
        left: -15px;
        padding-left: 18px;
        right: 0px;
        height: 19px;
        line-height: 17px;
        font-weight: normal;
        vertical-align: middle;
        white-space: nowrap;
        overflow: hidden;
        text-align: left !important;
    }
    
    .ob_iDdlICBC li i
    {
        background-position: 0px -38px;
        background-repeat: no-repeat;
        position: absolute;
        right: -15px;
        height: 19px;
        width: 15px;
        text-indent: 20px;
        overflow: hidden;
        font-size: 0px;
        white-space: nowrap;
    }
    
    .ob_iDdlICBR
	{
		position: absolute;
    	width: 12px;
    	height: 100%;
    	top: 0px;
    	right: -5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-vertical.png');
		background-position: -12px 0px;
		overflow: hidden;
		z-index: 5;
	}
	
	.ob_iDdlICBRI
	{
		position: absolute;
    	width: 6px;
    	height: 30px;
    	top: 0px;
    	right: 6px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-position: 0px -54px;
	}
	
	.ob_iDdlICF
	{
		position: absolute;
    	height: 12px;
    	font-size: 1px;
    	width: 100%;
    	left: 0px;
	}
	
	.ob_iDdlICFCL
	{
		position: absolute;
    	width: 12px;
    	height: 12px;
    	top: 0px;
    	left: -5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-position: 0px -84px;
		background-repeat: no-repeat;
		z-index:2;
	}
	
	.ob_iDdlICFCM
	{
		height: 12px;
    	margin-left: 7px;
	    margin-right: 7px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-position: 0px -96px;
		background-repeat: repeat-x;
		z-index:1;
	}
	
	.ob_iDdlICFCR
	{
		position: absolute;
    	width: 12px;
    	height: 12px;
    	top: 0px;
    	right: -5px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutDropDownList/images/drop-down-horizontal.png');
		background-position: 0px -108px;
		background-repeat: no-repeat;
	}
	
	.ob_iTCN
	{
		position: relative;
        display:-moz-inline-stack;
        display:inline-block;
        zoom:1;
        *display:inline;
        height: 21px;
        font-size: 10px;
    	padding: 0px;
    	text-align: center;
    	color: #2b4c61;
	}
	
	.ob_iTCN .ob_iTL
	{
		background-position: 0px 0px;
	}
	
	.ob_iTL
	{
		position: absolute;
    	font-size: 1px;
    	height: 21px;
    	width: 10px;
    	left: 0px;
    	background-image: url('../../../StyleSheet/black_glass/interface/OboutTextBox/images/textbox.png');
	}
	
	.ob_iTCN .ob_iTR
	{
		background-position: -10px 0px;
	}
	
	
	
	
	.ob_iTR
	{
		position: absolute;    	
    	font-size: 1px;
    	height: 21px;
		width: 10px;
		right: 0px;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutTextBox/images/textbox.png');
	}
	
	.ob_iTCN .ob_iTC
	{
		background-position: 0px -84px;
	}
	
	.ob_iTC
	{
		height: 21px;
		line-height: 21px;
		margin-left: 10px;
		margin-right: 10px;
		position: relative;
		background-image: url('../../../StyleSheet/black_glass/interface/OboutTextBox/images/textbox.png');
	}
	
	.ob_iTCN .ob_iTIE
	{
	    color: #2b4c61;
	}
		
	.ob_iTIE
	{
		width: 100%;
		position: absolute;
		left: 0px;
		right: 0px;
		top:0px;
		display: block;
    	background-color: transparent;
    	border: 0px;
    	margin: 0px;
    	padding: 0px;    	
    	margin-top: 4px !important;
    	font-family: Verdana !important;
		font-size: 10px !important;
		height: 13px !important;
		outline: 0;
	}
	
	.ob_gFlCont .ob_gCS
{
    bottom: 0px;
}





.ob_gFlCont .ob_gCS, .ob_gFlCont .ob_gCS_F
{
    position: absolute;
    top: 0px;    
    width: 1px;
    background-color: #EEEEEE;
    font-size: 1px;
}

.ob_gBCont
{
    position: relative;
    overflow: hidden;
    overflow-y: auto;
    margin-left: 5px;
    margin-right: 5px;
}

* html .ob_gBICont, .ob_gMCont_DT .ob_gBICont
{
    width: 100%;
}

.ob_gBICont
{
    position: relative;
    overflow: hidden;
}

.ob_gBody
{
    border-collapse: collapse;
    table-layout: fixed;
}

.ob_gBTLRV
{
    visibility: hidden;
    *display: none;
}

.ob_gBody .ob_gC, .ob_gBody .ob_gCW
{
    height: 25px;
    font-family: Verdana;
	font-size: 10px;
	font-weight: normal;
	text-align: left;
	vertical-align: middle;
}

.ob_gR
{
	background-image: url('../../../StyleSheet/black_glass/row1.png');
	background-position:50% bottom;
	background-repeat:repeat-x;
	background-color:#E5E5E5;	
	color: #2b4c61;
}

.ob_gBody .ob_gC div.ob_gCc1, .ob_gBody .ob_gCW div.ob_gCc1
{    
    #overflow: visible;
    #overflow-x: hidden;
    /*position: relative;*/
    padding-top: 2px;
    padding-bottom: 2px;
}

.ob_gRA
{
	background-image: url('../../../StyleSheet/black_glass/row2.png');
	background-position:50% bottom;
	background-repeat:repeat-x;
	background-color:#ffffff;
	color: #2b4c61;
}


.ob_gBCont .ob_gCS
{
    bottom: 0px;
}

.ob_gBCont .ob_gCS, .ob_gBCont .ob_gCS_F
{
    position: absolute;
    top: 0px;
    width: 1px;
    background-color: #EEEEEE;
    font-size: 1px;
    z-index: 10;
}

div.ob_gBLM
{
    bottom: 0px;
}
div.ob_gBLM, div.ob_gBLM_F
{
    position: absolute;
	background-color: #F6F7F7;
	left: 0px;
	right: 0px;
	top: 0px;
	text-align: center;
	z-index: 30;
	width: 100%;

}
div.ob_gBLM div, div.ob_gBLM_F div
{    
    position: absolute;
    font-family: Verdana;
	font-size: 10px;
	color:#0C416F;
	height: 10px;
	top: 50%;	
	margin-top: -5px;	
	left: 0px;
	right: 0px;
	margin-left: auto;
	margin-right: auto;
	text-align: center;
	width: 100%;
}

.ob_gPBC
{
    float: right;
    cursor: pointer;
    margin-top: 2px;
}

.ob_gPLC
{        
    float: right;
    padding-top: 2px;
    height: 22px;
}

.ob_gPLD
{
    font:normal 11px Verdana;
    color:#000000;
    text-decoration: none;
    cursor: pointer;
    padding:2px 3px 1px 3px;
    border-left: 1px solid #000;
    border-right: 1px solid #000;
    margin-left: 2px;
    margin-right: 2px;
    background-image:url('../../../StyleSheet/black_glass/page_number_selected.png');
    background-repeat: repeat-x;
    float: left;
    height: 20px;
}

.ob_gFPT
{
    float: right;
    padding-top: 4px;
}

.ob_gFCnC
{
	height: 16px;
	position: relative;
	background-image: url('../../../StyleSheet/black_glass/footer_corner_middle.png');
	background-repeat: repeat-x;
	font-size: 1px;
	margin-left: 20px;
	margin-right: 20px;
}

.ob_gFCnL
{
	background-image: url('../../../StyleSheet/black_glass/footer_corner_left.png');
	height: 16px;
	width: 25px;
	background-repeat: no-repeat;
	position: absolute;
	left: -25px;
}

.ob_gFCnR
{
	background-image: url('../../../StyleSheet/black_glass/footer_corner_right.png');
	height: 16px;
	width: 25px;
	background-repeat: no-repeat;
	position: absolute;
	right: -25px;
}

.ob_gBLSW_EL_H, .ob_gBLSW_EL_TF
{
    top: 46px;
}

.ob_gBLS
{
    position: absolute;
    top: 17px;
    bottom: 16px;
    left: -5px;
    margin-top: 0px;
    margin-bottom: 0px;    
    width: 10px;
    font-size: 1px;
    background-image: url('../../../StyleSheet/black_glass/left_side.png');
    background-repeat: repeat-y;
    z-index: 31;
}

.ob_gBRSW_EL_H, .ob_gBRSW_EL_TF
{
    top: 46px;
}

.ob_gBRS
{
    position: absolute;
    top: 17px;
    bottom: 16px;
    right: -5px;
    margin-top: 0px;
    margin-bottom: 0px;    
    width: 10px;
    font-size: 1px;
    background-image: url('../../../StyleSheet/black_glass/right_side.png');
    background-repeat: repeat-y;
    z-index: 31;
}

    </style>
<table class="tdMain" width ="100%">
    <tr>
        <td class="td " width="525">
            <table cellspacing="0" cellpadding="0" border="1">
                <tbody>
                    <tr>
                        <td>
                            <table>
                                <%--<td id="tdSave" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                        ValidationGroup="M1" Enabled="False"></asp:ImageButton>
                                </td>
                                <td id="tdFind" runat="server">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                        Enabled="False"></asp:ImageButton>
                                </td>
                                <td id="tdUpdate" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                        ValidationGroup="M1" Enabled="False"></asp:ImageButton>
                                </td>
                                <td id="tdDelete" runat="server">
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                        Enabled="False"></asp:ImageButton>
                                </td>--%>
                                <%--<td>
                            <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            
                            
                             ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton></td>--%>
                           
                          <td>
                           
                           <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                             </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" 
                                        ImageUrl="~/CommonImages/link_exit.png" onclick="imgbtnExit_Click">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" 
                                        ImageUrl="~/CommonImages/link_help.png" onclick="imgbtnHelp_Click">
                                    </asp:ImageButton>
                                </td>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>


    </tr>
    <tr>
        
     <td  align ="center" class="TableHeader tdinset" >
     
     <span class ="titleheading" >Indent Query Form</span>
     
        </td> 
    </tr>
    
    <tr>
        <td class="td">

  <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" 
    AllowFiltering="True" AutoGenerateColumns="False" FolderStyle="~/StyleSheet/black_glass"
                Serialize="false" AllowPageSizeSelection="false" 
    AllowMultiRecordSelection="False">
                <Columns>
                    <cc2:Column DataField="IND_NUMB" Align="Left" HeaderText="IND_NUMB" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="IND_TYPE" Align="Left" HeaderText="IND_TYPE" Width="90px">
                    </cc2:Column>
                    <cc2:Column DataField="IND_DATE" Align="Left" HeaderText="IND_DATE" Width="60px">
                    </cc2:Column>
                    <cc2:Column DataField="PREP_BY" Align="Left" HeaderText="PREP_BY" Width="60px">
                    </cc2:Column>
                    <cc2:Column DataField="CONF_FLAG" Align="Left" HeaderText="CONF_FLAG" Width="100px">
                    </cc2:Column>
                    <%--<cc2:Column DataField="CONF_BY" Align="Left" HeaderText="CONF_BY" Width="125px">
                    </cc2:Column>
                    <cc2:Column DataField="CONF_DATE" Align="Left" HeaderText="CONF_DATE" Width="90px">
                    </cc2:Column>--%>
                    <cc2:Column DataField="APPR_QTY" Align="Left" HeaderText="APPR_QTY" Width="60px">
                    </cc2:Column>
                    <cc2:Column DataField="RQST_QTY" Align="Left" HeaderText="RQST_QTY" Width="60px">
                    </cc2:Column>
                    <cc2:Column DataField="UOM" Align="Left" HeaderText="UOM" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="ITEM_CODE" Align="Left" HeaderText="ITEM_CODE" Width="125px">
                    </cc2:Column>
                    <cc2:Column DataField="ITEM_DESC" Align="Left" HeaderText="ITEM_DESC" Width="90px">
                    </cc2:Column>
                   
                </Columns>
                <PagingSettings Position="Bottom"/>
                <FilteringSettings InitialState="Visible" FilterPosition="Top" FilterLinksPosition="Top" />
            </cc2:Grid>
         </td>
        </tr>
      </table>
  
        
       
   


  
        

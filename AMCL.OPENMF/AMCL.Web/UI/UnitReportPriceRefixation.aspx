<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportPriceRefixation.aspx.cs" Inherits="UI_UnitReportPriceRefixation" Title=" Unit Price Refixation Report Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
     

    
    function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	
	function  fnReset()
	{
	    document.getElementById("<%=fundNameDropDownList.ClientID%>").value = "0";
	    document.getElementById("<%=refixationDateFromDropDownList.ClientID%>").value = "0";
	    document.getElementById("<%=refixationDateToDropDownList.ClientID%>").value = "0";
	    document.getElementById("<%=effectiveDateFromDropDownList.ClientID%>").value = "0";
	    document.getElementById("<%=effectiveDateToDropDownList.ClientID%>").value = "0";
	 
	    
	     return false;
	}
	
 </script>
    <style type="text/css">
        .style5
        {
            height: 30px;
        }
        .style6
        {
         border:solid 1px #A8ACAF;
        text-align: left;
    }
        .style8
        {
            font-size: small;
            font-weight: bold;
            text-align: right;
        }
        .style9
        {
            text-align: right;
        }
        .style10
        {
            font-size: small;
            font-weight: bold;
        }
        .auto-style1 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            COLOR: #000000;
            FONT-SIZE: 11px;
        }
        .auto-style2 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
        }
        </style>
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
<br />
<br />
 <table align="left" width="1000" align="left" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="FormTitle" align="center">
               Price Refixing Report Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

    <br />
     <br />
      <br />
    
<table width="1000" align="left" cellpadding="0" cellspacing="0" >
    <%--<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>--%>

    
    
     <tr >
        <td class="style9" style="font-size: small"  ><b>Fund Name :</b></td>
        
        <td align="left"   colspan="4"> <asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="fundNameDropDownList_SelectedIndexChanged" 
               ></asp:DropDownList>
            </td>
       
    </tr>
     <tr >
        <td align="left" class="style10" style="text-align: right"  >&nbsp;Refixation Date:</td>
        
        <td align="left"   colspan="4"> <asp:DropDownList ID="refixationDateFromDropDownList" runat="server" 
                    TabIndex="2" 
               ></asp:DropDownList>
            &nbsp;<strong>TO</strong> <asp:DropDownList ID="refixationDateToDropDownList" runat="server" 
                    TabIndex="2" 
               ></asp:DropDownList>
            </td>
       
    </tr>
    <tr class="style5">
       <td class="style8"  >
           Effective Date : </td>
        
        <td align="left" colspan="4">          
            <asp:DropDownList ID="effectiveDateFromDropDownList" runat="server" 
                    TabIndex="2" 
               ></asp:DropDownList>
            <span class="star">&nbsp;</span><strong><span class="auto-style1">TO</span><span class="star">
                </span>
            </strong><span class="star">          
            <asp:DropDownList ID="effectiveDateToDropDownList" runat="server" 
                    TabIndex="2" 
               ></asp:DropDownList>
        &nbsp;<asp:Button ID="findButton" 
                    runat="server" AccessKey="f" CssClass="buttonmid" 
                    onclick="findButton_Click"  Text="Find" />
                </span>
        </td>
         
      
    </tr>
    
     
    
    
  
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
       <tr>
        <td align="center" colspan="5">
        <asp:Button ID="printAllFundButton" runat="server" Text="Print All Fund Last Price" CssClass="auto-style2"
                 Width="151px" OnClick="printAllFundButton_Click"    />&nbsp;&nbsp;
        &nbsp;&nbsp;
        &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
            <table align="center">
                <tr>
                    <td>
                        <div id="dvGridSurrender" runat="server" style="text-align: center; display: block; overflow: auto;  height:350px;">
                            <asp:GridView ID="SurrenderListGridView" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                            
                                                             
                                    <asp:BoundField DataField="FUND_CD" HeaderText="Fund Code" />
                                    <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                    <asp:BoundField DataField="REFIX_DATE" HeaderText="Refixation Date" />
                                    <asp:BoundField DataField="EFFECTIVE_DT" HeaderText="Effective Date" />
                                    <asp:BoundField DataField="REFIX_SL_PR" HeaderText="Sale Price" />
                                    <asp:BoundField DataField="REFIX_REP_PR" HeaderText="Surrender Price" />
                                    <asp:BoundField DataField="NAV_MP" HeaderText="NAV @ Market Price" />
                                    <asp:BoundField DataField="NAV_CP" HeaderText="NAV @ Cost Price" />
                                   
                                 
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
  <tr>
        <td align="center" colspan="5">
        <asp:Button ID="printButton" runat="server" Text="Print" CssClass="buttoncommon"
                 AccessKey="p" OnClick="printButton_Click"    />&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        &nbsp;
        </td>
    </tr>
    
</table>


    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>


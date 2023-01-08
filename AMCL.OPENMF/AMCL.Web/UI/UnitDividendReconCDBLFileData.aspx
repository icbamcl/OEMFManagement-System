<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDividendReconCDBLFileData.aspx.cs" Inherits="UI_UnitDividendReconCDBLFileData" Title=" Dividend Reconsiliation CDBL Data File (Design and Developed by Sakhawat)" %>
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
	
	
	function fnCheqInput()
	{
	 
	     if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
                
            }
          
          
          
            if(document.getElementById("<%=ClosingDateDropDownList.ClientID%>").value =="")
            {                            
                document.getElementById("<%=ClosingDateDropDownList.ClientID%>").focus();
                alert("Please Select Closing Date");
                return false;
            }
	    
	}
 </script>
    
    
    <style type="text/css">
        .style7
        {
            color: #FF3300;
        }
        .style8
        {
            height: 22px;
        }
        .style9
        {
            height: 24px;
        }
        .auto-style2 {
            font-size: medium;
            text-decoration: underline;
        }
        .auto-style3 {
            color: #339933;
        }
        </style>
    
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
<br />
<br />
 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Unit
                Dividend Insert CDBL File Data Form</td>           
            
        </tr> 
      </table>
<br />
    <br />
 
<table width="500" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="100"></colgroup>
      
     <tr >
        <td  ><b>Fund Name :</b></td>
        
        <td align="left" > 
            
            <asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" 
                TabIndex="1">
            </asp:DropDownList>      
            <span class="style7">*</span></td>
        
      
       
    </tr>
  
     <tr >
       <td >
           <b>Closing Date : </b>
        </td>
        
        <td align="left" class="style9">
            <span >
            <asp:DropDownList ID="ClosingDateDropDownList" runat="server" TabIndex="4">
            </asp:DropDownList>
            </span><span class="style7">*</span></td>
         
      
    </tr>
    
    
  
    <tr>
        <td align="center" colspan="2" class="style12">&nbsp;</td>
    </tr>
      <tr>
         <td align="left" colspan="2" >
                <asp:Label ID="lbNodata" runat="server" Text=""></asp:Label>
            </td>
    </tr>
    <tr>
        <td align="center" colspan="2" class="style12">
            <asp:Button ID="showDataButton" runat="server" CssClass="buttoncommon"  OnClientClick="return fnCheqInput();" Text="Show  Data" OnClick="showDataButton_Click"  />
            &nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2">&nbsp;</td>
    </tr>
</table>   
    <br />
    <br />
     <table align="center" >
         <tr>
             <td align="center" class="auto-style2">
                 <strong><span class="auto-style3">Holder In U_MASTER But Not IN CDBL</span> </strong>
             </td>
         </tr>
      <tr>
        <td>
            <div style="height:300px; overflow:auto;" id="dvGrid" runat="server" visible="true">
            <asp:DataGrid id="grdShowUMasterDetails" runat="server"  style="border: #666666 1px solid;"  AutoGenerateColumns="False" CellPadding="4">                               
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>                                      
                   
                    <asp:BoundColumn HeaderText="Fund Code" DataField="REG_BK"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="BO" DataField="BO"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Name" DataField="HNAME"></asp:BoundColumn>  
                    <asp:BoundColumn HeaderText="No. of Shares" DataField="BALANCE" ></asp:BoundColumn>
                           
                    </Columns>          
            </asp:DataGrid>
            </div>
        </td>
      </tr>
          <tr>
             <td align="center" class="auto-style2">
                 <strong>Holder In CDBL But Not IN U_MASTER
             </strong>
             </td>
         </tr>
      <tr>
        <td>
            <div style="height:300px; overflow:auto;" id="Div1" runat="server" visible="true">
            <asp:DataGrid id="grdShowCDBLDetails" runat="server"  style="border: #666666 1px solid;"  AutoGenerateColumns="False" CellPadding="4">                               
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>                                      
                   
                    <asp:BoundColumn HeaderText="Fund Code" DataField="FUND_CODE"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Reg. No." DataField="REG_NO"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="BO" DataField="BO"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Name" DataField="NAME1"></asp:BoundColumn>  
                    <asp:BoundColumn HeaderText="No. of Shares" DataField="BALANCE" ></asp:BoundColumn>
                           
                    </Columns>          
            </asp:DataGrid>
            </div>
        </td>
      </tr>
    </table> 
    <br />
    <br />
    <br />
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDividendInsertCDBLFileData.aspx.cs" Inherits="UI_UnitDividendInsertCDBLFileData" Title=" Dividend Insert CDBL Data File (Design and Developed by Sakhawat)" %>
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
          
          
          
           if(document.getElementById("<%=DividendFYDropDownList.ClientID%>").value =="--Select FY--")
            {               
              
                document.getElementById("<%=DividendFYDropDownList.ClientID%>").focus();
                alert("Please Select Fiscal Year");
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
           <b>Fiscal Year : </b>
        </td>
        
        <td align="left">            
            
            <asp:DropDownList ID="DividendFYDropDownList" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="DividendFYDropDownList_SelectedIndexChanged" 
                TabIndex="3">
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
            &nbsp;&nbsp;<asp:Button ID="SaveButton" runat="server" CssClass="buttoncommon"  Text="Save  Data" Visible="False" OnClick="SaveButton_Click" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">&nbsp;</td>
    </tr>
</table>   
    <br />
    <br />
     <table align="center" >
      <tr>
        <td>
            <div style="height:300px; overflow:auto;" id="dvGrid" runat="server" visible="true">
            <asp:DataGrid id="grdShowDetails" runat="server"  style="border: #666666 1px solid;"  AutoGenerateColumns="False" CellPadding="4">                               
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>                                      
                    <asp:BoundColumn HeaderText="SI#" DataField="SI"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Fund Name" DataField="FUND_NAME"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="BO" DataField="BO"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Name" DataField="NAME1"></asp:BoundColumn>  
                    <asp:BoundColumn HeaderText="No. of Shares" DataField="BALANCE" ></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Bank" DataField="BANK_NAME"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Branch" DataField="BRANCH_NAME"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="A/C No" DataField="BANK_ACC_NO"></asp:BoundColumn>          
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


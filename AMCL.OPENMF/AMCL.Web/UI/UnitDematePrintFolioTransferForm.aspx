<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDematePrintFolioTransferForm.aspx.cs" Inherits="UI_UnitDematePrintFolioTransferForm" Title="Unit UnitDematePrintFolioTransferForm Form  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
   
       
      
      function fnCheckInputForSubmit()
        {
            

                if (document.getElementById("<%=DRRefNoDropDownList.ClientID%>").value == "0")
                {
                    document.getElementById("<%=DRRefNoDropDownList.ClientID%>").focus();
                    alert("Please Select Demate Request Referenc Number ");
                    return false;
                }
                        

        }
  
 
        
  function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}


	
	
	
         
         
  
</script>
 


    <style type="text/css">
        .style1
        {
            height: 13px;
        }
        .style2
        {
            height: 20px;
            text-align: left;
        }
        .auto-style1 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style3 {
            height: 13px;
            text-align: right;
        }
        </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Print FolioTransfer Form(<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="850" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" class="style1" colspan="2">
            </td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>Select Folio Transfer Reference No:</strong></td>
            <td align="left" class="style1">
            <asp:DropDownList ID="DRRefNoDropDownList" runat="server" OnSelectedIndexChanged="DRRefNoDropDownList_SelectedIndexChanged" AutoPostBack="True"  
                >
            </asp:DropDownList>
              </td>
               </tr>
        
            <tr>
            <td align="left" class="style1" colspan="2">
            </td>
      </tr>
      <tr>
        <td class="auto-style2" colspan="2" >&nbsp;<asp:Button ID="PrintTransferButton" runat="server" Text="Print Folio Transfer Form" OnClientClick="return fnCheckInputForSubmit()"
                CssClass="auto-style1"  Width="177px" Height="22px" OnClick="PrintTransferButton_Click"  /> 
           </td>
        
      </tr>
       <tr>
            <td align="left" colspan="2">
            
                <table align="left">
                    <tr>
                        <td>
                         <div id="dvGridSurrender" runat="server"  >
                         
                                <asp:GridView ID="SaleListGridView" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                                     
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />  
                                <asp:BoundField DataField="SL_NO" HeaderText="Sale No." />                                  
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="BO" HeaderText="Holder BO " /> 
                                <asp:BoundField DataField="OMNIBUS_FOLIO_BO" HeaderText="Folio BO " />
                                <asp:BoundField DataField="QTY" HeaderText="No of Units" />                                
                                                                                                                                                                                                                                                                           
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
       <tr>
              <td align="right" colspan="2" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="2" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="2" >&nbsp;</td>  
                                
      </tr>    
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>


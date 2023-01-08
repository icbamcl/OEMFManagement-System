<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDemateFolioTransferRequestForm .aspx.cs" Inherits="UI_UnitDemateFolioTransferRequestForm" Title="Unit Demate Request Form  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
   
       
      
      function fnCheckInputForList()
      {
         
    
            if(document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=fromSaleNoTextBox.ClientID%>").focus();
                alert("Please Enter Sale Number From ");
                return false;
               }
              
           if(document.getElementById("<%=toSaleNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=toSaleNoTextBox.ClientID%>").focus();
                alert("Please Enter Sale Number to ");
                return false;
               }
                                                        
         }
        function fnCheckInputForSubmit()
        {
            

               if(document.getElementById("<%=DRFRefNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=DRFRefNoTextBox.ClientID%>").focus();
                alert("Please Enter Demate Reference Number  ");
                return false;
               }
            var Confrm = confirm("Are Sure To Sumbit Folio transfer Request for the Selected Sale ");
            if (Confrm) {
                return true;
            }
            else {
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
	function fnOnChangeText1()
     {                                
         document.getElementById("<%=toSaleNoTextBox.ClientID%>").value=document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value;                                                                           
          document.getElementById("<%=toSaleNoTextBox.ClientID%>").focus();
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
        .auto-style4 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            BACKGROUND-COLOR: #FFFFDD;
            COLOR: #FF6699;
            FONT-SIZE: 12px;
            padding-left: 2px;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Folio Transfer Request Form(<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" class="style1" colspan="3">
            </td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>Sale Number:</strong></td>
            <td align="left" class="style1">
        <asp:TextBox ID="fromSaleNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText1();"
                CssClass= "auto-style4" Width="130px" TabIndex="1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<strong>TO</strong>
        <asp:TextBox ID="toSaleNoTextBox" runat="server"
                CssClass= "auto-style4" Width="130px" TabIndex="2" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<asp:Button ID="addToListButton" runat="server" Text="Add To List" 
                CssClass="auto-style1"  Width="102px" Height="22px" OnClientClick="return fnCheckInputForList();"
                onclick="addToListButton_Click"/> 
            </td>
            <td align="left" class="style1">
            </td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>&nbsp;Folio Transfer Reference No:</strong></td>
            <td align="left" class="style1">
        <asp:TextBox ID="DRFRefNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText1();"
                CssClass= "auto-style4" Width="130px" TabIndex="1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>  
            </td>
            <td align="left" class="style1">
            </td>
      </tr>
           <tr>
            <td align="left" class="style1" colspan="3">
            </td>
      </tr>
      <tr>
        <td class="auto-style2" colspan="3" ><asp:Button ID="SubmitButton" runat="server" Text="Submit Transfer Request" OnClientClick="return fnCheckInputForSubmit();"
                CssClass="auto-style1"  Width="189px" Height="22px" 
                onclick="SubmitButton_Click"/> 
           </td>
        
      </tr>
       <tr>
            <td align="left" colspan="3">
            
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
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="leftCheckBox" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>                              
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />  
                                <asp:BoundField DataField="SL_NO" HeaderText="Sale No." />                                  
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="BO" HeaderText="Holder BO " /> 
                                <asp:BoundField DataField="OMNIBUS_FOLIO_BO" HeaderText="Folio BO " /> 
                                <asp:BoundField DataField="QTY" HeaderText="No of Units" /> 
                                <asp:BoundField DataField="SL_PRICE" HeaderText="Rate" />  
                                <asp:BoundField DataField="TOTAL_AMT" HeaderText="Total Amount" />                                                                                                                                                                                                                       
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
       <tr>
              <td align="right" colspan="3" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="3" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="3" >&nbsp;</td>  
                                
      </tr>    
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>


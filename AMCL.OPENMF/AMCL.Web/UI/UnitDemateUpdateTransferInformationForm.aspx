<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDemateUpdateTransferInformationForm.aspx.cs" Inherits="UI_UnitDemateUpdateTransferInformationForm" Title="Unit Demate Update Transfer Information Form  (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
   
       
      
      function fnCheckInputForSubmit()
        {
            

                if (document.getElementById("<%=DRRefNoDropDownList.ClientID%>").value == "0")
                {
                    document.getElementById("<%=DRRefNoDropDownList.ClientID%>").focus();
                    alert("Please Select Demate Request Reference Number ");
                    return false;
                }
               
              
          var Confrm = confirm("Are Sure To Update DRN Information ");
          if (Confrm) {
              return true;
                        }
            else
            {
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
        .auto-style4 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            BACKGROUND-COLOR: #FFFFDD;
            COLOR: #FF6699;
            FONT-SIZE: 12px;
            padding-left: 2px;
        }
        .auto-style7 {
            font-size: small;
        }
        .auto-style8 {
            height: 13px;
            text-align: left;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Update Transfer Information Form(<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="850" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" class="style1" colspan="5">
            </td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>Select DR Reference No:</strong></td>
            <td align="left" class="style1" colspan="2">
            <asp:DropDownList ID="DRRefNoDropDownList" runat="server" OnSelectedIndexChanged="DRRefNoDropDownList_SelectedIndexChanged" AutoPostBack="True"  
                >
            </asp:DropDownList>
              </td>
            <td align="left" class="style1">
                &nbsp;</td>
            <td align="left" class="style1">
                &nbsp;</td>
               </tr>
        <tr>
            <td class="auto-style3">
                <strong>Transfer Sequence No From:</strong></td>
            <td align="left" class="auto-style6">
        <asp:TextBox ID="TRSeqFromNoTextBox" runat="server" 
                CssClass= "auto-style4" Width="130px" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>          
            <span class="star">
            *</span></td>
            <td class="auto-style3" colspan="2">
                &nbsp;</td>
            <td align="left" class="style1">
                &nbsp;</td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>Transfer Date :</strong></td>
            <td align="left" class="auto-style6">
                <asp:TextBox ID="TransferDateTextBox" runat="server" CssClass="textInputStyleDate" meta:resourcekey="saleDateTextBoxResource1" TabIndex="6" Width="130px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="chequeDateImageButton" TargetControlID="TransferDateTextBox" />
                <asp:ImageButton ID="chequeDateImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="10" />
                <span class="star">* </span></td>
            <td class="auto-style8" colspan="3">
                <asp:Button ID="addToListButton" runat="server" Text="Add To List" 
                CssClass="auto-style1"  Width="102px" Height="22px" 
                onclick="addToListButton_Click"/> 
              </td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>DRF No:</strong></td>
            <td align="left" class="auto-style6">
                <strong>
               &nbsp;<asp:Label ID="DRFNoLabel" runat="server" CssClass="auto-style7" Text="DRF No"></asp:Label>
                </strong></td>
            <td class="auto-style3" colspan="2">
                <strong>DRN :</strong></td>
            <td align="left" class="style1">
                <strong>
               &nbsp;<asp:Label ID="DRNLabel" runat="server" CssClass="auto-style7" Text="DRN"></asp:Label>
                </strong></td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>Demate Accept Date :</strong></td>
            <td align="left" class="auto-style6">
                &nbsp;<strong><asp:Label ID="DemateAcceptDateLabel" runat="server" CssClass="auto-style7" Text="Demate Accept Date "></asp:Label>
                </strong>&nbsp;</td>
            <td class="auto-style3" colspan="2">
                <strong>Register Folio Number:</strong></td>
            <td align="left" class="style1">
                <strong>
               &nbsp;<asp:Label ID="FolioNoLabel" runat="server" CssClass="auto-style7" Text="Register Folio No"></asp:Label>
                </strong></td>
      </tr>
          <tr>
            <td class="auto-style3">
                <strong>Certificate Number:</strong></td>
            <td align="left" class="auto-style6">
                <strong>
                &nbsp;<asp:Label ID="CertNoLabel" runat="server" CssClass="auto-style7" Text="Certificate Number"></asp:Label>
                </strong></td>
            <td class="auto-style3" colspan="2">
                <strong>Distinctive Number From: </strong></td>
            <td align="left" class="style1">
                <strong>
               &nbsp;<asp:Label ID="DistNoFromLabel" runat="server" CssClass="auto-style7" Text="Dist. No From"></asp:Label>
                </strong></td>
      </tr>
         <tr>
            <td class="auto-style3">
                <strong>Distinctive Number To :</strong></td>
            <td align="left" class="auto-style6">
                <strong>
               &nbsp;<asp:Label ID="DistinctNoToLabel" runat="server" CssClass="auto-style7" Text="Dist. No To"></asp:Label>
                </strong></td>
              <td class="auto-style3" colspan="2">
                  <strong>Total Units:</strong></td>
            <td align="left" class="style1">
                <strong>
              &nbsp;<asp:Label ID="TotalUnitLabel" runat="server" CssClass="auto-style7" Text="Total Units"></asp:Label>
                </strong> </td>
        
      </tr>
            <tr>
            <td align="left" class="style1" colspan="5">
            </td>
      </tr>
      <tr>
        <td class="auto-style2" colspan="5" ><asp:Button ID="SubmitButton" runat="server" Text="Update Transfer Information" 
                CssClass="auto-style1"  Width="203px" Height="22px" OnClientClick="return fnCheckInputForSubmit();"
                onclick="SubmitButton_Click"/> 
           </td>
        
      </tr>
       <tr>
            <td align="left" colspan="5">
            
                <table align="left">
                    <tr>
                        <td>
                         <div id="dvGridSurrender" runat="server"  >
                         
                                <asp:GridView ID="SaleListGridView" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="2" 
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
                                <asp:BoundField DataField="HOLDER_BO" HeaderText="Holder BO " /> 
                                <asp:BoundField DataField="QTY" HeaderText="No of Units" />                                
                                 <asp:TemplateField>
                                     <HeaderTemplate>Tr. Seq. No.</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TRSeqNoTextBox" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem," DRF_TR_SEQ_NO") %>' />
                                </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                           
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
       <tr>
              <td align="right" colspan="5" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="5" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="5" >&nbsp;</td>  
                                
      </tr>    
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>


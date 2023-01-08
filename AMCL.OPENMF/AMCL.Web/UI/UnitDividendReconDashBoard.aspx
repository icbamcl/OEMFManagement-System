<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDividendReconDashBoard.aspx.cs" Inherits="UI_UnitDividendReconDashBoard" Title=" Unit Dividend Reconcile (Design and Developed by Jagonnath)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <style type="text/css">
        .style6
        {
                height: 18px;
                font-weight: 700;
            }
            .style7
            {
                width: 127px;
                font-weight: 700;
            }
            .style8
            {
                height: 18px;
                font-weight: 700;
                width: 127px;
            }
            .style10
            {
                width: 147px;
            }
            .style11
            {
                width: 107px;
                font-weight: 700;
            }
            .style12
            {
                width: 107px;
            }
            .huy
            {
            display:none;
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
                margin-left: 0px;
            }
            .auto-style2 {
                text-align: left;
            }
            .auto-style3 {
                text-align: right;
            }
            .auto-style4 {
                font-size: medium;
                color: #0099CC;
            }
            .auto-style5 {
                color: #009933;
                font-size: medium;
            }
            .auto-style6 {
                text-decoration: underline;
                color: #FF3300;
                font-size: medium;
            }
            </style>
    <script language="javascript" type="text/javascript"> 
    
   

   
   
    
    </script>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
  <br />
     <table align="center">
            <tr>
                <td class="FormTitle" align="center">
                                   Unit Dividend 
                                    Reconciliation Dash Board&nbsp; 
                </td>           
            </tr> 
          </table>
    <br />
                
             <table width="1000" align="left" cellpadding="0" cellspacing="0" border="0" >  
            
        
                <tr>
                    
                    <td align="right">
                        <b>Fund Name :</b>
                     </td>
                    <td align="left">
                        <asp:DropDownList ID="FundNameDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FundNameDropDownList_SelectedIndexChanged">
                        </asp:DropDownList> 
                     </td>
                      <td align="left">   <b> Fiscal Year:</b>&nbsp;<asp:DropDownList ID="fyDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="fyDropDownList_SelectedIndexChanged" >
                        </asp:DropDownList> </td>
                     <td align="right"><b>Closing Date : </b>&nbsp;
                        <asp:DropDownList ID="ClosingDateDropDownList" runat="server" TabIndex="4" OnSelectedIndexChanged="ClosingDateDropDownList_SelectedIndexChanged" AutoPostBack="True">
                         </asp:DropDownList>
                         
                        <asp:Button ID="searchButton" runat="server" Text="Search" 
                        CssClass="auto-style1"  Width="87px" Height="28px" OnClick="searchButton_Click" Visible="False"  
                       /> 
                    </td>
                     
              </tr>
                 <tr>
                     <td colspan="4">
                           <table width="1000" align="left" cellpadding="2" cellspacing="2" border="2" > 
                               <colgroup width="250"></colgroup>
                               <colgroup width="150"></colgroup>
                               <colgroup width="200"></colgroup>
                               <colgroup width="200"></colgroup>
                                 <tr>
                                <td colspan="5">
                                     
                                    <table width="990" align="left" cellpadding="2" cellspacing="2" border="2" > 
                                        <colgroup width="200"></colgroup>
                                        <colgroup width="200"></colgroup>
                                        <colgroup width="100"></colgroup>
                                        <colgroup width="80"></colgroup>
                                        <colgroup width="50"></colgroup>
                                        <colgroup width="100"></colgroup>
                                        <colgroup width="100"></colgroup>
                                    
                                        <tr>
                                            <td colspan="8">
                                                <strong><span class="Underline">&nbsp;<span class="auto-style4">Dividend&nbsp; Process Summary</span></span> </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                  <td align="center" class="Underline">
                                      <strong>Fund Name 
                                  </strong>
                                  </td>
                                    <td align="center" class="Underline">
                                        <strong>Bank Account Inforation
                                  </strong>
                                  </td>
                                  <td align="center" class="Underline">
                                      <strong>No. of Unit Holders
                                  </strong>
                                  </td>
                                  <td align="center" class="Underline">
                                       <strong>No of Units&nbsp;
                                  </strong>
                                  </td>
                                  <td align="center" class="Underline">
                                       <strong> Rate&nbsp;
                                  </strong>
                                  </td>
                                   
                                   <td align="center" class="Underline">
                                       <strong>Gross Amount
                                  </strong>
                                  </td>
                                   <td align="center">
                                       <strong>Tax Diduct</strong>

                                   </td>
                                             <td align="center">
                                       <strong>Net Amount</strong>

                                   </td>

                              </tr>
                                         <tr>
                                  <td align="center">
                                      <strong>
                                      <asp:Label ID="fundNameLabel" runat="server" Text="Fund Name"></asp:Label>
                                      </strong>
                                  </td>
                                    <td align="center">
                                        <strong>
                                        <asp:Label ID="bankInfoLabel" runat="server" Text="Bank Information"></asp:Label>
                                        </strong>
                                  </td>
                                  <td align="center">
                                      <strong>
                                      <asp:Label ID="holderLabel" runat="server" Text="No of Holder"></asp:Label>
                                      </strong>
                                  </td>
                                  <td align="center">
                                       <strong>
                                       <asp:Label ID="unitsLabel" runat="server" Text="No of Units"></asp:Label>
&nbsp;</strong></td>
                                  <td align="center">
                                       <strong>
                                       <asp:Label ID="rateLabel" runat="server" Text="Rate"></asp:Label>
                                       </strong>
                                  </td>
                                   
                                   <td align="center">
                                       <strong>
                                       <asp:Label ID="grossLabel" runat="server" Text="Gross Amount"></asp:Label>
&nbsp;</strong></td>
                                   <td align="center">
                                       <strong>
                                       <asp:Label ID="taxAmountLabel" runat="server" Text="Tax Amount"></asp:Label>
                                       </strong>

                                   </td>
                                             <td align="center">
                                                 <strong>
                                                 <asp:Label ID="netLabel" runat="server" Text="Net Amount"></asp:Label>
&nbsp;</strong></td>

                              </tr>
                           

                                    </table>
                                 </td>
                             </tr>
                              <tr>
                                <td colspan="5">
                                     <strong><span class="Underline">&nbsp;<span class="auto-style5">Paid Summary</span></span> </strong>
                                 </td>
                             </tr>
                              <tr>
                                  <td align="center" class="Underline">
                                      <strong>Payment Status
                                  </strong>
                                  </td>
                                  <td align="center" class="Underline">
                                      <strong>No. of Unit Holders
                                  </strong>
                                  </td>
                                   <td align="center" class="Underline">
                                       <strong>Net Amount
                                  </strong>
                                  </td>
                                   <td align="center" class="Underline">
                                       <strong>Remarks
                                       </strong></td>
                                   <td align="center">
                                    &nbsp;
                                  </td>

                              </tr>
                              <tr>
                                  <td class="auto-style3">
                                      <strong>Paid Through BEFTN/ONLINE:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="paidLabel1" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidAmountLabel1" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidRemarksLabel1" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                    &nbsp;<asp:Button ID="paidEFTPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="paidEFTPrintButton_Click" 
                       /> 
                    &nbsp;</td>

                              </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>Paid After BEFTN/ONLINE Return:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="paidAfterReturnLabel" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidAmountAfterReturnLabel" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidAfterReturnRemarksLabel" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                    &nbsp;<asp:Button ID="paidAfterReturnButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="paidAfterReturnButton_Click" 
                       /> 
                    &nbsp;</td>

                              </tr>
                              <tr>
                                  <td class="auto-style3">
                                      <strong>Paid Through Warrant:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="paidLabel2" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidAmountLabel2" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidRemarksLabel2" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                    &nbsp;<asp:Button ID="paidWarrantPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="paidWarrantPrintButton_Click"  
                       /> 
                                  </td>

                              </tr>                             
                              <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp;Total Paid:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="paidLabel3" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidAmountLabel3" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="paidRemarksLabel3" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                    &nbsp;
                                  </td>

                              </tr>
                                 <tr>
                                <td colspan="5">
                                     <strong><span class="auto-style6">&nbsp;Not Paid Summary</span> </strong>
                                 </td>
                             </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp; Warrant in Hand:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="unpaidLabel1" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidAmountLabel1" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidRemarksLabel1" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                       <asp:Button ID="unpaidWarINHandPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="unpaidWarINHandPrintButton_Click" 
                       /> 
                                    &nbsp;
                                  </td>

                              </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp;BEFTN/ONLINE Return: </strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="unpaidLabel2" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidAmountLabel2" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidRemarksLabel2" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                       <asp:Button ID="unpaidEFTReturnPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="unpaidEFTReturnPrintButton_Click"  
                       /> 
                                  </td>

                              </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp;Courier/Post But Not Deposited:</strong> </td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="unpaidLabel3" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidAmountLabel3" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidRemarksLabel3" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                       <asp:Button ID="unpaidPostPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="unpaidPostPrintButton_Click"  
                       /> 
                    &nbsp;</td>

                              </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp;Hand Delivered But Not Deposited:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="unpaidLabel4" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidAmountLabel4" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidRemarksLabel4" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                       <asp:Button ID="unpaidDeliverdPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="unpaidDeliverdPrintButton_Click"  
                       /> 
                                  </td>

                              </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp; Suspense BO:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="unpaidLabel5" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidAmountLabel5" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidRemarksLabel5" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                    &nbsp;</td>

                              </tr>
                                <tr>
                                  <td class="auto-style3">
                                      <strong>&nbsp;Total Not Paid:</strong></td>
                                  <td class="auto-style2">
                                      <strong>
                                      &nbsp;<asp:Label ID="unpaidLabel6" runat="server" Text="No of Holder"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidAmountLabel6" runat="server" Text="Amount"></asp:Label>
                                       </strong>
                                  </td>
                                   <td class="auto-style2">
                                       <strong>
                                       &nbsp;<asp:Label ID="unpaidRemarksLabel6" runat="server" Text="Remarks"></asp:Label>
                                        &nbsp;</strong></td>
                                   <td align="center">
                                       <asp:Button ID="unpaidTotalPrintButton" runat="server" Text="Print" 
                        CssClass="auto-style1"  Width="60px" OnClick="unpaidTotalPrintButton_Click" 
                       /> 
                                  </td>

                              </tr>
                         </table>
                     </td>
                    
                 </tr>
     
       </table>
          
 
    <br />
    <br />           
</asp:Content>


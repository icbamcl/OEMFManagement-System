<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSIPSaleEntry.aspx.cs" Inherits="UI_UnitSIPSaleEntry" Title=" SIP Sale Voucher Posting Account Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 


 function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {                        
            document.getElementById("<%=sipDayDropDownList.ClientID%>").value = "0";
            document.getElementById("<%=branchNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=saleNoStartTexBox.ClientID%>").value = "";
            document.getElementById("<%=certNoTexBox.ClientID%>").value ="";
                      
            return false;
        }
        else
        {
            return true;
            
        }
    }




   function fnCheqInput() {
            if (document.getElementById("<%=sipDayDropDownList.ClientID%>").value == "0") {
                document.getElementById("<%=sipDayDropDownList.ClientID%>").focus();
                alert("Please Select Sale Date");
                return false;

            }
         if (document.getElementById("<%=branchNameDropDownList.ClientID%>").value == "0") {
                document.getElementById("<%=branchNameDropDownList.ClientID%>").focus();
                alert("Please Select Branch ");
                return false;

         }
        if (document.getElementById("<%=saleNoStartTexBox.ClientID%>").value == "") {
                document.getElementById("<%=saleNoStartTexBox.ClientID%>").focus();
                alert("Please Enter Start Sale No. ");
                return false;

            }
        }
</script>



    <style type="text/css">
    
        .hiddencol
          {
            display: none;
          }
        .style3
        {
            font-size: large;
        }
        A.TextLink:hover
        {
	        COLOR: #556677; 
	        font-weight:bold; 
	        font-size:large;
	        text-decoration:underline;
        	
        }
       
        .fontStyle
        {
            color: #FFFFFF;
            font-weight: bold;
        }
        tr .menuBarBottomSelected td a:hover
        {
        	background-color:#547DD3;
        }
       .menuBarBottomSelected
       {
       	         background-color: #666666;  
       }
        .style4
        {
            height: 25px;
            width: 196px;
        }
        .style5
        {
            height: 25px;
            width: 172px;
        }
        .auto-style3 {
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
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="center">
        Unit Holder SIP Sale&nbsp; Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
           <tr>
               <td></td>
           </tr>
      </table>
      
      
 <table width="1100px" align="left" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="260"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
     <tr>
        
                  <td align="right" ><b>Select Sale Date:</b></td>
                  <td align="left">
        
            <asp:DropDownList ID="sipDayDropDownList" runat="server" >
                                  
            </asp:DropDownList>                                                                                               
                                                   
            <span class="star">
                      *</span></td>
         <td align="right" ><b>Select Branch Name:</b></td>
        <td align="left" colspan="3">
        
                                <span class="star">         
            <asp:DropDownList ID="branchNameDropDownList" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="branchNameDropDownList_SelectedIndexChanged"
                ></asp:DropDownList>
            *</span></td>
       
       
    </tr>
      <tr>
         <td align="right" class="auto-style4" ><b>Starting Sale No:</b></td>
        <td align="left" class="auto-style4" >
        
            <span class="star">
        <asp:TextBox ID="saleNoStartTexBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" ></asp:TextBox> &nbsp;*</span></td>
        <td align="right" class="auto-style4" >
            <b>Starting Certificate No:</b></td>
       <td align="left" class="auto-style4" colspan="3">
        
            <span class="star">
        <asp:TextBox ID="certNoTexBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" ></asp:TextBox> &nbsp;</span>
                                <span class="star"><asp:Button ID="addListButton" 
                    runat="server"  CssClass="auto-style3"
                    
                   Text="Add to List" Width="97px" OnClick="addListButton_Click" OnClientClick="return fnCheqInput();"/>
                      </span>
                                                   
                                            
                                                   
                                </td>
    </tr>
     <%--  <tr>              
        <td align="right" class="auto-style4" >
            <b>SIP Fund Account Name:</b></td>
       <td align="left" class="auto-style4">
        
            <asp:Label ID="fundAccNameLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                   
                                            
                                                   
                                </td>
        <td align="right" class="auto-style4" ><b>SIP Account No.:</b></td>
        <td align="left" class="auto-style4" colspan="3">
            <asp:Label ID="fundAccNoLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>

           
            </tr>
            
    
     <tr>
         <td align="right" ><b>SIP&nbsp;Bank Name:</b></td>
        <td align="left">
            <asp:Label ID="BankNameLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
          <td align="right" ><b>&nbsp;SIP Branch Name:</b></td>
        <td align="left" colspan="">
            <asp:Label ID="BranchNameLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
         
    </tr>--%>   
    
  <%--  <tr>
       <td align="right" class="auto-style2" ><b>&nbsp;Total No. of Holder:</b></td>
        <td align="left" class="auto-style2">
            <asp:Label ID="totalHolderLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
        <td align="right" ><b>Total Debit Amount:</b></td>
        <td align="left" colspan="3">
            <asp:Label ID="TotalAmountLabel" runat="server"  
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
           
     
    </tr>--%>
   <tr>
    <td colspan="6">

        &nbsp;</td>
</tr>     
<tr>
    <td align="center" colspan="6">

        <asp:Button ID="saveSaleButton" runat="server" Text="Save Sale " CssClass="auto-style2" 
               Width="151px" Height="24px" OnClick="saveSaleButton_Click"  OnClientClick="return fnCheqInput();" />
        &nbsp;&nbsp;
        &nbsp;&nbsp;<asp:Button ID="sipResetButton" runat="server" Text="Reset" OnClientClick="return fnReset();" 
                CssClass="auto-style3"   
                Width="140px" Height="22px"
                  />

    </td>
</tr>    
 <tr>
    <td colspan="6">

        &nbsp;</td>
</tr>      
      <tr>
            <td align="center" colspan="6">
            
                <table align="left">
                    <tr>
                        <td align="center">
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
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />  
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code " /> 
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" /> 
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="SIP_NO" HeaderText="SIP No." />                                                                                                 
                                <asp:BoundField DataField="SCHEDULE_NO" HeaderText="Installment No. " />                                                                                                                                                                                                                        
                                <asp:BoundField DataField="UNIT_QTY" HeaderText="No of Units" />                                   
                                <asp:BoundField DataField="UNIT_RATE" HeaderText="Rate" />    
                                <asp:BoundField DataField="AMOUNT" HeaderText=" Amount" /> 
                                <asp:BoundField DataField="SL_NO" HeaderText=" Sale No." /> 
                                <asp:BoundField DataField="CERT_NO" HeaderText=" Certiicate No." />  
                                <asp:BoundField DataField="FRAC_AMT" HeaderText="Frac. Amt" />
                                <asp:BoundField DataField="ACC_FRAC_AMT_ADD" HeaderText="ACC_FRAC_AMT_ADD" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />                                                                                                                                                                                                                                                                                   
                               </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
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
    <br />
    <br />
     <br />
    <br />
    <br />
 
    <br />
</asp:Content>


<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSaleVoucherPostingAccount.aspx.cs" Inherits="UI_UnitSaleVoucherPostingAccount" Title="Unit Unit Sale Voucher Posting Account (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
    function fnCheckInput()
    {
    
         
         
         if(document.getElementById("<%=TranDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=TranDateTextBox.ClientID%>").focus();
                alert("Please Enter Transaction Date ");
                return false;
         }

       
          
          
     }
   function fnFindChek()
    {
    
         
         
         if(document.getElementById("<%=fromSaleDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=fromSaleDateTextBox.ClientID%>").focus();
                alert("Please Enter From Date ");
                return false;
         }
         if(document.getElementById("<%=toSaleDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=toSaleDateTextBox.ClientID%>").focus();
                alert("Please Enter From Date ");
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
        .hiddencol
          {
            display: none;
          }
        .style1
        {
            height: 13px;
        }
        .style2
        {
            height: 20px;
        }
        .auto-style1 {
            text-align: right;
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
        .auto-style3 {
            text-align: center;
        }
        </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit&nbsp; Sale Voucher Posting Account Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
             <table width="1200" align="left" cellpadding="0" cellspacing="0" border="0" >
                 <colgroup width="150"></colgroup>
                <colgroup width="600"></colgroup>
                <colgroup width="200"></colgroup>
      
        <tr>
            <td align="left" colspan="4" >
            </td>
      </tr>
      <tr>
        <td align="right" class="auto-style1" >
            <strong>Sale Date:</strong></td>
        <td align="left" >
        <asp:TextBox ID="fromSaleDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="15"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromSaleDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="7" />
            <b><span style="font-weight:bold; height:100px;"><span class="star">
            *</span>&nbsp;&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toSaleDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="16"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="toSaleDateTextBox" 
                PopupButtonID="toRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="toRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="9" />
          
            <span class="star">
            * <asp:Button 
                    ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                    onclick="findButton_Click" AccessKey="f" 
                    onclientclick="return fnFindChek();" TabIndex="2" 
                    meta:resourcekey="findButtonResource1" /></span></td>
           <td  align="right">         
               <b>Fund Name :</b></td>
        
        <td align="left">         
            <asp:DropDownList ID="fundNameDropDownList" runat="server"  AutoPostBack="true"
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            <span class="star">
            *</span></td>
      </tr>
        
       <tr>
           <td class="auto-style1" >         
            <b>Voucher&nbsp; Number :</b></td>
        
        <td align="left" class="style2" >         
            <span class="star">
        <asp:TextBox ID="VoucherNoTexBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" ></asp:TextBox> * </span></td>
          
         <td   align="right" ><b>Transaction Date :</b></td>
        
       <td align="left" >         
            <span class="star">
            <asp:TextBox ID="TranDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="4"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" 
                TargetControlID="TranDateTextBox" />
            <asp:ImageButton ID="ImageButton1" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
            *</span></td>
        
         
      </tr>
          <tr>
         <td class="auto-style3" >&nbsp;</td>
        
       <td colspan="2" class="auto-style3" >         
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" 
                TargetControlID="TranDateTextBox" />
        <asp:Button ID="SaleVoucherButton" runat="server" Text="Save Unit Sale Voucher" CssClass="auto-style2" OnClientClick="return fnCheckInput();"
               Width="151px" Height="24px" OnClick="SaleVoucherButton_Click" />
              </td>
        
        <td class="style2"  >         
            &nbsp;</td>
        
      
      </tr>
        <tr>
            <td align="left" colspan="4">
           &nbsp;</td>
      </tr>
       <tr>
            <td align="left" colspan="4">
            
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
                                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                                <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                <asp:BoundField DataField="RECEIPT_NO" HeaderText="Receipt No." />                                 
                                <asp:BoundField DataField="SALE_DATE" HeaderText="Sale Date" />                                
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="UNIT_QTY" HeaderText="No of Units" />                                   
                                <asp:BoundField DataField="RATE" HeaderText="Rate" />  
                                <asp:BoundField DataField="TOTAL_AMT" HeaderText="Total Amount" /> 
                                <asp:BoundField DataField="CHQ_DD_NO" HeaderText="Cheque No."  NullDisplayText=""/> 
                                <asp:BoundField DataField="CHQ_DATE" HeaderText="Cheque Date" /> 
                                <asp:BoundField DataField="SL_REP_DIFF" HeaderText="SL_REP_DIFF" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />                                                                                       
                                <asp:BoundField DataField="PAY_TYPE" HeaderText="Pay Type"  />                                                                                       
                               </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
      
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>


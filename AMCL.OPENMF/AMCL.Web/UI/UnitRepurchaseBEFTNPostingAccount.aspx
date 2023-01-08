<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRepurchaseBEFTNPostingAccount.aspx.cs" Inherits="UI_UnitRepurchaseBEFTNPostingAccount" Title="Unit UnitRepurchase BEFTN Posting Account (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
    function fnCheckInput()
    {
    
         
          if(document.getElementById("<%=BEFTNDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=BEFTNDateTextBox.ClientID%>").focus();
                alert("Please Enter BEFTN Date ");
                return false;
               }
            if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name ");
                return false;
            }

         if(document.getElementById("<%=Signatory1DropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=Signatory1DropDownList.ClientID%>").focus();
                alert("Please Select Signatory One Name ");
                return false;
                }
         if(document.getElementById("<%=Signatory2DropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=Signatory2DropDownList.ClientID%>").focus();
             alert("Please Select Signatory Two Name ");
                return false;
               }
               
             
         if(document.getElementById("<%=TranDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=TranDateTextBox.ClientID%>").focus();
                alert("Please Enter Transaction Date ");
                return false;
         }

        if(document.getElementById("<%=VoucherNoTexBox.ClientID%>").value =="")
               {
                document.getElementById("<%=VoucherNoTexBox.ClientID%>").focus();
                alert("Please Enter Voucher Number ");
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
        }
        .auto-style1 {
            text-align: right;
        }
        .auto-style2 {
            height: 24px;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Repurchase BEFTN Voucher Posting Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" colspan="5" class="style1">
            </td>
      </tr>
      <tr>
        <td   align="right" class="auto-style2" ><b>BEFTN Date :</b></td>
        
        <td align="left" colspan="2" class="auto-style2" >         
            <span class="star">
            <asp:TextBox ID="BEFTNDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="4"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="BEFTNDateTextBox" />
            <asp:ImageButton ID="RegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
            *</span></td>
        
     <td   align="right" class="auto-style2" ><b>Fund Name :</b></td>
        
        <td   align="right" style="text-align: left" class="auto-style2" ><asp:DropDownList ID="fundNameDropDownList" runat="server"  AutoPostBack="true"
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            <span class="star">* </span>
            </td>
        
      </tr>
      <tr>
        <td   align="right" ><b>Select First Sigantory :</b></td>
        
        <td align="left" >         
            <asp:DropDownList ID="Signatory1DropDownList" runat="server" 
               ></asp:DropDownList>
            <span class="star">
            * </span>
            </td>
        
        <td align="right" colspan="2" >  <b>Select Second Sigantory :</b>     
           </td>
        
        <td align="left" >  
        <asp:DropDownList ID="Signatory2DropDownList" runat="server" 
               ></asp:DropDownList>       
            &nbsp;<span class="star">* </span></td>
      </tr>
       <tr>
         <td   align="right" ><b>Transaction Date :</b></td>
        
       <td align="left" colspan="2" >         
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
        
         <td class="auto-style1" >         
            <b>Voucher&nbsp; Number :</b></td>
        
        <td align="left" class="style2" >         
            <span class="star">
            <asp:TextBox ID="VoucherNoTexBox" runat="server" CssClass="auto-style2" 
                TabIndex="4" Height="18px" Width="121px"></asp:TextBox>
            * </span></td>
      </tr>
          <tr>
         <td   align="right" >&nbsp;</td>
        
       <td align="left" colspan="2" >         
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" 
                TargetControlID="TranDateTextBox" />
              </td>
        
        <td align="left" class="style2" colspan="2" >         
        <asp:Button ID="BEFTNVoucherButton" runat="server" Text="Submit BEFTN Voucher" CssClass="buttoncommon" OnClientClick="return fnCheckInput();"
               Width="150px" Height="24px" onclick="BEFTNVoucherButton_Click"/></td>
        
      
      </tr>
        <tr>
            <td align="left" colspan="5">
           &nbsp;</td>
      </tr>
       <tr>
            <td align="left" colspan="5">
            
                <table align="left">
                    <tr>
                        <td>
                         <div id="dvGridSurrender" runat="server"  >
                         
                                <asp:GridView ID="SurrenderListGridView" runat="server" AutoGenerateColumns="False" 
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
                                                             
                               
                                <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />  
                                <asp:BoundField DataField="REP_NO" HeaderText="Repurchase No" />  
                                <asp:BoundField DataField="REP_DT" HeaderText="Repurchase Date" /> 
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="QTY" HeaderText="No of Units" />    
                                <asp:BoundField DataField="REP_PRICE" HeaderText="Rate" />  
                                <asp:BoundField DataField="TOTAL" HeaderText="Total Amount" /> 
                                <%--<asp:TemplateField HeaderText="Voucher No.">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="VoucherNumberTextBox" Text='<%#Eval("VOUCHER_NO") %>' Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField> --%>                                                                                      
                                                             
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
                                
      </tr>    
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>


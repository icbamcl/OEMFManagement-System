<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitPriceRefixationEntry.aspx.cs" Inherits="UI_UnitPriceRefixationEntry" Title="Unit Price Refixation Entry (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
    function fnCheckInput()
    {
    
         
          if(document.getElementById("<%=refixationDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=refixationDateTextBox.ClientID%>").focus();
                alert("Please Enter Refixation  Date ");
                return false;
          }
         if(document.getElementById("<%=NAVDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=NAVDateTextBox.ClientID%>").focus();
                alert("Please Enter NAV  Date ");
                return false;
               }
           
          
     }
       
      
       function fnCheckRegNo()
        {
            if(document.getElementById("<%=refixationDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=refixationDateTextBox.ClientID%>").focus();
                alert("Please Enter Refixation  Date ");
                return false;
                
            }
           if(document.getElementById("<%=NAVDateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=NAVDateTextBox.ClientID%>").focus();
                alert("Please Enter NAV  Date ");
                return false;
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
        .auto-style3 {
            height: 25px;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Price Refixation Entry Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" colspan="4" class="style1">
            </td>
      </tr>
          <tr>
        <td   align="right" class="auto-style3" ><b>&nbsp; NAV Date :</b></td>
        
        <td align="left" class="auto-style3" >         
            <span class="star">
            <asp:TextBox ID="NAVDateTextBox" runat="server" CssClass="textInputStyleDate" ></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="navDateCalendarExtender1" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="NAVDateTextBoxImageButton" 
                TargetControlID="NAVDateTextBox" />
            <asp:ImageButton ID="NAVDateTextBoxImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                />
               * 
                </span></td>
        
     <td   align="left" class="auto-style3" >                    
         &nbsp;</td>
        
        <td   align="right" style="text-align: left" class="auto-style3" >&nbsp;</td>
        
      </tr>
      <tr>
        <td   align="right" class="auto-style3" ><b>&nbsp; Price Refixation Date :</b></td>
        
        <td align="left" class="auto-style3" >         
            <span class="star">
            <asp:TextBox ID="refixationDateTextBox" runat="server" CssClass="textInputStyleDate" ></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="refixationDateTextBox" />
            <asp:ImageButton ID="RegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                />
               * <asp:Button ID="findButton" 
                    runat="server" AccessKey="f" CssClass="buttonmid" 
                    meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                    onclientclick="return fnCheckRegNo();" TabIndex="2" Text="Find" />
                </span></td>
        
     <td   align="left" class="auto-style3" >                    
         &nbsp;</td>
        
        <td   align="right" style="text-align: left" class="auto-style3" >&nbsp;</td>
        
      </tr>
     
   
         
        <tr>
            <td align="left" colspan="4">
           &nbsp;</td>
      </tr>
       <tr>
            <td align="left" colspan="4">
            
                <table align="center">
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
                               <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="leftCheckBox" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>--%>
                                                             
                                <asp:BoundField DataField="FUND_CD" HeaderText="Fund Code" />
                                <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                 <asp:TemplateField HeaderText="Effective Date">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="effectDateTextBox"  Width="80px" Value="0"  Text='<%#Eval("EFFECTIVE_DATE") %>' ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" Format="dd-MMM-yyyy" PopupButtonID="effectDateImageButton" TargetControlID="effectDateTextBox" />
                                     <asp:ImageButton ID="effectDateImageButton" runat="server"  AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                />
                                </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="Sale Price">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="salePriceTextBox"  Width="80px" Value="0"  Text='<%#Eval("REFIX_SL_PR") %>' ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField> 
                                                                                                              
                               <asp:TemplateField HeaderText="Surrender Price">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="surrenderPriceTextBox"  Width="80px" Value="0"  Text='<%#Eval("REFIX_REP_PR") %>' ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField HeaderText="NAV @ Cost Price">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="navCPTextBox"  Width="80px" Value="0"  Text='<%#Eval("NAV_CP") %>' ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField> 
                                                           
                                <asp:TemplateField HeaderText="NAV @ Market Price">
                                <ItemTemplate>
                                <asp:TextBox runat="server" ID="navMPTextBox"  Width="80px" Value="0"  Text='<%#Eval("NAV_MP") %>' ></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField> 
                                                                                                              
                                  
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                     <tr>
                         <td align="left" colspan="4">
                      &nbsp;</td>
                     </tr>
                </table>
            
            </td>   
      </tr>
          <tr>
            <td align="center" colspan="4">
        <asp:Button ID="savetButton" runat="server" Text="Save Price Refixation" 
                CssClass="buttoncommon"  Width="133px" Height="30px" OnClick="savetButton_Click" 
              /> 
              </td>
      </tr>
          <tr>
            <td align="left" colspan="4">
           &nbsp;</td>
      </tr>
       </table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>


<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRepurchaseCHEQUEPrintAccount.aspx.cs" Inherits="UI_UnitRepurchaseCHEQUEPrintAccount" Title="Unit Repurchase Cheque Print Account (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
  
 
 
      
    
            			               
  
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
            height: 20px;
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
           Unit Holder Repurchase Cheque Print Form&nbsp;
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
        <td   align="right" class="auto-style2" ><b>&nbsp;</b></td>
        
        <td align="left" colspan="2" class="auto-style2" >         
            </td>
        
     <td   align="right" class="auto-style2" ><b>Fund Name :</b></td>
        
        <td   align="right" style="text-align: left" class="auto-style2" ><asp:DropDownList ID="fundNameDropDownList" runat="server"  AutoPostBack="true"
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            <span class="star">* </span>
            </td>
        
      </tr>
      
          <tr>
         <td   align="right" >&nbsp;</td>
        
       <td align="left" colspan="2" >         
            &nbsp;</td>
        
        <td align="left" class="style2" colspan="2" >         
        <asp:Button ID="ChecqueVoucherButton" runat="server" Text="Print Cheque" CssClass="buttoncommon" 
               Width="150px" Height="24px" OnClick="ChecqueVoucherButton_Click"/></td>
        
       
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
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />                                 
                                <asp:BoundField DataField="TOTAL" HeaderText="Total Amount" /> 
                                <asp:BoundField DataField="CHEQUE_NO" HeaderText="Cheque No." />    
                                <asp:BoundField DataField="CHEQUE_DATE" HeaderText="Cheque Date" />  
                                                                                                              
                                                     
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


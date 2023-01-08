<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportCertBookInfo.aspx.cs" Inherits="UI_UnitReportCertBookInfo" Title=" Cert Book Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />  
    <script language="javascript" type="text/javascript"> 
    
   
                 
       
      
    </script>
    <style type="text/css">
      .style1
        {
            border: 1px solid #800080;
             text-align:center;
        }
        .style2
        {
            border: 1px solid #800080;
            text-align:center;
            background-color:White;
        }
         .style2
        {
            height:15px;
        }
    
        .style4
        {
            width: 74px;
        }
        .style5
        {
            width: 77px;
        }
    
     </style>
<br />

 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Certificate Book Issue Form
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
<div id="dvUpdatePanel" runat="server">
<asp:UpdatePanel ID="CertInfoUpdatePanel" runat="server">
<ContentTemplate>
<table width="400" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="150"></colgroup>

    <tr>
        <td align="left">Fund Name:</td>
        <td align="left">
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1" meta:resourcekey="fundNameDropDownListResource1" 
               
               ></asp:DropDownList><span class="star">*</span></td>
        
    </tr>    
     
    <tr>
       <td align="left">Branch Name:</td>
        <td align="left">
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2" meta:resourcekey="branchNameDropDownListResource1" 
               ></asp:DropDownList><span class="star">*</span></td>
      
    </tr>
    <tr>
            <td align="left"  >Certificate :</td>
            <td align="left" >
                <asp:DropDownList ID="certNoDropDownList" runat="server" 
                    CssClass="DropDownList"  TabIndex="5" 
                    Width="75px">
                    <asp:ListItem Value="0">--select--</asp:ListItem>
                    <asp:ListItem Value="A">A</asp:ListItem>
                    <asp:ListItem Value="B">B</asp:ListItem>
                    <asp:ListItem Value="C">C</asp:ListItem>
                    <asp:ListItem Value="D">D</asp:ListItem>
                    <asp:ListItem Value="E">E</asp:ListItem>
                    <asp:ListItem Value="F">F</asp:ListItem>
                    <asp:ListItem Value="G">G</asp:ListItem>
                    <asp:ListItem Value="H">H</asp:ListItem>
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="J">J</asp:ListItem>
                    <asp:ListItem Value="K">K</asp:ListItem>
                </asp:DropDownList>
            </td>
          
       
           
        </tr>
     <tr>
            <td align="left"  >&nbsp;</td>
            <td align="left" >                
                                &nbsp;</td>
          
       
           
        </tr>
        <tr>
            <td align="left"  >&nbsp;</td>
            <td align="left" >
                &nbsp;</td>
          
       
           
        </tr>
         <tr>
            <td align="left"  >&nbsp;</td>
            <td align="left" >
                &nbsp;</td>
          
       
           
        </tr>
    </table>
    <br />
</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="fundNameDropDownList" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>
</div>
<table width="1100">
 <tr>
    <td class="style4">&nbsp </td>
     <td class="style5">&nbsp </td>
        <td align="left" colspan="6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="ShowDataButton" runat="server" Text="Show Data" AccessKey="s" 
                CssClass="buttoncommon" onclick="ShowDataButton_Click" />&nbsp;&nbsp;
                <asp:Button ID="PrintButton" runat="server" Text="Print" AccessKey="p" CssClass="buttoncommon"  />&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Reset" CssClass="buttoncommon" AccessKey="r" OnClientClick="return fnReset();"   />&nbsp;&nbsp;  
         <asp:Button ID="Button3" runat="server" Text="Close" CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" /></td>
    </tr>
</table>
   <table align="center"  cellpadding="0" cellspacing="0" border="0" 
              style="width: 770px">
         <tr>
         <td> 
             <div id="dvSearchRegi" runat="server" style="text-align: center; display: block; overflow: auto; width:750; height:400px;">

               <asp:DataGrid ID="dgCertInfo" runat="server" AutoGenerateColumns="False" Width="996px"  
                    >
                   <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID"> </asp:BoundColumn>     
                    <asp:BoundColumn DataField="FUND_CD" HeaderText="FUND_CD"></asp:BoundColumn> 
                    <asp:BoundColumn DataField="BR_CD" HeaderText="BR_CD"></asp:BoundColumn> 
                    <asp:BoundColumn DataField="CERT_TYPE" HeaderText="CERT_TYPE"> </asp:BoundColumn>     
                    <asp:BoundColumn DataField="DELIV_DT" HeaderText="DELIVERY_DATE"></asp:BoundColumn> 
                    <asp:BoundColumn DataField="BOOK_NO_START" HeaderText="BOOK_NO_START"> </asp:BoundColumn>     
                    <asp:BoundColumn DataField="BOOK_NO_END" HeaderText="BOOK_NO_END"></asp:BoundColumn> 
                    <asp:BoundColumn DataField="CERT_NO_START" HeaderText="CERT_NO_START"> </asp:BoundColumn>     
                    <asp:BoundColumn DataField="CERT_NO_END" HeaderText="CERT_NO_END"></asp:BoundColumn> 
                    <asp:BoundColumn DataField="BOOK_NO_BALANCE" HeaderText="BOOK_NO_BALANCE"></asp:BoundColumn> 
                               
                  
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


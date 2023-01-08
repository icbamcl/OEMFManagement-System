<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitTransferEntryCDS.aspx.cs" Inherits="UI_UnitTransferEntryCDS" Title=" Unit Transfer Entry (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
  function fnReset()
  {
      var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
             document.getElementById("<%=transferNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=transferDateTextBox.ClientID%>").value ="";
             document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=tferorHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tferorjHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=tfereeHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=tfereejHolderNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitHoldingTextBox.ClientID%>").value ="";
              
             document.getElementById("<%=tdTferor.ClientID%>").innerHTML ="";
             document.getElementById("<%=tdTferee.ClientID%>").innerHTML ="";
             
             if(document.getElementById("<%=leftDataGrid.ClientID%>"))
             {
                 document.getElementById("<%=divLeftgrid.ClientID%>").innerHTML ="";
             }  
             return false;
        }
  }
 
   
    
    
     function fnCheckInput()
    {
          if(document.getElementById("<%=transferNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=transferNoTextBox.ClientID%>").focus();
                alert("Please Enter Transfer Number ");
                return false;
               }
        
          if(document.getElementById("<%=transferDateTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=transferDateTextBox.ClientID%>").focus();
            alert("Please Enter Date of Transfer  ");
            return false;
           }
          if(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=tferorRegNoTextBox.ClientID%>").focus();
            alert("Please Enter Registration Number of Transferor  ");
            return false;
           }
          if(document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").focus();
            alert("Please Enter Registration Number of Transferee  ");
            return false;
           }
           
                                            
             if(document.getElementById("<%=transferNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=transferNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=transferNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Transfer  Number");
                    return false;
                }
            }
           
            if(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=tferorRegNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Transferor Registration Number");
                    return false;
                }
            }
            
           if(document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Transferee Registration Number");
                    return false;
                }
            }
      
      
      
        if(document.getElementById("<%=transferDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=transferDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=transferDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
      
      
        if(document.getElementById("<%=leftDataGrid.ClientID%>"))
        {
          if(document.getElementById("<%=tferorRegNoTextBox.ClientID%>").value ==document.getElementById("<%=tfereeRegNoTextBox.ClientID%>").value )
            {
               alert("Transfer IN and OUT could not be Same Registration");
                return false;
            }
        
        }
       if(document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").focus();
                alert("Please Add Total Transfer Units ");
                return false;
               }
        if(document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Total Transfer Units");
                    return false;
                }
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
        .style4
        {
            height: 38px;
        }
        .style5
        {
            width: 159px;
        }
        .style6
        {
            width: 155px;
        }
        </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Death Transmission Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

      <br />
      <div id="dvUpdatePanel" runat="server">
     
      <div id="dvContent" runat="server" 
        
        style="width:658px; padding-left:80px; padding-right:100px; height: 271px;" >
        
          <div  id="dvLeftContent" style=" float:left; width:430;">
          <table width="420" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="300"></colgroup>
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
            
        </tr>
        <tr>
            <td align="left" >Transfer No:</td>
            <td align="left">
                <asp:TextBox ID="transferNoTextBox" runat="server"  MaxLength="6" 
                    CssClass= "TextInputStyleSmall" TabIndex="1" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*</span></td>
            
           
        </tr>
        <tr>
             <td align="left"  >&nbsp;Date of Transfer:</td>
            <td align="left" >
                <asp:TextBox ID="transferDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="2"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="transferDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                <span class="star">* </span></td>
             
        </tr>
        <tr>
            <td align="left" >Transferor Reg No:</td>
            <td align="left">
                <asp:TextBox ID="tferorFundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" TabIndex="3"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="tferorBranchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="tferorRegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="5" 
                    ontextchanged="tferorRegNoTextBox_TextChanged" AutoPostBack="True" 
                    Width="86px"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
           
           
        </tr>
         
        <tr>
            <td align="left" >Name of Transferor:</td>
            <td align="left">
                <asp:TextBox ID="tferorHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left">&nbsp;</td>
            <td align="left">
                 <asp:TextBox ID="tferorjHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox></td>
                                     
        </tr>
        <tr>
                  <td align="left" >&nbsp;</td>
           
        </tr>   
         <tr>
            <td align="left" >Transferee Reg No:</td>
            <td align="left">
                <asp:TextBox ID="tfereeFundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" AutoPostBack="True" 
                    TabIndex="8" Enabled="False"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="tfereeBranhCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="9"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="tfereeRegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="9" 
                    ontextchanged="tfereeRegNoTextBox_TextChanged" AutoCompleteType="Disabled" 
                    AutoPostBack="True" Width="86px"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
           
           
        </tr>
         
        <tr>
            <td align="left" >Name of Transferee:</td>
            <td align="left">
                <asp:TextBox ID="tfereeHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="10"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left">&nbsp;</td>
            <td align="left">
                 <asp:TextBox ID="tfereejHolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="11"></asp:TextBox></td>
                                     
        </tr>
            
        </table>
        </div>
          <div style="border-style: solid; border-color: inherit; border-width: 1px; width:222px; float:left; height:259px;">
                <table align="center" width="164" cellpadding="0" cellspacing="0" 
                    style="height: 261px">
                <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid">Signature of Transferor    </span></td></tr>
                   <tr>
                    <td align="left" id="tdTferor" runat="server" class="style4">
                  <asp:Image ID="SignImage" runat="server" Height="104px" Width="200px"  />
                    </td>    
                </tr>
                 <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid">Signature of Transferee</span></td>
                </tr>
                 <tr style="height:85px;">
                    <td align="left" id="tdTferee" runat="server">
                  <asp:Image ID="PhotoImage" runat="server" Height="115px" Width="200px"  />
                  </td>
                 </tr>
                </table>
           </div>
           
    </div>    
    
    <br />
    <table align="center" cellpadding="0" cellspacing="0" style="width: 470px">
     <colgroup width="250"></colgroup>
    
        <tr >
           <td align="right" ><b>Total Holding&nbsp; Units:</b></td>
           <td align="left">
                <asp:TextBox ID="TotalUnitHoldingTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#009933"></asp:TextBox>
            </td>           
        </tr>
         <tr >
           <td align="right" ><b>Selected Total Units for Transfer:</b></td>
            <td align="left">
                <asp:TextBox ID="TotalUnitRepurchaseTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#990033"></asp:TextBox>
        <asp:Button ID="addTotalSum" runat="server" Text="Add Total" CssClass="buttoncommon" 
                 AccessKey="s" onclick="addTotalSum_Click" />
            </td>           
        </tr>
          <tr>
              <td colspan="2">
        <table align="center"  cellpadding="1px"  cellspacing="1px">
                
                
         <tr>
         
         <td align="right" class="style6" ><b>SL_TR_NO.</b></td>
         <td class="style4"><b style="text-align: right">Transfer Units</b></td>
         <td class="style5"><b style="text-align: left">Exist Unit</b></td>
         </tr>
         <tr>
         <td colspan="3">
          <div id="divLeftgrid" runat="server" style="text-align: center; display: block; overflow: auto; width:400px; height:166px;">
                <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                        ShowHeader="False">
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                <ItemStyle CssClass="TableText"></ItemStyle>
                <HeaderStyle CssClass="TableHeader2"></HeaderStyle>
                <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                <Columns>
                
                <asp:TemplateColumn>
                
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" /> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>       
                 <asp:TemplateColumn HeaderText="SL_TR_No." >
                        <ItemTemplate>
                            <asp:TextBox ID="SL_TR_NoTextBox" runat="server"  Enabled="false" Width="100px"
                                Text='<%# DataBinder.Eval(Container.DataItem,"SL_TR_NO") %>'
                               
                                ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="Surrender_Units" >
                        <ItemTemplate>
                            <asp:TextBox ID="Sale_UnitsTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"SURRENDER_UNITS") %>' Width="100px"
                                onkeypress= "fncInputNumericValuesOnly()"   
                                meta:resourcekey="certNoTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"  Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                              
                 <asp:TemplateColumn HeaderText="Exist_Units">
                        <ItemTemplate>
                            <asp:TextBox ID="Exist_UnitsTextBox" runat="server"   
                                Text='<%# DataBinder.Eval(Container.DataItem,"EXIST_UNITS") %>'  
                                Width="100px" Enabled="false" ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                
                  
                  
                </Columns>
            </asp:DataGrid>
                   </div>
           </td>
            </tr>
            </table>        
         </td>
         </tr>
              
      </table>
    
    </div>
    <br />
    
    
    <table align="center" cellpadding="0" cellspacing="0" style="width: 464px">
     <tr>
        <td align="right">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheckInput();"       
                 AccessKey="s" onclick="SaveButton_Click"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" 
                  />
        
        </td>
        <td>
        &nbsp;
        </td>
    </tr>
   </table>
   
    <br />
    <br />
    <br />
    <br />
</asp:Content>


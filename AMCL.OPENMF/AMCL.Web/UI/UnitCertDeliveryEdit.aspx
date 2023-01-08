<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitCertDeliveryEdit.aspx.cs" Inherits="UI_UnitCertDeliveryEdit" Title=" Unit Certificate Print Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
    
     
    function fnEnable(Action)
        {
       
            if(Action.indexOf("SaleNoRadioButton")!=-1)
            {   
                if( document.getElementById("<%=SaleNoRadioButton.ClientID%>").checked)
                {                   
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=false;  
                     
                    
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").value=""; 
                                        
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=true;    
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=true;    
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=true;    
                                                               
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {   
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=true; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=true;  
                     
                                                                             
                }
              }
              
              else if(Action.indexOf("TrNoRadioButton")!=-1)
              {
               
                if(document.getElementById("<%=TrNoRadioButton.ClientID%>").checked)
                {       
                            
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=false;  
                     
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").value="";                       
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").value=""; 
                    
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=true; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=true;                       
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=true;    
                                                               
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {  document.getElementById("<%=fromTrNoTextBox.ClientID%>").value="";   
                   document.getElementById("<%=toTrNoTextBox.ClientID%>").value="";   
                   document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=true; 
                   document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=true;                                                              
                }
              }
              else if(Action.indexOf("RegNoRadioButton")!=-1)
              {
              
                if(document.getElementById("<%=RegNoRadioButton.ClientID%>").checked)
                {                   
                 
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=false;  
                     
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").value="";                      
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").value=""; 
                    
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=true; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=true;                       
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=true;    
                                                               
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {  document.getElementById("<%=fromRegNoTextBox.ClientID%>").value="";   
                   document.getElementById("<%=toRegNoTextBox.ClientID%>").value="";   
                   document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=true; 
                   document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=true;                                                              
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
	
	function  fnReset()
	{
	    document.getElementById("<%=SaleNoRadioButton.ClientID%>").checked=true;
        document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=false; 
        document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=false;  
         
       
        document.getElementById("<%=fromTrNoTextBox.ClientID%>").value=""; 
        document.getElementById("<%=toTrNoTextBox.ClientID%>").value=""; 
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").value=""; 
        document.getElementById("<%=toRegNoTextBox.ClientID%>").value=""; 
        
        
        document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=true;    
        document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=true;    
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=true;   
        document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=true;    
                                                   
        document.getElementById("<%=fromSaleNoTextBox.ClientID%>").focus(); 
        document.getElementById("<%=aouthorizeRadioButton.ClientID%>").checked=false;
        document.getElementById("<%=authorizeTextBox.ClientID%>").value="";
        document.getElementById("<%=selfRadioButton.ClientID%>").checked=true;
        if(document.getElementById("<%=dgCertInfo.ClientID%>"))
             {
                document.getElementById("<%=dvCertInfo.ClientID%>").innerHTML ="";
             }  
        document.getElementById("<%=totalRowCountLabel.ClientID%>").value="";
	            	   	    	   	    	    
	     return false;
	}
	function CheckAllDataGrid(checkVal)
     {
            if(document.getElementById("<%=dgCertInfo.ClientID%>"))
            {  
                
                var datagrid=document.getElementById("<%=dgCertInfo.ClientID%>")
                   
                var check = 0;                
                
                for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                {
                  var tr = datagrid.rows[rowCount];
                  var td= tr.childNodes[0]; 
                  var item = td.firstChild; 
                  var strType=item.type;
                  if(strType=="checkbox")
                  {
                        item.checked = checkVal; 
                  }                 
                }
            }
     }
     function fnOnChangeText()
     {
          if( document.getElementById("<%=SaleNoRadioButton.ClientID%>").checked)
                {                   
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=false;  
                     
                    
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").value=""; 
                                        
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=true;    
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=true;    
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=true;    
                   
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").value=document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value;                                           
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").focus();               
                             
                }
                if(document.getElementById("<%=TrNoRadioButton.ClientID%>").checked)
                {       
                            
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=false;  
                     
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").value="";                       
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").value=""; 
                    
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=true; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=true;                       
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=true;    
                     
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").value=document.getElementById("<%=fromTrNoTextBox.ClientID%>").value;                                                                
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").focus();               
                             
                }
               if(document.getElementById("<%=RegNoRadioButton.ClientID%>").checked)
                {                   
                 
                    document.getElementById("<%=fromRegNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").disabled=false;  
                     
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").value="";                      
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").value=""; 
                    
                    document.getElementById("<%=fromSaleNoTextBox.ClientID%>").disabled=true; 
                    document.getElementById("<%=toSaleNoTextBox.ClientID%>").disabled=true;                       
                    document.getElementById("<%=fromTrNoTextBox.ClientID%>").disabled=true;   
                    document.getElementById("<%=toTrNoTextBox.ClientID%>").disabled=true;    
                  
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").value=document.getElementById("<%=fromRegNoTextBox.ClientID%>").value;                                                                                                           
                    document.getElementById("<%=toRegNoTextBox.ClientID%>").focus();               
                             
                }
     }
     function fnCheqInput()
     {
        if(document.getElementById("<%=dgCertInfo.ClientID%>"))
        {
        
            var leftDatagrid=document.getElementById("<%=dgCertInfo.ClientID%>")              
            var chek=0;                         
            for( var rowCount = 1; rowCount < leftDatagrid.rows.length; rowCount++)
            {
              var tr = leftDatagrid.rows[rowCount];
              var td= tr.childNodes[0]; 
              var item = td.firstChild; 
              var strType=item.type;
              if(strType=="checkbox")
              {
                if(item.checked)
                {
                 chek=chek+1;                   
                } 
              }
            
            }
            if(chek==0)
            {
                alert("Please Select Any Row to Save");
                return false;
            }                 
        }
        else
        {
            alert("No Data to Save");
            return false;
        }
         if(document.getElementById("<%=aouthorizeRadioButton.ClientID%>").checked)
         {
            if(document.getElementById("<%=authorizeTextBox.ClientID%>").value=="")
            {
                alert("Please Enter Aouthorized Person's Name & Contact Number")
                document.getElementById("<%=authorizeTextBox.ClientID%>").focus();
                return false;
            }
         }
          if(document.getElementById("<%=dateTextBox.ClientID%>").value=="")
            {
                alert("Please Enter Delivery Date")
                document.getElementById("<%=dateTextBox.ClientID%>").focus();
                return false;
            }
           if(document.getElementById("<%=dateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=dateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=dateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
    }    
    </script>
        
    <style type="text/css">
        .style4
        {
            width: 275px;
        }
        .style5
        {
            color: #009900;
        }
    </style>
        
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    

 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Unit Cetificate Delivery Edit Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
 <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>
<table align="center" cellpadding="0" cellspacing="0" style="width: 650px" > 
<colgroup width="120"></colgroup>
<colgroup width="130"></colgroup>
<colgroup width="90"></colgroup>
 <tr>
        <td align="left">
         Fund Code:
        </td>
        <td align="left">
            <asp:DropDownList ID="fundCodeDDL" runat="server">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td align="left">
         Branch Code:
        </td>
        <td align="left">
            <asp:DropDownList ID="branchCodeDDL" runat="server">
            </asp:DropDownList>
        </td>
      </tr>
    <tr>
       <td align="left"  >
           <asp:RadioButton ID="SaleNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="1"  onclick="fnEnable('SaleNoRadioButton');"
                Text="Sale No:" style="font-weight: 700" Checked="True"/>
        </td>
        <td align="left" >
          <asp:TextBox ID="fromSaleNoTextBox" runat="server"  CssClass= "TextInputStyleSmall" TabIndex="2" onBlur="Javascript:fnOnChangeText();"
                     onkeypress= "fncInputNumericValuesOnly()" Width="95px" ></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="toSaleNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                                        onkeypress="fncInputNumericValuesOnly()" TabIndex="2" Width="95px"></asp:TextBox>
        </td>
      
      </tr>
<%--      <tr>
       <td align="left"  >
           <asp:RadioButton ID="RenNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="3" onclick="fnEnable('RenNoRadioButton');"
                Text="Renewal No:" style="font-weight: 700"  />
               
        </td>
        <td align="left"  colspan="3" >
                                    <asp:TextBox ID="fromRenNoTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4" 
                     onkeypress= "fncInputNumericValuesOnly()"
                    Width="95px" Enabled="False" ></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="toRenNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                                        Enabled="False" onkeypress="fncInputNumericValuesOnly()" TabIndex="4" 
                                        Width="95px"></asp:TextBox>
        </td>
      
    </tr>--%>
     <tr>
     <td align="left">
           <asp:RadioButton ID="TrNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="5" onclick="fnEnable('TrNoRadioButton');"
                Text="Transfer No:" style="font-weight: 700" />
        </td>
        <td align="left" class="style4">
                                    <asp:TextBox ID="fromTrNoTextBox" runat="server"  onBlur="Javascript:fnOnChangeText();"
                    CssClass= "TextInputStyleSmall" TabIndex="6" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" Enabled="False" ></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="toTrNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                                        Enabled="False" meta:resourcekey="regNoTextBoxResource1" 
                                        onkeypress="fncInputNumericValuesOnly()" TabIndex="6" Width="95px"></asp:TextBox>
        </td>
       
        
      
    </tr>
      <tr>
     <td align="left"    >
           <asp:RadioButton ID="RegNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="5" onclick="fnEnable('RegNoRadioButton');"
                Text="Regi No:" style="font-weight: 700" />
        </td>
        <td align="left" class="style4">
 <asp:TextBox ID="fromRegNoTextBox" runat="server"  onBlur="Javascript:fnOnChangeText();"
                    CssClass= "TextInputStyleSmall" TabIndex="7" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" Enabled="False"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="toRegNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                                        Enabled="False" meta:resourcekey="regNoTextBoxResource1" 
                                        onkeypress="fncInputNumericValuesOnly()" 
                TabIndex="7" Width="95px"></asp:TextBox>
        </td>
       
        
      
    </tr>
     <tr>
        <td align="center" colspan="2">
                &nbsp;</td>
    </tr>
    <tr>
     <td align="center" > 
            
            &nbsp; </td>
        <td align="left">  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="findButton" runat="server" AccessKey="f" 
                CssClass="buttoncommon" 
                Text="Find" 
                onclick="findButton_Click" />
            &nbsp; </td>
    </tr>
   
      <tr>
        <td align="left" colspan="2">&nbsp;</td>
      
    </tr>
</table>
<table align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="left"  style="width:150px;">
            <b>Total Record Count:</b></td>    
         <td align="left" style="width:100px;">
            
             <asp:Label ID="totalRowCountLabel" runat="server" Font-Bold="True" 
                 ForeColor="#CC3300" Text="0"></asp:Label>
            
        </td>
         <td align="left">
             <b>Certificate Received By</b> :
             <asp:RadioButton ID="selfRadioButton" runat="server" GroupName="certDelivery"  Checked="true"
                 Text="SELF" Font-Bold="True" />
        </td>
         <td align="left">
          <asp:RadioButton ID="aouthorizeRadioButton" runat="server" GroupName="certDelivery" 
                 Text="Authorized Person" Font-Bold="True" />
             :</td>
         <td align="left">
            <asp:TextBox ID="authorizeTextBox" runat="server"   CssClass= "textInputStyle" 
                 Width="200px"  ></asp:TextBox>
        </td>
        <td align="left">
            <asp:Label ID="dateLabel" runat="server" Font-Bold="True" 
                 Text="Date: "></asp:Label>
        </td>
        <td align="left"> 
            <asp:TextBox ID="dateTextBox" runat="server"   CssClass= "textInputStyle" 
                 Width="100px"   ></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="chequeDateImageButton" 
                TargetControlID="dateTextBox" />
                <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" />   
            </td>    
    </tr>
     <tr>
        <td align="center" colspan="7">
            &nbsp;
        </td>        
    </tr>
     <tr>
        <td align="center" colspan="7">
            <div id="dvCertInfo" runat="server" style="text-align: center; display: block; overflow: auto; width:1000px; height:300px;">

                       <asp:DataGrid ID="dgCertInfo" runat="server" AutoGenerateColumns="False"  
                             Width="990px" BorderColor="Black" CellPadding="1" CellSpacing="1" 
                           meta:resourcekey="dgLedgerResource1"  
                            >
                           <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                            <ItemStyle CssClass="TableText"></ItemStyle>
                            <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                            <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                     <HeaderTemplate>
                                            <input id="chkAllWarrant" type="checkbox" onclick="CheckAllDataGrid(this.checked)"> 
                                     </HeaderTemplate>
                                    <ItemTemplate> 
                                        <asp:CheckBox ID="leftCheckBox" runat="server"></asp:CheckBox> 
                                    </ItemTemplate>                    
                                </asp:TemplateColumn>   
                                <asp:BoundColumn DataField="REG_No" HeaderText="Regi No">
                               
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="Trans_Type">
                                
                                </asp:BoundColumn>                                                                                              
                             <asp:BoundColumn  DataField="SL_TR_RN_NO" HeaderText="SL_TR_RN_No">
                                
                                </asp:BoundColumn>    
                             <asp:BoundColumn DataField="CERTIFIACTE" HeaderText="Certificate">
                                
                                 <HeaderStyle Width="300px" />
                                
                                </asp:BoundColumn>                                                                                                
                                <asp:BoundColumn DataField="DELVERY_DT" HeaderText="Delivery_Date">
                                                               
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                                                               
                                </asp:BoundColumn>                                                                                          
                            <asp:BoundColumn  DataField="RECEIVED_BY" HeaderText="Received_By">
                                                               
                                </asp:BoundColumn>
                           
                           <asp:BoundColumn DataField="DELVERY_BY" HeaderText="Delivery_By"></asp:BoundColumn>
                           <asp:TemplateColumn> 
                           <HeaderTemplate>Cabinet No</HeaderTemplate>                                    
                             <ItemTemplate>
                             <asp:TextBox ID="cabinetNoTextBox" runat="server" Width="50px"  Text='<%# DataBinder.Eval(Container.DataItem,"CABI_NO") %>'></asp:TextBox>
                                
                        </ItemTemplate>
                          </asp:TemplateColumn>
                          <asp:TemplateColumn> 
                            <HeaderTemplate>Locker No</HeaderTemplate>                                    
                             <ItemTemplate>
                             <asp:TextBox ID="lockerNoTextBox" runat="server"  Width="50px" Text='<%# DataBinder.Eval(Container.DataItem,"LOCK_NO") %>'></asp:TextBox>                                
                        </ItemTemplate>
                         </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>              
                        </div>    
        </td>        
    </tr>
   </table>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
     
</Triggers>
</asp:UpdatePanel>
<table width="800" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
     <tr>
        <td align="right">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="SaveButton_Click" AccessKey="s" 
                meta:resourcekey="SaveButtonResource1"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="a" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>


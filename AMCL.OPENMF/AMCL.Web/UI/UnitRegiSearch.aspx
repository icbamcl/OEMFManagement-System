<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRegiSearch.aspx.cs" Inherits="UI_UnitRegiSearch" Title=" Registration Serch Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {
            
            document.getElementById("<%=regNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddressTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddressTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=certNoDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=certNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=TINTextBox.ClientID%>").value = "";
            document.getElementById("<%=mobileNumberTextBox.ClientID%>").value ="";
            document.getElementById("<%=NIDTextBox.ClientID%>").value ="";
            document.getElementById("<%=PassportNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=BirthCertNoTextBox.ClientID%>").value ="";
       
            return false;
        }
        else
        {
         return false;
        }
       
    }
    
   
     function fnCheqInput()
        {
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Registration Number");
                    return false;
                }
            }
            
    }
         
       
        function fnClearID(value)
        {
            if(value=='0')
            {
                document.getElementById("<%=certNoTextBox.ClientID%>").value="";
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
      .style7
     {
         font-size: 12px;
         font-weight: bold;
         color:Red; 
        
     }
      .style8
     {
         color: #990000;
     }
      .style9
     {
         height: 20px;
     }
      </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Registration Search &nbsp; Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <div id="dvContent" runat="server" style="width:900px; padding-left:80px; padding-right:100px" >
     
  
      
          
          <table width="450" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="140"></colgroup>
    
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
           
        </tr>
        <tr>
            <td align="right" >Registration No:</td>
            <td align="left">
                <asp:DropDownList ID="fundCodeDDL" runat="server"  ></asp:DropDownList>
                        <b>/</b> <asp:DropDownList ID="branchCodeDDL" runat="server"  ></asp:DropDownList>
                            
                        <b>/</b>  <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"   
                    CssClass= "TextInputStyleSmall" TabIndex="1" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    Width="95px"></asp:TextBox>
                             </td>
 
           
        </tr>
         
        <tr>
            <td align="right" >Name of Holder:</td>
            <td align="left">
                <asp:TextBox ID="holderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="2" 
                    meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
            </td>
           
               
           
           
        </tr>
        <tr>
            <td align="right" >Holder Address:</td>
            <td align="left">
                <asp:TextBox ID="holderAddressTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" ></asp:TextBox>
            </td>
           
            
            
        </tr>
        <tr>
           <td align="right" >Name of Joint Holder:</td>
            <td align="left">
                <asp:TextBox ID="jHolderTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
               <tr>
           <td align="right" class="style9" >Mobile Number:</td>
            <td align="left" class="style9">
                <asp:TextBox ID="mobileNumberTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="12" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
               <tr>
           <td align="right" >Email:</td>
            <td align="left">
                <asp:TextBox ID="emailTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
               <tr>
           <td align="right" >Bank:</td>
            <td align="left">
                <asp:DropDownList ID="bankNameDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="bankNameDropDownList_SelectedIndexChanged">
                </asp:DropDownList>
                                    </td>
          
            </tr>
               <tr>
           <td align="right" >Branch:</td>
            <td align="left">
                <asp:DropDownList ID="branchNameDropDownList" runat="server" >
                </asp:DropDownList>
                                    </td>
          
            </tr>
               <tr>
           <td align="right" >Bank Acc No:</td>
            <td align="left">
                <asp:TextBox ID="bankAccTextBox" runat="server" CssClass="textInputStyle" onkeypress="fncInputNumericValuesOnly()"  Width="218px"></asp:TextBox>
                                    </td>
          
            </tr>
             <tr>
           <td align="right" class="style9" >TIN:</td>
            <td align="left" class="style9">
                <asp:TextBox ID="TINTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="12" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
                <tr>
           <td align="right" >National ID No:</td>
            <td align="left">
                <asp:TextBox ID="NIDTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
              <tr>
           <td align="right" >Passport No:</td>
            <td align="left">
                <asp:TextBox ID="PassportNoTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
              <tr>
           <td align="right" >Birth Certificate No:</td>
            <td align="left">
                <asp:TextBox ID="BirthCertNoTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="4" ></asp:TextBox>
                                    </td>
          
            </tr>
        <tr>
            <td align="right" >Certificate:</td>
            <td align="left" >
                 <asp:DropDownList runat="server" ID="certNoDropDownList" onchange=" fnClearID(this.value)"
                     CssClass="DropDownList" Width="75px" TabIndex="5" >
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
                 <asp:ListItem Value="L">L</asp:ListItem>
                 <asp:ListItem Value="Y">Y</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="certNoTextBox" runat="server" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6" Width="205px"></asp:TextBox>
                                    </td>
            
        </tr>   
        <tr>
                  <td align="left">&nbsp;</td>
                  <td align="left">
                      &nbsp;</td>
           
         </tr>
          <tr>
            <td align="left" class="style7">&nbsp;</td>
            <td align="left" id="tdCIP" runat="server" class="style7" >
                &nbsp;</td>
                
        </tr>     
       
          
        <tr>
                  
            <td align="center" colspan="2">
        <asp:Button ID="SerachButton" runat="server" Text="Search" 
                CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="S" onclick="SerachButton_Click" style="height: 20px" 
               />&nbsp;&nbsp;
        <asp:Button ID="ResetButton0" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;
        <asp:Button ID="CloseButton0" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
            </td>
                 
        </tr>           
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>  
        <tr>
            <td class="style8" ><b>Total Record count : </b></td>
            <td colspan="2" style="text-align: left">&nbsp;
                <asp:Label ID="totalRecordCountLabel" runat="server" 
                    style="font-weight: 700; color: #009933; font-size: small"></asp:Label>
            </td>
        </tr> 
        
            
        </table>
     
    
   <br />
   <br />
    <table align="center"  cellpadding="0" cellspacing="0" border="0" 
              style="width: 770px">
         <tr>
         <td> 
             <div id="dvSearchRegi" runat="server" style="text-align: center; display: block; overflow: auto; width:750; height:400px;">

               <asp:DataGrid ID="dgSearchRegi" runat="server" AutoGenerateColumns="False" Width="996px"  
                    >
                   <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>
                    <asp:BoundColumn DataField="REG_BK" HeaderText="Reg_Bk"> </asp:BoundColumn>     
                    <asp:BoundColumn DataField="REG_BR" HeaderText="Reg_Br"></asp:BoundColumn> 
                    <asp:BoundColumn  DataField="REG_NO" HeaderText="Reg_No"> 
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                        </asp:BoundColumn>
                    <asp:BoundColumn DataField="HNAME" HeaderText="Holder Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FMH_NAME" HeaderText="Father's Name"></asp:BoundColumn>  
                     <asp:BoundColumn DataField="MO_NAME" HeaderText="Mother's Name"></asp:BoundColumn> 
                     <asp:BoundColumn DataField="ADDRESS" HeaderText="Address">                         
                        </asp:BoundColumn>     
                    <asp:BoundColumn DataField="JNT_NAME" HeaderText="Joint Holder"></asp:BoundColumn>                     
                      
                   
                                       
                  
                </Columns>
            </asp:DataGrid>              
                </div>          
           </td>
         </tr>
    </table>
  
    
     
    </div>
    <br />
    <table width="450" align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="right">
            &nbsp;</td>
        <td align="left">&nbsp;</td>
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


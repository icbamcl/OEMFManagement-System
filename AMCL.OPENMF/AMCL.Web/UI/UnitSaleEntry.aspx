<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSaleEntry.aspx.cs" Inherits="UI_UnitSaleEntry" Title=" Unit Sale Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {
            document.getElementById("<%=saleDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleNumberTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleRateTextBox.ClientID%>").value ="";          
            document.getElementById("<%=saleRemarksTextBox.ClientID%>").value =""; 
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
          
       
            
         document.getElementById("<%=aTextBox.ClientID%>").value ="";
         document.getElementById("<%=bTextBox.ClientID%>").value ="";
         document.getElementById("<%=cTextBox.ClientID%>").value ="";
         document.getElementById("<%=dTextBox.ClientID%>").value ="";
         document.getElementById("<%=eTextBox.ClientID%>").value ="";
         document.getElementById("<%=fTextBox.ClientID%>").value ="";
         document.getElementById("<%=gTextBox.ClientID%>").value ="";
         document.getElementById("<%=hTextBox.ClientID%>").value ="";
         document.getElementById("<%=iTextBox.ClientID%>").value ="";
         document.getElementById("<%=jTextBox.ClientID%>").value ="";
         
        document.getElementById("<%=kTextBox.ClientID%>").value ="";
         document.getElementById("<%=lTextBox.ClientID%>").value ="";
         document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";
         document.getElementById("<%=SignImage.ClientID%>").src ="";
        
            return false;
        }
        else
        {
         return false;
        }
       
    }
    
    
     function fnResetAll()
    {
       
              
            document.getElementById("<%=saleDateTextBox.ClientID%>").value ="";
            
            
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=saleNumberTextBox.ClientID%>").value ="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value ="";          
            document.getElementById("<%=saleRemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleDateTextBox.ClientID%>").value="";
            document.getElementById("<%=saleRateTextBox.ClientID%>").value="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
            
             document.getElementById("<%=aTextBox.ClientID%>").value ="";
             document.getElementById("<%=bTextBox.ClientID%>").value ="";
             document.getElementById("<%=cTextBox.ClientID%>").value ="";
             document.getElementById("<%=dTextBox.ClientID%>").value ="";
             document.getElementById("<%=eTextBox.ClientID%>").value ="";
             document.getElementById("<%=fTextBox.ClientID%>").value ="";
             document.getElementById("<%=gTextBox.ClientID%>").value ="";
             document.getElementById("<%=hTextBox.ClientID%>").value ="";
             document.getElementById("<%=iTextBox.ClientID%>").value ="";
             document.getElementById("<%=jTextBox.ClientID%>").value ="";
              document.getElementById("<%=kTextBox.ClientID%>").value ="";
             document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";
             document.getElementById("<%=SignImage.ClientID%>").src ="";
             document.getElementById("<%=lTextBox.ClientID%>").value ="";
             
             
             alert("Invalid Registration Number");
             return false;
      
    }
     function fnCheqInput()
        {
        
        
              //Input Text Checking
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
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
            
           if(document.getElementById("<%=saleDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleDateTextBox.ClientID%>").focus();
                alert("Please Enter Sale Date");
                return false;
                
            }
            if(document.getElementById("<%=saleDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=saleDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=saleDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
            if(document.getElementById("<%=saleNumberTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleNumberTextBox.ClientID%>").focus();
                alert("Please Enter Sale Number");
                return false;
                
            }
            
          
            if(document.getElementById("<%=saleRateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleRateTextBox.ClientID%>").focus();
                alert("Please Enter Sale Rate");
                return false;
                
            }
            if(document.getElementById("<%=unitQtyTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=unitQtyTextBox.ClientID%>").focus();
                alert("Please Enter Unit Quantity");
                return false;
                
            }
          
            if(!document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               alert("Plese Add Certificate to The List");
                     return false;
            }
          
           //Data Grid Checking
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                    var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                    
                    var wightNo=0;
                    wightNo=parseInt(wightNo);
                    var dinotxt = "";
                    var weighttxt = "";
                   

                   for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].type =="text")
                        {
                            if(inputs[i].id.indexOf("weightTextBox") != -1)
                            {
                              
                               
                                wightNo=wightNo+parseInt(inputs[i].value)
                            }
                          }
                    }
                    var unitQty=document.getElementById("<%=unitQtyTextBox.ClientID%>").value
                    if(wightNo!=parseInt(unitQty))
                    {
                        document.getElementById("<%=unitQtyTextBox.ClientID%>").focus();
                        alert(" Save Failed:Unit Quantity and List Quantity is not Equal");
                        return false;
                    }
             }
        
        }
        
         function fnCheckRegNo()
            {
                if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Registration Number");
                    return false;
                    
                }
            }
        
        function fnCheqQty()// check Add to lsit validation
        {
            var SumQty=0;
            if(document.getElementById("<%=unitQtyTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=unitQtyTextBox.ClientID%>").focus();
                alert("Please Enter Unit Quantity");
                return false;
                
            }
            if(document.getElementById("<%=aTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=aTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=aTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination A");
                    return false;
                }
                SumQty=parseInt(SumQty)+(1*parseInt(document.getElementById("<%=aTextBox.ClientID%>").value));
            }
           if(document.getElementById("<%=bTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=bTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=bTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination B");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(5*parseInt(document.getElementById("<%=bTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=cTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=cTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=cTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination C");
                    return false;
                }
                SumQty=parseInt(SumQty)+(10*parseInt(document.getElementById("<%=cTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=dTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=dTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=dTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination D");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(20*parseInt(document.getElementById("<%=dTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=eTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=eTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=eTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination E");
                    return false;
                }
                SumQty=parseInt(SumQty)+(50*parseInt(document.getElementById("<%=eTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=fTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=fTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=fTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination F");
                    return false;
                }
                SumQty=parseInt(SumQty)+(100*parseInt(document.getElementById("<%=fTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=gTextBox.ClientID%>").value !="")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=gTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=gTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination G");
                    return false;
                }
                SumQty=parseInt(SumQty)+(250*parseInt(document.getElementById("<%=gTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=hTextBox.ClientID%>").value!="")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=hTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=hTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination H");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(500*parseInt(document.getElementById("<%=hTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=iTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=iTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=iTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination I");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(1000*parseInt(document.getElementById("<%=iTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=jTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=jTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=jTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination J");
                    return false;
                }
               SumQty=parseInt(SumQty)+(5000*parseInt(document.getElementById("<%=jTextBox.ClientID%>").value));
            }
           if(document.getElementById("<%=kTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=kTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=kTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination K");
                    return false;
                }
               SumQty=parseInt(SumQty)+(10000*parseInt(document.getElementById("<%=kTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=lTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=lTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=lTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination L");
                    return false;
                }
               SumQty=parseInt(SumQty)+(20000*parseInt(document.getElementById("<%=lTextBox.ClientID%>").value));
            }
        
        
         if(parseInt(SumQty)>0)
         {
            if(parseInt(SumQty)!=parseInt(document.getElementById("<%=unitQtyTextBox.ClientID%>").value))
            {
                alert("Unit Quantity and Dinomination Quantity is not Equal:"+parseInt(SumQty));
                return false;
            }
         }
        }
        function fnDinoReset()//Dinomination Reset
        {
             document.getElementById("<%=aTextBox.ClientID%>").value ="";
             document.getElementById("<%=bTextBox.ClientID%>").value ="";
             document.getElementById("<%=cTextBox.ClientID%>").value ="";
             document.getElementById("<%=dTextBox.ClientID%>").value ="";
             document.getElementById("<%=eTextBox.ClientID%>").value ="";
             document.getElementById("<%=fTextBox.ClientID%>").value ="";
             document.getElementById("<%=gTextBox.ClientID%>").value ="";
             document.getElementById("<%=hTextBox.ClientID%>").value ="";
             document.getElementById("<%=iTextBox.ClientID%>").value ="";
             document.getElementById("<%=jTextBox.ClientID%>").value ="";
             document.getElementById("<%=kTextBox.ClientID%>").value ="";
             document.getElementById("<%=lTextBox.ClientID%>").value ="";
             return false;
         }
         
       function  fnFindChek()//check Find Button validation
       {
         if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
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
            if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
       }
       
       
       
       function fnValidateWeight(dinoObj)
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var wightNo=0;
                var dinotxt = "";
                var weighttxt = "";
               
                
                for(var i=0;i<inputs.length;i++)
                {
                    if(inputs[i].type =="text")
                    {
                        if(inputs[i].id.indexOf("dinoTextBox") != -1)
                        {
                            dinotxt = inputs[i];
                            if(dinoObj.id==dinotxt.id)
                            { 
                             wightNo=i+2;
                            }
                            
                        }
                      }
                }
                if(dinoObj.value.indexOf("L")==0 ||dinoObj.value.indexOf("l")==0)
                  {
                      var weight=inputs[wightNo].value;
                        if(weight!=20000)
                        {
                           inputs[wightNo].focus();
                           alert("Invalid Certificate Weight");
                           return false;
                        }                   
                       
                  }  
               else if(dinoObj.value.indexOf("K")==0 ||dinoObj.value.indexOf("k")==0)
                  {
                   
                        var weight=inputs[wightNo].value;
                        if(weight!=10000)
                        {
                           inputs[wightNo].focus();
                           alert("Invalid Certificate Weight");
                           return false;
                        }
                     
                  }  
                else if (dinoObj.value.indexOf("J")==0 ||dinoObj.value.indexOf("j")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=5000)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
                else if(dinoObj.value.indexOf("i")==0 ||dinoObj.value.indexOf("I")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=1000)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("h")==0 ||dinoObj.value.indexOf("H")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=500)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
              else if(dinoObj.value.indexOf("g")==0 ||dinoObj.value.indexOf("G")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=250)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
              else if(dinoObj.value.indexOf("f")==0 ||dinoObj.value.indexOf("F")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=100)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("e")==0 ||dinoObj.value.indexOf("E")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=50)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("d")==0 ||dinoObj.value.indexOf("D")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=20)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("c")==0 ||dinoObj.value.indexOf("C")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=10)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("b")==0 ||dinoObj.value.indexOf("B")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=5)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("a")==0 ||dinoObj.value.indexOf("A")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=1)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
                else if(dinoObj.value.indexOf("Y")==0 ||dinoObj.value.indexOf("y")==0)// for checking CIP
                {
                
                    if( !(document.getElementById("<%=saleTypeDropDownList.ClientID%>").value=="CIP" && ( document.getElementById("<%=fundCodeTextBox.ClientID%>").value=="CFUF")||document.getElementById("<%=fundCodeTextBox.ClientID%>").value=="IAMCL"))
                    {
                     alert("Invalid Certificate Dinomination");
                        return false;
                    }
                  
                }
               else
                  {
                        alert("Invalid Certificate Dinomination");
                        return false;  
                   }
            }
            
       }
      
       function fnValidateDino(weightObj)
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var dinoNo=0;
                var dinotxt = "";
                var weighttxt = "";
                
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].type =="text")
                        {
                            if(inputs[i].id.indexOf("weightTextBox") != -1)
                            {
                                weighttxt = inputs[i];
                                if(weightObj.id==weighttxt.id)
                                { 
                                 dinoNo=i-2;
                                }
                                
                            }
                          }
                    }
                   if(weightObj.value==20000)
                    {
                 
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="L")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }                     
                    }
                   else if(weightObj.value==10000)
                    {
                 
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="K")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }                     
                    }
                    else if(weightObj.value==5000)
                    {
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="J")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==1000)
                    {
                       var dino=inputs[dinoNo].value;
                         if(dino.toUpperCase()!="I")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==500)
                    {
                        var dino=inputs[dinoNo].value;
                         if(dino.toUpperCase()!="H")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==250)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="G")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==100)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="F")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==50)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="E")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                  else if(weightObj.value==20)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="D")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                 else if(weightObj.value==10)
                    {
                        var dino=inputs[dinoNo].value;
                       if(dino.toUpperCase()!="C")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                   else if(weightObj.value==5)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="B")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                   else if(parseInt(weightObj.value)==1)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="A")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if((document.getElementById("<%=saleTypeDropDownList.ClientID%>").value=="CIP") &&  (document.getElementById("<%=fundCodeTextBox.ClientID%>").value=="CFUF"||document.getElementById("<%=fundCodeTextBox.ClientID%>").value=="IAMCL"))
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="Y")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else
                    {
                        alert("Invalid Weight");
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
      .style6
        {
            font-size: smaller;
            font-weight: bold;
            color: #009933;
        }
     .style19
     {
         font-size: large;
         font-weight: bold;
         color: #75C8FF;
     }
     .style20
     {
         color: #FF3300;
     }
     </style>



    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
              Unit Holder Sale Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      <br />
      <div id="dvContent" runat="server">
   <%--  <asp:UpdatePanel ID="HolderInfoUpdatePanel" runat="server">--%>
     <%-- <ContentTemplate>--%>
    <table width="1100" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="320"></colgroup>
    <colgroup width="160"></colgroup>
    <colgroup width="160"></colgroup>
    <tr>
        <td align="right"><b>Sale No:</b></td>
        <td align="left">
            <asp:TextBox ID="saleNumberTextBox" runat="server" 
                CssClass="textInputStyleDate" MaxLength="6" 
                meta:resourcekey="saleNumberTextBoxResource1" 
                onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
            <span class="star">*</span></td>
        <td class="style19" align="center">Denomination</td>
        <td colspan="2" align="center"><span class="style6"  
                           style="border:1px solid">Signature    and 
                       Photo</span></td>
    </tr>
     <tr>
        <td align="right">Money Receipt No:</td>
        <td align="left"><asp:TextBox ID="MoneyReceiptNoTextBox" runat="server" 
                    CssClass= "textInputStyleDate" 
                    meta:resourcekey="saleNumberTextBoxResource1" 
                    onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox></td>
        <td rowspan="13">
            <table align="center" cellpadding="0" cellspacing="0" border="1px">
            <colgroup width="80px"></colgroup>
            <tr>
                <td align="right">
                    <b>A:1x</b></td>
                <td align="left">
                    <asp:TextBox ID="aTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="aTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>B:5x</b></td>
                <td align="left">
                    <asp:TextBox ID="bTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="bTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>C:10x&nbsp;</b></td>
                <td align="left">
                    <asp:TextBox ID="cTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="cTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>D:20x</b></td>
                <td align="left">
                    <asp:TextBox ID="dTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="dTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>E:50x</b></td>
                <td align="left">
                    <asp:TextBox ID="eTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="eTextBoxResource2" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                     <b>F:100x</b></td>
                <td align="left">
                    <asp:TextBox ID="fTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="fTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>G:250x</b></td>
                <td align="left">
                    <asp:TextBox ID="gTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="gTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>H:500x</b></td>
                <td align="left">
                    <asp:TextBox ID="hTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="hTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                <b>I:1000x</b></td>
                <td align="left">
                    <asp:TextBox ID="iTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="iTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>J:5000x</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="jTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="jTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                <b>K:10000x</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="kTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td align="right">
                <b>L:20000x</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="lTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
           <%-- <tr>
                <td align="right">
                </td>
                <td align="left">
                </td>
            </tr>--%>
            </table>
        </td>
        <td colspan="2" rowspan="10" align="center">
            <asp:Image ID="SignImage" runat="server" Height="202px" 
                meta:resourcekey="SignImageResource1" Width="286px" />
         </td>
    </tr>
     <tr>
        <td align="right">Registration No:</td>
        <td align="left"> 
        
                    <asp:TextBox ID="fundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="branchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"   
                    CssClass= "TextInputStyleSmall" TabIndex="1" AutoPostBack="True" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" 
                        ontextchanged="regNoTextBox_TextChanged"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;<asp:Button 
                    ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                    onclick="findButton_Click" AccessKey="f" 
                    onclientclick="return fnFindChek();" TabIndex="2" 
                    meta:resourcekey="findButtonResource1" /></td>
    </tr>
     <tr>
       <td align="right">Payment Type:</td>
        <td align="left"><table>
            <tr>
                <td  align="center">
                     <asp:RadioButton ID="ChqRadioButton" runat="server" AutoPostBack="True" 
                    Checked="True" GroupName="payType" Text="CHQ" 
                    oncheckedchanged="ChqRadioButton_CheckedChanged" TabIndex="2" />
                </td>
                 <td  align="center">
                 <asp:DropDownList ID="ChequeTypeDropDownList" runat="server" 
                    CssClass="DropDownList" 
                    meta:resourcekey="saleTypeDropDownListResource1" Height="27px">
                <asp:ListItem Value="CHQ" Selected="True">CHQ</asp:ListItem>
                <asp:ListItem Value="DD">DD</asp:ListItem>
                <asp:ListItem Value="PO">PO</asp:ListItem>
                </asp:DropDownList>
                </td>
                 <td  align="center">
                   <asp:RadioButton ID="CashRadioButton" runat="server" AutoPostBack="True" 
                    GroupName="payType" Text="CASH" 
                    oncheckedchanged="CashRadioButton_CheckedChanged" TabIndex="3" />
                </td>
                 <td  align="center">
                  <asp:RadioButton ID="BothRadioButton" runat="server" AutoPostBack="True" 
                    GroupName="payType" Text="BOTH" 
                    oncheckedchanged="BothRadioButton_CheckedChanged" TabIndex="4" />
                </td>
                 <td  align="center">
                 <asp:RadioButton ID="MultiRadioButton" runat="server" Text="MULTIPLE" 
                    GroupName="payType" AutoPostBack="True" 
                    oncheckedchanged="MultiRadioButton_CheckedChanged" TabIndex="5" />
                </td>
            </tr>
            </table></td>
    </tr>
     <tr>
      <td align="right">Cheque/DD/PO No:</td>
        <td align="left">
           <table>
            <tr>
                <td class="style18">
                <asp:TextBox ID="CHQDDNoRemarksTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="6" Width="148px"></asp:TextBox>
                </td>
                <td>
                 Date:
                </td>
                   <td align="left" class="style11">
                <asp:TextBox ID="chequeDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="6" meta:resourcekey="saleDateTextBoxResource1"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="chequeDateTextBox" PopupButtonID="chequeDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
                <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                <span class="star">* </span></td>
            </tr>
            </table>
                                </td>
     </tr>
     <tr>
        <td align="right">Bank Name:</td>
        <td align="left">
            <asp:DropDownList ID="bankNameDropDownList" runat="server" AutoPostBack="True" 
                meta:resourcekey="bankNameDropDownListResource1" 
                onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" TabIndex="7">
            </asp:DropDownList>
                                </td>
    </tr>
     <tr>
      <td align="right">Branch Name:</td>
       <td align="left">
           <asp:DropDownList ID="branchNameDropDownList" runat="server" 
               meta:resourcekey="branchNameDropDownListResource1" TabIndex="8">
           </asp:DropDownList>
                                </td>
    </tr>
     <tr>
        <td align="right">Cash Amount:</td>
       <td align="left">
           <asp:TextBox ID="CashAmountTextBox" runat="server" 
               CssClass="TextInputStyleLarge" MaxLength="55" 
               meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="9"></asp:TextBox>
                                </td>
    </tr>
     <tr>
        <td align="right">Multiple Payment:</td>
       <td align="left">
           <asp:TextBox ID="MultiplePayTypeTextBox" runat="server" 
               CssClass="TextInputStyleLarge" MaxLength="256" 
               meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="10" Width="280px"></asp:TextBox>
                                </td>
     </tr>
   <tr>
        <td align="right">Sale Date:</td>
       <td align="left">
           <asp:TextBox ID="saleDateTextBox" runat="server" CssClass="textInputStyleDate" 
               meta:resourcekey="saleDateTextBoxResource1" TabIndex="11"></asp:TextBox>
           <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
               Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
               TargetControlID="saleDateTextBox" />
           <asp:ImageButton ID="RegDateImageButton" runat="server" 
               AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
               meta:resourcekey="RegDateImageButtonResource1" TabIndex="10" />
           <span class="star">* </span></td>
    </tr>
     <tr>
       <td align="right">Sale Type:</td>
       <td align="left">
           <asp:DropDownList ID="saleTypeDropDownList" runat="server" 
               CssClass="DropDownList" meta:resourcekey="saleTypeDropDownListResource1" 
               TabIndex="12">
               <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Value="SL">SALE</asp:ListItem>
               <asp:ListItem meta:resourcekey="ListItemResource2" Value="CIP">CIP</asp:ListItem>
           </asp:DropDownList>
           <span class="star">*</span></td>
    </tr>
      <tr>
        <td align="right">Sale Rate:</td>
        <td style="text-align: left">
            <asp:TextBox ID="saleRateTextBox" runat="server" CssClass="textInputStyleDate" 
                MaxLength="6" meta:resourcekey="saleRateTextBoxResource1" TabIndex="13"></asp:TextBox>
            <span class="star">*</span></td>
        <td style="text-align: right"><span class="style20"><b style="text-align: right">CIP 
            :&nbsp; </b></span></td>
        <td style="text-align: left; color:Red" id="tdCIP" runat="server"></td>
    </tr>
     <tr>
        <td align="right">Unit Quantity:</td>
        <td style="text-align: left"><asp:TextBox ID="unitQtyTextBox" runat="server"  MaxLength="10"
                    CssClass= "textInputStyleDate" TabIndex="14" 
                    meta:resourcekey="unitQtyTextBoxResource1" 
                    onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*</span></td>
        <td align="right">Name of Holder :</td>
        <td align="left">                <asp:TextBox ID="holderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" 
                    meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td align="right">Remarks:</td>
        <td style="text-align: left">
            <asp:TextBox ID="saleRemarksTextBox" runat="server" 
                CssClass="TextInputStyleLarge" MaxLength="55" 
                meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="15" Width="278px"></asp:TextBox>
         </td>
        <td align="right" >Name of Joint Holder :</td>
        <td align="left">
                <asp:TextBox ID="jHolderTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td> 
                <asp:Button ID="DinoResetButton" runat="server" Text="Reset" 
                CssClass="buttonmid"  OnClientClick="return fnDinoReset();" AccessKey="r" 
                    meta:resourcekey="DinoResetButtonResource1" /></td>
         <td align="right" >Address1 :</td>
           <td align="left">
                <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td colspan="3" align="center">
                <asp:Button ID="addListButton" runat="server" 
                    Text="Add to List" CssClass="buttoncommon" onclick="addListButton_Click" 
                    OnClientClick="return fnCheqQty();" AccessKey="l" 
                    meta:resourcekey="addListButtonResource1" TabIndex="16"/></td>
        <td align="right" >Address2 :</td>
         <td align="left">
                <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
     </tr>
     <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
         <td align="right" >Telephone/Mobile :</td>
         <td align="left">
                <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td colspan="3" rowspan="10" align="center">
            <div style="text-align: center; display: block; overflow: auto; width:302; height:150px;">
                <asp:DataGrid ID="dinoGridView" runat="server" AutoGenerateColumns="False" 
                    meta:resourcekey="dinoGridViewResource1">
                    <SelectedItemStyle HorizontalAlign="Center" />
                    <ItemStyle CssClass="TableText" />
                    <HeaderStyle CssClass="TableHeader2" />
                    <AlternatingItemStyle CssClass="AlternatColor" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Denomination">
                            <ItemTemplate>
                                <asp:TextBox ID="dinoTextBox" runat="server" 
                                    meta:resourcekey="dinoTextBoxResource1" 
                                    onBlur="Javascript:fnValidateWeight(this);" 
                                    Text='<%# DataBinder.Eval(Container.DataItem,"dino") %>' Width="50px"></asp:TextBox>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Certificate">
                            <ItemTemplate>
                                <asp:TextBox ID="certNoTextBox" runat="server" 
                                    meta:resourcekey="certNoTextBoxResource1" 
                                    onkeypress="fncInputNumericValuesOnly()" 
                                    Text='<%# DataBinder.Eval(Container.DataItem,"cert_no") %>' Width="80px"></asp:TextBox>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Weight">
                            <ItemTemplate>
                                <asp:TextBox ID="weightTextBox" runat="server" 
                                    meta:resourcekey="weightTextBoxResource1" 
                                    onBlur="Javascript:fnValidateDino(this);" 
                                    onkeypress="fncInputNumericValuesOnly()" 
                                    Text='<%# DataBinder.Eval(Container.DataItem,"cert_weight") %>' Width="70px"></asp:TextBox>
                                &nbsp;
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                VerticalAlign="Middle" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <br />
            </div>
         </td>
        <td align="right" ></td>
                <td align="left" >
                    &nbsp;</td>
    </tr>
     <tr>
        <td></td>
        <td></td>
    </tr>
     <tr>
        <td></td>
        <td></td>
     </tr>
   <tr>
        <td></td>
        <td></td>
    </tr>
     <tr>
        <td></td>
        <td></td>
    </tr>
      <tr>
        <td></td>
        <td></td>
    </tr>
      <tr>
        <td></td>
        <td></td>
    </tr>
      <tr>
        <td></td>
        <td></td>
    </tr>
      <tr>
        <td></td>
        <td></td>
    </tr>
      <tr>
        <td></td>
        <td></td>
    </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
   
    </table>
   <%-- </ContentTemplate>--%>
       <%-- <Triggers>       
            <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="regNoTextBox" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="addListButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>--%>
     </div>
     <table width="1100" align="center" cellpadding="0" cellspacing="0" border="0" >
      <tr>
        <td colspan="5" align="center">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="SaveButton_Click" AccessKey="s" 
                meta:resourcekey="SaveButtonResource1"/>&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="a" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        </td>
    </tr>
     </table>
    <br />
  </asp:Content> 

<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="ShellCompanies.aspx.cs" Inherits="IPAdministrator_ShellCompanies" Title="Investment Proposal" %>

<asp:Content id="Content1" ContentPlaceHolderid="ContentPlaceHolder1" Runat="Server">
    <div style="margin-left:2em">
    <table style="width: 98%">
        <tr>
        <td class="cHeadTile">
            <asp:Label id="approvalLimitsLabel" runat="server" Text="EPG Group" 
                Font-Bold="True" ForeColor="White"></asp:Label></td>        
        </tr>
    </table>
    
    <br />
    
    <table style="width: 70%">
        <tr class="cHeadTile">
            <td style="width:30%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label id="Label1" runat="server" Text="Company Name:"></asp:Label>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="companyNameTextBox" ErrorMessage="Company name is required">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox id="companyNameTextBox" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label id="Label2" runat="server" Text="Country:"></asp:Label>
                <asp:CompareValidator id="CompareValidator1" runat="server" 
                    ControlToValidate="countriesDropDownList" ErrorMessage="Select Country" 
                    Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
            </td>
            <td>
                <asp:DropDownList id="countriesDropDownList" runat="server">
                 <asp:ListItem Value="-1">--Select Country--</asp:ListItem>
                 <asp:ListItem Value="1">Abkhazia</asp:ListItem>
                 <asp:ListItem Value="2">Afghanistan</asp:ListItem>
                 <asp:ListItem Value="3">Akrotiri and Dhekelia</asp:ListItem>
                 <asp:ListItem Value="4">Aland</asp:ListItem>
                 <asp:ListItem Value="5">Albania</asp:ListItem>
                 <asp:ListItem Value="6">Algeria</asp:ListItem>
                 <asp:ListItem Value="7">American Samoa</asp:ListItem>
                 <asp:ListItem Value="8">Andorra</asp:ListItem>
                 <asp:ListItem Value="9">Angola</asp:ListItem>
                 <asp:ListItem Value="10">Anguilla</asp:ListItem>
                 <asp:ListItem Value="11">Antigua</asp:ListItem>
                 <asp:ListItem Value="12">Argentina</asp:ListItem>
                 <asp:ListItem Value="13">Armenia</asp:ListItem>
                 <asp:ListItem Value="14">Aruba</asp:ListItem>
                 <asp:ListItem Value="15">Ascension Island</asp:ListItem>
                 <asp:ListItem Value="16">Australia</asp:ListItem>
                 <asp:ListItem Value="17">Austria</asp:ListItem>
                 <asp:ListItem Value="18">Azerbaijan</asp:ListItem>
                 <asp:ListItem Value="19">Bahamas</asp:ListItem>
                 <asp:ListItem Value="20">Bahrain</asp:ListItem>
                 <asp:ListItem Value="21">Bangladesh</asp:ListItem>
                 <asp:ListItem Value="22">Barbados</asp:ListItem>
                 <asp:ListItem Value="23">Belarus</asp:ListItem>
                 <asp:ListItem Value="24">Belgium</asp:ListItem>
                 <asp:ListItem Value="25">Belize</asp:ListItem>
                 <asp:ListItem Value="26">Benin Republic</asp:ListItem>
                 <asp:ListItem Value="27">Bermuda</asp:ListItem>
                 <asp:ListItem Value="28">Bhutan</asp:ListItem>
                 <asp:ListItem Value="29">Bolivia</asp:ListItem>
                 <asp:ListItem Value="30">Bosnia and Herzegovina</asp:ListItem>
                 <asp:ListItem Value="31">Botswana</asp:ListItem>
                 <asp:ListItem Value="32">Brazil</asp:ListItem>
                 <asp:ListItem Value="33">Brunei</asp:ListItem>
                 <asp:ListItem Value="34">Bulgaria</asp:ListItem>
                 <asp:ListItem Value="35">Burkina Faso</asp:ListItem>
                 <asp:ListItem Value="36">Burma</asp:ListItem>
                 <asp:ListItem Value="37">Burundi</asp:ListItem>
                 <asp:ListItem Value="38">Cambodia</asp:ListItem>
                 <asp:ListItem Value="39">Cameroon</asp:ListItem>
                 <asp:ListItem Value="40">Canada</asp:ListItem>
                 <asp:ListItem Value="41">Cape Verde</asp:ListItem>
                 <asp:ListItem Value="42">Cayman Islands</asp:ListItem>
                 <asp:ListItem Value="43">Central African Republic</asp:ListItem>
                 <asp:ListItem Value="44">Chad</asp:ListItem>
                 <asp:ListItem Value="45">Chile</asp:ListItem>
                 <asp:ListItem Value="46">China</asp:ListItem>
                 <asp:ListItem Value="47">Christmas Island</asp:ListItem>
                 <asp:ListItem Value="48">Cocos (Keeling) Islands</asp:ListItem>
                 <asp:ListItem Value="49">Colombia</asp:ListItem>
                 <asp:ListItem Value="50">Comoros</asp:ListItem>
                 <asp:ListItem Value="51">Congo</asp:ListItem>
                 <asp:ListItem Value="52">Congo</asp:ListItem>
                 <asp:ListItem Value="53">Cook Islands</asp:ListItem>
                 <asp:ListItem Value="54">Costa Rica</asp:ListItem>
                 <asp:ListItem Value="55">Côte d'Ivoire</asp:ListItem>
                 <asp:ListItem Value="56">Croatia</asp:ListItem>
                 <asp:ListItem Value="57">Cuba</asp:ListItem>
                 <asp:ListItem Value="58">Cyprus</asp:ListItem>
                 <asp:ListItem Value="59">Czech Republic</asp:ListItem>
                 <asp:ListItem Value="60">Denmark</asp:ListItem>
                 <asp:ListItem Value="61">Djibouti</asp:ListItem>
                 <asp:ListItem Value="62">Dominica</asp:ListItem>
                 <asp:ListItem Value="63">Dominican Republic</asp:ListItem>
                 <asp:ListItem Value="64">East Timor</asp:ListItem>
                 <asp:ListItem Value="65">Ecuador</asp:ListItem>
                 <asp:ListItem Value="66">Egypt</asp:ListItem>
                 <asp:ListItem Value="67">El Salvador</asp:ListItem>
                 <asp:ListItem Value="68">Equatorial Guinea</asp:ListItem>
                 <asp:ListItem Value="69">Eritrea</asp:ListItem>
                 <asp:ListItem Value="70">Estonia</asp:ListItem>
                 <asp:ListItem Value="71">Ethiopia</asp:ListItem>
                 <asp:ListItem Value="72">Falkland Islands</asp:ListItem>
                 <asp:ListItem Value="73">Faroe Islands</asp:ListItem>
                 <asp:ListItem Value="74">Fiji</asp:ListItem>
                 <asp:ListItem Value="75">Finland</asp:ListItem>
                 <asp:ListItem Value="76">France</asp:ListItem>
                 <asp:ListItem Value="77">French Polynesia</asp:ListItem>
                 <asp:ListItem Value="78">Gabon</asp:ListItem>
                 <asp:ListItem Value="79">Gambia</asp:ListItem>
                 <asp:ListItem Value="80">Georgia</asp:ListItem>
                 <asp:ListItem Value="81">Germany</asp:ListItem>
                 <asp:ListItem Value="82">Ghana</asp:ListItem>
                 <asp:ListItem Value="83">Gibraltar</asp:ListItem>
                 <asp:ListItem Value="84">Greece</asp:ListItem>
                 <asp:ListItem Value="85">Greenland</asp:ListItem>
                 <asp:ListItem Value="86">Grenada</asp:ListItem>
                 <asp:ListItem Value="87">Guam</asp:ListItem>
                 <asp:ListItem Value="88">Guatemala</asp:ListItem>
                 <asp:ListItem Value="89">Guernsey</asp:ListItem>
                 <asp:ListItem Value="90">Guinea</asp:ListItem>
                 <asp:ListItem Value="91">Guinea-Bissau</asp:ListItem>
                 <asp:ListItem Value="92">Guyana</asp:ListItem>
                 <asp:ListItem Value="93">Haiti</asp:ListItem>
                 <asp:ListItem Value="94">Honduras</asp:ListItem>
                 <asp:ListItem Value="95">Hong Kong</asp:ListItem>
                 <asp:ListItem Value="96">Hungary</asp:ListItem>
                 <asp:ListItem Value="97">Iceland</asp:ListItem>
                 <asp:ListItem Value="98">India</asp:ListItem> 
                 <asp:ListItem Value="99">Indonesia</asp:ListItem>
                 <asp:ListItem Value="100">Iran</asp:ListItem>
                 <asp:ListItem Value="101">Iraq</asp:ListItem>
                 <asp:ListItem Value="102">Ireland</asp:ListItem>
                 <asp:ListItem Value="103">Isle of Many</asp:ListItem>
                 <asp:ListItem Value="104">Israel</asp:ListItem>
                 <asp:ListItem Value="105">Italy</asp:ListItem>
                 <asp:ListItem Value="106">Ivory Coast</asp:ListItem>
                 <asp:ListItem Value="107">Jamaica</asp:ListItem> 
                 <asp:ListItem Value="108">Japan</asp:ListItem>
                 <asp:ListItem Value="109">Jersey</asp:ListItem>
                 <asp:ListItem Value="110">Jordan</asp:ListItem>
                 <asp:ListItem Value="111">Kazakhstan</asp:ListItem>
                 <asp:ListItem Value="112">Kenya</asp:ListItem>
                 <asp:ListItem Value="113">Kiribati</asp:ListItem>
                 <asp:ListItem Value="114">Kosovo</asp:ListItem>
                 <asp:ListItem Value="115">Kuwait</asp:ListItem>
                 <asp:ListItem Value="116">Kyrgyzstan</asp:ListItem>
                 <asp:ListItem Value="117">Laos</asp:ListItem>
                 <asp:ListItem Value="118">Latvia</asp:ListItem>
                 <asp:ListItem Value="119">Lebanon</asp:ListItem>
                 <asp:ListItem Value="120">Lesotho</asp:ListItem>
                 <asp:ListItem Value="121">Liberia</asp:ListItem>
                 <asp:ListItem Value="122">Libya</asp:ListItem>
                 <asp:ListItem Value="123">Liechtenstein</asp:ListItem>
                 <asp:ListItem Value="124">Lithuania</asp:ListItem>
                 <asp:ListItem Value="125">Luxembourg</asp:ListItem>
                 <asp:ListItem Value="126">Macao</asp:ListItem>
                 <asp:ListItem Value="127">Macedonia</asp:ListItem>
                 <asp:ListItem Value="128">Madagascar</asp:ListItem>
                 <asp:ListItem Value="129">Malawi</asp:ListItem>
                 <asp:ListItem Value="130">Malaysia</asp:ListItem> 
                 <asp:ListItem Value="131">Maldives</asp:ListItem>   
                 <asp:ListItem Value="132">Mali</asp:ListItem>   
                 <asp:ListItem Value="133">Malta</asp:ListItem>  
                 <asp:ListItem Value="134">Marshall Islands</asp:ListItem>   
                 <asp:ListItem Value="135">Mauritania</asp:ListItem>  
                 <asp:ListItem Value="136">Mauritius</asp:ListItem>   
                 <asp:ListItem Value="137">Mayotte</asp:ListItem>  
                 <asp:ListItem Value="138">Mexico</asp:ListItem>   
                 <asp:ListItem Value="139">Micronesia</asp:ListItem>  
                 <asp:ListItem Value="140">Moldova</asp:ListItem>  
                 <asp:ListItem Value="141">Monaco</asp:ListItem>   
                 <asp:ListItem Value="142">Mongolia</asp:ListItem> 
                 <asp:ListItem Value="143">Montenegro</asp:ListItem> 
                 <asp:ListItem Value="144">Montserrat</asp:ListItem>  
                 <asp:ListItem Value="145">Morocco</asp:ListItem>  
                 <asp:ListItem Value="146">Mozambique</asp:ListItem>   
                 <asp:ListItem Value="147">Myanmar</asp:ListItem>
                 <asp:ListItem Value="148">Nagorno</asp:ListItem> 
                 <asp:ListItem Value="149">Namibia</asp:ListItem>  
                 <asp:ListItem Value="150">Nauru</asp:ListItem>  
                 <asp:ListItem Value="151">Nepal</asp:ListItem>  
                 <asp:ListItem Value="152">Netherlands</asp:ListItem>  
                 <asp:ListItem Value="153">Netherlands Antilles</asp:ListItem>
                 <asp:ListItem Value="154">New Caledonia</asp:ListItem> 
                 <asp:ListItem Value="155">New Zealand</asp:ListItem> 
                 <asp:ListItem Value="156">Nicaragua</asp:ListItem>  
                 <asp:ListItem Value="157">Niger</asp:ListItem>  
                 <asp:ListItem Value="158">Nigeria</asp:ListItem> 
                 <asp:ListItem Value="159">Niue</asp:ListItem>
                 <asp:ListItem Value="160">Norfolk Island</asp:ListItem>  
                 <asp:ListItem Value="161">Northern Cyprus</asp:ListItem>  
                 <asp:ListItem Value="162">Northern Mariana Islands</asp:ListItem>  
                 <asp:ListItem Value="163">North Korea</asp:ListItem>
                 <asp:ListItem Value="164">Norway</asp:ListItem>
                 <asp:ListItem Value="165">Oman</asp:ListItem>  
                 <asp:ListItem Value="166">Pakistan</asp:ListItem>   
                 <asp:ListItem Value="167">Palau</asp:ListItem>    
                 <asp:ListItem Value="168">Palestine</asp:ListItem>  
                 <asp:ListItem Value="169">Panama</asp:ListItem>  
                 <asp:ListItem Value="170">Papua New Guinea</asp:ListItem>  
                 <asp:ListItem Value="171">Paraguay</asp:ListItem>  
                 <asp:ListItem Value="172">People's Republic of China</asp:ListItem> 
                 <asp:ListItem Value="173">Peru</asp:ListItem>  
                 <asp:ListItem Value="174">Philippines</asp:ListItem>  
                 <asp:ListItem Value="175">Pitcairn Islands</asp:ListItem> 
                 <asp:ListItem Value="176">Poland</asp:ListItem>  
                 <asp:ListItem Value="177">Portugal</asp:ListItem>  
                 <asp:ListItem Value="178">Pridnestrovie</asp:ListItem>
                 <asp:ListItem Value="179">Puerto Rico</asp:ListItem>  
                 <asp:ListItem Value="180">Qatar</asp:ListItem>
                 <asp:ListItem Value="181">Romania</asp:ListItem> 
                 <asp:ListItem Value="182">Russia</asp:ListItem>  
                 <asp:ListItem Value="183">Rwanda</asp:ListItem>  
                 <asp:ListItem Value="184">Saint Barthélemy</asp:ListItem>  
                 <asp:ListItem Value="185">Saint Helena</asp:ListItem>  
                 <asp:ListItem Value="186">Saint Kitts and Nevis</asp:ListItem>  
                 <asp:ListItem Value="187">Saint Lucia</asp:ListItem> 
                 <asp:ListItem Value="188">Saint Martin</asp:ListItem>  
                 <asp:ListItem Value="189">Saint Pierre and Miquelon</asp:ListItem>  
                 <asp:ListItem Value="190">Saint Vincent and the Grenadines</asp:ListItem> 
                 <asp:ListItem Value="191">Samoa</asp:ListItem>  
                 <asp:ListItem Value="192">San Marino</asp:ListItem>  
                 <asp:ListItem Value="193">São Tomé and Príncipe</asp:ListItem>  
                 <asp:ListItem Value="194">Saudi Arabia</asp:ListItem>  
                 <asp:ListItem Value="195">Senegal</asp:ListItem>  
                 <asp:ListItem Value="196">Serbia</asp:ListItem>   
                 <asp:ListItem Value="197">Seychelles</asp:ListItem>  
                 <asp:ListItem Value="198">Sierra Leone</asp:ListItem>  
                 <asp:ListItem Value="199">Singapore</asp:ListItem>  
                 <asp:ListItem Value="200">Slovakia</asp:ListItem>
                 <asp:ListItem Value="201">Slovenia</asp:ListItem>  
                 <asp:ListItem Value="202">Solomon Islands</asp:ListItem> 
                 <asp:ListItem Value="203">Somalia</asp:ListItem>
                 <asp:ListItem Value="204">Somaliland</asp:ListItem> 
                 <asp:ListItem Value="205">South Africa</asp:ListItem>  
                 <asp:ListItem Value="206">South Korea</asp:ListItem>
                 <asp:ListItem Value="207">South Ossetia</asp:ListItem>  
                 <asp:ListItem Value="208">Spain</asp:ListItem>  
                 <asp:ListItem Value="209">Sri Lanka</asp:ListItem>  
                 <asp:ListItem Value="210">Sudan</asp:ListItem>  
                 <asp:ListItem Value="211">Suriname</asp:ListItem>  
                 <asp:ListItem Value="212">Svalbard</asp:ListItem>
                 <asp:ListItem Value="213">Swaziland</asp:ListItem>  
                 <asp:ListItem Value="214">Sweden</asp:ListItem>  
                 <asp:ListItem Value="215">Switzerland</asp:ListItem>  
                 <asp:ListItem Value="216">Syria</asp:ListItem>  
                 <asp:ListItem Value="217">Taiwan</asp:ListItem>  
                 <asp:ListItem Value="218">Tajikistan</asp:ListItem>  
                 <asp:ListItem Value="219">Tanzania</asp:ListItem>  
                 <asp:ListItem Value="220">Thailand</asp:ListItem> 
                 <asp:ListItem Value="221">Timor-Leste</asp:ListItem>
                 <asp:ListItem Value="222">Togo</asp:ListItem>  
                 <asp:ListItem Value="223">Tokelau</asp:ListItem>
                 <asp:ListItem Value="224">Tonga</asp:ListItem>  
                 <asp:ListItem Value="225">Transnistria</asp:ListItem>   
                 <asp:ListItem Value="226">Trinidad and Tobago</asp:ListItem>  
                 <asp:ListItem Value="227">Tristan da Cunha</asp:ListItem>
                 <asp:ListItem Value="228">Tunisia</asp:ListItem>   
                 <asp:ListItem Value="229">Turkey</asp:ListItem>  
                 <asp:ListItem Value="230">Turkmenistan</asp:ListItem> 
                 <asp:ListItem Value="231">Turks and Caicos Islands</asp:ListItem>
                 <asp:ListItem Value="232">Tuvalu</asp:ListItem> 
                 <asp:ListItem Value="233">Uganda</asp:ListItem>  
                 <asp:ListItem Value="234">Ukraine</asp:ListItem> 
                 <asp:ListItem Value="235">United Arab Emirates</asp:ListItem> 
                 <asp:ListItem Value="236">United Kingdom</asp:ListItem>  
                 <asp:ListItem Value="237">United States</asp:ListItem>  
                 <asp:ListItem Value="238">Uruguay</asp:ListItem> 
                 <asp:ListItem Value="239">Uzbekistan</asp:ListItem>  
                 <asp:ListItem Value="240">Vanuatu</asp:ListItem>  
                 <asp:ListItem Value="241">Vatican City</asp:ListItem>  
                 <asp:ListItem Value="242">Venezuela</asp:ListItem>  
                 <asp:ListItem Value="243">Vietnam</asp:ListItem>  
                 <asp:ListItem Value="244">Virgin Islands, British</asp:ListItem>  
                 <asp:ListItem Value="245">Virgin Islands, United States</asp:ListItem>  
                 <asp:ListItem Value="246">Wallis and Futuna</asp:ListItem>  
                 <asp:ListItem Value="247">Western Sahara</asp:ListItem>  
                 <asp:ListItem Value="248">Yemen</asp:ListItem>  
                 <asp:ListItem Value="249">Zaire</asp:ListItem>
                 <asp:ListItem Value="250">Zambia</asp:ListItem>  
                 <asp:ListItem Value="251">Zimbabwe</asp:ListItem>    
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button id="submitButton" runat="server" Text="Submit" 
                    onclick="submitButton_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
        <asp:GridView id="shellCompaniesGridView" runat="server" Width="50%">
        </asp:GridView>
    <br />
    <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    
    </div>
</asp:Content>


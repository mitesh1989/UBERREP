<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAccount.ascx.cs" Inherits="UBERREP.Controls.MyAccount" %>
<div class="portlet-title">
                            <h4>
                                <i class="icon-reorder"></i>Create Account</span>
                            </h4>
                            <div class="tools hidden-phone">
                                <a href="javascript:;" class="collapse"></a>
                                <a href="#portlet-config" data-toggle="modal" class="config"></a>
                                <a href="javascript:;" class="reload"></a>
                                <a href="javascript:;" class="remove"></a>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            
                                <div class="form-wizard">
                                    <div class="navbar steps">
                                        <div class="navbar-inner">
                                            <ul class="row-fluid">
                                                <li class="span3">
                                                    <a href="#tab1" data-toggle="tab" class="step active">
                                                        <span class="number">1</span>
                                                        <span class="desc"><i class="icon-ok"></i>Account Setup</span>
                                                    </a>
                                                </li>
                                                <li class="span3">
                                                    <a href="#tab2" data-toggle="tab" class="step">
                                                        <span class="number">2</span>
                                                        <span class="desc"><i class="icon-ok"></i>Profile Setup</span>
                                                    </a>
                                                </li>
                                                <li class="span3">
                                                    <a href="#tab3" data-toggle="tab" class="step">
                                                        <span class="number">3</span>
                                                        <span class="desc"><i class="icon-ok"></i>Billing Setup</span>
                                                    </a>
                                                </li>
                                                <li class="span3">
                                                    <a href="#tab4" data-toggle="tab" class="step">
                                                        <span class="number">4</span>
                                                        <span class="desc"><i class="icon-ok"></i>Confirm</span>
                                                    </a>
                                                </li>                                                
                                            </ul>
                                        </div>
                                    </div>
                                    <div id="bar" class="progress progress-success progress-striped">
                                        <div class="bar"></div>
                                    </div>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab1">
                                            <h3 class="block">Provide your account details</h3>
                                            <div class="control-group">
                                                <label class="control-label">Username</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTUsername" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Provide your username</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Password</label>
                                                <div class="controls">
                                                    <asp:TextBox  ID="TXTPassword" runat="server" class="span6 m-wrap"></asp:TextBox>
                                                    <span class="help-inline">Provide your username</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Confirm Password</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTConfirmPassword" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Confirm your password</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab2">
                                            <h3 class="block">Provide your profile details</h3>
                                            <div class="control-group">
                                                <label class="control-label">Fullname</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTName" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Provide your fullname</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Email</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTEmail" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Provide your email address</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Phone Number</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTPhone" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Provide your phone number</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Gender</label>
                                                <div class="controls">
                                                    <label class="radio">
                                                        <asp:RadioButton runat="server" type="radio" name="optionsRadios" GroupName="Gender" ID="RDOMale" value="option1" Checked="true" />
                                                        Male
                                                    </label>
                                                    <div class="clearfix"></div>
                                                    <label class="radio">
                                                        <asp:RadioButton runat="server" type="radio" name="optionsRadios" ID="RDOFemale" GroupName="Gender" value="option2" />
                                                        Female
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Address</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTAddress" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Provide your street address</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">City/Town</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTCity" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline">Provide your city or town</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Remarks</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTRemarks" runat="server" TextMode="MultiLine" class="span6 m-wrap" Rows="3" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab3">
                                            <h3 class="block">Provide your billing and credit card details</h3>
                                            <div class="control-group">
                                                <label class="control-label">Card Holder Name</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTHolderName" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline"></span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Bank Name</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTBankName" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline"></span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Debit/Credit Card Number</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTCCNumber" runat="server" class="span6 m-wrap" />
                                                    <span class="help-inline"></span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">CVC</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTCVC" runat="server" class="m-wrap" />
                                                    <span class="help-inline"></span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Expiration Date(MM/YYYY)</label>
                                                <div class="controls">
                                                    <asp:TextBox ID="TXTMonth" runat="server" placeholder="MM" class="m-wrap small" />
                                                    <asp:TextBox ID="TXTYear" runat="server" placeholder="YYYY" class="m-wrap small" />
                                                    <span class="help-inline"></span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Payment Options</label>
                                                <div class="controls">
                                                    <label class="checkbox line">
                                                        <asp:CheckBox ID="CHKAutoPay" runat="server" type="checkbox" value="" />
                                                        Auto-Pay with this Credit Card
                                                    </label>
                                                    <label class="checkbox line">
                                                        <asp:CheckBox ID="CHKEmail" runat="server" type="checkbox" value="" />
                                                        Email me monthly billing
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab4">
                                            <h3 class="block">Confirm your account</h3>
                                            <div class="control-group">
                                                <label class="control-label">Fullname:</label>
                                                <div class="controls">
                                                    <span class="text">Bob Nilson</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Email:</label>
                                                <div class="controls">
                                                    <span class="text">bob@nilson.com</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Phone:</label>
                                                <div class="controls">
                                                    <span class="text">101234023223</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label">Credit Card Number:</label>
                                                <div class="controls">
                                                    <span class="text">*************1233</span>
                                                </div>
                                            </div>
                                            <div class="control-group">
                                                <label class="control-label"></label>
                                                <div class="controls">
                                                    <label class="checkbox">
                                                        <input type="checkbox" value="" />
                                                        I confirm my account
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions clearfix">
                                        <a href="#tab1"data-toggle="tab" class="btn button-previous">
                                            <i class="m-icon-swapleft"></i>Back 
                                        </a>
                                        
                                        <a href="#tab2"data-toggle="tab" class="btn black button-next">Continue <i class="m-icon-swapright m-icon-white"></i>
                                        </a>
                                        <i class="m-icon-swapright m-icon-white"></i>
                                        <asp:Button runat="server" ID="BTNSubmit" OnClick="BTNSubmit_Click" CssClass="btn red button-submit" Text="Submit" />
                                    </div>
                                    
                                </div>
                            
                        </div>

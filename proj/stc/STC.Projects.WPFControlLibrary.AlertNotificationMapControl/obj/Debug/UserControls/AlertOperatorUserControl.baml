   M S B A M L   `   `   `  ����u  qSTC.Projects.WPFControlLibrary.AlertNotificationMapControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Hclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl:STC.Projects.WPFControlLibrary.AlertNotificationMapControl  5      [ WSTC.Projects.ClassLibrary.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�cclr-namespace:STC.Projects.ClassLibrary.Common.Converters;assembly=STC.Projects.ClassLibrary.Common+STC.Projects.ClassLibrary.Common.Converters 5      �Rclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModelDSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel  5	      �Sclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.PropertiesESTC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties  5
      f    `STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.AlertOperatorUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      k gTelerik.Windows.Controls.Input, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p
 lTelerik.Windows.Controls.Navigation, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7GTelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
 5      \localHclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl    	 
 5      {	Convertercclr-namespace:STC.Projects.ClassLibrary.Common.Converters;assembly=STC.Projects.ClassLibrary.Common    	 
 5      cVMRclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel    	 
 5	      cpSclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties    	 
 5
      -   5          >�Name$  ucAlertOperator��5       J� RenderTransformOrigin$ 0.5,.5&�5      $��
45,670,0,0q�6-   z�5      T� 5   
    T� X$	 400Q�6    � 	Resources 5       �� MergedDictionaries �� Source   MyStoryboardOpenRight �� 
TargetName �� TargetProperty W� From		 W� To
 g� Duration g� 
SpeedRatio g� 	BeginTime  MyStoryboardCloseRight  hideAlertOpUC R� KeyTime  System.Windows.Visibility   Hidden R� Value  showAndHideAlertOpUC   Visible�� 5   
    5       �� 5!      $ ../Style/style.xaml?�6%   %�  &	        &	 4    &	 i    &	 U    �� 5$      .��5%      W� $ ucAlertOperator��6"   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6J   $	 400X�6�   $	 0X�6�   $
 0:0:4D�6�   $ 3Q�6�   W� 5'      $ Opacity�6"   $ ucAlertOperator��6F   $
 0:0:2D�6n   $ 0:0:0X�6   $ .3X�6�   $	 1X�6�   5(      �� 5*      .��5+      W� $ ucAlertOperator��6"   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6J   $ 0X�6�   $		 500X�6�   $
 0:0:1D�6�   $ 4Q�6�   W� 5,      $ Opacity�6"   $ ucAlertOperator��6F   $
 0:0:2D�6n   $ 0:0:0X�6   $	 0.9X�6�   $	 0X�6�   5-      �� 5/      .��50      W� $ ucAlertOperator��6"   $ Opacity�6J   $
 0:0:1D�6n   $		 0.0X�6   T� 51      $ 
Visibility�60   $ ucAlertOperator��6W   ..�52      i� $ 0:0:1��6-   # Z 6=   53      54      �� 56      .��57      W� $ ucAlertOperator��6"   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6J   $	 400X�6�   $	 0X�6�   $ 0:0:0X�6�   $
 0:0:3D�6�   $ 3Q�6�   T� 58      $ ucAlertOperator��60   $ 
Visibility�6X   ..�59      i� $ 0:0:0��6-   # Z 6=   5:      W� 5;      $ Opacity�6"   $ ucAlertOperator��6F   $
 0:0:2D�6n   $ 0:0:0X�6   $ .3X�6�   $	 1X�6�   W� 5=      $ Opacity�6"   $ ucAlertOperator��6F   $
 0:0:3D�6n   $ 0:0:10X�6   $	 0X�6�   W� 5A      $ ucAlertOperator��6"   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6J   $ 0X�6�   $		 400X�6�   $
 0:0:1D�6�   $ 0:0:12X�6�   $ 6Q�6�   T� 5C      $ ucAlertOperator��60   $ 
Visibility�6X   ..�5D      i� $ 0:0:13��6-   # Z 6>   5E      5F      5H      .��5P      � -   5         �Name$ gridAlertOperator��5P      $y�Visible=�6&   .+�5Q   
   �� $	��300��6   $	��140��6   ��5R      !� 5S       j� Stretch$
 Fill=�6    j� 
AlignmentX$
 Left=�5T       j� 
AlignmentY$	 Top=�6    !� Visual 5U      F� 5V      
����ح�
�������$��1��6>   O����        �     '� ' '�@Ys���р�� 'р�� 'р��    �        	$��5,5,5,5q�5X      5Z      ._�5]      �  � RowDefinitions 5^      �� 5_      �� 5`      �� 5a      �� 5b      �� 5c      �� 5e      $
��0.7* �6$    � ColumnDefinitions 5g      �� 5h      �� 5i      .+�5l      �� -   5         ��Name$  btnCloseAlertReportedAlert��5l      $��1��6=   $��Right=�6M   $��25��6i   $��20��6t    K� ZIndex$ 1��6�   ��5m      �� 5n      .R�5o      ��  ��Name$
 brdr��6%   
���   � J� Opacity$ .8Q�6F   ._�5p   "   ��  ��Name$ 	imgLogout��6(   $ ��../Images/close_normal.jpg��6;   $��25��6_   $��22��6j   $��Center=�6v   $��Center=�6�   $��Uniform=�6�   5q       �� Triggers 5r      P� 5s   "    P� Property �@J�IsMouseOver P� Value . . �5t   &   ��   �� Property  �@�Cursor! �� Value$
! Hand��6?   �� 5u   &   " �� 
TargetName$" 	imgLogout��6-     � ��$! ../Images/close_hover.jpg��6V   �� 5v   &   $
" brdr��6-     � ��
! ���� 6U   5w   #   5y      5|      �� 5~      U   OSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties.Resources#   strReportedDangerous#��Z# 6   
�������$��0��5       $��0��6-   $ �� TextAlignment$$ Center=�6=   $
��Left=�6T   $��Center=�6o   $
��Wrap=�5�       $
��10,2q�64   �� 5�      
�������$��0��5�       $��1��6-   $$ Center=�6=   $
��Left=�6T   $��Center=�6o   $
��Wrap=�5�       $
��10,2q�64   ��5�      �� *@>SupervisorNotificationDTO.DangerousViolatorDetails.PlateNumber+5�   4   �� 5�      %   strAlertStatus#��Z% 6   
�������$��01��5�       $��0��6.   $$ Center=�6>   $
��Left=�6U   $��Center=�6p   $
��Wrap=�5�       $
��10,2q�64   �� 5�      
�������$��01��5�       $��1��6.   $$ Center=�6>   $
��Left=�6U   $��Center=�6p   $
��Wrap=�5�       $
��10,2q�64   ��5�      �� *;9SupervisorNotificationDTO.DangerousViolatorDetails.Status+5�   4   �� 5�      &   strComments#��Z& 6   
�������$��2��5�       $��0��6-   $��2��6=   $$ Center=�6N   $
��Left=�6e   $��Center=�6�   $
��Wrap=�5�       $
��10,2q�64   �� 5�      
�������$��2��5�       $��1��6-   $��2��6=   $$ Center=�6N   $
��Left=�6e   $��Center=�6�   $
��Wrap=�5�       $
��10,2q�64   ��5�      �� *ECSupervisorNotificationDTO.DangerousViolatorDetails.NotificationText+5�   4   �� 5�      -   5        $ btnViewDetails��5�      '   strViewDetails#��Z' 61   
����ح�
�������  btnWhiteBGstyle#��[ 6�   $��13(�6�   $��4��5�       $��0��6-   $��90��6=   $	��2,2q�6H   6U   �� 5�      -   5        $ btnOk��5�      (   strOK#��Z( 6(   
����ح�
�������#��[ 6y   $��13(�6�   $��4��5�       $��1��6-   $��90��6=   $	��2,2q�6H   6U   5�      5�      5�      5�      
   M S B A M L   `   `   `  ����u  qSTC.Projects.WPFControlLibrary.AlertNotificationMapControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Hclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl:STC.Projects.WPFControlLibrary.AlertNotificationMapControl  5      �Oclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.HelperASTC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper  5      [ WSTC.Projects.ClassLibrary.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�cclr-namespace:STC.Projects.ClassLibrary.Common.Converters;assembly=STC.Projects.ClassLibrary.Common+STC.Projects.ClassLibrary.Common.Converters 5	      �Rclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModelDSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel  5
      �Sclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.PropertiesESTC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties  5      �Uclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControlsGSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls  5      u    oSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.NotifiedAlertViolatorDetailsUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      k gTelerik.Windows.Controls.Input, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p
 lTelerik.Windows.Controls.Navigation, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7GTelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
 5      \localHclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl    	 
 5      g	ConverterOclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper    	 
 5      �ConverterCommoncclr-namespace:STC.Projects.ClassLibrary.Common.Converters;assembly=STC.Projects.ClassLibrary.Common    	 
 5	      cVMRclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.ViewModel    	 
 5
      cpSclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties    	 
 5      fUCUclr-namespace:STC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls    	 
 5      -   5          >�Name$+  %NotifiedAlertViolatorDetailsUIControl��5       J� RenderTransformOrigin$ 0.5,.5&�5      z�5      T� 5   
    T� X$	 400Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 860X�6u   	 W� To$ 0X�6�    g� Duration$ 0:0:1D�6�   	 g� 	BeginTime$	 	0:0:0.001X�6�   
 g� 
SpeedRatio$
 3Q�6�   5      5      5      5       � 	Resources 5       �� MergedDictionaries �� Source   MyStoryboard �� 
TargetNameh   bSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.Helper.AlertStatusToVisibilityConverter $  AlertStatusToVisibilityConverterX  RSTC.Projects.ClassLibrary.Common.Converters.StringEnglishToArabicSelectorConverter * &StringEnglishToArabicSelectorConverter�� 5!   
    5"      �� 5#      $ ../Style/style.xaml?�6%   %  &	        &	 �     &	 �     �� 5&      .��5'      W� $+ %NotifiedAlertViolatorDetailsUIControl��6"   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6`   $ 0X�6�   $	 860X�6�   $ 0:0:1D�6�   $
 4Q�6�   5(        5*      69     5+      6E   5,      .��54      � -   5         �Name$! gridNotifiedViolatorDetails��54      $
��Auto=�55      $	��300��6?   
���   � J� Opacity$	 0.9Q�6a    � RowDefinitions 5=   
   �� 5>      $��5* �6   �� 5@      $��2* �6   .+�5T   
   �� -   5         ��Name$ btnClosePopups��5T      $��1��6)    � Cursor$
 Hand��69    K� ZIndex$ 2��6G   $��Right=�5U   !   $	��Top=�6=   $��20��6r   $��20��6}   $��10,1,5,0q�6�     IsMoseOvernone#��[ 6�   .��5V      � .+�5W      � -   5        $��backgroundGrid��5W      $��48��6-   $��48��68   $y�	Collapsed=�6D   F� 5X      ����p�� �e���"@� $� @]� Q,�,�e���"@��_Z㮻 ���! $� ��_Z�,�,�,�_Z� $� ��!��� �_Z�yh���"@�,�@]�  $� �yh���"@㮻 p $� D1y��@���gf"@D1y��@j9+���@O�a�ef"@j9+���@ $� �j9+���@��[���gf"@�Y� $� �Y�ӱ�[�Y��Y�"m]�Y� $� S�Y�QL� 33"@��[D1y��@ $� D1y��@� $�     �1kz     H��4����@ H� $� � H�kz1kz H� $�  H��<4����@ H�kz $� S    �4����@<4����@ $� 	 �� Stretch$ Uniform=�5Y   '   ���#00FECE$��17��6H   $��17��6S   $��0,0,0,0q�6_   $ 0.5,0.5&�6p   z�5Z      V� 5[      ~�5]   "   ��  �� Angle$ 0Q�62   �� 5^   "    �� ScaleX$ 1Q�61    �� ScaleY$ 1Q�6<   5`      5b      5c      5f      9� 5i   
   -   5         9�Name$% expanderNotifiedViolatorDetails��5i      $��0��6<     ExpanderStyle1#��[ 6I   $��0q�6�   $	��396��6�   $��Bottom=�6�    9� 
IsExpanded 6q   �� *ViolatorDetailsExpanded+��5j      �� 5k      U   OSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.Properties.Resources   strVehicleDetailsBig#��Z 6   $
��Left=�6O   $��14(�6j   ���#00ffcc$��20,0,0,0q�6�   .��5m      �� ���#00ffcc$��0 1 0 1q�6+   $	��2,2q�6E   ._�5n      �  5p      �� 5q      $��0 �6(   �� 5r      �� 5s      �� 5t      �� 5u      $��0 �6(   �� 5v      �� 5w      �� 5x      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      $��0 �6(   �� 5      �� 5�      �� 5�       � ColumnDefinitions 5�      �� 5�      $
��0.8* �6+   �� 5�      $	��100��6+   $	��200��6:   .+�5�      �� $��1��6       strPlateNum#��Z 6-   $
��10,5q�6W    �� TextAlignment$ Center=�6e   $
��Left=�6|   �� 5�      $��1��6!   $��1��6.   $ Center=�5�   !   $
��Left=�68   $
��10,2q�6S   �� 5�      $�:��6   6)    �� 5�      �6   �� **(VehicleDetailsResponse.PlateInfo.PlateNo+5�      �� 5�      $��2��6       strCode#��Z 6-   $��10,6,0,4q�6S   $ Center=�6e   $
��Left=�6|   �� 5�      $��2��6!   $��1��6.   $ Center=�6>   $
��Left=�6U   $
��10,2q�6p   �� 5�      $�:��6   6)    �� 5�   *   �6/   `� 5�   .     `� 	Converter#  [ 6;   .1�5�   "   �� ! �� Path$:! 4VehicleDetailsResponse.PlateInfo.PlateKindArabicDesc�6*   " �� FallbackValue$"  ��6f   �� 5�   "   $;! 5VehicleDetailsResponse.PlateInfo.PlateKindEnglishDesc�6*   $"  ��6g   �� 5�   "   # �� Mode$# OneWay=�6*   5�      5�   6   5�      �� 5�      $��3��6    $   	strSource#��Z$ 6-   $
��10,5q�6U   $ Center=�6c   $
��Left=�6z   �� 5�      $��3��6!   $��1��6.   $
��10,2q�5�   !   $ Center=�60   $
��Left=�6G   �� 5�      $�:��6   6)    �� 5�   *   �6/   `� 5�   .   #  [ 6;   .1�5�   "   �� $<! 6VehicleDetailsResponse.PlateInfo.PlateSourceArabicDesc�6*   $"  ��6h   �� 5�   "   $=! 7VehicleDetailsResponse.PlateInfo.PlateSourceEnglishDesc�6*   $"  ��6i   �� 5�   "   $# OneWay=�6*   5�      5�   6   5�      �� 5�      $��4��6     %   strViolationDetectionDate#��Z% 6-   $
��Wrap=�6e   $	��130��6y   $��	10,0,10,0q�6�   $
 Left=�6�   $
��Left=�6�   �� 5�      $��4��6!   $��1��6.   $
��Wrap=�6�   $ Center=�6�   $
��Left=�6�   $
��10,2q�6  ��6>   �� *,*SupervisorNotificationDTO.NotificationTime+  DateTimeToStringConverter& �� 	Converter#& [ ' �� ConverterParameter$' DateTime��6  �� 5�      $��5��6    (   strVehicleColor#��Z( 6-   $
��10,5q�6[   $ Center=�6i   $
��Left=�6�   �� 5�      $��5��6!   $��1��6.   $ Center=�6>   $
��Left=�6U   $
��10,2q�6p   �� 5�      $�:��6   6)    �� 5�   *   �6/   `� 5�   .   #  [ 6;   .1�5�   "   �� $,! &VehicleDetailsResponse.ColorArabicDesc�6*   $"  ��6X   �� 5�   "   $-! 'VehicleDetailsResponse.ColorEnglishDesc�6*   $"  ��6Y   �� 5�   "   $# OneWay=�6*   5�      5�   6   5�      �� 5�      $��6��6    )   strType#��Z) 6-   $
��10,5q�6S   $ Center=�6a   $
��Left=�6x   �� 5�      $��6��6!   $��1��6.   $ Center=�6>   $
��Left=�6U   $
��10,2q�6p   �� 5�      $�:��6   6)    �� 5�   *   �6/   `� 5�   .   #  [ 6;   .1�5�   "   �� $:! 4VehicleDetailsResponse.PlateInfo.PlateTypeArabicDesc�6*   $"  ��6f   �� 5�   "   $;! 5VehicleDetailsResponse.PlateInfo.PlateTypeEnglishDesc�6*   $"  ��6g   �� 5�   "   $# OneWay=�6*   5�      5�   6   5�      �� 5�      $��7��6    *   strModel#��Z* 6-   $
��10,5q�6T   $ Center=�6b   $
��Left=�6y   �� 5�      $��7��6!   $��1��6.   $ Center=�6>   $
��Left=�6U   $
��10,2q�6p   �� 5�      $�:��6   6)    �� 5�   *   �6/   `� 5�   .   #  [ 6;   .1�5�   "   �� $,! &VehicleDetailsResponse.ModelArabicDesc�6*   $"  ��6X   �� 5�   "   $-! 'VehicleDetailsResponse.ModelEnglishDesc�6*   $"  ��6Y   �� 5�   "   $# OneWay=�6*   5�      5�   6   5�      �� 5�      $��8��6    +   strManufacturer#��Z+ 6-   $
��10,5q�6[   $ Center=�6i   $
��Left=�6�   �� 5�      $��8��6!   $��1��6.   $ Center=�6>   $
��Left=�6U   $
��10,2q�6p   �� 5�      $�:��6   6)    �� 5�   *   �6/   `� 5�   .   #  [ 6;   .1�5�   "   �� $+! %VehicleDetailsResponse.MakeArabicDesc�6*   $"  ��6W   �� 5�   "   $,! &VehicleDetailsResponse.MakeEnglishDesc�6*   $"  ��6X   �� 5�   "   $# OneWay=�6*   5�      5�   6   5�      �� 5�      $��9��6    ,   strManufactureYear#��Z, 6-   $
��10,5q�6^   $ Center=�6l   $
��Left=�6�   �� 5�      $��9��6!   $��1��6.   $ Center=�6>   $
��Left=�6U   $
��10,2q�6p   �� 5�   "   $�:��6&   61    �� 5�      �6   �� *VehicleDetailsResponse.Year+5�      �� 5�      $��10��6    -   strLicenceExpiryDate#��Z- 6.   $
��10,5q�6a   $ Center=�6o   $
��Left=�6�   �� 5�      $��10��6!   $��1��6/   $ Center=�6?   $
��Left=�6V   $
��10,2q�6q   �� 5�   "   $�:��6&   61    �� 5�      �6   �� **(LicenseDetailsResponse.LicesenExpiryDate+5�      �� 5�      $��11��6    .   strDriverAge#��Z. 6.   $
��10,5q�6Y   $ Center=�6g   $
��Left=�6~   �� 5�      $��11��6!   $��1��6/   $ Center=�6?   $
��Left=�6V   $
��10,2q�6q   �� 5�   "   $�:��6&   61    �� 5�      �6   �� *	DriverAge+5�      �� 5     $��12��6    /   strNationality#��Z/ 6.   $
��10,5q�6[   $ Center=�6i   $
��Left=�6�   �� 5     $��12��6!   $��1��6/   $ Center=�6?   $
��Left=�6V   $
��10,2q�6q   �� 5     $�:��6   6)    �� 5  *   �6/   `� 5  .   #  [ 6;   .1�5  "   �� $2! ,VehicleDetailsResponse.NationalityArabicDesc�6*   $"  ��6^   �� 5  "   $3! -VehicleDetailsResponse.NationalityEnglishDesc�6*   $"  ��6_   �� 5	  "   $# OneWay=�6*   5
     5  6   5     �� 5     $��14��6    0   strLastKnownLocation#��Z0 6.   $
��10,5q�6a   $ Center=�6o   $
��Left=�6�   �� 5     $��14��6!   $��1��6/   $ Center=�6?   $
��Left=�6V   $
��10,2q�6q   �� 5  "   $�:��6&   61   5     �� 5     $��15��6    $��0��6.   1   strComments#��Z1 6>   $
��10,5q�6h   $ Center=�6w   $
��Left=�6�   �� 5     -   5        2 ��Name$2 txtBoxComment��5      $��15��67   $��1��6E   $��SemiBold"�6W   $
��Wrap=�6m   $	��200��6�   $
��10,2q�5     $
��Left=�6)   �� 5  "   $�:��6&   61    �� 5      �6   �� *ECSupervisorNotificationDTO.DangerousViolatorDetails.NotificationText+6u   5!     �� 5#     $��16��6    $��0��6.   3   strViewAttachment#��Z3 6>   $
��10,5q�6n   $ Center=�6}   $
��Left=�6�   �� 5$     $��16��6    $��1��6.   $��SemiBold"�6@   $
��Wrap=�6V   $	��200��6j   $
��10,2q�5%     $
��Left=�6(   �� 5&  "   $�:��6&   61   5'     �� 5(     -   5        $��16��5(     $��1��6*   $��20��6:   $��20��6E   $
 Hand��6Q   $
��Left=�5)     $
��20,0q�67   $#��../Images/icon_attachment.png��6E   {�5(  _   �� *IsAttachmentAvailable+5)  l   5,     5-     5.     9� 50  
   -   5        $&  expanderNotifiedViolatorHiostory��50     $��1��6=   #��[ 6J   $y�	Collapsed=�6�    6r   �� *ViolatorHisotoryExpanded+��51     �� 52     4   strViolationsHistory#��Z4 6   $
��Left=�6O   $��14(�6j   ���#ffd452$��20,0,0,0q�6�   .��54     �� ���#00ffcc$��.5q�6+   $	��2,2q�6@   ._�55     � $��0��6   $��0,10,0,0q�6$   $��80��66    56     �� 57     $
��Auto �6(   �� 58     $
��Auto �6(   .+�5=     �� $��1��6   $��30��6,   $	��150��6;   $��5q�6K   ���#00ffcc���#0A1114���#002b22$��	Helvetica)�6�   $��12(�6�   $
��Auto=�5>  #   $
��Auto=�6U   $��1q�6�   ��5=  �   �� *+)DangerousVehicleDetails.VehicleViolations+ 5?     5 �� 
TargetType6 �� Property7 �� Value8 �� 
TargetType9 �� Triggers: P� Property; P� Value��%�  '��       �� 5C     "5 ��6$   .�5D  "   �� 6 � ��7 �#0e171b6?   �� 5E  "   6 � ��7 5F  &   �� 5G  *   "8 ��6:   .R�5H  .   �� ���#0e171b$	��0.1q�6J   $	��5,2q�6`   $	 0.7Q�6m   ._�5I  2   �� 5J  /   5L  +   5N  #   9 5O  "   P� 5P  &   : �@J�IsMouseOver; . . �5Q  *   �� 6 �@�Cursor$7 Arrow��6C   �� 5R  *   6 �@J�Opacity$7 .9Q�6D   5S  '   5V     5W     < �� View< 5Y     �� 5Z     != �� ColumnHeaderContainerStyle= 5[  "   �� 5\  &   "5 ��6,   .�5]  *   �� 6 � ��7 5^  .   �� 5_  2   "8 ��6B   .R�5`  6   �� #��z��6@   > �� Padding$> 5q�6a   #��z��6m   $
 Left=�6�   $��WrapWithOverflow=�6�   $ Arrow��6�   5a  3   5c  +   �� 5e  *   6 � ��7 �#00ffcc6G   �� 5f  *   6 � ��$7 13(�6E   �� 5g  *   6 � ��$7 2q�6D   5h  '   .P�5l  "   �� ?   strViolationID#��Z? 61   @ �� Width$@ 60��6�   A �� DisplayMemberBindingA 6`   �� *	MessageId+6�   �� 5m  "   B   strViolationType#��ZB 61   $@ 60��6�   A 6b   �� *ViolationTypeName+6�   �� 5o  "   C   strViolationDateTime#��ZC 61   $@ 80��6f   D �� CellTemplateD 5p  &   �� 5q  *   .R�5r  .   �� E ��Name$E brderTxt��65   $��0q�6G   ._�5s  2   �� $
��Wrap=�6�   $ Arrow��6�   ��6<   �� *	DateTaken+#& [ $' DateTime��6�   5t  /   5u  +   5w  #   �� 5|  "   F   strImage#��ZF 61   $@ 45��6Z   D 5}  &   �� 5~  *   .R�5  .   �� $E 
brderImage��65   $��0q�6I   $��20��6]   
������ ._�5�  2   �� 
������ ��69   Z� ��6J   �� G �� RelativeSourceG �� H �� AncestorType"H >�I �� Mode$I FindAncestor=�$"! DataContext.ShowImageCommand���5�  5   �� ��5�  6   �� 5�  :   .R�5�  >   �� J �� Orientation$J 
Horizontal=�6I   .+�5�  B   �� $��20��6H   $!��../Images/Image_iconNew.jpg��6S   5�  ?   5�  ;   5�  3   5�  /   K �� TriggersK 5�  .   P� 5�  2   : �@J�IsMouseOver; . . �5�  6   �� 6 �@�Cursor$
7 Hand��6O   �� 5�  6   L �� 
TargetName$L 
brderImage��6=   6 � ��
7 ���� 6k   5�  3   5�  +   5�  #   �� 5�  "   M   strVideo#��ZM 61   $@ 40��6Z   D 5�  &   �� 5�  *   .R�5�  .   �� $E 
brderImage��65   $��0q�6I   $��20��6]   
������ ._�5�  2   �� 
������ ��69   Z� ��6J   �� G �� "H >�$I FindAncestor=�$"! DataContext.ShowVideoCommand���5�  5   �� ��5�  6   �� 5�  :   .R�5�  >   �� $J 
Horizontal=�6I   .+�5�  B   �� $��20��6H   $"��../Images/VideoI_iconNew.jpg��6S   5�  ?   5�  ;   5�  3   5�  /   K 5�  .   P� 5�  2   : �@J�IsMouseOver; . . �5�  6   �� 6 �@�Cursor$
7 Hand��6O   �� 5�  6   $L 
brderImage��6=   6 � ��
7 ���� 6k   5�  3   5�  +   5�  #   5�     5�     5�     5�     5�     9� 5�  
   -	   5        $( "expanderNotifiedViolatorActionArea��5�     $��1��6?   #��[ 6L    . ��5�     �� 5�     N   strSupervisorComments#��ZN 6   $
��Left=�6P   $��14(�6k   ���#00ffcc$��20,0,0,0q�6�   .��5�     �� -
   5        $E borderApproveRejectArea��5�     ���#00ffcc$��0q�6L   $	��2,2q�5�     $��1��6   y�6+   �� *" SupervisorNotificationDTO.Status+#& [ ._�5�     � $	��130��6    5�     �� 5�     $
��Auto �6(   �� 5�     �� 5�     �� 5�     �� 5�     $
��1.5* �6(    5�     �� 5�     �� 5�     .+�5�     �� $��0��6    $��0��6-   #��Z1 6=   $
��10,0q�6g   $ Center=�6v   $
��Left=�6�   �� 5�     -   5        O ��Name$O txtBoxComment2��5�     $��1��66   $��0��6C   $��3��6S   $��2��6d   P �� TextWrapping$
P Wrap=�6x   ���#0e2b2b$
��10,5q�5�  4   ��6   �� *
Comments+64   �� 5�     -   5        $��4��5�     $��Stretch=�6C   $
��10,0q�6a   $��25��6o   $��13(�6~   ��5�     �� 5�     Q   
strApprove#��ZQ 6(   5�     �� 5�     -   5        $ btnRejectReportedAlert��5�     $��4��6b   $��1��6o   $��13(�6   $��Stretch=�5�     $
��10,0q�67   $��25��6E   ��5�     �� 5�     R   	strReject#��ZR 6(   5�     5�     5�     5�     j   dSTC.Projects.WPFControlLibrary.AlertNotificationMapControl.UserControls.ImageMediaGalleryUserControl  5�  
   -   5        S  Name$S imgMediaGalleryUc��5�  *   $ 1Q�6E   $
��Auto=�6Q   $ 10��6�   $��1��6�   $��2��6�   $	��Top=�6�   $��-520,-300,-480,-200q�6�   5�     5�     5�     
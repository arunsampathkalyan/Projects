   M S B A M L   `   `   `  ����o  kSTC.Projects.WPFControlLibrary.VideoStreamingControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null��clr-namespace:STC.Projects.WPFControlLibrary.VideoStreamingControl;assembly=STC.Projects.WPFControlLibrary.VideoStreamingControl4STC.Projects.WPFControlLibrary.VideoStreamingControl  5      ` \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties 5      `   ZSTC.Projects.WPFControlLibrary.SOPBox.UserControls.PatrolCamerasListActionPanelUserControl   5      �VideoStreamingControl�clr-namespace:STC.Projects.WPFControlLibrary.VideoStreamingControl;assembly=STC.Projects.WPFControlLibrary.VideoStreamingControl  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      -   5          >�Name$%  PatrolsListActionPanelUIcontrol��5	       J� Opacity$
 0.95Q�5      z�5      T� 5   
    T� X$	 385Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 300X�6u   	 W� To$ 0X�6�    g� Duration$ 0:0:1D�6�   	 g� 	BeginTime$	 	0:0:0.001X�6�   
 g� 
SpeedRatio$
 3Q�6�   5      5      5      5       � 	Resources 5         MyStoryboard �� 
TargetName  IsMoseOvernone �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName  IsMoseOverGrid��%�  &	        &	 �     &	 �    �� 5   
   .��5      W� $% PatrolsListActionPanelUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6V   $ 0X�6�   $	 300X�6�   $ 0:0:1D�6�   $
 4Q�6�   5      �� 5    
   " ��6'   .�5!      ��  � ��
 ���� 6+   �� 5"       � ��
 ���� 6,   �� 5#       � ��$ Center=�6;   �� 5$       � ��$ Center=�62   �� 5%       � �� �#ffffff6+   �� 5&       �@�Cursor$
 Hand��6'   �� 5'       � �� 5(      �� 5)      " ��6&   .R�5*      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5+      �� #��z��6/   #��z��6k   5,       5-      P� 5.       �@J�IsMouseOver . . �5/   "   �� $ bg��6S    � ��
 ���� �� 50   "    � ��
 ���� 6?   �� 51   "    � ��
 ���� 6@   �� 52   "    �@J�Opacity$	 0.5Q�6<   6J   53      55      57      58      �� 5:   
   " ��6'   .�5;      ��  � ��$ 	Helvetica)�6+   �� 5<       � ��
 ���� 6+   �� 5=       � �� �#008C996,   �� 5>       � ��$ Center=�64   �� 5?       � ��$ Center=�62   �� 5@       � �� �#00ffcc6+   �� 5A       �@�Cursor$
 Hand��6'   �� 5B       � �� 5C      �� 5D      " ��6&   .R�5E      �� $ bg��6!   #��z��6-   $��1q�6W   ���#008C99._�5F      �� #��z��6/   #��z��6k   5G       5H      P� 5I       �@J�IsMouseOver . . �5J   "   �� $ bg��6O    � �� �#49b13c�� 5K   "    � �� �#00ffcc6?   �� 5L   "    � �� �#008C996@   5M      5O      5Q      5R      5S      .��5U      �� $	��300��6   $	��315��6   ��5V   
   �� 5W       �� ImageSource$N H/STC.Projects.WPFControlLibrary.SOPBox;component/images/big_box_blue.png��6    j� Stretch$
 Fill=�6p   ._�5Z   
   � $��15,10,15,10q�6   ��5\      �� 5]      $K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    � RowDefinitions 5`      �� 5a      $
��Auto �6    60   �� 5b      $
��Auto �6    �� 5c      $	��200 �6    �� 5d      $
��Auto �6    60   .+�5g      �� @  :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   strCCTVs#��Z 6   $��0��6?   ���#00ffcc$��0��6a   $��20(�6q   $��	Helvetica)�6   $
��Left=�6�   $	��Top=�6�   $��	15,15,0,0q�6�   $��25��6�   $	��200��6�   �� 5i      -   5         ��Name$ Btnback��5i      $��0��6&   $��5q�6N   $��20��6Y   $��20��6d   $��Right=�6p   $��Disabled=�6�   $	��Top=�6�   #���  6  ��6�   Z� ��6�   Z� .��5j      �� $��18��6   $��18��6#   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6/   $��LeftToRight=�6   5k      �� 5m      -   5         ��Name$ PatrolsList��5m      $��15,10,10,10q�60   $��0��6E   $��Visible=�5n      $��Visible=�6L   $��2��6    � Cursor$
 Hand��6�   ��5m   U   �� *PatrolsList+��5o      �� 5p      .R�5q      � $	��380��6%   $	��180��61    K� IsItemsHost . 5r      5s      ��5u      �� 5v      .R�5w      � 
������ $	��170��68   $��5q�6D    5x      �� 5y   "   $��27 �60   6>   �� 5z   "   $
��Auto �60   6@   �� 5{   "   62    � ColumnDefinitions 5~      �� 5   "   $
��Auto �63   6B   �� 5�   "   $
��Auto �63   6B   .+�5�      �� $��0��6(   $��16(�65   $��	Helvetica)�6C   ���#00ffcc$��	10,5,10,5q�6o   $��0��6�   ��6�   �� *PatrolPlateNo+6�   �� 5�      $��0��6(   $��16(�65   $��	Helvetica)�6C   ���#00ffcc$��	10,5,10,5q�6o   $��1��6�   ��6�   �� *
PatrolCode+6�   G   ASTC.Projects.WPFControlLibrary.VideoStreamingControl.UserControl1  5�      -   5        $��1��5�   A   $��0��6�   $��10,10,10,10q�6�   $	��170��6�   $	��150��6�      TextExposedInXaml 6�   �� *	CameraURL+5�      5�      5�      �� 5�      -   5        $��flt_canvas_MediaPlayer��5�   t   $	��300��6   $	��180��6"     K� ZIndex$	  500��6/   $y�Hidden=�6C   $��Center=�6W   $��10q�6�   $��2��6�   5�      �� 5�      -   5        !   
strConfirm#��Z! 5�      $��15(�6A   $��3��6P   $��0��6]   $��
15,3,100,0q�6�   $	��124��6�   $��30��6�   #��[ 6�   5�      5�      5�      
   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      Y    SSTC.Projects.WPFControlLibrary.SOPBox.UserControls.RadarsListActionPanelUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      e aTelerik.Windows.Controls, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.ImageEditor, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Chart, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
 5      -   5          >�Name$$  RadarsListActionPanelUIcontrol��5	       J� Opacity$
 0.95Q�5       J� RenderTransformOrigin$ 0.5,.5&�5	   6   z�5      T� 5   
    T� X$	 385Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 300X�6u   	 W� To$ 0X�6�   	 g� Duration$	 0:0:1D�6�   
 g� 	BeginTime$
 	0:0:0.001X�6�    g� 
SpeedRatio$ 3Q�6�   5      5      5      5       � 	Resources 5         MyStoryboard �� 
TargetName  IsMoseOvernone �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName  IsMoseOverGrid��%�  &	        &	 �     &	 �    �� 5   
   .��5      W� $$ RadarsListActionPanelUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6U   $ 0X�6�   $	 300X�6�   $	 0:0:1D�6�   $ 4Q�6�   5      �� 5    
   " ��6'   .�5!      ��  � ��
 ���� 6+   �� 5"       � ��
 ���� 6,   �� 5#       � ��$ Center=�6;   �� 5$       � ��$ Center=�62   �� 5%       � �� �#ffffff6+   �� 5&       �@�Cursor$
 Hand��6'   �� 5'       � �� 5(      �� 5)      " ��6&   .R�5*      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5+      �� #��z��6/   #��z��6k   5,       5-      P� 5.       �@J�IsMouseOver . . �5/   "   �� $ bg��6S    � ��
 ���� �� 50   "    � ��
 ���� 6?   �� 51   "    � ��
 ���� 6@   �� 52   "    �@J�Opacity$	 0.5Q�6<   6J   53      55      57      58      �� 5:   
   " ��6'   .�5;      ��  � ��$ 	Helvetica)�6+   �� 5<       � ��
 ���� 6+   �� 5=       � �� �#008c996,   �� 5>       � ��$ Center=�64   �� 5?       � ��$ Center=�62   �� 5@       � �� �#00ffcc6+   �� 5A       �@�Cursor$
 Hand��6'   �� 5B       � �� 5C      �� 5D      " ��6&   .R�5E      �� $ bg��6!   #��z��6-   $��1q�6W   ���#008c99._�5F      �� #��z��6/   #��z��6k   5G       5H      P� 5I       �@J�IsMouseOver . . �5J   "   �� $ bg��6O    � �� �#00ffcc�� 5K   "    � ��
 �   �6?   �� 5L   "    � �� �#008c996@   5M      5O      5Q      5R      5T      .��5V      � $	��260��6   $
��Auto��6   ��5X   
   �� 5Y       �� ImageSource$K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    � RowDefinitions 5\   
   �� 5]      $
��Auto �6   �� 5^       � ColumnDefinitions 5a   
   �� 5b      $
��Auto �6   6.   �� 5c      6!   .+�5f   
   �� @   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   	strAction#��Z 6   $��0��6<   ���#00ffcc$��0��6^   $��20(�6n   $��	Helvetica)�6|   $
��Left=�6�   $��2��6�   �� 5h   
   -   5         ��Name$ Btnback��5h      $��0��6"   $��1��6/   $��20��6Z   $��20��6e   $��Right=�6q   $��Disabled=�6�   #���  6�   ��6�   Z� ��6�   Z� .��5i      �� $��18��6   $��18��6   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6+   $��LeftToRight=�6{   5j      �� 5l   
   $��0��6   $��2��6!   $��2��66   ���#10302d$��1q�6Z    �� CornerRadius$ 2��6n   ._�5m      � $��10,10,10,10q�6    5n      �� 5o      $
��Auto �6$   �� 5p      $
��Auto �6$   �� 5q      $
��Auto �6$   �� 5r      $
��Auto �6$    5t      �� 5u      �� 5v      .+�5y      ��    strNewSpeedLimit#��Z 6   $��14(�6K   $��	Helvetica)�6Y   $��0��6p   ���#00ffcc$��0��6�   $��2��6�   $��
15,11,15,9q�6�   $��Center=�6�   $��22��6�   / 	 )Telerik.Windows.Controls.RadNumericUpDown  5z      -   5          Name$ 
SpeedLimit��5z   +   $��50��6?   $��16(�6J   $��0��6X   $��1��6�   $��	0,11,10,8q�6�      NumberDecimalDigits$ 0��6�   +  %Telerik.Windows.Controls.RadRangeBase    Minimum$  20Q�6�   !   Maximum$	! 120Q�6�   "   	IsInteger" . #   UpdateValueEvent$# PropertyChanged=�6�   $��Right=�6  $   SmallChange$$ 10Q�6,  %   Value% 6e   �� *NewSpeedValue+6,  �� 5|      -   5        &   	strChange#��Z& 5|      #���  6D   $��14(�6m   $��1��6{   $��0��6�   $��2��6�   $��3q�6�   $	��130��6�   $��30��6�   �� 5~      -   5        $��lblCurrentSpeed��5~      $��2��63   $��0��6@   $��2��6P   $��5q�6d   $��16(�6�   $��	Helvetica)�6�   ���#00ffcc$��Center=�6�   ' �� TextAlignment$' Center=�6�   $��30��6�   ��6o   �� *OldSpeedValue+6�   �� 5�      -   5        (   
strConfirm#��Z( 5�      $��14(�6E   $��3��6T   $��0��6a   $��20,10,10,10q�6�   $��80��6�   $��30��6�   #���  6�   �� 5�      -   5        )   strSkip#��Z) 5�      $��14(�6B   $��3��6Q   $��1��6^   $��	0,10,0,10q�6�   $��80��6�   $��30��6�   #���  6�   5�      5�      5�      5�      
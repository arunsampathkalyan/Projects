   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      N    HSTC.Projects.WPFControlLibrary.SOPBox.UserControls.RadarsListUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      e aTelerik.Windows.Controls, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.ImageEditor, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Chart, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
 5      Pp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     	 
 5        J� Opacity$
  0.95Q�5      $��0q�6.    � 	Resources 5
         RadarsListTemplate �� Orientation � ColumnDefinitions �� Padding��%�  &	        �� 5   
   .R�5      �� $ Vertical=�6   $��0q�60   ��6;   Z� .+�5      � 
������  5      �� 5      $
��Auto �6+   �� 5      $
��Auto �6+   �� 5      $��* �6+   .+�5      �� $��
ImgChecked��6)   $��0��6   $��15��6_   $��20��6j   $��0,0,7,0q�6v   $��0��6�   ��6;   �� *ImgCheckedSource+6�   �� 5      $��0��6    ���#00ffcc$��14(�6B   $��	Helvetica)�6P   $��0q�6�   $��1��6�   $
��Wrap=�6�   $ 5q�6�   ��6g   �� *
ItemName+6�   �� 5      $	��Img��6)   $��0��6   $)��#../images/icons/vitronic_radars.png��64   $��20��6a   $��25��6l   $��5,0,7,0q�6x   $��2��6�   5!      5"      5%      5'      .��5+      � $
��Auto��6   .+�5,   
   �� -   5        $��
RadarsList��5,      #��[  6H    �� SelectionMode$ Single=�5-      $��Stretch=�6Y   $��Disabled=�5.      $��Disabled=�6G   $	��Top=�5/      
������ $��0q�6B   ���#00ffcc��5,   %   �� *
RadarsList+��51      �� 52       �� 
TargetType" ��6    �� Triggers 53      P� 54       P� Property �@J�IsMouseOver	 P� Value	 . . �55      �� 
 �� Property
 � �� �� Value
 ���� 6;   �� 56      
 � ��$ 0q�6@   �� 57      
 �@�Cursor$
 Hand��67   58      5?      5A      5C      5E      
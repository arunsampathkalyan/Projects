   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      w@clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.UserControls2STC.Projects.WPFControlLibrary.SOPBox.UserControls  5      s>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters0STC.Projects.WPFControlLibrary.SOPBox.Converters  5      �Eclr-namespace:STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors7STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors  5	      �Iclr-namespace:STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel;STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel  5
      S    MSTC.Projects.WPFControlLibrary.SOPBox.UserControls.AssignedPatrolsUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      RSOPUC@clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.UserControls     5      U
converters>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters     5      ctemplateSelectorsEclr-namespace:STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors     5	      kuserControlsViewModelIclr-namespace:STC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel     5
      -   5          >�Name$  AssignedPatrolsUC��5   .    J� Opacity$
 0.95Q�6   z�5      T� 5   
    T� X$	 400Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 300X�6u   	 W� To$ 0X�6�    g� Duration$ 0:0:1D�6�   	 g� 	BeginTime$	 	0:0:0.001X�6�   
 g� 
SpeedRatio$
 3Q�6�   5      5      5      5       � 	Resources 5      L   FSTC.Projects.WPFControlLibrary.SOPBox.Converters.DoubleFormatConverter   DoubleFormatConverterP   JSTC.Projects.WPFControlLibrary.SOPBox.Converters.BoolToVisibilityConverter  BoolToVisibilityConverterR   LSTC.Projects.WPFControlLibrary.SOPBox.UserControlsViewModel.ViewModelLocator  Locator  MyStoryboard �� 
TargetName  IsMoseOvernone �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	      &	 &     &	 9     &	 �       5   
   6+     5   
   6/     5   
   61   �� 5    
   .��5!      W� $ AssignedPatrolsUC��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6H   $ 0X�6�   $	 500X�6�   $ 0:0:1D�6�   $
 4Q�6�   5"      �� 5%   
   " ��6'   .�5&      ��  � ��
 ���� 6+   �� 5'       � ��
 ���� 6,   �� 5(       � ��$ Center=�6;   �� 5)       � ��$ Center=�62   �� 5*       � �� �#ffffff6+   �� 5+       � �� 5,      �� 5-      " ��6&   .R�5.      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5/      �� #��z��6/   #��z��6k   50       51      P� 52       �@J�IsMouseOver . . �53   "   �� $ bg��6S    � ��
 ���� �� 54   "    � ��
 ���� 6?   �� 55   "    � ��
 ���� 6@   �� 56   "    �@J�Opacity$	 0.5Q�6<   6J   57      59      5;      5<      5>      .��5@      � -   5         �Name$ LayoutRootGrid��5@      $
��Auto��6#   .+�5A   
   � 
���   �$
 0.95Q�6"   $	��255��61   $	��280��6@    � RowDefinitions 5B      �� 5C      $��25 �6    �� 5D      $��5* �6    �� 5E      �� 5F      $	��200 �6    �� 5G      $	��200 �6    �� 5H      $	��200 �6    .+�5K      �� -   5         ��Name$ Btnback��5K      $��1��6&   $��Right=�66   $	��Top=�6R   $��20��6�   $��20��6�    K� ZIndex$ 6��6�   #���  6�   ��6�   Z� ��6�   Z� .��5L      �� $��18��6   $��18��6#   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6/   $��LeftToRight=�6   5M      �� 5O      @   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   strAssignedPatrol#��Z 6   $��0��6H   $��Open Sans - Regular)�6U   ���#00FECE$��0��6�   $��16(�6�   $��10,5,5,0q�6�   $
��Left=�6�   �� 5Q      -   5        $��PatrolsList��5Q      $��1��6W   ���#00FECE$��0 1 0 1q�6z    �� SelectionMode$ Single=�5R      $��Stretch=�6Z   $��Disabled=�5S      $��Disabled=�6G   $	��Top=�5T      
������ ���#00ffcc$��0,15,0,0q�6W   $	��200��6i   ��5Q   *   �� *AssignedPatrolsUI+��5U      �� 5V      .R�5W      ��  �� Orientation$ Vertical=�6%   $��5q�6<   ��6G   Z� .+�5Y      � 
������  5Z   "   �� 5[   &   �� 5\   &    � ColumnDefinitions 5^   "   �� 5_   &   $
��Auto �67   �� 5`   &   $
��Auto �67   �� 5a   &   $
��Auto �67   .+�5d   "   �� $ Vertical=�6-   $��0��6D   $��0��6R   .+�5f   &   �� ���#00ffcc$��14(�6E   $��	Helvetica)�6S   $��0q�6�   $
��Wrap=�6�    �� Padding$ 3q�6�   ��6j   �� *PatrolDtoObj.PatrolCode+6�   5h   #   �� 5i   "   $	��Img��65   $��0��6(   $*��$../images/icons/patrol_available.png��6@   $��20��6n   $��25��6y   $��5,0,5,0q�6�   $��2��6�   $��Right=�6�   �� 5m   "   $ 
brderImage��6)   $��1��6=   $��1��6J   $��0q�6Z   $��15��6n   
������ $	��5,5q�6�   y�6�   �� *IsImageAvailable+ �� 	Converter# [ ._�5n   &   �� 
������ ��6-   Z� ��6>   ��   �� RelativeSource  �� ! �� AncestorType"! >�" �� Mode$" FindAncestor=�# �� Path$"# DataContext.ShowImageCommand���5o   5   �� ��5p   *   �� 5q   .   .R�5r   2   �� $ 
Horizontal=�6=   .+�5s   6   �� $��20��6<   $U��O/STC.Projects.WPFControlLibrary.SOPBox;component/images/icons/Image_iconNew.jpg��6G   5t   3   5u   /   5x   '   5y   #   �� 5|   "   $ 
brderVideo��6)   $��1��6=   $��2��6J   $��0q�6Z   $��15��6n   
������ $ � Cursor$
$ Hand��6�   $	��5,5q�6�   y�6�   �� *IsVideoAvailable+# [ ._�5}   &   �� 
������ ��6-   Z� ��6>   ��   �� "! >�$" FindAncestor=�$"# DataContext.ShowVideoCommand���5~   5   �� ��5   *   �� 5�   .   .R�5�   2   �� $ 
Horizontal=�6=   .+�5�   6   �� $��20��6<   $V��P/STC.Projects.WPFControlLibrary.SOPBox;component/images/icons/VideoI_iconNew.jpg��6G   5�   3   5�   /   5�   '   5�   #   5�      5�      5�      ��5�      �� 5�      " ��6   % �� Triggers% 5�      P� 5�       �@J�IsMouseOver . . �5�   "   ��  � ��
 ���� 6?   �� 5�   "    � ��$ 0q�6D   �� 5�   "    �@�Cursor$
 Hand��6;   5�      5�      5�      5�      U   OSTC.Projects.WPFControlLibrary.SOPBox.UserControls.ImageMediaGalleryUserControl  5�   
   -   5        &  Name$& imgMediaGalleryUc��5�   -   $ 1Q�6H   $
��Auto=�6T   $ 10��6�   $��1��6�   $��2��6�   $	��Top=�6�   $��-520,-300,-480,-200q�6�   5�      5�      5�      
   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      �Eclr-namespace:STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors7STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors  5      s>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters0STC.Projects.WPFControlLibrary.SOPBox.Converters  5	      O    ISTC.Projects.WPFControlLibrary.SOPBox.UserControls.PatrolsListUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      Z VEsri.ArcGISRuntime, Version=10.2.7.0, Culture=neutral, PublicKeyToken=8fc3cc631e44ad866esri+http://schemas.esri.com/arcgis/runtime/2013  5      ]templateSelectorsEclr-namespace:STC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors  5      O
converters>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters  5	      -   5          >�Name$  PatrolsListUC��5
   A    J� Opacity$
 0.95Q�6   $��0q�6-    � 	Resources 5      L   FSTC.Projects.WPFControlLibrary.SOPBox.Converters.DoubleFormatConverter   DoubleFormatConverterP   JSTC.Projects.WPFControlLibrary.SOPBox.Converters.BoolToVisibilityConverter  BoolToVisibilityConverter  MyStoryboard �� 
TargetName �� TargetProperty W� From	 W� To g� Duration g� 
SpeedRatio  IsMoseOverGrid	 �� 
TargetType
 �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName  PatrolsListTemplate � Cursor � ColumnDefinitions �� Orientation ��Name �� Padding �� 	Converter@   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   EtaUnit �� Triggers �� Value �� Binding �� RelativeSource �� Mode �� AncestorType �� Path  PatrolsOfficerListTemplate  CrispListTemplate  �Name! K� ZIndex" �� CornerRadius# F�Name$ �� Stretch% J� RenderTransformOrigin& �� Angle' �� ScaleX( �� ScaleY) � Points  PatrolCommonListTemplate* ^� 
Conditions+ �� Binding, �� Value- �� PropertyY   SSTC.Projects.WPFControlLibrary.SOPBox.TemplateSelectors.PatrolsListTemplateSelector  PatrolsListTemplateSelector.   PatrolTempalte/   CrispTemplate0   PatrolOfficerTempalte 	 IsMoseOvernone��%+  &	        &	      &	 &     &	 �     &	 �    7   &	 �
    7   &	 �    7   &	     7   &	 �'    7  7  7  &		 �'      5   
   6+     5   
   6/   �� 5   
   .��5      W� $ PatrolsListUC��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6D   $ 0X�6�   $	 600X�6�   $ 0:0:1D�6�   $ 4Q�6�   5      �� 5   
   "	 ��6'   .�5      �� 
 � ��$ 	Helvetica)�6+   �� 5      
 � ��
 ���� 6+   �� 5      
 � �� �#008c996,   �� 5      
 � ��$ Center=�64   �� 5      
 � ��$ Center=�62   �� 5      
 � �� �#00FECE6+   �� 5      
 �@�Cursor$
 Hand��6'   �� 5      
 � �� 5      �� 5      " ��6&   .R�5       �� $ bg��6!   #��z��6-   $��1q�6W   ���#008c99._�5!      �� #��z��6/   #��z��6k   5"       5#      P� 5$       �@J�IsMouseOver . . �5%   "   �� $ bg��6O   
 � �� �#00FECE�� 5&   "   
 � ��
 �   �6?   �� 5'   "   
 � �� �#008c996@   5(      5*      5,      5-      �� 50   
   .R�51      �� $
 brdr��6   ���#00FECE$��1q�6:   $	��5,5q�6N   $	��110��6[   $
 Hand��6j   ._�52      �  53      �� 54      $
��Auto �6+   �� 55      $
��Auto �6+   .+�57      �� $	��img��6,   $��0��6   $��30��67   $$��../images/icons/patrol_car.png��6B   $��20��6k   $��3,2,3,2q�6w   �� 59      $ Vertical=�6!   $��0��68   $��1��6F   .+�5:      �� $ txtBlkPatrolCode��6$   ���#00FECE$��11(�6S   $��Open Sans - Regular)�6a   $��0q�6�   $
��Wrap=�6�   $ 1q�6�   ��6�   �� *
PatrolCode+6�   �� 5;      $ 	txtBlkEta��6$   ���#8B8F96$��11(�6L   $��Open Sans - Regular)�6Z   $��0q�6|   $
��Wrap=�6�   $ 1q�6�   �� 5<      $	�ETA��6"   6/    �� 5=      $�:��6"   6-    �� 5>      �6"   �� *	ETATime+8   6n    �� 5?      6%    �� 5@      #�Z 6"   6J   5A      5D      5G      5H       5I      P� 5J       �@J�IsMouseOver . . �5K      �� 
 �@�Cursor$
 Hand��6/   �� 5L      $
 brdr��6   
 � �� �#00FECE6E   �� 5M      $
 brdr��6   
 � ��
 ��� �6F   �� 5N      $	 img��6   
 � ��$* $../images/icons/patrol_car_hover.png��6@   �� 5O      $ txtBlkPatrolCode��6   
 � ��
 �   �6Q   �� 5P      $ 	txtBlkEta��6   
 � ��
 �   �6J   5Q      �� 5R      $
 True��6�    6   ��  �� $ FindAncestor=�" ��$ 
IsSelected�.a�5S      �� $
 brdr��6   
 � �� �#00FECE6E   �� 5T      $
 brdr��6   
 � ��
 ��� �6F   �� 5U      $	 img��6   
 � ��$* $../images/icons/patrol_car_hover.png��6@   �� 5V      $ txtBlkPatrolCode��6   
 � ��
 �   �6Q   �� 5W      $ 	txtBlkEta��6   
 � ��
 �   �6J   5X      5Z      �� 5\   
   .R�5]      �� $
 brdr��6   ���#00FECE$��1q�6:   $	��5,5q�6N   $	��110��6[   $
 Hand��6j   ._�5^      �  5_      �� 5`      $
��Auto �6+   �� 5a      $
��Auto �6+   .+�5c      �� $	��img��6,   $��0��6   $(��"../images/icons/patrol_officer.png��67   $��30��6c   $��20��6n   $��3,2,3,2q�6z   �� 5e      $ Vertical=�6!   $��0��68   $��1��6F   .+�5f      �� $ txtBlkPatrolCode��6$   ���#00FECE$��11(�6S   $��Open Sans - Regular)�6a   $��0q�6�   $
��Wrap=�6�   $ 1q�6�   ��6�   �� *
PatrolCode+6�   �� 5g      $ 	txtBlkEta��6$   ���#8B8F96$��11(�6L   $��Open Sans - Regular)�6Z   $��0q�6|   $
��Wrap=�6�   $ 1q�6�   �� 5h      $	�ETA��6"   6/    �� 5i      $�:��6"   6-    �� 5j      �6"   �� *	ETATime+8   6n    �� 5k      6%    �� 5l      #�Z 6"   6J   5m      5o      5p      5q       5r      P� 5s       �@J�IsMouseOver . . �5t      �� 
 �@�Cursor$
 Hand��6/   �� 5u      $
 brdr��6   
 � �� �#00FECE6E   �� 5v      $
 brdr��6   
 � ��
 ��� �6F   �� 5w      $	 img��6   
 � ��$. (../images/icons/patrol_officer_hover.png��6@   �� 5x      $ txtBlkPatrolCode��6   
 � ��
 �   �6Q   �� 5y      $ 	txtBlkEta��6   
 � ��
 �   �6J   5z      �� 5{      $
 True��6�    6   ��  �� $ FindAncestor=�" ��$ 
IsSelected�.a�5|      �� $
 brdr��6   
 � �� �#00FECE6E   �� 5}      $
 brdr��6   
 � ��
 ��� �6F   �� 5~      $	 img��6   
 � ��$. (../images/icons/patrol_officer_hover.png��6@   �� 5      $ txtBlkPatrolCode��6   
 � ��
 �   �6Q   �� 5�      $ 	txtBlkEta��6   
 � ��
 �   �6J   5�      5�      �� 5�   
   .R�5�      �� $
 brdr��6   ���#00FECE$��1q�6:   $	��5,5q�6N   $	��120��6[   $
 Hand��6j   ._�5�      �  5�      �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   .+�5�      � $��0��6   $��3,-1,3,0q�6+   $	��Top=�6=   .+�5�      � $  gridPathTick��6   $��LeftToRight=�65   $y�Visible=�6Q   $��0,0,0,0q�6f   $��0��6w   $��Center=�6�   $��Center=�6�   $! 2��6�   .+�5�      �� $��20��6%   $��20��60   ���#64605F$" 20��6Q   $y�	Collapsed=�6c   F� 5�      $# pathTick��6#   Q�����ǭ    Q H��g�0 �@�Bj� �H-Q    ��m��@�#��@���NP�@�w,� p^� 	$$ Uniform=�5�      
���   �$��10��6>   $��10��6I   $��0,0,0,0q�6U   $% 0.5,0.5&�6f   z�5�   "   V� 5�   &   ~�5�   .   �� $& -5Q�6>   �� 5�   .   $' 1Q�6=   $( 1Q�6H   5�   '   5�      5�      � 5�      +) �   ���  z  -1 $� ���  $� 
��� ���6D   5�      �� 5�      $	��img��6,   $��1��6   $$��../images/icons/patrol_car.png��67   $��30��6_   $��20��6j   $��3,2,3,2q�6v   �� 5�      $ Vertical=�6!   $��0��68   $��2��6F   .+�5�      �� $ txtBlkPatrolCode��6$   ���#00FECE$��11(�6S   $��Open Sans - Regular)�6a   $��0q�6�   $
��Wrap=�6�   $ 1q�6�   ��6�   �� *
PatrolCode+6�   �� 5�      $ 	txtBlkEta��6$   ���#8B8F96$��11(�6L   $��Open Sans - Regular)�6Z   $��0q�6|   $
��Wrap=�6�   $ 1q�6�   �� 5�      $	�ETA��6"   6/    �� 5�      $�:��6"   6-    �� 5�      �6"   �� *	ETATime+8   6n    �� 5�      6%    �� 5�      #�Z 6"   6J   5�      5�      5�      5�       5�      P� 5�       �@J�IsMouseOver . . �5�      �� 
 �@�Cursor$
 Hand��6/   �� 5�      $
 brdr��6   
 � �� �#00FECE6E   �� 5�      $
 brdr��6   
 � ��
 ��� �6F   �� 5�      $	 img��6   
 � ��$. (../images/icons/patrol_officer_hover.png��6@   �� 5�      $ txtBlkPatrolCode��6   
 � ��
 �   �6Q   �� 5�      $ 	txtBlkEta��6   
 � ��
 �   �6J   5�      �� 5�      $
 True��6�    6   ��  �� $ FindAncestor=�" ��$ 
IsSelected�.a�5�      �� $
 brdr��6   
 � �� �#00FECE6E   �� 5�      $
 brdr��6   
 � ��
 ��� �6F   �� 5�      $	 img��6   
 � ��$. (../images/icons/patrol_officer_hover.png��6@   �� 5�      $ txtBlkPatrolCode��6   
 � ��
 �   �6Q   �� 5�      $ 	txtBlkEta��6   
 � ��
 �   �6J   5�      5�      �� 5�   
   .R�5�      �� $
 brdr��6   ���#00FECE$��1q�6:   $	��5,5q�6N   ._�5�      �  5�      �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   �� 5�      $
��Auto �6+   .+�5�      � $  gridRecommended��6   $��0��64   $��3,-1,3,0q�6D   $	��Top=�6V   $y�	Collapsed=�6n   .+�5�      � $  gridPathTick��6   $��LeftToRight=�65   $y�Visible=�6Q   $��0,0,0,0q�6f   $��0��6w   $��Center=�6�   $��Center=�6�   $! 2��6�   .+�5�      �� $��20��6%   $��20��60   ���#64605F$" 20��6Q   $y�	Collapsed=�6c   F� 5�      $# pathTick��6#   Q�����ǭ    Q H��g�0 �@�Bj� �H-Q    ��m��@�#��@���NP�@�w,� p^� 	$$ Uniform=�5�      
���   �$��10��6>   $��10��6I   $��0,0,0,0q�6U   $% 0.5,0.5&�6f   z�5�   "   V� 5�   &   ~�5�   .   �� $& -5Q�6>   �� 5�   .   $' 1Q�6=   $( 1Q�6H   5�   '   5�      5�      � 5�      +) �   ���  z  -1 $� ���  $� 
��� ���6D   5�      �� 5�      $	��img��6,   $��1��6   $$��../images/icons/patrol_car.png��67   $��40��6_   $��20��6j   $��3,2,3,2q�6v   �� 5�      $ Vertical=�6!   $��0��68   $��2��6F   .+�5�      �� ���#00FECE$��11(�69   $��Open Sans - Regular)�6G   $��0q�6�   $
��Wrap=�6�   $ 1q�6�   ��6h   �� *
PatrolCode+6�   �� 5�      ���#8B8F96$��11(�69   $��Open Sans - Regular)�6G   $��0q�6i   $
��Wrap=�6t   $ 1q�6�   �� 5�      $	�ETA��6"   6/    �� 5�      $�:��6"   6-    �� 5�      �6"   �� *	ETATime+8   6n    �� 5�      6%    �� 5�      #�Z 6"   6J   5�      5�      5�      5�       5�      �� 5�      $
 True��6@    6   �� *IsRecommended+.a�5�      �� $ gridRecommended��6   
 � y�$ Visible=�6P   �� 5�      $	 img��6   
 � ��$$ ../images/icons/patrol_car.png��6@   5�      ^� 5�      * 5�      �� 5�      + 6$   �� *IsRecommended+, True6F   �� 5�      - �@J�IsMouseOver, . 6;   0�5�      �� 5�      $ gridRecommended��6!   
 � y�$ Visible=�6T   �� 5�      $
 brdr��6!   
 � �� �#00FECE6I   �� 5�      $
 brdr��6!   
 � ��
 ��� �6J   �� 5�      $	 img��6!   
 � ��$* $../images/icons/patrol_car_hover.png��6D   �� 5�      
 � ��
 �   �6C   5�      P� 5�       �@J�IsMouseOver . . �5      �� $
 brdr��6   
 � �� �#00FECE6E   �� 5     $
 brdr��6   
 � ��
 ��� �6F   �� 5     $	 img��6   
 � ��$. (../images/icons/patrol_officer_hover.png��6@   �� 5     
 � ��
 �   �6?   5     �� 5     $
 True��6�    6   ��  �� $ FindAncestor=�" ��$ 
IsSelected�.a�5     �� $
 brdr��6   
 � �� �#00FECE6E   �� 5     $
 brdr��6   
 � ��
 ��� �6F   �� 5     $	 img��6   
 � ��$. (../images/icons/patrol_officer_hover.png��6@   �� 5	     
 � ��
 �   �6?   5
     5       5  
   8.   8/  80  6�   �� 5  
   "	 ��6'   .�5     �� 
 � ��
 ���� 6+   �� 5     
 � ��
 ���� 6,   �� 5     
 � ��$ Center=�6;   �� 5     
 � ��$ Center=�62   �� 5     
 � �� �#ffffff6+   �� 5     
 � �� 5     �� 5     " ��6&   .R�5     �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5     �� #��z��6/   #��z��6k   5      5     P� 5      �@J�IsMouseOver . . �5  "   �� $ bg��6S   
 � ��
 ���� �� 5  "   
 � ��
 ���� 6?   �� 5  "   
 � ��
 ���� 6@   �� 5   "   
 �@J�Opacity$	 0.5Q�6<   6J   5!     5#     5%     5&     5'     z�5)     T� 5*  
   1 T� X$	1 385Q�6   2 � Triggers2 5-     :� 5.  
   3 :� RoutedEvent$3 Page.Loaded��6   .[�5/     �� .��50     �� .��51     W� $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&   $	 300X�6u   $ 0X�6�   $ 0:0:1D�6�   4 g� 	BeginTime$4 	0:0:0.001X�6�   $ 3Q�6�   53     54     55     56     .��5;  
   � $
��Auto��6   .+�5=  
   � $��1��6   
���   �$
 0.95Q�6/   $	��500��6>   5 � RowDefinitions5 5>     �� 5?     $
��Auto �6    �� 5@     �� 5A     $
��Auto �6    �� 5B     $
��Auto �6    �� 5C     $	��250 �6    .+�5F     �� 6   strDispatchPatrol#��Z6 6   $��0��6H   $��Open Sans - Regular)�6U   ���#00FECE$��0��6�   $��16(�6�   $��10,5,5,0q�6�   $
��Left=�6�   �� 5I     -   5        7 ��Name$7 btnClosePopups��5I     $��1��6-   $
 Hand��6=   $��Right=�5J  !   $	��Top=�6=   $��20��6s   $��20��6~   $��10,5,5,0q�6�   #��[	 6�   .��5K     � .+�5L     � -   5        $��backgroundGrid��5L     $��48��61   $��48��6<   $y�	Collapsed=�6H   F� 5M     ����p�� �e���"@� $� @]� Q,�,�e���"@��_Z㮻 ���! $� ��_Z�,�,�,�_Z� $� ��!��� �_Z�yh���"@�,�@]�  $� �yh���"@㮻 p $� D1y��@���gf"@D1y��@j9+���@O�a�ef"@j9+���@ $� �j9+���@��[���gf"@�Y� $� �Y�ӱ�[�Y��Y�"m]�Y� $� S�Y�QL� 33"@��[D1y��@ $� D1y��@� $�     �1kz     H��4����@ H� $� � H�kz1kz H� $�  H��<4����@ H�kz $� S    �4����@<4����@ $� 	$$ Uniform=�5N  '   ���#00FECE$��17��6H   $��17��6S   $��0,0,0,0q�6_   $% 0.5,0.5&�6p   z�5O     V� 5P     ~�5R  &   �� $& 0Q�66   �� 5S  &   $' 1Q�65   $( 1Q�6@   5U     5W     5X     5[     �� 5]     ���#00FECE$��0 1 0 1q�6+   $��1��6E   ._�5_     �� -   5        $��listViewPatrolsList��5_     #��[ 6Z   8 �� SelectionMode$8 Single=�5`     $��Stretch=�6�   $��Disabled=�5a     $
��Auto=�6G   $	��Top=�5b     
������ $��0q�6B   ���#00FECE$	��255��6k   $	��100��6z   ��5_  6   �� *PatrolsList+9 �� SelectedItem9 5`  Z   �� *SelectedPatrol+��5c     �� 5d     .R�5e     � $	��260��6(   $	��250��67   $
��Left=�6G   $	��2,2q�6b   : � Orientation$: 
Horizontal=�6o   5f     ��5h     �� 5i     "	 ��6    .�5j     �� 
 � ��
 ���� 6;   �� 5k     
 � �� 5l  "   �� 5m  &   " ��66   .R�5n  *   �� ; ��Name$; SopListContentPresenter��6;   5o  '   5q     5z     5|     5}     � 5     -   5        $  gridSelectedPatrolInfo��5     $��2��63   $	��5,5q�6@   {�6M   �� *IsPatrolDetailsEnabled+.+�5�     � 5 5�     �� 5�     �� 5�     �� 5�     �� 5�     �� 5�      5�     �� 5�     �� 5�     .+�5�     �� ���#00FECE$��0��65   $��13(�6E   $��Open Sans - Regular)�6S   $��0q�6u   $
��Wrap=�6�   $
��Left=�6�   $ 1q�6�   �� 5�     <   strPatrolCode#�Z< 6   6L   5�     �� 5�     ���#00FECE$��1��65   $��13(�6E   $��Open Sans - Regular)�6S   $	��5,5q�6�   $
��Left=�6�   $
��Wrap=�6�   $ 1q�6�   ��6u   �� $ SelectedPatrol.PatrolCode�6�   �� 5�     ���#00FECE$��1��65   $��0��6B   $��13(�6R   $��Open Sans - Regular)�6`   $��0q�6�   $
��Wrap=�6�   $
��Left=�6�   $ 1q�6�   �� 5�     =   strPatrolPlateNo#�Z= 6   6O    �� 5�     $�:��6   6)   5�     �� 5�     ���#00FECE$��1��65   $��1��6B   $��13(�6R   $��Open Sans - Regular)�6`   $	��5,5q�6�   $
��Left=�6�   $
��Wrap=�6�   $ 1q�6�   ��6�   �� $" SelectedPatrol.PatrolPlateNo�6�   �� 5�     ���#00FECE$��2��65   $��0��6B   $��13(�6R   $��Open Sans - Regular)�6`   >   strComments#��Z> 6�   $��0q�6�   $
��Wrap=�6�   $
��Left=�6�   $ 1q�6�   �� 5�     -   5        ? ��Name$? txtBlkComments��5�     $��70��66   $��3��6E   $��0��6R   $��2��6b   ���#0e2b2b���#00FECE$��13(�6�   $��Open Sans - Regular)�6�   ��6�   �� *CommentText+6�   � 5�     $y�	Collapsed=�6   .+�5�     )  #Esri.ArcGISRuntime.Controls.MapView  -   5        $��EsriMapView��5�  H   $y�Hidden=�6'   $��3��6;   $��	15,5,15,5q�6[   
@   Map.@ 5�     %  Esri.ArcGISRuntime.Controls.Map  A   Layers.A 5�  "   :  4Esri.ArcGISRuntime.Layers.ArcGISTiledMapServiceLayer  %  Esri.ArcGISRuntime.Layers.Layer	B   ID$B BaseMap��6C   C   
ServiceUri$aC [http://services.arcgisonline.com/arcgis/rest/services/Canvas/World_Dark_Gray_Base/MapServer��6P   5�     5�     �� 5�     $y�	Collapsed=�6%   $! 5��6<   
������ $ 
Horizontal=�6f   $��3��6   $��40��6�   $��75��6�   $	��Top=�6�   $��Right=�6�   $��	10,7,10,0q�6�   .+�5�     �� -   5        D ��Name$D 	btnZoomIn��5�  +   $��3,0,0,0q�6>   E � ToolTip$E 
تكبير��6O   $��7��6_   $
��Left=�6o   $��35��6�   $��35��6�   ��6�   Z� ��6�   Z� .��5�  "   �� $��30��6(   $��30��63   $`��Zpack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Zoom_IN.png��6?   5�     �� 5�     -	   5        $D 
btnZoomOut��5�  +   $E 
تصغير��6?   $��7��6O   $
��Left=�6_   $��35��6z   $��35��6�   ��6�   Z� ��6�   Z� .��5�  "   �� $��30��6(   $��30��63   $a��[pack://application:,,,/STC.Projects.ClassLibrary.Common;component/Images/icons/Zoom_Out.png��6?   5�     5�     5�     �� 5�     -
   5        $7 btnDispatchPatrol��5�     $��4��68   $��0��6E   $��0,10,5,5q�6U   $
 Hand��6h   #��Z6 5�     $��13(�6P   $	��100��6_   $��25��6k   #���  6w   6�   �� 5�     -   5        $7 btnDispatchCancel��5�     $��4��68   $��1��6E   $��5,10,5,5q�6U   $
 Hand��6g   F   	strCancel#��ZF 5�     $��13(�6H   $��70��6V   $��25��6a   #���  6m   6�   5�     5�     5�     5�     5�     
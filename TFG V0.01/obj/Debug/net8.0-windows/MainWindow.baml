   M S B A M L   `   `   `  ����D  @TFG V0.01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:TFG_V0._01
TFG_V0._01  5   	       TFG_V0._01.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:TFG_V0._01     5   	     � Title$  TFG Application��5   	   $	��300��6!   $	��300��6.    � WindowStyle$
 None=�5	   	    � AllowsTransparency$
 True��6   
������  � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�6.   .��5      �� -   5         �� CornerRadius$	 150��5      ��5   
   �� 5       �� Color$ 	#801E1E2E��6    �� Opacity$
 0.85Q�60   ._�5   
   � .+�5      � $	��200��6   $	��200��6   .+�5      A� $	��200��6   $	��200��6&   ���	#40FFFFFF$��10��6F   F� 5      -   5         F�Name$ ProgressPath��5      $��10��6-   	 �� StrokeStartLineCap$	 Round=�5      
 �� StrokeEndLineCap$
 Round=�62   ��5      �� 5       �� 
StartPoint$	 0,0&�6.    �� EndPoint$	 1,1&�6?   .��5      �  � Color$ #3084F2��6+    � Offset$ 0Q�6;   � 5      $ #8B5CF6��6+   $ 1Q�6;   5      ��5!      B� 5"      .��5#      E� -   5        ProgressFigure�� E� 
StartPoint$ 100,20&�5#   A   .��5$   "   �� -   5        ProgressArc�� �� Point$ 100,20&�5$   B    �� Size$ 90,90��5%   -    �� SweepDirection$ 	Clockwise=�6:    �� 
IsLargeArc$ False��5&   -   5'      5(      5*      �� 5-      $	��120��6   $	��120��6%   $ 60��62   ���	#80000000 J� Opacity$	 0.5Q�5.   0   �� 51      $��Center=�6   $��Center=�68   .+�52      �� -   5         ��Name$ PercentageText��52       $��0%��68   $��36(�6B   
�������$��Center=�53   3   $��SemiBold"�54       �� 55      $��Cargando��6    $��14(�60   ���	#EEFFFFFF$��Center=�56       $��0,5,0,0q�6=   57      58      �� 5;      -   5        $��×��5;      $��16(�6!   $��24��6/   $��24��6:   $��Right=�5<      $	��Top=�61   $��	0,20,20,0q�6I   ���	#40000000
�������$��0q�5=   ?    � Cursor$
 Hand��5>   /   ��5?      �� 5@       �� 
TargetType" ��6   .�5A      ��  �� Property � �� �� Value 5B      �� 5C   "    �� 
TargetType" ��62   .R�5D   &   �� #��z��6-   $ 12��5E   -   ._�5F   *   �� $��Center=�6;   $��Center=�5G   ;   5H   '   5I   #   5K       �� Triggers 5L      P� 5M       P� Property �@J�IsMouseOver P� Value$
 True��6=   . �5N   "   ��  � �� �	#800000006?   �� 5O   "    � ��
 �����6?   5P      5R      5T      �� 5W      $	 150��6   $	 0.2Q�6(   ��5X      
� 5Y      .��5Z      � $ #FFFFFF��6'   $ 0Q�67   � 5[      $ Transparent��6'   $ 1Q�6;   5\      5^      5_      5`      5a      
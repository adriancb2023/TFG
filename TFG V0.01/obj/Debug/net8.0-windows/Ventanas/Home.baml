   M S B A M L   `   `   `  ����D  @TFG V0.01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TFG_V0._01.VentanasTFG_V0._01.Ventanas  5   	   e aMicrosoft.Web.WebView2.Wpf, Version=1.0.3179.45, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf 5   	       TFG_V0._01.Ventanas.Home   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TFG_V0._01.Ventanas     5   	   \wv2Lclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf     5   	     � Title$  
TFG  v0.01��5	   	   $	��850��6   $
��1200��6)    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5         ThemeButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value  RoundedButtonStyle ��Name �� 
TargetName2  ,System.Windows.Media.Animation.QuadraticEase  
SmoothEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode  	CardStyle �� Color �� Opacity3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   Opacity��%9  &	        &	     &	     &	 9    �� 5   
   " ��6)   .�5      ��  � ��
 ���� 6+   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 20��6K   ._�5      �� $ 	ThemeIcon��6$   $2��,/TFG V0.01;component/Recursos/Iconos/sol.png��67   $��20��6m   $��20��6x   5      	 5      P� 5      
 �@J�IsMouseOver$
 True��6=   . �5   "   ��  �@J�Opacity$	 0.8Q�6<   5      5      5      5       �� 5"   
   " ��6+   .�5#      ��  � �� �#FFFFFF6+   �� 5$       � �� �#3333336+   �� 5%       � ��$ 0q�60   �� 5&       � ��$ 45��6'   �� 5'       � ��$ 14(�6)   �� 5(       � ��$ SemiBold"�6+   �� 5)       �@�Cursor$
 Hand��6'   �� 5*       � �� 5+      �� 5,      " ��6&   .R�5-      �� $ border��6!   #��z��61   $ 20��5.      #��z��6+   #��z��5/      ._�50      �� $��Center=�6/   $��Center=�6L   51      	 52      P� 53      
 �@J�IsMouseOver$
 True��6=   . �54   "   �� $ border��6L    � ��
 ��� �55      P� 56      
 � ��$
 true��6;   . �57   "   �� $ border��6O    � �� �#DDDDDD58      5:      5<      5=        5?   
   $ 	EaseInOut=�6+   �� 5A   
   " ��6"   .�5B      ��  �@��CornerRadius$ 20��6-   �� 5C       � �� 5D      �� 5E      colortarjeta��$ #454545��6<   $	 0.4Q�6L   5G      �� 5H       � �� �#3336,   �� 5I       � ��$ 1q�60   �� 5J       �@��Padding$ 15q�6(   �� 5K       � ��$ 10q�6'   �� 5L       �@J�Effect 5M        5N      $ Black��6'   $ 15Q�65   $ 2Q�6E   $	 0.3Q�6U   5P      5Q      5S      .��5U      �  � ColumnDefinitions 5V   
   �� 5W      $
��AUTO �6   �� 5X      $��2 �6   �� 5Y      $��* �6   ��5\   
   �� 5]      -   5        backgroundFondo�� �� ImageSource$> 8/TFG V0.01;component/Recursos/Background/oscuro/main.png��5]   2   .+�5`   
   � -   5         �Name$
 menu��5`      $��0��6   $y�Visible=�6-   .+�5a      �� -   5        $ backgroun_menu��5a      ���#454545$��0q�6C   $��0,0,0,0q�6W   $ 15��6h   ���	#30FFFFFF$��Center=�6�    J� Effect 5b        5c      $ 15Q�6'      	Direction$	 -90Q�67      RenderingBias$ Quality=�5d      $ 2Q�6/   $ 	#20000000��5e      ._�5g      �� $��Center=�6   .+�5h      �� -   5         ��Name$ btnHome��5h      $��10q�6.   #��[ 6:   $��Stretch=�6f   $	��Top=�6�   
������ .��5i      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5j      �� -   5        $ imagenHome2��5j   $   $3��-/TFG V0.01;component/Recursos/Iconos/home.png��69   $��25��6p   $��25��6{   �� 5k      -   5         ��Name$ inicio��5k   (   $y�	Collapsed=�68   $��Inicio��6O   $��Center=�6]   $
��10,0q�6x   5l      5m      �� 5n      -   5        $ 	btnBuscar��5n      $��10q�60   #��[ 6<   $��Stretch=�6h   $	��Top=�6�   
������ 
������ .��5o      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5p      �� -   5        $ imagenBuscar2��5p   $   $5��//TFG V0.01;component/Recursos/Iconos/buscar.png��6;   $��25��6t   $��25��6   �� 5q      -	   5        $ buscar��5q   (   $y�	Collapsed=�68   $��Buscar Jurisprudencia��6O   $��Center=�6l   $
��10,0q�6�   5r      5s      �� 5t      -
   5        $ 	btnAgenda��5t      $��10q�60   #��[ 6<   $��Stretch=�6h   $	��Top=�6�   
������ .��5u      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   
������ .+�5v      �� -   5        $ imagenAgenda2��5v   $   $5��//TFG V0.01;component/Recursos/Iconos/agenda.png��6;   $��25��6t   $��25��6   �� 5w      -   5        $ agenda��5w   (   $y�	Collapsed=�68   $��Agenda��6O   $��Center=�6]   $
��10,0q�6x   5x      5y      �� 5z      -   5        $ btnCasos��5z      $��10q�6/   #��[ 6;   $��Stretch=�6g   $	��Top=�6�   
������ .��5{      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5|      �� -   5        $ imagenCasos2��5|   $   $4��./TFG V0.01;component/Recursos/Iconos/casos.png��6:   $��25��6r   $��25��6}   �� 5}      -   5        $ casos��5}   (   $y�	Collapsed=�67   $��Casos��6N   $��Center=�6[   $
��10,0q�6v   5~      5      �� 5�      -   5        $ btnClientes��5�      $��10q�62   #��[ 6>   $��Stretch=�6j   $	��Top=�6�   
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenClientes2��5�   $   $7��1/TFG V0.01;component/Recursos/Iconos/clientes.png��6=   $��25��6x   $��25��6�   �� 5�      -   5        $ clientes��5�   (   $y�	Collapsed=�6:   $��Clientes��6Q   $��Center=�6a   $
��10,0q�6|   5�      5�      �� 5�      -   5        $ btnDocumentos��5�      $��10q�64   #��[ 6@   $��Stretch=�6l   $	��Top=�6�   
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenDocumentos2��5�   $   $9��3/TFG V0.01;component/Recursos/Iconos/documentos.png��6?   $��25��6|   $��25��6�   �� 5�      -   5        $ 
documentos��5�   (   $y�	Collapsed=�6<   $��
Documentos��6S   $��Center=�6e   $
��10,0q�6�   5�      5�      �� 5�      -   5        $ 
btnAjustes��5�      $��10q�61   #��[ 6=   $��Stretch=�6i   $	��Top=�6�   
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenAjustes2��5�   $   $6��0/TFG V0.01;component/Recursos/Iconos/ajustes.png��6<   $��25��6v   $��25��6�   �� 5�      -   5        $ ajustes��5�   (   $y�	Collapsed=�69   $��Ajustes��6P   $��Center=�6_   $
��10,0q�6z   5�      5�      �� 5�      -   5        $ btnAyuda��5�      $��10q�6/   #��[ 6;   $��Stretch=�6g   $	��Top=�6�   
������ 
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenAyuda2��5�   $   $4��./TFG V0.01;component/Recursos/Iconos/ayuda.png��6:   $��25��6r   $��25��6}   5�      5�      5�      5�      5�      �� 5�   
   -   5        $ ThemeButton��5�      $��35��6&   $��35��61   $��0q�6=   $��Right=�6H   $��Bottom=�6d   #��[  6�   $��2��6�   � 5�   
   $��2��6   $��20q�6    � RowDefinitions 5�      �� 5�      $
��Auto �6    �� 5�      $��* �6    �� 5�      $��0.10* �6    .+�5�      � $��0��6   $��	0,10,0,20q�6     5�      �� 5�      $��* �6'   �� 5�      $
��Auto �6'   .+�5�      �� $��0��6   .+�5�      �� -   5        $ txtBienvenida��5�       $��Bienvenido, Usuario��67   $��24(�6R   $
��Bold"�6`   
�������6r   �� 5�      $��Panel de Control��6    $��16(�68   ���	#CCFFFFFF$��0,5,0,0q�6]   5�      �� 5�      $��1��6   $ 
Horizontal=�6-   $��Center=�6F   .+�5�      �� ���	#30FFFFFF$ 20��64    �� Padding$
 15,8q�6F   $��0,0,15,0q�6U   ._�5�      �� $ 
Horizontal=�6%   .+�5�      �� $3��-/TFG V0.01;component/Recursos/Iconos/home.png��6$   $��20��6[   $��20��6f   �� 5�      $��5��6(   
�������$��8,0,0,0q�6D   $��Center=�6U   5�      5�      �� 5�      ���	#30FFFFFF$ 20��64   $
 15,8q�6F   ._�5�      �� $ 
Horizontal=�6%   .+�5�      �� $7��1/TFG V0.01;component/Recursos/Iconos/clientes.png��6$   $��20��6_   $��20��6j   �� 5�      $��Perfil��6(   
�������$��8,0,0,0q�6I   $��Center=�6Z   5�      5�      5�      5�      �� 5�      $��1��6   $
��Auto=�6(   .��5�      �  5�      �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      � $��0��6    5�      �� 5�      $��* �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   �� 5�      $��* �6/   .+�5�      �� $��0��6!   #��[ 61   $��0,0,10,0q�6T   ._�5�      �  5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $��* �64   .+�5�   "   �� $��Casos Activos��6,   $��14(�6A   ���	#CCFFFFFF$��0��6f   �� 5�   "   $��1��6-   $ 
Horizontal=�6:   $��0,15,0,0q�6S   .+�5�   &   �� $��12��60   $��36(�6:   $
��Bold"�6H   
�������6Z   �� 5�   &   ���#4CAF50$ 5��6B   $	 5,2q�6S   $��	15,10,0,0q�6a   $	��Top=�6t   ._�5�   *   �� $��+2��64   $��12(�6>   
�������6L   5�   '   5�   #   5�      5�      �� 5�      $��1��6!   #��[ 61   $��	10,0,10,0q�6T   ._�5�      �  5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $��* �64   .+�5�   "   �� $��Clientes��6,   $��14(�6<   ���	#CCFFFFFF$��0��6a   �� 5�   "   $��1��6-   $ 
Horizontal=�6:   $��0,15,0,0q�6S   .+�5�   &   �� $��48��60   $��36(�6:   $
��Bold"�6H   
�������6Z   �� 5�   &   ���#2196F3$ 5��6B   $	 5,2q�6S   $��	15,10,0,0q�6a   $	��Top=�6t   ._�5�   *   �� $��+5��64   $��12(�6>   
�������6L   5�   '   5�   #   5�      5�      �� 5�      $��2��6!   #��[ 61   $��	10,0,10,0q�6T   ._�5�      �  5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $��* �64   .+�5�   "   �� $��
Documentos��6,   $��14(�6>   ���	#CCFFFFFF$��0��6c   �� 5�   "   $��1��6-   $ 
Horizontal=�6:   $��0,15,0,0q�6S   .+�5   &   �� $	��156��60   $��36(�6;   $
��Bold"�6I   
�������6[   �� 5  &   ���#FF9800$ 5��6B   $	 5,2q�6S   $��	15,10,0,0q�6a   $	��Top=�6t   ._�5  *   �� $	��+23��64   $��12(�6?   
�������6M   5  '   5  #   5     5     �� 5	     $��3��6!   #��[ 61   $��10,0,0,0q�6T   ._�5
     �  5  "   �� 5  &   $
��Auto �64   �� 5  &   $��* �64   .+�5  "   �� $��Eventos Próximos��6,   $��14(�6D   ���	#CCFFFFFF$��0��6i   �� 5  "   $��1��6-   $ 
Horizontal=�6:   $��0,15,0,0q�6S   .+�5  &   �� $��8��60   $��36(�69   $
��Bold"�6G   
�������6Y   �� 5  &   ���#E91E63$ 5��6B   $	 5,2q�6S   $��	15,10,0,0q�6a   $	��Top=�6t   ._�5  *   �� $	��Hoy��64   $��12(�6?   
�������6M   5  '   5  #   5     5     5     � 5     $��1��6   $��0,20,0,0q�6(    5     �� 5     $��2* �6/   �� 5     $��1* �6/   .+�5"     �� $��0��6!   #��[ 61   $��0,0,10,0q�6T   ._�5#     �  5$  "   �� 5%  &   $
��Auto �64   �� 5&  &   $��* �64   .+�5)  "   � $��0��6'    5*  &   �� 5+  *   $��* �6;   �� 5,  *   $
��Auto �6;   .+�5.  &   �� $��Calendario - Abril 2025��60   $��16(�6O   $��SemiBold"�6]   
�������6s   �� 5/  &   $��1��61   $ 
Horizontal=�6A   .+�50  *   �� $��<��61   #��[ 6@   $��30��6l   $��30��6w   $��0,0,5,0q�6�   ���	#30FFFFFF6�   �� 51  *   $��>��61   #��[ 6@   $��30��6l   $��30��6w   ���	#30FFFFFF6�   52  '   53  #   � 55  "   $��1��6'   $��0,15,0,0q�64   .+�57  &   �  58  *   �� 59  .   $
��Auto �6<   �� 5:  .   $��* �6<   .+�5>  *   � $��0��6/    5?  .   �� 5@  2   $��* �6C   �� 5A  2   $��* �6C   �� 5B  2   $��* �6C   �� 5C  2   $��* �6C   �� 5D  2   $��* �6C   �� 5E  2   $��* �6C   �� 5F  2   $��* �6C   .+�5H  .   �� $��0��68   $	��Lun��6H   $��Center=�6S   ���	#CCFFFFFF6p   �� 5I  .   $��1��68   $	��Mar��6H   $��Center=�6S   ���	#CCFFFFFF6p   �� 5J  .   $��2��68   $
��Mié��6H   $��Center=�6S   ���	#CCFFFFFF6p   �� 5K  .   $��3��68   $	��Jue��6H   $��Center=�6S   ���	#CCFFFFFF6p   �� 5L  .   $��4��68   $	��Vie��6H   $��Center=�6S   ���	#CCFFFFFF6p   �� 5M  .   $��5��68   $
��Sáb��6H   $��Center=�6S   ���	#CCFFFFFF6p   �� 5N  .   $��6��68   $	��Dom��6H   $��Center=�6S   ���	#CCFFFFFF6p   5O  +   � 5R  *   $��1��6/   $��0,10,0,0q�6<    5S  .   �� 5T  2   $
��Auto �6@   �� 5U  2   $
��Auto �6@   �� 5V  2   $
��Auto �6@   .+�5Z  .   �� $��0��65   ���#3F51B5$ 10��6W   $ 10q�6i   $��0,5,0,5q�6v   ._�5[  2   �  5\  6   �� 5]  :   $
��Auto �6K   �� 5^  :   $��* �6K   .+�5`  6   �� $��0��6=   ���	#30FFFFFF$ 5��6d   $	 8,5q�6u   $��0,0,10,0q�6�   ._�5a  :   �� $��17��6D   $
��Bold"�6N   
�������6`   5b  7   �� 5c  6   $��1��6A   .+�5d  :   �� $��Audiencia Preliminar��6D   $��SemiBold"�6`   
�������6v   �� 5e  :   $)��#Caso Martínez vs. Corporación XYZ��6D   ���	#CCFFFFFF$��12(�6�   �� 5f  :   $��10:00 - 12:00 | Sala 305��6D   ���	#CCFFFFFF$��12(�6{   $��0,5,0,0q�6�   5g  7   5h  3   5i  /   �� 5l  .   $��1��65   ���#009688$ 10��6W   $ 10q�6i   $��0,5,0,5q�6v   ._�5m  2   �  5n  6   �� 5o  :   $
��Auto �6K   �� 5p  :   $��* �6K   .+�5r  6   �� $��0��6=   ���	#30FFFFFF$ 5��6d   $	 8,5q�6u   $��0,0,10,0q�6�   ._�5s  :   �� $��18��6D   $
��Bold"�6N   
�������6`   5t  7   �� 5u  6   $��1��6A   .+�5v  :   �� $��Reunión con Cliente��6D   $��SemiBold"�6_   
�������6u   �� 5w  :   $)��#Revisión de Contrato - Empresa ABC��6D   ���	#CCFFFFFF$��12(�6�   �� 5x  :   $'��!15:30 - 16:30 | Oficina Principal��6D   ���	#CCFFFFFF$��12(�6�   $��0,5,0,0q�6�   5y  7   5z  3   5{  /   �� 5~  .   $��2��65   ���#FF5722$ 10��6W   $ 10q�6i   $��0,5,0,5q�6v   ._�5  2   �  5�  6   �� 5�  :   $
��Auto �6K   �� 5�  :   $��* �6K   .+�5�  6   �� $��0��6=   ���	#30FFFFFF$ 5��6d   $	 8,5q�6u   $��0,0,10,0q�6�   ._�5�  :   �� $��20��6D   $
��Bold"�6N   
�������6`   5�  7   �� 5�  6   $��1��6A   .+�5�  :   �� $��Fecha Límite��6D   $��SemiBold"�6X   
�������6n   �� 5�  :   $0��*Presentación de Recurso - Caso Rodríguez��6D   ���	#CCFFFFFF$��12(�6�   �� 5�  :   $��Antes de las 14:00��6D   ���	#CCFFFFFF$��12(�6u   $��0,5,0,0q�6�   5�  7   5�  3   5�  /   5�  +   5�  '   5�  #   5�     5�     �� 5�     $��1��6!   #��[ 61   $��10,0,0,0q�6T   ._�5�     �  5�  "   �� 5�  &   $
��Auto �64   �� 5�  &   $��* �64   �� 5�  &   $
��Auto �64   .+�5�  "   �� $��0��6,   $��Tareas Pendientes��69   $��16(�6R   $��SemiBold"�6`   
�������6v   �� 5�  "   $��1��6-   $��0,15,0,0q�6:   .+�5�  &   �� ���	#30FFFFFF$ 10��6D   $ 10q�6V   $��0,0,0,10q�6c   ._�5�  *   �  5�  .   �� 5�  2   $
��Auto �6C   �� 5�  2   $��* �6C   .+�5�  .   �� $��0��67   $��0,0,10,0q�6G   $��Center=�6Y   �� 5�  .   $��1��69   .+�5�  2   �� $'��!Revisar contrato de arrendamiento��6<   
�������$
��Wrap=�6x   �� 5�  2   $��	Vence hoy��6<   ���#FF5252$��12(�6b   $��0,5,0,0q�6p   5�  /   5�  +   5�  '   �� 5�  &   ���	#30FFFFFF$ 10��6D   $ 10q�6V   $��0,0,0,10q�6c   ._�5�  *   �  5�  .   �� 5�  2   $
��Auto �6C   �� 5�  2   $��* �6C   .+�5�  .   �� $��0��67   $��0,0,10,0q�6G   $��Center=�6Y   �� 5�  .   $��1��69   .+�5�  2   �� $(��"Preparar alegatos para caso Pérez��6<   
�������$
��Wrap=�6x   �� 5�  2   $��Vence en 2 días��6<   ���#FFC107$��12(�6h   $��0,5,0,0q�6v   5�  /   5�  +   5�  '   �� 5�  &   ���	#30FFFFFF$ 10��6D   $ 10q�6V   $��0,0,0,10q�6c   ._�5�  *   �  5�  .   �� 5�  2   $
��Auto �6C   �� 5�  2   $��* �6C   .+�5�  .   �� $��0��67   $��0,0,10,0q�6G   $��Center=�6Y   �� 5�  .   $��1��69   .+�5�  2   �� $2��,Llamar a testigo para confirmar declaración��6<   
�������$
��Wrap=�6�   �� 5�  2   $��Vence en 3 días��6<   ���#8BC34A$��12(�6h   $��0,5,0,0q�6v   5�  /   5�  +   5�  '   �� 5�  &   ���	#30FFFFFF$ 10��6D   $ 10q�6V   ._�5�  *   �  5�  .   �� 5�  2   $
��Auto �6C   �� 5�  2   $��* �6C   .+�5�  .   �� $��0��67   $��0,0,10,0q�6G   $��Center=�6Y   �� 5�  .   $��1��69   .+�5�  2   �� $+��%Actualizar expediente del caso Gómez��6<   
�������$
��Wrap=�6{   �� 5�  2   $��Vence en 5 días��6<   ���#8BC34A$��12(�6h   $��0,5,0,0q�6v   5�  /   5�  +   5�  '   5�  #   �� 5�  "   $��2��6)   $��+ Añadir Tarea��66   #��[ 6O   $��0,15,0,0q�6{   ���	#30FFFFFF6�   5�     5�     5�     �� 5�     $��2��6   #��[ 6*   $��0,20,0,0q�6M   ._�5�     �  5�     �� 5�  "   $
��Auto �60   �� 5�  "   $��* �60   .+�5�     � $��0��6#    5�  "   �� 5�  &   $��* �67   �� 5�  &   $
��Auto �67   .+�5�  "   �� $��Casos Recientes��6,   $��16(�6C   $��SemiBold"�6Q   
�������6g   �� 5�  "   $��1��6)   $��	Ver Todos��69   #��[ 6M   ���	#30FFFFFF6y   5�     � 5�     $��1��6#   $��0,15,0,0q�60    5�  "   �� 5�  &   $
��Auto �64   �� 5�  &   $
��Auto �64   �� 5�  &   $
��Auto �64   .+�5�  "   � $��0��6'   $��0,0,0,10q�64    5�  &   �� 5�  *   $��* �6;   �� 5�  *   $��* �6;   �� 5�  *   $��* �6;   �� 5   *   $��* �6;   �� 5  *   $
��Auto �6;   .+�5  &   �� $��0��60   $��Nº Caso��6@   ���	#CCFFFFFF6O   �� 5  &   $��1��60   $��Cliente��6@   ���	#CCFFFFFF6O   �� 5  &   $��2��60   $
��Tipo��6@   ���	#CCFFFFFF6L   �� 5  &   $��3��60   $��Estado��6@   ���	#CCFFFFFF6N   �� 5  &   $��4��60   $��Acciones��6@   ���	#CCFFFFFF6P   5  #   �� 5  "   $��1��6)   ���	#30FFFFFF$ 10��6M   $ 10q�6_   $��0,0,0,10q�6l   ._�5  &   �  5  *   �� 5  .   $��* �6?   �� 5  .   $��* �6?   �� 5  .   $��* �6?   �� 5  .   $��* �6?   �� 5  .   $
��Auto �6?   .+�5  *   �� $��0��64   $��
C-2025-042��6D   
�������$��Center=�6i   �� 5  *   $��1��64   $��María Rodríguez��6D   
�������$��Center=�6n   �� 5  *   $��2��64   $��Civil - Contractual��6D   
�������$��Center=�6r   �� 5  *   $��3��65   $ 
Horizontal=�6E   $��Center=�6^   .+�5  .   �� ���#4CAF50$ 5��6J   $	 8,3q�6[   ._�5  2   �� $��Activo��6<   
�������$��12(�6]   5  /   5  +   �� 5  *   $��4��65   $ 
Horizontal=�6E   .+�5  .   �� #��[ 65   $��35��6a   $��35��6l   ���	#30FFFFFF$��0,0,5,0q�6�   .��5  2   �� $3��-/TFG V0.01;component/Recursos/Iconos/luna.png��68   $��15��6o   $��15��6z   5  /   �� 5   .   #��[ 65   $��35��6a   $��35��6l   ���	#30FFFFFF.��5!  2   �� $2��,/TFG V0.01;component/Recursos/Iconos/sol.png��68   $��15��6n   $��15��6y   5"  /   5#  +   5$  '   5%  #   �� 5(  "   $��2��6)   ���	#30FFFFFF$ 10��6M   $ 10q�6_   ._�5)  &   �  5*  *   �� 5+  .   $��* �6?   �� 5,  .   $��* �6?   �� 5-  .   $��* �6?   �� 5.  .   $��* �6?   �� 5/  .   $
��Auto �6?   .+�51  *   �� $��0��64   $��
C-2025-039��6D   
�������$��Center=�6i   �� 52  *   $��1��64   $��Juan Martínez��6D   
�������$��Center=�6l   �� 53  *   $��2��64   $��Laboral��6D   
�������$��Center=�6f   �� 54  *   $��3��65   $ 
Horizontal=�6E   $��Center=�6^   .+�55  .   �� ���#FF9800$ 5��6J   $	 8,3q�6[   ._�56  2   �� $��
En Proceso��6<   
�������$��12(�6a   57  /   58  +   �� 59  *   $��4��65   $ 
Horizontal=�6E   .+�5:  .   �� #��[ 65   $��35��6a   $��35��6l   ���	#30FFFFFF$��0,0,5,0q�6�   .��5;  2   �� $2��,/TFG V0.01;component/Recursos/Iconos/sol.png��68   $��15��6n   $��15��6y   5<  /   �� 5=  .   #��[ 65   $��35��6a   $��35��6l   ���	#30FFFFFF.��5>  2   �� $3��-/TFG V0.01;component/Recursos/Iconos/luna.png��68   $��15��6o   $��15��6z   5?  /   5@  +   5A  '   5B  #   5C     5D     5E     5F     5G     �� 5J     $��2��6   ���	#30FFFFFF$ 10��69   $ 15q�6K   $��0,20,0,0q�6X   ._�5K     � .+�5L     �� $��© 2025 TFG v0.01 - TFG��6    ���	#CCFFFFFF$��Center=�6U   �� 5M     $��Versión 0.01��6    ���	#CCFFFFFF$��Center=�6K   $��Right=�6f   5N     5O     5P     5Q     5R     
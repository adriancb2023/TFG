   M S B A M L   `   `   `  ����D  @TFG V0.01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TFG_V0._01.VentanasTFG_V0._01.Ventanas  5   	       TFG_V0._01.Ventanas.Ayuda   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TFG_V0._01.Ventanas     5   	     � Title$  Ayuda��5   	   $	��850��6   $
��1200��6$    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5         ThemeButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name	 �� Triggers
 P� Property P� Value  RoundedButtonStyle ��Name �� 
TargetName  	CardStyle �� Color �� Opacity3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth   Opacity��%�  &	        &	     &	     �� 5   
   " ��6)   .�5      ��  � ��
 ���� 6+   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 20��6K   ._�5      �� $ 	ThemeIcon��6$   $2��,/TFG V0.01;component/Recursos/Iconos/sol.png��67   $��20��6m   $��20��6x   5      	 5      P� 5      
 �@J�IsMouseOver$
 True��6=   . �5   "   ��  �@J�Opacity$	 0.8Q�6<   5      5      5      5      �� 5!   
   " ��6+   .�5"      ��  � �� �#FFFFFF6+   �� 5#       � �� �#3333336+   �� 5$       � ��$ 0q�60   �� 5%       � ��$ 45��6'   �� 5&       � ��$ 14(�6)   �� 5'       � ��$ SemiBold"�6+   �� 5(       �@�Cursor$
 Hand��6'   �� 5)       � �� 5*      �� 5+      " ��6&   .R�5,      �� $ border��6!   #��z��61   $ 20��5-   !   #��z��63   #��z��5.   !   ._�5/      �� $��Center=�6/   $��Center=�6L   50      	 51      P� 52      
 �@J�IsMouseOver$
 True��6=   . �53   "   �� $ border��6L    � ��
 ��� �54      P� 55      
 � ��$
 true��6;   . �56   "   �� $ border��6O    � �� �#DDDDDD57      59      5;      5<      �� 5>   
   " ��6"   .�5?      ��  �@��CornerRadius$ 20��6-   �� 5@       � �� 5A      �� 5B      $ #454545��6&   $	 0.4Q�66   5D      �� 5E       � �� �#3336,   �� 5F       � ��$ 1q�60   �� 5G       �@��Padding$ 15q�6(   �� 5H       � ��$ 10q�6'   �� 5I       �@J�Effect 5J        5K      $ Black��6'   $ 15Q�65   $ 2Q�6E   $	 0.3Q�6U   5M      5N      5O      .��5Q      �  � ColumnDefinitions 5R   
   �� 5S      $
��AUTO �6   �� 5T      $��2 �6   �� 5U      $��* �6   ��5X   
   �� 5Y      -   5        backgroundFondo�� �� ImageSource$> 8/TFG V0.01;component/Recursos/Background/oscuro/main.png��5Y   2   .+�5]   
   � -   5         �Name$
 menu��5]      $��0��6   $y�Visible=�6-   .+�5^      �� -   5        $ backgroun_menu��5^      ���#454545$��0q�6C   $��0,0,0,0q�6W   $ 15��6h   ���	#30FFFFFF$��Center=�6�    J� Effect 5_        5`      $ 15Q�6'      	Direction$	 -90Q�67      RenderingBias$ Quality=�6G   $ 2Q�6_   $ 	#20000000��6o   ._�5b      �� $��Center=�6   .+�5c      �� -   5         ��Name$ btnHome��5c      $��10q�6.   #��[ 6:   $��Stretch=�6f   $	��Top=�6�   
������ .��5d      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5e      �� -   5        $ imagenHome2��5e   $   $3��-/TFG V0.01;component/Recursos/Iconos/home.png��69   $��25��6p   $��25��6{   �� 5f      -   5         ��Name$ inicio��5f   (   $y�	Collapsed=�68   $��Inicio��6O   $��Center=�6]   $
��10,0q�6x   5g      5h      �� 5i      -   5        $ 	btnBuscar��5i      $��10q�60   #��[ 6<   $��Stretch=�6h   $	��Top=�6�   
������ .��5j      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5k      �� -   5        $ imagenBuscar2��5k   $   $5��//TFG V0.01;component/Recursos/Iconos/buscar.png��6;   $��25��6t   $��25��6   �� 5l      -	   5        $ buscar��5l   (   $y�	Collapsed=�68   $��Buscar Jurisprudencia��6O   $��Center=�6l   $
��10,0q�6�   5m      5n      �� 5o      -
   5        $ 	btnAgenda��5o      $��10q�60   #��[ 6<   $��Stretch=�6h   $	��Top=�6�   
������ .��5p      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5q      �� -   5        $ imagenAgenda2��5q   $   $5��//TFG V0.01;component/Recursos/Iconos/agenda.png��6;   $��25��6t   $��25��6   �� 5r      -   5        $ agenda��5r   (   $y�	Collapsed=�68   $��Agenda��6O   $��Center=�6]   $
��10,0q�6x   5s      5t      �� 5u      -   5        $ btnCasos��5u      $��10q�6/   #��[ 6;   $��Stretch=�6g   $	��Top=�6�   
������ .��5v      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5w      �� -   5        $ imagenCasos2��5w   $   $4��./TFG V0.01;component/Recursos/Iconos/casos.png��6:   $��25��6r   $��25��6}   �� 5x      -   5        $ casos��5x   (   $y�	Collapsed=�67   $��Casos��6N   $��Center=�6[   $
��10,0q�6v   5y      5z      �� 5{      -   5        $ btnClientes��5{      $��10q�62   #��[ 6>   $��Stretch=�6j   $	��Top=�6�   
������ .��5|      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5}      �� -   5        $ imagenClientes2��5}   $   $7��1/TFG V0.01;component/Recursos/Iconos/clientes.png��6=   $��25��6x   $��25��6�   �� 5~      -   5        $ clientes��5~   (   $y�	Collapsed=�6:   $��Clientes��6Q   $��Center=�6a   $
��10,0q�6|   5      5�      �� 5�      -   5        $ btnDocumentos��5�      $��10q�64   #��[ 6@   $��Stretch=�6l   $	��Top=�6�   
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenDocumentos2��5�   $   $9��3/TFG V0.01;component/Recursos/Iconos/documentos.png��6?   $��25��6|   $��25��6�   �� 5�      -   5        $ 
documentos��5�   (   $y�	Collapsed=�6<   $��
Documentos��6S   $��Center=�6e   $
��10,0q�6�   5�      5�      �� 5�      -   5        $ 
btnAjustes��5�      $��10q�61   #��[ 6=   $��Stretch=�6i   $	��Top=�6�   
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenAjustes2��5�   $   $6��0/TFG V0.01;component/Recursos/Iconos/ajustes.png��6<   $��25��6v   $��25��6�   �� 5�      -   5        $ ajustes��5�   (   $y�	Collapsed=�69   $��Ajustes��6P   $��Center=�6_   $
��10,0q�6z   5�      5�      �� 5�      -   5        $ btnAyuda��5�      $��10q�6/   #��[ 6;   $��Stretch=�6g   $	��Top=�6�   
������ .��5�      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   $��0q�6t   .+�5�      �� -   5        $ imagenAyuda2��5�   $   $4��./TFG V0.01;component/Recursos/Iconos/ayuda.png��6:   $��25��6r   $��25��6}   5�      5�      5�      5�      5�      �� 5�   
   -   5        $ ThemeButton��5�      $��35��6&   $��35��61   $��0q�6=   $��Right=�6H   $��Bottom=�6d   #��[  6�   $��2��6�   � 5�   
   $��2��6   $��20q�6    � RowDefinitions 5�      �� 5�      $
��Auto �6    �� 5�      $��* �6    .+�5�      �� $��Centro de Ayuda��6   $��24(�6/   $
��Bold"�6=   
�������$��0,0,0,20q�6b   �� 5�      $��1��6   $
��Auto=�6(   .��5�      �� .+�5�      �� #��[ 6   $��0,0,0,20q�6@   ._�5�      �� .+�5�      �� $��Preguntas Frecuentes��6(   $��20(�6D   $
��Bold"�6R   
�������$��0,0,0,15q�6w   9� 5�      $$��¿Cómo buscar jurisprudencia?��6'   
�������$	��0,5q�6`   .��5�   "   �� $����Utiliza la sección de búsqueda de jurisprudencia para encontrar casos relevantes. Puedes filtrar por jurisdicción, tipo de resolución y fecha.��6,   $
��Wrap=�5�   ,   $��10q�6@   
�������6L   5�      9� 5�      $��¿Cómo gestionar casos?��6'   
�������$	��0,5q�6Z   .��5�   "   �� $����En la sección de casos puedes crear, editar y dar seguimiento a tus casos. Organiza la información relevante y mantén un registro de las actuaciones.��6,   $
��Wrap=�5�   ,   $��10q�6@   
�������6L   5�      9� 5�      $#��¿Cómo administrar clientes?��6'   
�������$	��0,5q�6_   .��5�   "   �� $����La sección de clientes te permite mantener una base de datos organizada de tus clientes, incluyendo información de contacto y casos asociados.��6,   $
��Wrap=�5�   ,   $��10q�6@   
�������6L   5�      5�      5�      �� 5�      #��[ 6   ._�5�      �� .+�5�      �� $��Contacto y Soporte��6(   $��20(�6B   $
��Bold"�6P   
�������$��0,0,0,15q�6u   �� 5�      $E��?Si necesitas ayuda adicional, puedes contactarnos a través de:��6(   
�������$��0,0,0,10q�6�   �� 5�      $
��0,10q�6)   .+�5�   "   �� $%��📧 Email: soporte@ejemplo.com��6,   
�������$	��0,5q�6d   �� 5�   "   $%��📞 Teléfono: +34 900 123 456��6,   
�������$	��0,5q�6c   �� 5�   "   $0��*⏰ Horario: Lunes a Viernes, 9:00 - 18:00��6,   
�������$	��0,5q�6o   5�      5�      5�      5�      5�      5�      5�      5�      
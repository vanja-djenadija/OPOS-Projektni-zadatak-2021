   M S B A M L   `   `   `  ����T  PGraphicalUserInterfaceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:_2_GraphicalUserInterfaceApp_2_GraphicalUserInterfaceApp  5   	   .    (_2_GraphicalUserInterfaceApp.TasksWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:_2_GraphicalUserInterfaceApp     5   	   -   5          � Title$  TasksWindow��5   	   $	��400��6   $	��700��6*    � 
ResizeMode$ CanMinimize=�6N   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6    � 	Resources 5   
    �� 
TargetType �� Property �� Value��%X   '�       �� 5      " �6   .�5      ��  � �� �#FDF7E26/   5      5      .+�5   
   �� -   5         ��Name$ createNewTask��5   .   $��0��6   $��0��6!   $
��auto��6E   $	��100��6S   $
��Left=�6_   $	��Top=�6z   ��6�   Z� ��6�   Z� .��5      �� .+�5      �� $��/img/add.ico��6   $��30��6.   $��30��6:   �� 5      $	��100��6   	 �� TextAlignment$	 Center=�6(   Create New Task6Q   5      5      �� 5   
   -   5        $
 save��5   .   $��0��6   $��1��6!   $
��auto��6<   $	��100��6J   $
��Left=�6V   $	��Top=�6q   ��6�   Z� ��6�   Z� .��5      �� .+�5       �� $��/img/save.ico��6   $��30��6/   $��30��6;   �� 5!      $	��100��6   $	 Center=�6(   Save6F   5"      5#      �� 5%   
   $��1��6   $��0��6%   $	��600��62   .+�5&      �� $��Tasks��6   $��Center=�6$   $��14(�6A   $��Normal"�6O   5'      �� 5(   
   $��1��6   $��1��6'   $��Visible=�64   $��Visible=�6Z   $
��True��6�   $	��320��6�   .��5)      �� -   5        
 ��Name$
 tasksSP��5)       �� Orientation$ Vertical=�6*    �� CanVerticallyScroll$
 True��6A   5*      5+      5,      
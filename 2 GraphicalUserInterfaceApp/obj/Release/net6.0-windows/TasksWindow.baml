   M S B A M L   `   `   `  ���� T  PGraphicalUserInterfaceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:_2_GraphicalUserInterfaceApp_2_GraphicalUserInterfaceApp  .    (_2_GraphicalUserInterfaceApp.TasksWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:_2_GraphicalUserInterfaceApp     -     � Title$  TasksWindow��$	��400��$	��700�� � 
ResizeMode$ CanMinimize=�.���  � ColumnDefinitions �� $
��auto ��� $
��auto � � RowDefinitions �� $
��auto ��� $
��auto � � 	Resources    AddImage  	SaveImage �� 
TargetType �� Property �� Value��%n   &	        &	      '� '     �� $��add.ico���� $��save.ico���� " �.���  � �� �#FDF7E2.+��� -    ��Name$ createNewTask��$��0��$��0��$
��auto��$	��100��$
��Left=�$	��Top=���Z� ��Z� .���� .+��� $��add.ico��$��30��$��30���� $	��100��	 �� TextAlignment$	 Center=�Create New Task�� -   $
 save��$��0��$��1��$
��auto��$	��100��$
��Left=�$	��Top=���Z� ��Z� .���� .+��� $��save.ico��$��30��$��30���� $	��100��$	 Center=�Save�� $��1��$��0��$	��600��.+��� $��Tasks��$��Center=�$��14(�$��Normal"��� $��1��$��1��$��Visible=�$��Visible=�$
��True��$	��320��.���� -   
 ��Name$
 tasksSP�� �� Orientation$ Vertical=� �� CanVerticallyScroll$
 True��
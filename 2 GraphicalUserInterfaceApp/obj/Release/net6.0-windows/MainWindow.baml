   M S B A M L   `   `   `  ���� T  PGraphicalUserInterfaceApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:_2_GraphicalUserInterfaceApp_2_GraphicalUserInterfaceApp  -    '_2_GraphicalUserInterfaceApp.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:_2_GraphicalUserInterfaceApp       � Title$  TaskScheduler Settings��$	��400��$	��500�� � 
ResizeMode$ NoResize=�.��� $��10 10 10 10q� � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto � � 	Resources  �� 
TargetType �� Property �� Value��%9  '�       '�� )     '�� �     '	� �     �� " �.���  � �� �#FDF7E2�� " ��.���  � ��$ 40����  �@��Orientation$ 
Horizontal=���  � ��$ 10 0 0 0q��� " ��.���  � ��$ Segoe UI)���  � ��$ 13(���  � ��$	 250���� " 	�.���  � ��$ Segoe UI)�.+��� $��0��.+��� $��Task Scheduler Settings��$��	160 0 0 0q�$��15(��� $��1��.+��� $��Maximum Number of Cores���� -    ��Name$ coresCB��$	��150��$��5 5 5 5q��� $��2��.+��� $(��"Maximum Number of Concurrent Tasks���� -    ��Name$ maxNoConcurrentTasksTB��$	��150��$��5 5 5 5q��� $��3��.+��� $��Priority Scheduling��	� -   	 	�Name$	 
priorityRB��
 d� 	IsChecked$

 TrueY� 	� 	GroupName$ priority��$��Priority��$��5 5 5 5q�	� -   $	 nonPriorityRB��$ priority��$��Non-Priority��$��5 5 5 5q��� $��4��.+��� $��Preemtive Scheduling��	� -   $	 preemptiveRB��$

 TrueY�$ 
preemptive��$��
Preemptive��$��5 5 5 5q�	� -   $	 nonPeemptiveRB��$ 
preemptive��$��Non-Preemptive��$��5 5 5 5q��� $��5��$��Right=�$��0 0 0 0q�.+��� -    ��Name$ startBtn��$��Start��$	��100��$��0 0 20 0q����	#FF1699BC
�������$
��Bold"�